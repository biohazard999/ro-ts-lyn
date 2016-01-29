using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rotslyn.Transpilers.Tests.Samples
{
    public enum SelfReferencingEnum
    {
        Value1 = 10,
        Value2 = Value1,
        Value3 = Value2,
        Value4 = Value3,
        Value5 = Value4,
    }
}
