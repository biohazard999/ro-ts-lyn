module Rotslyn.Transpilers.Tests.Samples {
    export enum FlagsEnum {
        None = 0,
        HasClaws = 1 << 0,
        CanFly = 1 << 1,
        EatsFish = 1 << 2,
        Endangered = 1 << 3,
        EndangeredFlyingClawedFishEating = HasClaws | CanFly | EatsFish | Endangered
    }
}