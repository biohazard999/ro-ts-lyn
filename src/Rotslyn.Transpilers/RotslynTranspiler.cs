using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Rotslyn.Transpilers
{
    public static class RotslynTranspiler
    {
        public static string Transpile(string code, Language language)
        {
            var stringBuilder = new StringBuilder();

            var syntaxTree = SyntaxFactory.ParseSyntaxTree(code);
            var node = (CompilationUnitSyntax)syntaxTree.GetRoot();

            if (node.Members[0] is NamespaceDeclarationSyntax)
            {
                var namespaceDeclaration = (NamespaceDeclarationSyntax)node.Members[0];

                stringBuilder.Append("module ");
                stringBuilder.Append(namespaceDeclaration.Name);
                stringBuilder.AppendLine(" {");

                var classNode = namespaceDeclaration.Members.OfType<ClassDeclarationSyntax>().FirstOrDefault();
                if (classNode != null)
                {
                    stringBuilder.AppendTab(1);
                    stringBuilder.Append("export class ");
                    stringBuilder.Append(classNode.Identifier.Text);
                    stringBuilder.AppendLine(" {");

                    foreach (var method in classNode.Members.OfType<MethodDeclarationSyntax>())
                    {
                        stringBuilder.AppendTab(2);

                        if (method.Modifiers.Any(m => m.Kind() == SyntaxKind.PublicKeyword))
                            stringBuilder.Append("public ");

                        if (method.Modifiers.Any(m => m.Kind() == SyntaxKind.StaticKeyword))
                            stringBuilder.Append("static ");

                        stringBuilder.Append(method.Identifier.ToCamelCase());
                        stringBuilder.AppendLine("() {");
                        stringBuilder.AppendTab(3);

                        stringBuilder.Append("console.log(\"Hello World!\");");

                        stringBuilder.AppendLine();
                        stringBuilder.AppendTabLine(2, "}");
                    }

                    stringBuilder.AppendTabLine(1, "}");
                }
                stringBuilder.AppendTab(0, "}");
            }

            return stringBuilder.ToString();
        }
    }

    public static class SyntaxHelpers
    {
        public static string ToCamelCase(this SyntaxToken token)
        {
            var firstCharLower = token.Text.Select(char.ToLower).First();
            var camelCaseString = string.Concat(new[] { firstCharLower }.Concat(token.Text.Skip(1)));
            return camelCaseString;
        }

        static readonly Dictionary<int, string> Tabs = new Dictionary<int, string>();

        private static string GetTab(int length)
        {
            if (!Tabs.ContainsKey(length))
            {
                Tabs[length] = new string(' ', 4 * length);
            }
            return Tabs[length];
        }

        public static void AppendTab(this StringBuilder builder, int indentation, string str = null)
        {
            builder.Append(GetTab(indentation));
            builder.Append(str);
        }

        public static void AppendTabLine(this StringBuilder builder, int count, string str = null)
        {
            builder.Append(GetTab(count));
            builder.Append(str);
            builder.AppendLine();
        }
    }

    public enum Language
    {
        CSharp,
        VisualBasic
    }
}
