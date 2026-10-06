using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using VMFramework.Core.Pools;
using Assert = NUnit.Framework.Assert;

namespace VMFramework.Tests
{
    public sealed class CollectionPoolTests
    {
        [Test]
        [Repeat(2)]
        public void RequestedModesRetainIndependentCollections()
        {
            var local = CollectionPool<List<RetainedEntry>>.Default;
            var shared = CollectionPool<List<RetainedEntry>>.Shared;
            Assert.That(local.Capacity, Is.EqualTo(500));
            Assert.That(CollectionPool<List<RetainedEntry>>.Default, Is.SameAs(local));
            Assert.That(CollectionPool<List<RetainedEntry>>.Shared, Is.SameAs(shared));

            var localItem = local.Get(out bool localFresh);
            var sharedItem = shared.Get(out bool sharedFresh);
            var expectedLocal = localItem;
            var expectedShared = sharedItem;
            try
            {
                Assert.That(sharedItem, Is.Not.SameAs(localItem));
                bool localRetained = local.Return(localItem);
                localItem = null;
                bool sharedRetained = shared.Return(sharedItem);
                sharedItem = null;
                Assert.That(localRetained, Is.True);
                Assert.That(sharedRetained, Is.True);
                localItem = local.Get(out localFresh);
                sharedItem = shared.Get(out sharedFresh);
                Assert.That(localItem, Is.SameAs(expectedLocal));
                Assert.That(sharedItem, Is.SameAs(expectedShared));
                Assert.That(localFresh, Is.False);
                Assert.That(sharedFresh, Is.False);
            }
            finally
            {
                if (localItem != null) local.Return(localItem);
                if (sharedItem != null) shared.Return(sharedItem);
            }
        }

        [Test]
        public void DefaultFactoryClearsRetainedContents()
        {
            var pool = CollectionPool<List<FactoryEntry>>.Default;
            var item = pool.Get(out _);
            item.Add(new FactoryEntry());
            Assert.That(pool.Return(item), Is.True);
            var borrowed = CollectionPoolFactory<List<FactoryEntry>, FactoryEntry>.CreateFromDefaultPool();
            try
            {
                Assert.That(borrowed, Is.SameAs(item));
                Assert.That(borrowed, Is.Empty);
            }
            finally
            {
                Assert.That(pool.Return(borrowed), Is.True);
            }
        }

        [Test]
        public void ConcurrentFirstRequestsPublishOneSharedPool()
        {
            var products = new DefaultConcurrentPool<List<ConcurrentEntry>>[4];
            var factories = new Func<List<ConcurrentEntry>>[4];
            var collections = new List<ConcurrentEntry>[4];
            var failures = new Exception[4];
            var threads = new Thread[4];
            using var start = new ManualResetEventSlim(false);
            for (int index = 0; index < threads.Length; index++)
            {
                int slot = index;
                threads[index] = new Thread(() =>
                {
                    try
                    {
                        start.Wait();
                        factories[slot] = CollectionPoolFactory<List<ConcurrentEntry>, ConcurrentEntry>.CreateFromSharedPool;
                        products[slot] = CollectionPool<List<ConcurrentEntry>>.Shared;
                        collections[slot] = factories[slot]();
                    }
                    catch (Exception error)
                    {
                        failures[slot] = error;
                    }
                }) { IsBackground = true };
                threads[index].Start();
            }
            start.Set();
            try
            {
                foreach (var thread in threads)
                    Assert.That(thread.Join(5000), Is.True, "The original shared initialization did not finish.");
                Assert.That(failures, Is.All.Null);
                Assert.That(products[0], Is.Not.Null);
                for (int index = 0; index < products.Length; index++)
                {
                    Assert.That(products[index], Is.SameAs(products[0]));
                    Assert.That(factories[index], Is.SameAs(factories[0]));
                    Assert.That(collections[index], Is.Empty);
                }
            }
            finally
            {
                for (int index = 0; index < collections.Length; index++)
                    if (collections[index] != null) products[index].Return(collections[index]);
            }
        }

        private sealed class RetainedEntry { }
        private sealed class FactoryEntry { }
        private sealed class ConcurrentEntry { }
    }
}
