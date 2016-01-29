module Rotslyn.Transpilers.Tests.Samples {
    export class BasicClassWithMixedProperties {

        private normalPrivateProperty: number;

        protected normalProtectedProperty: number;

        public normalPublicProperty: number;

        private privatePropertyWithNoAccessors: number;

        public publicPropertyWithPrivateGetAccessorAndNormalSetAccessor: number;

        public publicPropertyWithProtectedGetAccessorAndNormalSetAccessor: number;

        public protectedPropertyWithPrivateGetAccessorAndNormalSetAccessor: number;

        private _____publicPropertyWithPrivateGetterOnly: number;
        public get publicPropertyWithPrivateGetterOnly(): number {
            return this._____publicPropertyWithPrivateGetterOnly;
        }

        private _____privatePropertyWithProtectedGetterOnly: number;
        private get privatePropertyWithProtectedGetterOnly(): number {
            return this._____privatePropertyWithProtectedGetterOnly;
        }

        protected _____protectedPropertyWithPrivateGetterOnly: number;
        protected get protectedPropertyWithPrivateGetterOnly(): number {
            return this._____protectedPropertyWithPrivateGetterOnly;
        }

        private privateMember: number;

        protected protectedMember: number;

        public publicMember: number;

        private memberWithNoAccessors: number;
    }
}