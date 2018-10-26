import { Router } from '@angular/router';
import { Component, OnInit, OnDestroy, OnChanges } from '@angular/core';
import { UserService } from '../services/mvc-api/services/Jack.FullStack.MvcAngular.API.Controllers.User.Service';
import { MD5 } from '../auth/md5';
import { RoleEnumConverter, RoleEnum } from '../services/mvc-api/enums/Jack.FullStack.MvcAngular.API.Dtos.RoleEnum';
import { LoginSingleton } from '../auth/login-singleton.service';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.scss']
})
export class UserRegisterComponent implements OnInit, OnDestroy, OnChanges {

  username: string;
  password: string;
  givenName: string;
  surname: string;
  role: RoleEnum;
  roles = RoleEnumConverter.all.filter(role => role != 'Any');
  dateOfBirth: string;

  constructor(public user: UserService, public login: LoginSingleton, public router: Router) { 

    
  }

  ngOnInit() {

  }

  ngOnDestroy(){

  }

  ngOnChanges(){

  }

  registerUser(){
    this.user.Register({
      UserType: this.role,
      Id: this.username,
      PasswordHash: MD5.encrypt(this.password),
      GivenName: this.givenName,
      Surname: this.surname,
      DateOfBirth: this.dateOfBirth
    }).subscribe(res => {
      if(res.Success){
        this.login.token = res.LoginResult;
        this.router.navigate(['/']);
      }
      else{

      }
    });
  }

}
