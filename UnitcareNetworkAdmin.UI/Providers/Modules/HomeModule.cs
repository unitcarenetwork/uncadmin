using System;
using System.Collections.Generic;
using System.Security.Claims;
using JWT;
using Nancy;
using Nancy.ModelBinding;
using UnitcareNetworkAdmin.UI.Models;
using UnitcareNetworkAdmin.UI.Providers;
using UnitcareNetworkAdmin.UI.Security;

namespace UnitcareNetworkAdmin.UI.Modules
{
    public class HomeModule : NancyModule
    {
        private readonly IUnitcareJwtWrapper _unitcareJwtWrapper;

        public HomeModule(IConfigProvider configProvider, IUnitcareJwtWrapper unitcareJwtWrapper)
        {
            _unitcareJwtWrapper = unitcareJwtWrapper;
            Get["/login"] = _ => View["Login"];

            Post["/login"] = _ =>
            {
                var user = this.Bind<UserCredentials>();
                //Verify user/pass
                if (user.User != "fred" && user.Password != "securepwd")
                {
                    return 401;
                }

                //generate token
                var jwttoken = new UnitcareJwtToken()
                {
                    Issuer = "http://issuer.com",
                    Audience = "http://mycoolwebsite.com",
                    Claims =
                        new List<Claim>(new[]
                        {
                            new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Administrator"),
                            new Claim(ClaimTypes.Name, "Fred")
                        }),
                    Expiry = DateTime.UtcNow.AddDays(7)
                };
                
                var token = _unitcareJwtWrapper.Encode(jwttoken, configProvider.GetAppSetting("securekey"), JwtHashAlgorithm.HS256);
                return Negotiate.WithModel(token);
            };

            Get["/"] = _ => "Hello Secure World!";
        }
    }
}
