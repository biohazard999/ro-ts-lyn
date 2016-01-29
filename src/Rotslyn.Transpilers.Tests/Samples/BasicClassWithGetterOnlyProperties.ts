﻿module Rotslyn.Transpilers.Tests.Samples {
    export class BasicClassWithGetterOnlyProperties {
        private _____intMember: number;
        public get intMember(): number {
            return this._____intMember;
        }

        private _____doubleMember: number;
        public get doubleMember(): number {
            return this._____doubleMember;
        }

        private _____doubleTypeMember: number;
        public get doubleTypeMember(): number {
            return this._____doubleTypeMember;
        }

        private _____floatMember: number;
        public get floatMember(): number {
            return this._____floatMember;
        }

        private _____floatTypeMember: number;
        public get floatTypeMember(): number {
            return this._____floatTypeMember;
        }

        private _____decimalMember: number;
        public get decimalMember(): number {
            return this._____decimalMember;
        }

        private _____decimalTypeMember: number;
        public get decimalTypeMember(): number {
            return this._____decimalTypeMember;
        }

        private _____int16Member: number;
        public get int16Member(): number {
            return this._____int16Member;
        }

        private _____int32Member: number;
        public get int32Member(): number {
            return this._____int32Member;
        }

        private _____int64Member: number;
        public get int64Member(): number {
            return this._____int64Member;
        }

        private _____stringKeywordMember: string;
        public get stringKeywordMember(): string {
            return this._____stringKeywordMember;
        }

        private _____stringTypeMember: string;
        public get stringTypeMember(): string {
            return this._____stringTypeMember;
        }

        private _____booleanKeywordMember: boolean;
        public get booleanKeywordMember(): boolean {
            return this._____booleanKeywordMember;
        }

        private _____booleanTypeMember: boolean;
        public get booleanTypeMember(): boolean {
            return this._____booleanTypeMember;
        }
    }
}