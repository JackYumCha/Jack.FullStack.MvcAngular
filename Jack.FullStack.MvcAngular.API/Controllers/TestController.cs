using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcAngular;
using Jack.FullStack.MvcAngular.API.Dtos;

namespace Jack.FullStack.MvcAngular.API.Controllers
{
    [Angular, Route("[controller]/[action]")]
    public class TestController: Controller
    {
        [HttpPost]
        public IntValue Add([FromBody] AddDto add)
        {
            return new IntValue()
            {
                Value = add.a + add.b
            };
        }

        [HttpGet]
        public int Add2(int a, int b)
        {
            return a + b;
        }
    }
}
