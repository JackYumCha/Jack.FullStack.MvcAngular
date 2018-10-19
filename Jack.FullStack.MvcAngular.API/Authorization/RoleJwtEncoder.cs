using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Jack.FullStack.MvcAngular.API.Authorization
{
    public class RoleJwtEncoder
    {
        private readonly string secret;
        private readonly IJwtAlgorithm algorithm;
        private readonly IJsonSerializer serializer;
        private readonly IBase64UrlEncoder urlEncoder;
        private readonly IDateTimeProvider provider;
        private readonly IJwtValidator validator;
        private readonly IJwtEncoder encoder;
        private readonly IJwtDecoder decoder;
        public RoleJwtEncoder(JwtSecretOptions jwtSecretOptions)
        {
            secret = jwtSecretOptions.Secret;
            algorithm = new HMACSHA256Algorithm();
            serializer = new JsonNetSerializer();
            urlEncoder = new JwtBase64UrlEncoder();
            provider = new UtcDateTimeProvider();
            validator = new JwtValidator(serializer, provider);
            encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            decoder = new JwtDecoder(serializer, validator, urlEncoder);
        }

        public string Encode(IDictionary<string, string> payload)
        {
            return encoder.Encode(payload, secret);
        }

        public JObject Decode(string jwt)
        {
            return JsonConvert.DeserializeObject<JObject>(decoder.Decode(jwt));
        }
    }
}
