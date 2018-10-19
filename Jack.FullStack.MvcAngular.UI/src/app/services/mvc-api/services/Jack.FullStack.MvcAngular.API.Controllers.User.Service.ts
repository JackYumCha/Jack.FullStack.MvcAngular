/** 
 * Auto Generated Code
 * Please do not modify this file manually 
 * Assembly Name: "Jack.FullStack.MvcAngular.API"
 * Source Type: "C:\Users\zhenyu.shi\Documents\GitHub\Jack.FullStack.MvcAngular\Jack.FullStack.MvcAngular.API\bin\Debug\netcoreapp2.1\Jack.FullStack.MvcAngular.API.dll"
 * Generated At: 2018-10-19 16:51:40.986
 */
import { LoginRequest } from '../datatypes/Jack.FullStack.MvcAngular.API.Dtos.LoginRequest';
import { LoginToken } from '../datatypes/Jack.FullStack.MvcAngular.API.Dtos.LoginToken';
import { BooleanValue } from '../datatypes/Jack.FullStack.MvcAngular.API.Dtos.BooleanValue';
import { UserRegisterRequest } from '../datatypes/Jack.FullStack.MvcAngular.API.Dtos.UserRegisterRequest';
import { UserRegisterResponse } from '../datatypes/Jack.FullStack.MvcAngular.API.Dtos.UserRegisterResponse';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable()
export class UserService {
	constructor(private $httpClient: HttpClient) {{}}
	public $baseURL: string = '<Jack.FullStack.MvcAngular.API>';
	public Login(loginRequest: LoginRequest): Observable<LoginToken> {
		return this.$httpClient.post<LoginToken>(this.$baseURL + 'User/Login', loginRequest, {});
	}
	public Logoff(): Observable<BooleanValue> {
		return this.$httpClient.post<BooleanValue>(this.$baseURL + 'User/Logoff', null, {});
	}
	public Renew(): Observable<LoginToken> {
		return this.$httpClient.post<LoginToken>(this.$baseURL + 'User/Renew', null, {});
	}
	public Register(registerRequest: UserRegisterRequest): Observable<UserRegisterResponse> {
		return this.$httpClient.post<UserRegisterResponse>(this.$baseURL + 'User/Register', registerRequest, {});
	}
}
