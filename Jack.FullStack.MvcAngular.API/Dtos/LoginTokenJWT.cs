using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jack.FullStack.MvcAngular.API.Dtos
{
    public class LoginTokenJWT
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public RoleEnum? Role { get; set; }
        public string ExpiringDate { get; set; }
    }
}
