using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rotslyn.Transpilers.TSSyntax
{
    public class TSClassDeclaration : TSMemberDeclarationSyntax
    {
        public string Name { get; set; }

        public IList<TSMemberDeclarationSyntax> Members { get; } = new List<TSMemberDeclarationSyntax>();

        public TSSyntaxToken Keyword { get; } = new TSSyntaxToken { Kind = TSKind.Class };

        public IList<TSSyntaxToken> Modifiers { get; } = new List<TSSyntaxToken>();

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendTab(1, string.Join(" ", Modifiers.Select(m => m.ToString()).Concat(new[] { Keyword }.Select(m => m.ToString()))));

            sb.AppendLine($" {Name} {{");

            foreach (var member in Members)
                sb.AppendLine(member.ToString());

            sb.AppendTab(1, "}");

            return sb.ToString();
        }
    }
}