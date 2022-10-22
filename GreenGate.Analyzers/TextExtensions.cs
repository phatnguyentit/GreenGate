using System.Text;
using System.Text.RegularExpressions;

namespace GreenGate.Analyzers
{
    public static class TextExtensions
    {
        public static string MakeTextEasier(this string text)
        {
            var regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            var output = regex.Replace(text.Trim().Normalize(NormalizationForm.FormD), string.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            return output.RemoveSpecialCharacters();
        }

        public static string RemoveSpecialCharacters(this string str)
        {
            var sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString().Replace(" ", string.Empty).ToLower();
        }
    }
}