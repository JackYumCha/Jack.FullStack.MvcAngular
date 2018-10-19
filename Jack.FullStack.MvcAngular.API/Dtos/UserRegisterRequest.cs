using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcAngular;

namespace Jack.FullStack.MvcAngular.API.Dtos
{
    [AngularType]
    public class UserRegisterRequest
    {
        public RoleEnum UserType { get; set; }
        public string Id { get; set; }
        public string Surname { get; set; }
        public string GivenName { get; set; }
        public string PasswordHash { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
