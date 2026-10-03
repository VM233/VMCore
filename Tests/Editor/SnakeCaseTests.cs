using System;
using System.Globalization;
using NUnit.Framework;
using Unity.Profiling;
using VMFramework.Core;
using Assert = NUnit.Framework.Assert;

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

            const ProfilerRecorderOptions options = ProfilerRecorderOptions.SumAllSamplesInFrame |
                ProfilerRecorderOptions.CollectOnlyOnCurrentThread;
            bool positiveRecorderValid;
            long positiveAllocations;
            using (var recorder = ProfilerRecorder.StartNew(ProfilerCategory.Internal, "GC.Alloc", 1, options))
            {
                positiveRecorderValid = recorder.Valid;
                byte[] positiveControl = null;
                for (int i = 0; i < 16; i++)
                {
                    positiveControl = new byte[1024];
                }
                GC.KeepAlive(positiveControl);
                recorder.Stop();
                positiveAllocations = recorder.Count == 0 ? 0 : recorder.GetSample(0).Count;
            }

            bool canonicalRecorderValid;
            long canonicalAllocations;
            string normalized = null;
            using (var recorder = ProfilerRecorder.StartNew(ProfilerCategory.Internal, "GC.Alloc", 1, options))
            {
                canonicalRecorderValid = recorder.Valid;
                for (int i = 0; i < iterations; i++)
                {
                    normalized = input.ToSnakeCase();
                }
                recorder.Stop();
                canonicalAllocations = recorder.Count == 0 ? 0 : recorder.GetSample(0).Count;
            }

            bool wordRecorderValid;
            long wordPipelineAllocations;
            string control = null;
            using (var recorder = ProfilerRecorder.StartNew(ProfilerCategory.Internal, "GC.Alloc", 1, options))
            {
                wordRecorderValid = recorder.Valid;
                for (int i = 0; i < iterations; i++)
                {
                    control = input.GetWords().ToSnakeCase();
                }
                recorder.Stop();
                wordPipelineAllocations = recorder.Count == 0 ? 0 : recorder.GetSample(0).Count;
            }

            Assert.That(positiveRecorderValid && canonicalRecorderValid && wordRecorderValid, Is.True);
            Assert.That(positiveAllocations, Is.GreaterThan(0), "The native recorder must observe the known allocation.");
            Assert.That(normalized, Is.SameAs(input));
            Assert.That(control, Is.EqualTo(input));
            Assert.That(canonicalAllocations, Is.Zero);
            Assert.That(wordPipelineAllocations, Is.GreaterThan(0));
            TestContext.WriteLine($"Positive allocations: {positiveAllocations}; canonical allocations: {canonicalAllocations}; word pipeline allocations: {wordPipelineAllocations}; calls: {iterations}");
        }
    }
}
