import { Injectable } from '@angular/core';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot, CanActivate} from '@angular/router';
import { LoginSingleton } from './login-singleton.service';
import { map } from 'rxjs/operators';
import { LoginToken } from '../services/mvc-api/datatypes/Jack.FullStack.MvcAngular.API.Dtos.LoginToken';

class RouterGuardBase implements CanActivate {
  constructor(public login: LoginSingleton ) {}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if(this.login.token){
      return this.check(this.login.token);
    }
    else{
      return this.login.fetchToken()
        .pipe(map(token=> this.check(token)));
    }
  }
  check = (token: LoginToken) => false;
}
