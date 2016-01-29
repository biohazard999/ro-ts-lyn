using System;
using System.Collections.Generic;
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

                        foreach (var member in classNode.Members)
                        {
                            if (member is MethodDeclarationSyntax)
                            {
                                var method = member as MethodDeclarationSyntax;

                                var tsMethod = new TSMemberFunctionDeclaration
                                {
                                    Name = method.Identifier.Text,
                                    Body = "console.log(\"Hello World!\");",
                                    ReturnType = method.ReturnType.ToString(),
                                };

                                tsClassNode.Members.Add(tsMethod);

                                if (method.Modifiers.Any(m => m.Kind() == SyntaxKind.PublicKeyword))
                                    tsMethod.Modifiers.Add(new TSSyntaxToken {Kind = TSKind.Public});

                                if (method.Modifiers.Any(m => m.Kind() == SyntaxKind.StaticKeyword))
                                    tsMethod.Modifiers.Add(new TSSyntaxToken {Kind = TSKind.Static});
                            }

                            if (member is PropertyDeclarationSyntax)
                            {
                                var property = member as PropertyDeclarationSyntax;

                                var tsProperty = new TSPropertyDeclaration
                                {
                                    Name = property.Identifier.Text,
                                    //Body = "console.log(\"Hello World!\");",
                                    ReturnType = property.Type.GetTSType(),
                                };

                                if(property.AccessorList.Accessors.Any(accessor => accessor.Keyword.IsKind(SyntaxKind.GetKeyword)))
                                {
                                    var getAccessor = property.AccessorList.Accessors.First(accessor => accessor.Keyword.IsKind(SyntaxKind.GetKeyword));

                                    var tsAccessor = new TSAccessorSyntax();

                                    tsProperty.Getter = tsAccessor;

                                    if (getAccessor.Modifiers.Any(m => m.Kind() == SyntaxKind.PublicKeyword))
                                        tsAccessor.Modifiers.Add(new TSSyntaxToken { Kind = TSKind.Public });

                                    if (getAccessor.Modifiers.Any(m => m.Kind() == SyntaxKind.PrivateKeyword))
                                        tsAccessor.Modifiers.Add(new TSSyntaxToken { Kind = TSKind.Private });

                                    if (getAccessor.Modifiers.Any(m => m.Kind() == SyntaxKind.ProtectedKeyword))
                                        tsAccessor.Modifiers.Add(new TSSyntaxToken { Kind = TSKind.Protected });

                                    if (getAccessor.Modifiers.Any(m => m.Kind() == SyntaxKind.StaticKeyword))
                                        tsAccessor.Modifiers.Add(new TSSyntaxToken { Kind = TSKind.Static });
                                }

                                if (property.AccessorList.Accessors.Any(accessor => accessor.Keyword.IsKind(SyntaxKind.SetKeyword)))
                                {
                                    var getAccessor = property.AccessorList.Accessors.First(accessor => accessor.Keyword.IsKind(SyntaxKind.SetKeyword));

                                    var tsAccessor = new TSAccessorSyntax();

                                    tsProperty.Setter = tsAccessor;

                                    if (getAccessor.Modifiers.Any(m => m.Kind() == SyntaxKind.PublicKeyword))
                                        tsAccessor.Modifiers.Add(new TSSyntaxToken { Kind = TSKind.Public });

                                    if (getAccessor.Modifiers.Any(m => m.Kind() == SyntaxKind.PrivateKeyword))
                                        tsAccessor.Modifiers.Add(new TSSyntaxToken { Kind = TSKind.Private });

                                    if (getAccessor.Modifiers.Any(m => m.Kind() == SyntaxKind.ProtectedKeyword))
                                        tsAccessor.Modifiers.Add(new TSSyntaxToken { Kind = TSKind.Protected });

                                    if (getAccessor.Modifiers.Any(m => m.Kind() == SyntaxKind.StaticKeyword))
                                        tsAccessor.Modifiers.Add(new TSSyntaxToken { Kind = TSKind.Static });
                                }

                                tsClassNode.Members.Add(tsProperty);

                                if (property.Modifiers.Any(m => m.Kind() == SyntaxKind.PublicKeyword))
                                    tsProperty.Modifiers.Add(new TSSyntaxToken { Kind = TSKind.Public });

                                if (property.Modifiers.Any(m => m.Kind() == SyntaxKind.StaticKeyword))
                                    tsProperty.Modifiers.Add(new TSSyntaxToken { Kind = TSKind.Static });
                            }
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

    public static class TypeMap
    {
        public const string NumberType = "number";
        public const string BooleanType = "boolean";
        public const string StringType = "string";
        public const string VoidType = "void";
        public const string AnyType = "any";

        static TypeMap()
        {
            TypeMappings.AddRange(NumberTypeMappings);
            TypeMappings.AddRange(BooleanTypeMappings);
            TypeMappings.AddRange(StringTypeMappings);
            TypeMappings.AddRange(AnyTypeMappings);
        }

        private static void AddRange(this IDictionary<string, string> dictionary,
            IDictionary<string, string> dictionaryToAdd)
        {
            foreach(var pair in dictionaryToAdd)
                dictionary.Add(pair);
        }

        public static Dictionary<string, string> TypeMappings = new Dictionary<string, string>();

        public static Dictionary<string, string> NumberTypeMappings = new Dictionary<string, string>()
        {
            [nameof(Int32)] = NumberType,
            [nameof(Int16)] = NumberType,
            [nameof(Int64)] = NumberType,
            [nameof(Double)] = NumberType,
            [nameof(Single)] = NumberType,
            [nameof(Decimal)] = NumberType,
        };

        public static Dictionary<string, string> BooleanTypeMappings = new Dictionary<string, string>()
        {
            [nameof(Boolean)] = BooleanType,
        };

        public static Dictionary<string, string> StringTypeMappings = new Dictionary<string, string>()
        {
            [nameof(String)] = StringType,
        };

        public static Dictionary<string, string> AnyTypeMappings = new Dictionary<string, string>()
        {
            [nameof(Object)] = AnyType,
        };

        public static List<SyntaxKind> NumberKinds { get; }= new List<SyntaxKind>
        {
            SyntaxKind.IntKeyword,
            SyntaxKind.DoubleKeyword,
            SyntaxKind.FloatKeyword,
            SyntaxKind.DecimalKeyword
        };

        public static List<SyntaxKind> BooleanKinds { get; } = new List<SyntaxKind>
        {
            SyntaxKind.BoolKeyword,
        };

        public static List<SyntaxKind> StringKinds { get; } = new List<SyntaxKind>
        {
            SyntaxKind.StringKeyword,
        };

        public static List<SyntaxKind> VoidKinds { get; } = new List<SyntaxKind>
        {
            SyntaxKind.VoidKeyword,
        };
        public static List<SyntaxKind> AnyKinds { get; } = new List<SyntaxKind>
        {
            SyntaxKind.ObjectKeyword,
        };

        public static string GetTSType(this TypeSyntax typeSyntax)
        {
            if (typeSyntax is PredefinedTypeSyntax)
            {
                var predefinedType = (typeSyntax as PredefinedTypeSyntax);
                var kind = predefinedType.Keyword.Kind();
                if (NumberKinds.Contains(kind))
                    return NumberType;

                if (BooleanKinds.Contains(kind))
                    return BooleanType;

                if (StringKinds.Contains(kind))
                    return StringType;

                if (VoidKinds.Contains(kind))
                    return VoidType;

                if (AnyKinds.Contains(kind))
                    return AnyType;
            }

            var identifier = typeSyntax.ToString();
            if (TypeMappings.ContainsKey(identifier))
                return TypeMappings[identifier];

            return identifier;
        }
    }
}
