using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rotslyn.Transpilers.Tests.Samples
{
    public enum FlagsEnum
    {
        None = 0,
        HasClaws = 1 << 0,
        CanFly = 1 << 1,
        EatsFish = 1 << 2,
        Endangered = 1 << 3,

        EndangeredFlyingClawedFishEating = HasClaws | CanFly | EatsFish | Endangered,
    }
}
