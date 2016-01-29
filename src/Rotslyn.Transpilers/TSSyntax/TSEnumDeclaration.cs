using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rotslyn.Transpilers.TSSyntax
{
    public class TSEnumDeclaration : TSMemberDeclarationSyntax
    {
        public string Name { get; set; }

        public IList<TSEnumMemberDeclaration> Members { get; } = new List<TSEnumMemberDeclaration>();

        public TSSyntaxToken Keyword { get; } = new TSSyntaxToken { Kind = TSKind.Enum };

        public IList<TSSyntaxToken> Modifiers { get; } = new List<TSSyntaxToken>();

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendTab(1, string.Join(" ", Modifiers.Select(m => m.ToString()).Concat(new[] { Keyword }.Select(m => m.ToString()))));

            sb.AppendLine($" {Name} {{");

            sb.Append(string.Join($",{System.Environment.NewLine}", Members.Select(m => $"{2.GetTab()}{m.ToString()}")));

            sb.AppendLine();
            sb.AppendTab(1, "}");

            return sb.ToString();
        }
    }
}