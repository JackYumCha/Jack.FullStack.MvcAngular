using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Jack.FullStack.MvcAngular.API.Dtos;
using Jack.DataScience.Common;
using Autofac;

namespace Jack.FullStack.MvcAngular.API.Authorization
{
    public static class LoginTokenExtensions
    {
        public static Dictionary<string, string> ToDictionary(this LoginToken loginToken)
       => new Dictionary<string, string>()
       {
             {nameof(LoginToken.Key), loginToken.Key},
             {nameof(LoginToken.Name), loginToken.Name },
             {nameof(LoginToken.Role), loginToken.Role.ToString() },
             {nameof(LoginToken.ExpiringDate), loginToken.ExpiringDate.HasValue ? loginToken.ExpiringDate.Value.ToString("yyyyMMddHHmmss") : "" }
       };

        public static LoginToken ToToken(this LoginTokenJWT loginTokenJWT)
        {
            LoginToken loginToken = new LoginToken();
            loginToken.Key = loginTokenJWT.Key;
            loginToken.Name = loginTokenJWT.Name;
            loginToken.Role = loginTokenJWT.Role;
            DateTime parsedDate;
            if (DateTime.TryParseExact(loginTokenJWT.ExpiringDate, "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                loginToken.ExpiringDate = parsedDate;
            }
            else
            {
                loginToken.ExpiringDate = DateTime.MinValue;
            }
            return loginToken;
        }

        public static LoginToken WriteJWTCookie(this HttpResponse response, LoginToken token)
        {
            var jwtEncoder = Startup.ApplicationContainer.Resolve<RoleJwtEncoder>();
            token.JWT = jwtEncoder.Encode(token.ToDictionary());
            AuthOptions authOptions = Startup.ApplicationContainer.Resolve<AuthOptions>();
            response.Cookies.Append(authOptions.JWTCookieKey, token.JWT, new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(authOptions.TokenExpiringDays)
            });
            return token;
        }

        public static void DeleteJWTCookie(this HttpResponse response)
        {
            AuthOptions authOptions = Startup.ApplicationContainer.Resolve<AuthOptions>();
            response.Cookies.Delete(authOptions.JWTCookieKey);
        }

        public static LoginToken ReadJWTCookie(this HttpRequest request)
        {
            string jwt = null;
            AuthOptions authOptions = Startup.ApplicationContainer.Resolve<AuthOptions>();
            if (request.Cookies.TryGetValue(authOptions.JWTCookieKey, out jwt))
            {
                var jwtEncoder = Startup.ApplicationContainer.Resolve<RoleJwtEncoder>();
                var login = jwtEncoder.Decode(jwt).ToObject<LoginTokenJWT>().ToToken();
                return login;
            }
            return null;
        }
    }
}
