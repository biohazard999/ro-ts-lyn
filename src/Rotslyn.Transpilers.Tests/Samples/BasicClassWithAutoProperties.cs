using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rotslyn.Transpilers.Tests.Samples
{
    public class BasicClassWithAutoProperties
    {
        public int IntMember { get; set; }
        public double DoubleMember { get; set; }
        public Double DoubleTypeMember { get; set; }
        public float FloatMember { get; set; }
        public Single FloatTypeMember { get; set; }
        public decimal DecimalMember { get; set; }
        public Decimal DecimalTypeMember { get; set; }
        public Int16 Int16Member { get; set; }
        public Int32 Int32Member { get; set; }
        public Int64 Int64Member { get; set; }
        public string StringKeywordMember { get; set; }
        public String StringTypeMember { get; set; }
        public bool BooleanKeywordMember { get; set; }
        public Boolean BooleanTypeMember { get; set; }
    }
}
