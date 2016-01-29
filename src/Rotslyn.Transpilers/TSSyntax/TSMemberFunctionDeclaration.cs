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

        public string ReturnType { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendTab(2, string.Join(" ", Modifiers.Select(m => m.ToString())));
            sb.Append($" {Name.ToCamelCase()}(): {ReturnType}");
            sb.AppendLine($" {{");

            sb.AppendTabLine(3, Body);

            sb.AppendTab(2, "}");

            return sb.ToString();
        }
    }

    public class TSPropertyDeclaration : TSMemberDeclarationSyntax
    {
        public string Name { get; set; }
        public IList<TSSyntaxToken> Modifiers { get; } = new List<TSSyntaxToken>();

        //public string Body { get; set; }

        public string ReturnType { get; set; }
        public TSAccessorSyntax Getter { get; set; }
        public TSAccessorSyntax Setter { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (Getter != null && Setter != null && !Getter.Modifiers.Any(m => m.Kind == TSKind.Private || m.Kind == TSKind.Protected))
            {
                sb.AppendTab(2, string.Join(" ", Modifiers.Select(m => m.ToString())));
                sb.Append($" {Name.ToCamelCase()}: {ReturnType};");

            } else if (Getter != null)
            {
                sb.AppendTabLine(2, $"private _____{Name.ToCamelCase()}: {ReturnType};");
                sb.AppendTab(2, string.Join(" ", Modifiers.Select(m => m.ToString())));
                sb.AppendLine($" get {Name.ToCamelCase()}(): {ReturnType} {{");
                sb.AppendTabLine(3, $"return this._____{Name.ToCamelCase()};");
                sb.AppendTab(2, "}");
                if (Setter != null)
                {
                    sb.AppendLine();
                    sb.AppendTab(2, string.Join(" ", Modifiers.Select(m => m.ToString())));
                    sb.AppendLine($" set {Name.ToCamelCase()}(value: {ReturnType}) {{");
                    sb.AppendTabLine(3, $"this._____{Name.ToCamelCase()} = value;");
                    sb.AppendTab(2, "}");
                }
            }

            return sb.ToString();
        }
    }

    public class TSAccessorSyntax
    {
        public IList<TSSyntaxToken> Modifiers { get; } = new List<TSSyntaxToken>();

    }
}