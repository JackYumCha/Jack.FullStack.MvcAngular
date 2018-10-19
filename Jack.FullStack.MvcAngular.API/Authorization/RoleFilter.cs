using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Jack.FullStack.MvcAngular.API.Dtos;

namespace Jack.FullStack.MvcAngular.API.Authorization
{
    public class RoleFilter : IAuthorizationFilter
    {
        private readonly RoleEnum[] roles;

        public RoleFilter(RoleEnum[] roles)
        {
            this.roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //string jwt = null;
            var login = context.HttpContext.Request.ReadJWTCookie();
            if (login != null && login.ExpiringDate.HasValue && login.ExpiringDate.Value > DateTime.Now)
            {
                if (roles.Any(role => role == RoleEnum.Any || role == login.Role))
                    return;
            }
            //if (context.HttpContext.Request.Cookies.TryGetValue("jwt", out jwt))
            //{
            //    var jwtEncoder = Startup.ApplicationContainer.Resolve<RoleJwtEncoder>();
            //    var login = jwtEncoder.Decode(jwt).ToObject<LoginTokenJWT>().ToToken();
            //    if (login.ExpiringDate.HasValue && login.ExpiringDate.Value > DateTime.Now)
            //    {
            //        if (roles.Any(role => role == RoleEnum.Any || role == login.Role))
            //            return;
            //    }
            //}
            context.Result = new UnauthorizedResult();
        }
    }
}
