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
using Jack.DataScience.Data.Arango;
using ArangoDB.Client;

namespace Jack.FullStack.MvcAngular.API.Controllers
{
    [Angular, Route("[controller]/[action]")]
    public class UserController : Controller
    {

        private readonly RoleJwtEncoder jwtEncoder;
        private readonly CookieOptions loginCookieOptions;
        private readonly IArangoDatabase arangoDatabase;

        public UserController(
            RoleJwtEncoder jwtEncoder, 
            AuthOptions mediGraphOptions,
            ArangoConnection arangoConnection)
        {
            this.jwtEncoder = jwtEncoder;
            loginCookieOptions = new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(mediGraphOptions.TokenExpiringDays),
                HttpOnly = true
            };
            arangoDatabase = arangoConnection.CreateClient();
        }

        [HttpPost]
        public async Task<LoginToken> Login([FromBody] LoginRequest loginRequest)
        {
            var found = await arangoDatabase.Query<User>()
                .Filter(u =>
                u._key == loginRequest.Id
                && u.PasswordHash == loginRequest.PasswordHash
                && u.Role == loginRequest.UserType).ToListAsync();

            // check login via data base
            var user = found.FirstOrDefault();
            if (user != null)
            {
                var token = new LoginToken()
                {
                    Key = user._key,
                    Name = $"{user.Surname} {user.GivenName}",
                    Role = loginRequest.UserType,
                    ExpiringDate = DateTime.Now.AddDays(15)
                };
                return Response.WriteJWTCookie(token);
            }

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

            User user = new User()
            {
                _key = registerRequest.Id,
                PasswordHash = registerRequest.PasswordHash,
                DateOfBirth = registerRequest.DateOfBirth,
                GivenName = registerRequest.GivenName,
                Surname = registerRequest.Surname
            };

            var foundExisting = await arangoDatabase.Query<User>().Filter(u => u._key == registerRequest.Id).ToListAsync();

            if(foundExisting.Count > 0)
            {
                return new UserRegisterResponse()
                {
                    LoginResult = null,
                    Success = false
                };
            }
 
            arangoDatabase.Upsert(user);

            var token = new LoginToken()
            {
                Key = user._id,
                Name = $"{user.Surname} {user.GivenName}",
                Role = registerRequest.UserType,
                ExpiringDate = DateTime.Now.AddDays(15)
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
