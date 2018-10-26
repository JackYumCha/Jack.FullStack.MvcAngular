import { AppRouterModule } from './app-router-module';
import { TestService } from './services/mvc-api/services/Jack.FullStack.MvcAngular.API.Controllers.Test.Service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { MvcAngularInterceptor } from './services/mvc-api/mvcangular.interceptor';
import { UserService } from './services/mvc-api/services/Jack.FullStack.MvcAngular.API.Controllers.User.Service';
import { RouterModule } from '@angular/router';
import { UserLoginComponent } from './user-login/user-login.component';
import { UserRegisterComponent } from './user-register/user-register.component';
import { UserViewComponent } from './user-view/user-view.component';
import { AdminViewComponent } from './admin-view/admin-view.component';
import { DashboardViewComponent } from './dashboard-view/dashboard-view.component';
import { UserRouterGuard, AdminRouterGuard } from './auth/auth-guard';

@NgModule({
  declarations: [
    AppComponent,
    UserLoginComponent,
    UserRegisterComponent,
    UserViewComponent,
    AdminViewComponent,
    DashboardViewComponent
  ],
  imports: [
    AppRouterModule,
    BrowserModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    UserService,
    TestService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MvcAngularInterceptor,
      multi: true
    },
    UserRouterGuard,
    AdminRouterGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
