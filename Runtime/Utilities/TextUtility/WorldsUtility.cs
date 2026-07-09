using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace VMFramework.Core
{
    public readonly struct WordSegment
    {
        public string Word { get; }

        public int StartIndex { get; }

        public int Length => Word.Length;

        public int EndIndex => StartIndex + Length;

        public WordSegment(string word, int startIndex)
        {
            Word = word;
            StartIndex = startIndex;
        }
    }

    public static class WorldsUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<string> GetWords(this string input)
        {
            if (input.IsNullOrWhiteSpace())
            {
                return Array.Empty<string>();
            }

            var words = new List<string>();
            StringBuilder currentWord = new StringBuilder();
            var isDigit = true;
            var isCurrentWorldAllUpper = true;

            void ClearCurrentWorld()
            {
                if (currentWord.Length > 0)
                {
                    words.Add(currentWord.ToString());
                    currentWord.Clear();
                    isCurrentWorldAllUpper = true;
                    isDigit = true;
                }
            }

            for (int i = 0; i < input.Length; i++)
            {
                var c = input[i];
                if (char.IsLetter(c))
                {
                    if (currentWord.Length > 1 && isDigit)
                    {
                        ClearCurrentWorld();
                    }

                    isDigit = false;

                    if (char.IsUpper(c))
                    {
                        if (currentWord.Length > 0 && isCurrentWorldAllUpper)
                        {
                            if (i != input.Length - 1)
                            {
                                var nextChar = input[i + 1];

                                if (char.IsLower(nextChar))
                                {
                                    ClearCurrentWorld();
                                }
                            }
                        }
                        else
                        {
                            ClearCurrentWorld();
                        }
                    }
                    else
                    {
                        isCurrentWorldAllUpper = false;
                    }
                }
                else if (char.IsDigit(c))
                {
                    if (currentWord.Length == 0 || isDigit == false)
                    {
                        ClearCurrentWorld();
                    }
                }
                else
                {
                    ClearCurrentWorld();
                    continue;
                }

                currentWord.Append(c);
            }

            if (currentWord.Length > 0)
            {
                words.Add(currentWord.ToString());
            }

            return words;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<WordSegment> GetWordSegments(this string input)
        {
            var words = input.GetWords();

            if (words.Count == 0)
            {
                return Array.Empty<WordSegment>();
            }

            var segments = new List<WordSegment>(words.Count);

            int searchStartIndex = 0;
            foreach (var word in words)
            {
                int index = input.IndexOf(word, searchStartIndex, StringComparison.Ordinal);
                if (index < 0)
                {
                    break;
                }

                segments.Add(new WordSegment(word, index));
                searchStartIndex = index + word.Length;
            }

            return segments;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToSnakeCase(this string input)
        {
            return input.GetWords().Select(word => word.ToLower()).ToFormattedString("_");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToPascalCase(this string input, string step = "")
        {
            return input.GetWords().Select(LetterUtility.CapitalizeFirstLetter).ToFormattedString(step);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToCamelCase(this string input, string step = "")
        {
            return ToCamelCase(input.GetWords(), step);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToSnakeCase(this IEnumerable<string> words)
        {
            return words.Select(word => word.ToLower()).ToFormattedString("_");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToPascalCase(this IEnumerable<string> words, string step = "")
        {
            return words.Select(LetterUtility.CapitalizeFirstLetter).ToFormattedString(step);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToCamelCase(this IEnumerable<string> words, string step = "")
        {
            string result = string.Empty;

            bool isFirst = true;
            foreach (var word in words)
            {
                if (isFirst)
                {
                    result += word.ToLower();
                    isFirst = false;

                    continue;
                }

                result += step + word.CapitalizeFirstLetter();
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> RemoveWordsSuffix(this string input, IEnumerable<string> suffixes)
        {
            var words = input.GetWords().ToList();

            var suffixesList = suffixes.ToArray();

            for (int i = suffixesList.Length - 1; i >= 0; i--)
            {
                var lastWord = words[^1];

                if (string.Equals(lastWord, suffixesList[i], StringComparison.CurrentCultureIgnoreCase))
                {
                    words.RemoveAt(words.Count - 1);
                }
            }

            return words;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<string> MakeWordsSuffix(this string input, string suffix)
        {
            var inputWords = input.GetWords().ToList();
            var suffixWords = suffix.GetWords().ToArray();

            int overlappingCount = inputWords.CountOverlappingElements(suffixWords, StringComparison.OrdinalIgnoreCase);
            if (overlappingCount >= suffixWords.Length)
            {
                return inputWords;
            }

            for (int i = overlappingCount; i < suffixWords.Length; i++)
            {
                inputWords.Add(suffixWords[i]);
            }

            return inputWords;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<string> GetSameWords(this IReadOnlyList<string> inputs)
        {
            if (inputs == null || inputs.Count == 0)
            {
                return Array.Empty<string>();
            }
            
            var sameWords = new List<string>();
            var firstInput = inputs[0];
            
            sameWords.AddRange(firstInput.GetWords());
            
            for (int i = 1; i < inputs.Count; i++)
            {
                var input = inputs[i];
                var words = input.GetWords();

                sameWords.RemoveAll(word => words.Contains(word, StringComparer.OrdinalIgnoreCase) == false);

                if (sameWords.Count == 0)
                {
                    break;
                }
            }
            
            return sameWords;
        }
    }
}
