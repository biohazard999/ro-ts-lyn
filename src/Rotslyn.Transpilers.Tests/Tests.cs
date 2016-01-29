using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalTests.Utilities;
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

        [Fact]
        public void BasicPublicStaticClass()
        {
            var csCode = ReadFile(nameof(BasicPublicStaticClass), FileType.CS);
            var tsCode = ReadFile(nameof(BasicPublicStaticClass), FileType.TS);
            var transipledTsCode = RotslynTranspiler.Transpile(csCode, Language.CSharp);

            if (!tsCode.Equals(transipledTsCode))
            {
                tsCode.DiffWith(transipledTsCode);
                transipledTsCode.ShouldBe(tsCode);
            }
        }
    }
}
