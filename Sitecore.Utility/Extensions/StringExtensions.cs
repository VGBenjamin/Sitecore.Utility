using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sitecore.Utility.Extensions
{
    public static class StringExtensions
    {
        private static readonly string[] ReservedKeywords =
        {
            "ancestor",
            "ancestor-or-self",
            "child",
            "descendant",
            "descendant-or-self",
            "following",
            "parent",
            "preceding",
            "self",
            "*"
        };

        /// <summary>
        /// Sanitize an XPath query. This method encapulate the query segments by # # to avoid the errors 
        /// with the reserved keywords or the issues with the items who begin by 0
        /// </summary>
        /// <param name="query">The query to sanitize</param>
        /// <returns>The sanitized query</returns>
        /// <example>
        /// "/sitecore/content/TestSites/Test/Data/Ambiances/2016/09/28/15/56/STRASS BLANC 1 SDB/ancestor::*[@@templateid='{54A1C811-C8A6-40D9-B6C3-00930EFF1CDE}']/*[@@templateid='{C79967AC-E386-4AAA-B5CF-78184172E138}']".SanitizeQuery();
        /// This return : /#sitecore#/#content#/#TestSites#/#Schmidt Test#/#Data#/#Ambiances#/#2016#/#09#/#28#/#15#/#56#/#STRASS BLANC 1 SDB#/ancestor::*[@@templateid='{54A1C811-C8A6-40D9-B6C3-00930EFF1CDE}']/*[@@templateid='{C79967AC-E386-4AAA-B5CF-78184172E138}']
        /// </example>
        public static string SanitizeQuery(this string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return query;
            var splitted = query.Split(new char[] { '/' });

            string returned = query.StartsWith("/") ? "/" : string.Empty;
            Regex paramRegex = new Regex("^(.*?)(\\[.*?\\])?$");

            foreach (string segment in splitted)
            {
                if (!string.IsNullOrEmpty(segment))
                {
                    if (segment.StartsWithReservedKeyword())
                    {
                        returned = $"{returned}{segment}/";
                    }
                    else
                    {
                        returned = $"{returned}{paramRegex.Replace(segment, "#$1#$2/")}";
                    }
                }
            }

            if (!query.EndsWith("/") && returned.EndsWith("/"))
                returned = returned.Remove(returned.Length - 1);

            return returned;
        }

        private static bool StartsWithReservedKeyword(this string segment)
        {
            foreach (var reservedKeyword in ReservedKeywords)
            {
                if (segment.StartsWith(reservedKeyword, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
