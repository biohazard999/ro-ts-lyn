using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
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

                foreach (var memberNode in namespaceDeclaration.Members)
                {
                    if (memberNode is ClassDeclarationSyntax)
                    {
                        var classNode = memberNode as ClassDeclarationSyntax;
                        var tsClassNode = new TSClassDeclaration
                        {
                            Modifiers = {new TSSyntaxToken {Kind = TSKind.Export}},
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
                                tsMethod.Modifiers.Add(new TSSyntaxToken {Kind = TSKind.Public});

                            if (method.Modifiers.Any(m => m.Kind() == SyntaxKind.StaticKeyword))
                                tsMethod.Modifiers.Add(new TSSyntaxToken {Kind = TSKind.Static});
                        }
                    }

                    if (memberNode is EnumDeclarationSyntax)
                    {
                        var enumNode = memberNode as EnumDeclarationSyntax;

                        var tsEnumNode = new TSEnumDeclaration
                        {
                            Modifiers = { new TSSyntaxToken { Kind = TSKind.Export } },
                            Name = enumNode.Identifier.Text
                        };

                        tsModule.Members.Add(tsEnumNode);

                        foreach (var enumValue in enumNode.Members)
                        {
                            var tsEnumValueNode = new TSEnumMemberDeclaration
                            {
                                Name = enumValue.Identifier.Text
                            };

                            tsEnumNode.Members.Add(tsEnumValueNode);

                            if (enumValue.EqualsValue != null)
                            {
                                tsEnumValueNode.Value = enumValue.EqualsValue.Value.ToString();
                            }
                        }
                    }
                }
            }

            return tsUnit.ToString();
        }
    }
}
