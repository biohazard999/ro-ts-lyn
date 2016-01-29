using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Rotslyn.Transpilers.Tests
{
    public class Tests
    {
        [Fact]
        public void OneIsOne()
        {
            1.ShouldBe(1);
        }
    }
}
