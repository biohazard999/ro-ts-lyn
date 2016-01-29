using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rotslyn.Transpilers.Tests.Samples
{
    public class BasicClassWithSetterOnlyProperties
    {
        public int IntMember { private get; set; }
        public double DoubleMember { protected get; set; }
        public Double DoubleTypeMember { private get; set; }
        public float FloatMember { protected get; set; }
        public Single FloatTypeMember { private get; set; }
        public decimal DecimalMember { protected get; set; }
        public Decimal DecimalTypeMember { private get; set; }
        public Int16 Int16Member { protected get; set; }
        public Int32 Int32Member { private get; set; }
        public Int64 Int64Member { protected get; set; }
        public string StringKeywordMember { private get; set; }
        public String StringTypeMember { protected get; set; }
        public bool BooleanKeywordMember { private get; set; }
        public Boolean BooleanTypeMember { protected get; set; }
        public object ObjectKeywordMember { private get; set; }
        public Object ObjectTypeMember { internal get; set; }
    }
}
