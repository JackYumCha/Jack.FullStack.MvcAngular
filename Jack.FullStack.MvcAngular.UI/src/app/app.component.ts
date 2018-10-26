import { LoginSingleton } from './auth/login-singleton.service';
import { IntValue } from './services/mvc-api/datatypes/Jack.FullStack.MvcAngular.API.Dtos.IntValue';
import { TestService } from './services/mvc-api/services/Jack.FullStack.MvcAngular.API.Controllers.Test.Service';
import { Component } from '@angular/core';
import { merge, forkJoin } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  
 

  public constructor(public login: LoginSingleton){

  }

  get hasLogin(): boolean {
    return this.login && this.login.token && this.login.token.Key && this.login.token.Name && true;
  }
  get username(): string{
    return this.login.token.Name;
  }
 
}
