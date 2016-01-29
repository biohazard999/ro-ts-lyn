using System.Collections.Generic;

namespace Rotslyn.Transpilers.TSSyntax
{
    public abstract class TSMemberDeclarationSyntax : TSSyntaxNode
    {
        public IList<TSMemberDeclarationSyntax> Members { get; } = new List<TSMemberDeclarationSyntax>();
    }
}