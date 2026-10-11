using System;
using System.Collections.Generic;
using NUnit.Framework;
using VMFramework.Core.Pools;

namespace VMFramework.Tests
{
    public sealed class PoolItemAcquisitionTests
    {
        private sealed class Item : ICreatablePoolItem, ICreatablePoolItem<string>
        {
            public Action Create, Get, Return, Clear;
            public int Created, Retrieved, Returned, Cleared;
            public void OnCreate() { Created++; Create?.Invoke(); }
            public void OnCreate(string argument) => OnCreate();
            public void OnGet() { Retrieved++; Get?.Invoke(); }
            public void OnReturn() { Returned++; Return?.Invoke(); }
            public void OnClear() { Cleared++; Clear?.Invoke(); }
        }

        private static PoolItemsPool<Item> Pool(bool argument, Func<Item> factory) => argument
            ? new CreatablePoolItemsPool<Item, string>("fixture", _ => factory(), 1)
            : new CreatablePoolItemsPool<Item>(factory, 1);

        [TestCase(false)]
        [TestCase(true)]
        public void FailedFreshInitializationClearsWithoutRetainingPartialState(bool argument)
        {
            var primary = new InvalidOperationException("fresh initialization");
            var failed = new Item { Create = () => throw primary };
            var control = new Item();
            int creates = 0;
            var pool = Pool(argument, () => creates++ == 0 ? failed : control);
            Item unpublished = null;
            Assert.That(Assert.Throws<InvalidOperationException>(() => unpublished = pool.Get(out _)), Is.SameAs(primary));
            Assert.That(unpublished, Is.Null);
            Assert.That(failed.Cleared, Is.EqualTo(1));
            Assert.That(failed.Returned, Is.Zero);
            Assert.That(pool.Count, Is.Zero);
            Assert.That(pool.Get(out bool fresh), Is.SameAs(control));
            Assert.That(fresh, Is.True);
            Assert.That(control.Created, Is.EqualTo(1));
            pool.Return(control);
        }

