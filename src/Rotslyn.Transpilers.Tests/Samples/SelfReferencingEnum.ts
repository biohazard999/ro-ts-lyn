module Rotslyn.Transpilers.Tests.Samples {
    export enum SelfReferencingEnum {
        Value1 = 10,
        Value2 = Value1,
        Value3 = Value2,
        Value4 = Value3,
        Value5 = Value4
    }
}