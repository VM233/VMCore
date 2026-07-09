using System.Text.RegularExpressions;

namespace VMFramework.Core
{
    public static class RegularUtility
    {
        /// <summary>
        /// 从字符串中移除所有非数字字符
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>仅包含数字的字符串</returns>
        public static string RemoveNonNumeric(this string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            return Regex.Replace(input, @"\D", "");
        }
    }
}