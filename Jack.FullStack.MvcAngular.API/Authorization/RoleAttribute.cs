using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Jack.FullStack.MvcAngular.API.Dtos;

namespace Jack.FullStack.MvcAngular.API.Authorization
{
    public class RoleAttribute : TypeFilterAttribute
    {
        public RoleAttribute(params RoleEnum[] roles) : base(typeof(RoleFilter))
        {
            Arguments = new object[] { roles };
        }
    }
}
