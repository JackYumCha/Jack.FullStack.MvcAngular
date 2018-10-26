using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArangoDB.Client;
using MvcAngular;

namespace Jack.FullStack.MvcAngular.API.Dtos
{
    [AngularType]
    public class User: VertexBase
    {
        public string PasswordHash { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public RoleEnum Role { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
