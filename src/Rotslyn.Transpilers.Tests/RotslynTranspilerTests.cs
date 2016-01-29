using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalTests.Utilities;
using Rotslyn.Transpilers.Tests.Samples;
using Shouldly;
using Xunit;

namespace Rotslyn.Transpilers.Tests
{
    [UseReporter(typeof(DiffReporter))]
    public class RotslynTranspilerTests
    {
        enum FileType
        {
            CS,
            TS
        }

        private string ReadFile(string name, FileType type)
        {
            var directory = "Samples";
            var fileName = $"{name}.{type.ToString().ToLowerInvariant()}";
            var filePath = Path.Combine(directory, fileName);
            return File.ReadAllText(filePath);
        }

        [Theory]
        [InlineData(nameof(BasicPublicStaticClass))]
        [InlineData(nameof(EmptyEnum))]
        [InlineData(nameof(BasicEnum))]
        [InlineData(nameof(EnumWithValues))]
        [InlineData(nameof(SelfReferencingEnum))]
        [InlineData(nameof(FlagsEnum))]
        [InlineData(nameof(MultipleTypesInSameFileClass))]
        [InlineData(nameof(Rotslyn.Transpilers.Tests.Samples.EmptyNamespace))]
        public void IntegrationTests(string testname)
        {
            var csCode = ReadFile(testname, FileType.CS);
            var tsCode = ReadFile(testname, FileType.TS);
            var transipledTsCode = RotslynTranspiler.Transpile(csCode, Language.CSharp);

            if (!tsCode.Equals(transipledTsCode))
            {
                tsCode.DiffWith(transipledTsCode);
                transipledTsCode.ShouldBe(tsCode);
            }
        }
    }
}
