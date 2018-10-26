import { TestService } from './../services/mvc-api/services/Jack.FullStack.MvcAngular.API.Controllers.Test.Service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard-view',
  templateUrl: './dashboard-view.component.html',
  styleUrls: ['./dashboard-view.component.scss']
})
export class DashboardViewComponent implements OnInit {

  a: number = 0;
  b: number = 1;
  c: number = 1;

  constructor(public test: TestService) { }

  ngOnInit() {
  }

  calculate1(){
    this.test.Add({
      a: this.a,
      b: this.b
    }).subscribe(intValue=>{
      this.c = intValue.Value;
    });
  }

  calculate2(){
    this.test.Add2(this.a, this.b).subscribe(value=>{
      this.c = value;
    });
  }
}
