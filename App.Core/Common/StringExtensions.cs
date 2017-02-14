using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace App.Core.Common
{
    public static class StringExtensions
    {
        public static int ToInt(this string source)
        {
            return int.Parse(source);
        }


        public static bool ToBoolean(this string s)
        {
            return s.ToBoolean(false);
        }

        public static bool ToBoolean(this string s, bool def)
        {
            bool result;
            if (Boolean.TryParse(s, out result))
                return result;
            else
                return def;
        }

        public static string RemoveHtmlTags(this string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }
    }
}
