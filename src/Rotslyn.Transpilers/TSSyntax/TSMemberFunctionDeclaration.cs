using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rotslyn.Transpilers.TSSyntax
{
    public class TSMemberFunctionDeclaration : TSMemberDeclarationSyntax
    {
        public string Name { get; set; }
        public IList<TSSyntaxToken> Modifiers { get; } = new List<TSSyntaxToken>();

        public string Body { get; set; }
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendTab(2, string.Join(" ", Modifiers.Select(m => m.ToString())));
            sb.AppendLine($" {Name.ToCamelCase()}() {{");

            sb.AppendTabLine(3, Body);

            sb.AppendTab(2, "}");

            return sb.ToString();
        }
    }
}