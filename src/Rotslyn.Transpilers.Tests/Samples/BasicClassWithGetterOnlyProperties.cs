using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rotslyn.Transpilers.Tests.Samples
{
    public class BasicClassWithGetterOnlyProperties
    {
        public int IntMember { get; }
        public double DoubleMember { get; }
        public Double DoubleTypeMember { get; }
        public float FloatMember { get; }
        public Single FloatTypeMember { get; }
        public decimal DecimalMember { get; }
        public Decimal DecimalTypeMember { get; }
        public Int16 Int16Member { get; }
        public Int32 Int32Member { get; }
        public Int64 Int64Member { get; }
        public string StringKeywordMember { get; }
        public String StringTypeMember { get; }
        public bool BooleanKeywordMember { get; }
        public Boolean BooleanTypeMember { get; }
    }
}
