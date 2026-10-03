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
}
