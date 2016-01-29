namespace Rotslyn.Transpilers.TSSyntax
{
    public class TSEnumMemberDeclaration : TSMemberDeclarationSyntax
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            if (Value != null)
                return $"{2.GetTab()}{Name} = {Value}";

            return $"{2.GetTab()}{Name}";
        }
    }
}