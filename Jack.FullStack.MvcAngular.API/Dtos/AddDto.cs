using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcAngular;

namespace Jack.FullStack.MvcAngular.API.Dtos
{
    [AngularType]
    public class AddDto
    {
        public int a { get; set; }
        public int b { get; set; }
    }
}
