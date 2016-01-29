namespace Rotslyn.Transpilers.TSSyntax
{
    public class TSEnumMemberDeclaration : TSMemberDeclarationSyntax
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            if (Value != null)
                return $"{Name} = {Value}";

            return Name;
        }
    }
}