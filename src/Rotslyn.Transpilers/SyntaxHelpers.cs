using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rotslyn.Transpilers
{
    public static class SyntaxHelpers
    {
        public static string ToCamelCase(this string token)
        {
            var firstCharLower = token.Select(char.ToLower).First();
            var camelCaseString = string.Concat(new[] { firstCharLower }.Concat(token.Skip(1)));
            return camelCaseString;
        }

        static readonly Dictionary<int, string> Tabs = new Dictionary<int, string>();

        public static string GetTab(this int length)
        {
            if (!Tabs.ContainsKey(length))
            {
                Tabs[length] = new string(' ', 4 * length);
            }
            return Tabs[length];
        }

        public static void AppendTab(this StringBuilder builder, int indentation, string str = null)
        {
            builder.Append(GetTab(indentation));
            builder.Append(str);
        }

        public static void AppendTabLine(this StringBuilder builder, int count, string str = null)
        {
            builder.Append(GetTab(count));
            builder.Append(str);
            builder.AppendLine();
        }
    }
}