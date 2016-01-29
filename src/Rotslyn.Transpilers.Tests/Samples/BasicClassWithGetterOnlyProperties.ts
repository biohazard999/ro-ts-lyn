module Rotslyn.Transpilers.Tests.Samples {
    export class BasicClassWithGetterOnlyProperties {
        private _intMember: number;
        public get intMember(): number {
            return this._intMember;
        }

        private _doubleMember: number;
        public get doubleMember(): number {
            return this._doubleMember;
        }

        private _doubleTypeMember: number;
        public get doubleTypeMember(): number {
            return this._doubleTypeMember;
        }

        private _floatMember: number;
        public get floatMember(): number {
            return this._floatMember;
        }

        private _floatTypeMember: number;
        public get floatTypeMember(): number {
            return this._floatTypeMember;
        }

        private _decimalMember: number;
        public get decimalMember(): number {
            return this._decimalMember;
        }

        private _decimalTypeMember: number;
        public get decimalTypeMember(): number {
            return this._decimalTypeMember;
        }

        private _int16Member: number;
        public get int16Member(): number {
            return this._int16Member;
        }

        private _int32Member: number;
        public get int32Member(): number {
            return this._int32Member;
        }

        private _int64Member: number;
        public get int64Member(): number {
            return this._int64Member;
        }

        private _stringKeywordMember: string;
        public get stringKeywordMember(): string {
            return this._stringKeywordMember;
        }

        private _stringTypeMember: string;
        public get stringTypeMember(): string {
            return this._stringTypeMember;
        }

        private _booleanKeywordMember: boolean;
        public get booleanKeywordMember(): boolean {
            return this._booleanKeywordMember;
        }

        private _booleanTypeMember: boolean;
        public get booleanTypeMember(): boolean {
            return this._booleanTypeMember;
        }
    }
}