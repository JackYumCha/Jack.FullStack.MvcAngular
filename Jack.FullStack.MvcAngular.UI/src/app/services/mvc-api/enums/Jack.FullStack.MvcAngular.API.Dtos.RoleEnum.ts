/** 
 * Auto Generated Code
 * Please do not modify this file manually 
 * Assembly Name: "Jack.FullStack.MvcAngular.API"
 * Source Type: "C:\Users\zhenyu.shi\Documents\GitHub\Jack.FullStack.MvcAngular\Jack.FullStack.MvcAngular.API\bin\Debug\netcoreapp2.1\Jack.FullStack.MvcAngular.API.dll"
 * Generated At: 2018-10-19 16:36:15.226
 */

export type RoleEnum = 'Admin'|'User'|'Any';

declare global{
	interface Number{
		toRoleEnum (): RoleEnum;
	}
}

export class RoleEnumConverter extends Number {
	public static convert (value: number): RoleEnum {
		switch(value){
			case 0:
				return 'Admin';
			case 1:
				return 'User';
			case 2:
				return 'Any';
		}
	}
	public static parse (value: string): number | undefined {
		switch(value){
			case 'Admin':
				return 0;
			case 'User':
				return 1;
			case 'Any':
				return 2;
		}
		return undefined;
	}
	public static all: RoleEnum[] = ['Admin', 'User', 'Any'];
}

class RoleEnumExtensions extends Number {
	public toRoleEnum (): RoleEnum {
		return RoleEnumConverter.convert(<any>this);
	}
}

Number.prototype.toRoleEnum = RoleEnumExtensions.prototype.toRoleEnum;

export module RoleEnum {
	export const Admin = 'Admin';
	export const User = 'User';
	export const Any = 'Any';
}
