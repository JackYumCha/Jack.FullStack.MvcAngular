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

@NgModule({
  declarations: [
    AppComponent
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
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
