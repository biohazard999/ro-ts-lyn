using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rotslyn.Transpilers.Tests.Samples
{
    public class BasicClassWithMixedProperties
    {
        private int NormalPrivateProperty { get; set; }
        protected int NormalProtectedProperty { get; set; }
        public int NormalPublicProperty { get; set; }

        int PrivatePropertyWithNoAccessors { get; set; }
        public int PublicPropertyWithPrivateGetAccessorAndNormalSetAccessor { private get; set; }
        public int PublicPropertyWithProtectedGetAccessorAndNormalSetAccessor { protected get; set; }
        protected int ProtectedPropertyWithPrivateGetAccessorAndNormalSetAccessor { private get; set; }

        public int PublicPropertyWithPrivateGetterOnly { get; }
        private int PrivatePropertyWithProtectedGetterOnly { get; }
        protected int ProtectedPropertyWithPrivateGetterOnly { get; }
        
        public int PublicPropertyWithNoGetAccessorAndProtectedSetAccessor { get; protected set; }
        public int PublicPropertyWithNoGetAccessorAndPrivateSetAccessor { get; private set; }


        private int PrivateMember;
        protected int ProtectedMember;
        public int PublicMember;
        int MemberWithNoAccessors;
    }
}
