using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcAngular;

namespace Jack.FullStack.MvcAngular.API.Dtos
{
    [AngularType]
    public class LoginToken
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public RoleEnum? Role { get; set; }
        public DateTime? ExpiringDate { get; set; }
        public string JWT { get; set; }
    }
}
