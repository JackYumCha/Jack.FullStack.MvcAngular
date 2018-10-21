/** 
 * Auto Generated Code
 * Please do not modify this file manually 
 * Assembly Name: "Jack.FullStack.MvcAngular.API"
 * Source Type: "C:\Users\erris\Documents\GitHub\Jack.FullStack.MvcAngular\Jack.FullStack.MvcAngular.API\bin\Debug\netcoreapp2.1\Jack.FullStack.MvcAngular.API.dll"
 * Generated At: 2018-10-21 18:11:43.62
 */
import { AddDto } from '../datatypes/Jack.FullStack.MvcAngular.API.Dtos.AddDto';
import { IntValue } from '../datatypes/Jack.FullStack.MvcAngular.API.Dtos.IntValue';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable()
export class TestService {
	constructor(private $httpClient: HttpClient) {{}}
	public $baseURL: string = '<Jack.FullStack.MvcAngular.API>';
	public Add(add: AddDto): Observable<IntValue> {
		return this.$httpClient.post<IntValue>(this.$baseURL + 'Test/Add', add, {});
	}
	public Add2(a: number, b: number): Observable<number> {
		return this.$httpClient.get<number>(this.$baseURL + 'Test/Add2' + `?a=${encodeURIComponent(String(a))}&b=${encodeURIComponent(String(b))}`, {});
	}
}