        [Test]
        public void FailedPooledInitializationReturnsOnlyTheDequeuedItem()
        {
            var item = new Item();
            var pool = Pool(true, () => item);
            pool.Return(pool.Get(out _));
            var primary = new InvalidOperationException("pooled initialization");
            item.Get = () => throw primary;
            Assert.That(Assert.Throws<InvalidOperationException>(() => pool.Get(out _)), Is.SameAs(primary));
            Assert.That(item.Returned, Is.EqualTo(2));
            Assert.That(item.Cleared, Is.Zero);
            Assert.That(pool.Count, Is.EqualTo(1));
            item.Get = null;
            Assert.That(pool.Get(out bool fresh), Is.SameAs(item));
            Assert.That(fresh, Is.False);
            Assert.That(item.Created, Is.EqualTo(1));
            pool.Return(item);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void FreshInitializationAndClearFailuresRetainOriginalErrors(bool argument)
        {
            var primary = new InvalidOperationException("fresh initialization");
            var secondary = new InvalidOperationException("fresh clear");
            var item = new Item { Create = () => throw primary, Clear = () => throw secondary };
            var pool = Pool(argument, () => item);
            var product = Assert.Throws<PoolItemAcquisitionException>(() => pool.Get(out _));
            Assert.That(product.InnerExceptions, Has.Count.EqualTo(2));
            Assert.That(product.InnerExceptions[0], Is.SameAs(primary));
            Assert.That(product.InnerExceptions[1], Is.SameAs(secondary));
            Assert.That(product.Pool, Is.SameAs(pool));
            Assert.That(product.Item, Is.SameAs(item));
            Assert.That(product.Phase, Is.EqualTo("create.clear"));
            Assert.That(item.Cleared, Is.EqualTo(1));
            Assert.That(item.Returned, Is.Zero);
            Assert.That(pool.Count, Is.Zero);
        }

        [Test]
        public void PooledInitializationAndReturnFailuresRetainOriginalErrors()
        {
            var item = new Item();
            var pool = Pool(true, () => item);
            pool.Return(pool.Get(out _));
            var primary = new InvalidOperationException("pooled initialization");
            var secondary = new InvalidOperationException("pooled return");
            item.Get = () => throw primary;
            item.Return = () => throw secondary;
            var product = Assert.Throws<PoolItemAcquisitionException>(() => pool.Get(out _));
            Assert.That(product.InnerExceptions[0], Is.SameAs(primary));
            Assert.That(product.InnerExceptions[1], Is.SameAs(secondary));
            Assert.That(product.Pool, Is.SameAs(pool));
            Assert.That(product.Item, Is.SameAs(item));
            Assert.That(product.Phase, Is.EqualTo("get.return"));
            Assert.That(item.Returned, Is.EqualTo(2));
            Assert.That(item.Cleared, Is.EqualTo(1));
            Assert.That(pool.Count, Is.Zero);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void FactoryFailureDoesNotRetireAnUnacquiredItem(bool argument)
        {
            var primary = new InvalidOperationException("factory");
            var pool = Pool(argument, () => throw primary);
            Assert.That(Assert.Throws<InvalidOperationException>(() => pool.Get(out _)), Is.SameAs(primary));
            Assert.That(pool.Count, Is.Zero);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void ReentrantFullPoolUsesTheExistingClearContract(bool clearFails)
        {
            var a = new Item();
            var b = new Item();
            var available = new Queue<Item>(new[] { a, b });
            var pool = Pool(true, () => available.Dequeue());
            Assert.That(pool.Get(out _), Is.SameAs(a));
            Assert.That(pool.Get(out _), Is.SameAs(b));
            pool.Return(a);
            var primary = new InvalidOperationException("pooled initialization");
            var secondary = new InvalidOperationException("capacity clear");
            a.Get = () => { pool.Return(b); throw primary; };
            if (clearFails) a.Clear = () => throw secondary;
            if (clearFails)
            {
                var product = Assert.Throws<PoolItemAcquisitionException>(() => pool.Get(out _));
                Assert.That(product.InnerExceptions[0], Is.SameAs(primary));
                Assert.That(product.InnerExceptions[1], Is.SameAs(secondary));
                Assert.That(product.Pool, Is.SameAs(pool));
                Assert.That(product.Item, Is.SameAs(a));
                Assert.That(product.Phase, Is.EqualTo("get.return"));
            }
            else Assert.That(Assert.Throws<InvalidOperationException>(() => pool.Get(out _)), Is.SameAs(primary));
            Assert.That(a.Cleared, Is.EqualTo(1));
            Assert.That(a.Returned, Is.EqualTo(2));
            Assert.That(pool.Count, Is.EqualTo(1));
            Assert.That(pool.Get(out _), Is.SameAs(b));
            pool.Return(b);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void ClearWithdrawsAllHeldEntriesAndAttemptsEveryStorage(bool lastAlsoFails)
        {
            var firstFailure = new InvalidOperationException("first storage");
            var lastFailure = new InvalidOperationException("last storage");
            var a = new Item { Clear = () => throw firstFailure };
            var b = new Item();
            var c = new Item { Clear = lastAlsoFails ? () => throw lastFailure : null };
            var control = new Item();
            var incoming = new Queue<Item>(new[] { a, b, c, control });
            var pool = new CreatablePoolItemsPool<Item, string>("fixture", _ => incoming.Dequeue(), 3);
            var first = pool.Get(out _); var middle = pool.Get(out _); var last = pool.Get(out _);
            pool.Return(first); pool.Return(middle); pool.Return(last);
            AggregateException product = lastAlsoFails ? Assert.Throws<AggregateException>(() => pool.Clear()) :
                Assert.Throws<PoolItemAcquisitionException>(() => pool.Clear());
            var firstProduct = lastAlsoFails ? (PoolItemAcquisitionException)product.InnerExceptions[0] :
                (PoolItemAcquisitionException)product;
            Assert.That(firstProduct.Pool, Is.SameAs(pool));
            Assert.That(firstProduct.Item, Is.SameAs(a));
            Assert.That(firstProduct.Phase, Is.EqualTo("clear.storage"));
            Assert.That(firstProduct.InnerExceptions[0], Is.SameAs(firstFailure));
            if (lastAlsoFails)
            {
                Assert.That(product.InnerExceptions, Has.Count.EqualTo(2));
                var lastProduct = (PoolItemAcquisitionException)product.InnerExceptions[1];
                Assert.That(lastProduct.Item, Is.SameAs(c));
                Assert.That(lastProduct.Pool, Is.SameAs(pool));
                Assert.That(lastProduct.InnerExceptions[0], Is.SameAs(lastFailure));
            }
            Assert.That(new[] { a.Cleared, b.Cleared, c.Cleared }, Is.EqualTo(new[] { 1, 1, 1 }));
            Assert.That(new[] { a.Returned, b.Returned, c.Returned }, Is.EqualTo(new[] { 1, 1, 1 }));
            Assert.That(pool.Count, Is.Zero);
            Assert.That(pool.Get(out bool fresh), Is.SameAs(control));
            Assert.That(fresh, Is.True);
            pool.Return(control); pool.Clear();
            Assert.That(control.Returned, Is.EqualTo(1));
            Assert.That(control.Cleared, Is.EqualTo(1));
        }

        [Test]
        public void ClearOwnsOnlyItsEntrySnapshotAndKeepsNewAdmissions()
        {
            var a = new Item(); var b = new Item(); var later = new Item();
            var incoming = new Queue<Item>(new[] { a, b, later });
            var pool = new CreatablePoolItemsPool<Item, string>("fixture", _ => incoming.Dequeue(), 3);
            pool.Get(out _); pool.Get(out _); pool.Get(out _);
            pool.Return(a); pool.Return(b);
            a.Clear = () => pool.Return(later);
            pool.Clear();
            Assert.That(a.Cleared, Is.EqualTo(1));
            Assert.That(b.Cleared, Is.EqualTo(1));
            Assert.That(later.Cleared, Is.Zero);
            Assert.That(pool.Count, Is.EqualTo(1));
            Assert.That(pool.Get(out bool fresh), Is.SameAs(later));
            Assert.That(fresh, Is.False);
            pool.Return(later); pool.Clear();
            Assert.That(later.Cleared, Is.EqualTo(1));
        }
    }
}
