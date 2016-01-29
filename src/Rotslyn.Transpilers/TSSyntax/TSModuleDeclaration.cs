using System.Text;

namespace Rotslyn.Transpilers.TSSyntax
{
    public class TSModuleDeclaration : TSMemberDeclarationSyntax
    {
        public string Name { get; set; }
        public TSSyntaxToken Keyword { get; } = new TSSyntaxToken { Kind = TSKind.Module };
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendTab(0, Keyword.ToString());
            sb.AppendLine($" {Name} {{");

            foreach (var member in Members)
                sb.AppendLine(member.ToString());

            sb.AppendTab(0, "}");

            return sb.ToString();
        }
    }
}