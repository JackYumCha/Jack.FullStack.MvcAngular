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
  title = 'UI';

  public constructor(public testService: TestService){

  }

  a: number = 0;
  b: number = 0;
  c: number = 0;

  calculate(){
    forkJoin(
    this.testService.Add2(this.a, this.b),
    this.testService.Add({a: this.a, b: this.b})
    ).subscribe(this.processResponseForkJoin);
  }

  processResponse = (value: IntValue) => {
    this.c = value.Value;
  }

  processResponse2 = (value: number) => {
    this.c = value;
  }

  processResponseForkJoin = (v1: [number, IntValue]) => {
    this.c = v1[0];
    console.log(v1[1]);
  }

  processResponseMerge = (v1: number|IntValue) => {
    if(typeof v1 == 'number'){
      this.c = v1;
      console.log('number: ', v1);
    }
    else{
      console.log('object: ', v1);
    }
  }
}
