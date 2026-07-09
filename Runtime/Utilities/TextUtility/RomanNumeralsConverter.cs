using System;
using System.Text;

namespace VMFramework.Core
{
    public static class RomanNumeralsConverter
    {
        private static readonly (int value, string numeral)[] numerals = {
            (1000, "M"),
            (900,  "CM"),
            (500,  "D"),
            (400,  "CD"),
            (100,  "C"),
            (90,   "XC"),
            (50,   "L"),
            (40,   "XL"),
            (10,   "X"),
            (9,    "IX"),
            (5,    "V"),
            (4,    "IV"),
            (1,    "I")
        };
        
        public static string IntToRoman(this int number)
        {
            if (number is < 1 or > 3999)
                throw new ArgumentOutOfRangeException(nameof(number), "Number must be between 1 and 3999.");

            var result = new StringBuilder();

            foreach (var item in numerals)
            {
                while (number >= item.value)
                {
                    result.Append(item.numeral);
                    number -= item.value;
                }
            }

            return result.ToString();
        }
    }
}