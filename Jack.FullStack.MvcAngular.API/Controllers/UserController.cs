using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MvcAngular;
using Newtonsoft.Json.Linq;
using Jack.FullStack.MvcAngular.API.Dtos;
using Jack.FullStack.MvcAngular.API.Authorization;
using Jack.DataScience.Common;

namespace Jack.FullStack.MvcAngular.API.Controllers
{
    [Angular, Route("[controller]/[action]")]
    public class UserController : Controller
    {

        private readonly RoleJwtEncoder jwtEncoder;
        private readonly CookieOptions loginCookieOptions;

        public UserController(RoleJwtEncoder jwtEncoder, AuthOptions mediGraphOptions)
        {
            this.jwtEncoder = jwtEncoder;
            loginCookieOptions = new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(mediGraphOptions.TokenExpiringDays),
                HttpOnly = true
            };
        }

        [HttpPost]
        public async Task<LoginToken> Login([FromBody] LoginRequest loginRequest)
        {
            // check login via data base
            //var user = logins.FirstOrDefault();
            //if (user != null)
            //{
            //    var token = new LoginToken()
            //    {
            //        Key = user._key,
            //        Name = $"{user.Surname} {user.GivenName}",
            //        Role = loginRequest.UserType,
            //        ExpiringDate = DateTime.Now.AddDays(15)
            //    };
            //    return Response.WriteJWTCookie(token);
            //}

            return new LoginToken();
        }

        [HttpPost]
        public BooleanValue Logoff()
        {
            Response.DeleteJWTCookie();
            return new BooleanValue() { Value = true };
        }

        
        [HttpPost]
        [Role(RoleEnum.Any)]
        public LoginToken Renew()
        {
            string jwt = null;
            if (Request.Cookies.TryGetValue("jwt", out jwt))
            {
                var loginToken = jwtEncoder.Decode(jwt).ToObject<LoginTokenJWT>().ToToken();
                if (loginToken.ExpiringDate.HasValue && loginToken.ExpiringDate.Value > DateTime.Now)
                {
                    loginToken.ExpiringDate = DateTime.Now.AddDays(15);
                    loginToken.JWT = jwtEncoder.Encode(loginToken.ToDictionary());
                    Response.Cookies.Append("jwt", loginToken.JWT, loginCookieOptions);
                    return loginToken;
                }
            }
            return new LoginToken();
        }

        [HttpPost]
        public async Task<UserRegisterResponse> Register([FromBody] UserRegisterRequest registerRequest)
        {
            // create user in the database

            var token = new LoginToken()
            {
                //Key = user._key,
                //Name = $"{user.Surname} {user.GivenName}",
                //Role = registerRequest.UserType,
                //ExpiringDate = DateTime.Now.AddDays(15)
            };
            token = Response.WriteJWTCookie(token);

            return new UserRegisterResponse()
            {
                Success = true,
                LoginResult = token
            };
        }
    }
}
