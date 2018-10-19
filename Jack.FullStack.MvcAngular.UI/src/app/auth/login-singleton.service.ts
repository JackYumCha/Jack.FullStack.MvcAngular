import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { take, map } from 'rxjs/operators';
import { LoginToken } from "../services/mvc-api/datatypes/Jack.FullStack.MvcAngular.API.Dtos.LoginToken";
import { UserService } from "../services/mvc-api/services/Jack.FullStack.MvcAngular.API.Controllers.User.Service";

@Injectable({
    providedIn: 'root'
})
export class LoginSingleton{
    token: LoginToken;
    private tokenHolder: Subject<LoginToken> = new Subject<LoginToken>();
    constructor(private user: UserService){ }

    get roleName(): string {
        return String(this.token.Role);
    }

    fetchToken(){
        return this.user.Renew()
            .pipe(map(token => {
                this.token = token;
                return token;
            }));
    }
}