using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Rotslyn.Transpilers.TSSyntax;

namespace Rotslyn.Transpilers
{
    public static class RotslynTranspiler
    {
        public static string Transpile(string code, Language language)
        {
            var syntaxTree = SyntaxFactory.ParseSyntaxTree(code);
            var node = (CompilationUnitSyntax)syntaxTree.GetRoot();

            var tsUnit = new TSSourceUnit();

            foreach (var namespaceDeclaration in node.Members.OfType<NamespaceDeclarationSyntax>())
            {
                var tsModule = new TSModuleDeclaration
                {
                    Name = namespaceDeclaration.Name.ToString(),
                };

                tsUnit.Members.Add(tsModule);

                foreach (var classNode in namespaceDeclaration.Members.OfType<ClassDeclarationSyntax>())
                {
                    var tsClassNode = new TSClassDeclaration
                    {
                        Modifiers = { new TSSyntaxToken {Kind = TSKind.Export} },
                        Name = classNode.Identifier.Text
                    };

                    tsModule.Members.Add(tsClassNode);

                    foreach (var method in classNode.Members.OfType<MethodDeclarationSyntax>())
                    {
                        var tsMethod = new TSMemberFunctionDeclaration
                        {
                            Name = method.Identifier.Text,
                            Body = "console.log(\"Hello World!\");"
                        };

                        tsClassNode.Members.Add(tsMethod);

                        if (method.Modifiers.Any(m => m.Kind() == SyntaxKind.PublicKeyword))
                            tsMethod.Modifiers.Add(new TSSyntaxToken {Kind = TSKind.Public });

                        if (method.Modifiers.Any(m => m.Kind() == SyntaxKind.StaticKeyword))
                            tsMethod.Modifiers.Add(new TSSyntaxToken { Kind = TSKind.Static });
                    }
                }
            }

            return tsUnit.ToString();
        }
    }

    public static class SyntaxHelpers
    {
        public static string ToCamelCase(this string token)
        {
            var firstCharLower = token.Select(char.ToLower).First();
            var camelCaseString = string.Concat(new[] { firstCharLower }.Concat(token.Skip(1)));
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
