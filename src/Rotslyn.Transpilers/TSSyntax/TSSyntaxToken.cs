namespace Rotslyn.Transpilers.TSSyntax
{
    public class TSSyntaxToken
    {
        public TSKind Kind { get; set; }

        public override string ToString()
        {
            return Kind.ToString().ToLowerInvariant();
        }
    }
}