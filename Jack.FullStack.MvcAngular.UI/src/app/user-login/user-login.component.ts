import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/mvc-api/services/Jack.FullStack.MvcAngular.API.Controllers.User.Service';
import { Router } from '@angular/router';
import { RoleEnum, RoleEnumConverter } from '../services/mvc-api/enums/Jack.FullStack.MvcAngular.API.Dtos.RoleEnum';
import { MD5 } from '../auth/md5';
import { LoginSingleton } from '../auth/login-singleton.service';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.scss']
})
export class UserLoginComponent implements OnInit {

  role: RoleEnum;
  roles= RoleEnumConverter.all.filter(r => r != 'Any');

  username: string;
  password: string;

  constructor(public user: UserService, public loginSingleton: LoginSingleton, public router: Router) { }

  ngOnInit() {
    
  }

  login(){
    this.user.Login({
      UserType: this.role,
      Id: this.username,
      PasswordHash: MD5.encrypt(this.password)
    }).subscribe(loginToken =>{
      this.loginSingleton.token = loginToken;
      
    });
  }
}
