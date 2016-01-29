using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rotslyn.Transpilers.TSSyntax
{
    public class TSSourceUnit : TSSyntaxNode
    {
        public IList<TSMemberDeclarationSyntax> Members { get; } = new List<TSMemberDeclarationSyntax>();

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var member in Members)
                sb.Append(member);

            return sb.ToString();
        }
    }
}
