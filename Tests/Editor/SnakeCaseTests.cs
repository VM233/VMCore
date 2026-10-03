using System;
using System.Globalization;
using NUnit.Framework;
using VMFramework.Core;

namespace VMFramework.Tests
{
    public sealed class SnakeCaseTests
    {
        [TestCase("a")]
        [TestCase("ability")]
        [TestCase("a_b")]
        [TestCase("extra_unit_value")]
        [TestCase("unit_value")]
        [TestCase("buffer_regeneration")]
        [TestCase("property")]
        [TestCase("collision_knockup")]
        public void CanonicalInputRetainsReference(string input)
        {
            Assert.That(input.ToSnakeCase(), Is.SameAs(input));
        }

        [Test]
        public void LongCanonicalInputRetainsReference()
        {
            string input = new string('a', 8192);
            Assert.That(input.ToSnakeCase(), Is.SameAs(input));
        }

        [Test]
        public void NormalizationMatchesWordPipelineAcrossCultures()
        {
            string[] inputs =
            {
                null, "", " ", "\t\n", "a", "a_b", "a__b", "_a", "a_", "__",
                "AbilityItem", "XMLHttpRequest", "ABC", "ABc", "fooBar",
                "foo123Bar", "a1b", "12abc", "abc12", "a12b", "entity_2",
                "I", "II", "İ", "ı", "Straße", "Élan", "éclair", "击退能力",
                "foo- Bar", "foo.bar", "a/b", "a\0b", "a\uD800b", "a\u0301b"
            };
            string[] cultures = { "", "en-US", "tr-TR", "az-Latn-AZ", "de-DE", "zh-CN" };
            CultureInfo originalCulture = CultureInfo.CurrentCulture;
            try
            {
                foreach (string culture in cultures)
                {
                    CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(culture);
                    foreach (string input in inputs)
                    {
                        Assert.That(input.ToSnakeCase(), Is.EqualTo(input.GetWords().ToSnakeCase()),
                            $"Culture: {culture}, input: {input}");
                    }
                }
            }
            finally
            {
                CultureInfo.CurrentCulture = originalCulture;
            }
        }

        [Test]
        public void ExhaustiveShortInputsMatchWordPipeline()
        {
            char[] alphabet = { 'a', 'b', '_', 'I', '1', ' ', 'é' };
            char[] buffer = new char[4];
            string[] cultures = { "", "tr-TR" };
            CultureInfo originalCulture = CultureInfo.CurrentCulture;
            try
            {
                foreach (string culture in cultures)
                {
                    CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(culture);
                    int count = 1;
                    for (int length = 0; length <= buffer.Length; length++)
                    {
                        for (int index = 0; index < count; index++)
                        {
                            int encoded = index;
                            for (int position = 0; position < length; position++)
                            {
                                buffer[position] = alphabet[encoded % alphabet.Length];
                                encoded /= alphabet.Length;
                            }

                            string input = new string(buffer, 0, length);
                            Assert.That(input.ToSnakeCase(), Is.EqualTo(input.GetWords().ToSnakeCase()),
                                $"Culture: {culture}, length: {length}, index: {index}");
                        }

                        count *= alphabet.Length;
                    }
                }
            }
            finally
            {
                CultureInfo.CurrentCulture = originalCulture;
            }
        }

        [Test]
        public void CanonicalNormalizationRemovesWordPipelineAllocations()
        {
            const string input = "extra_unit_value";
            const int iterations = 4096;
            for (int i = 0; i < 64; i++)
            {
                input.ToSnakeCase();
                input.GetWords().ToSnakeCase();
            }

            long before = GC.GetAllocatedBytesForCurrentThread();
            string normalized = null;
            for (int i = 0; i < iterations; i++)
            {
                normalized = input.ToSnakeCase();
            }

            long canonicalBytes = GC.GetAllocatedBytesForCurrentThread() - before;
            before = GC.GetAllocatedBytesForCurrentThread();
            string control = null;
            for (int i = 0; i < iterations; i++)
            {
                control = input.GetWords().ToSnakeCase();
            }

            long wordPipelineBytes = GC.GetAllocatedBytesForCurrentThread() - before;
            Assert.That(normalized, Is.SameAs(input));
            Assert.That(control, Is.EqualTo(input));
            Assert.That(canonicalBytes, Is.Zero);
            Assert.That(wordPipelineBytes, Is.GreaterThan(0));
            TestContext.WriteLine($"Canonical bytes: {canonicalBytes}; word pipeline bytes: {wordPipelineBytes}; calls: {iterations}");
        }
    }
}
