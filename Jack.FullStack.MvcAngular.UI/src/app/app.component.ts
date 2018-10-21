import { TestService } from './services/mvc-api/services/Jack.FullStack.MvcAngular.API.Controllers.Test.Service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'UI';

  public constructor(public testService: TestService){

  }

  a: number = 0;
  b: number = 0;

  calculate(){

  }
}
