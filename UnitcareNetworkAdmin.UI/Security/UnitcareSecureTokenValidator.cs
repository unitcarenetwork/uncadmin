using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using JWT;
using UnitcareNetworkAdmin.Auth;
using UnitcareNetworkAdmin.UI.Providers;

namespace UnitcareNetworkAdmin.UI.Security
{
  
    public class UnitcareSecureTokenValidator : ITokenValidator
    {
        private readonly IConfigProvider configProvider;

        public UnitcareSecureTokenValidator(IConfigProvider configProvider)
        {
            this.configProvider = configProvider;
        }

        public ClaimsPrincipal ValidateUser(string token)
        {
            try
            {
                var decodedtoken = JsonWebToken.DecodeToObject(token, configProvider.GetAppSetting("securekey")) as Dictionary<string, object>;

                var jwttoken = new UnitcareJwtToken()
                {
                    Audience = (string)decodedtoken["Audience"],
                    Issuer = (string)decodedtoken["Issuer"],
                    Expiry = DateTime.Parse(decodedtoken["Expiry"].ToString()),
                };

                if (decodedtoken.ContainsKey("Claims"))
                {
                    var claims = new List<Claim>();

                    for (var i = 0; i < ((ArrayList)decodedtoken["Claims"]).Count; i++)
                    {
                        var type = ((Dictionary<string, object>)((ArrayList)decodedtoken["Claims"])[i])["Type"].ToString();
                        var value = ((Dictionary<string, object>)((ArrayList)decodedtoken["Claims"])[i])["Value"].ToString();
                        claims.Add(new Claim(type, value));
                    }

                    jwttoken.Claims = claims;
                }

                if (jwttoken.Expiry < DateTime.UtcNow)
                {
                    return null;
                }

                //TODO Tidy on 3.8 Mono release
                var claimsPrincipal = new ClaimsPrincipal();
                var claimsIdentity = new ClaimsIdentity("Token");
                claimsIdentity.AddClaims(jwttoken.Claims);
                claimsPrincipal.AddIdentity(claimsIdentity);
                return claimsPrincipal;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
