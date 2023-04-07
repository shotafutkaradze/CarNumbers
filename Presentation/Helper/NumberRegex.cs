using System.Text.RegularExpressions;

namespace Doamin.Helper
{
    public class NumberRegex
    {
        public static bool CarNumberRegex(string number)
        {
            var res = Regex.IsMatch(number, @"^[A-Z]{2}[0-9]{3}[A-Z]{2}$", RegexOptions.IgnoreCase);
            return res;
        }
    }
}
