import { IntValue } from './services/mvc-api/datatypes/Jack.FullStack.MvcAngular.API.Dtos.IntValue';
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
  c: number = 0;

  calculate(){
    this.testService.Add2(this.a, this.b).subscribe(this.processResponse2);
  }

  processResponse = (value: IntValue) => {
    this.c = value.Value;
  }

  processResponse2 = (value: number) => {
    this.c = value;
  }

}
