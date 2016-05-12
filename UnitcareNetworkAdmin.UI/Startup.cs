using System.Collections.Generic;
using Owin;
using UnitcareNetworkAdmin.Auth;
using UnitcareNetworkAdmin.UI.Providers;
using UnitcareNetworkAdmin.UI.Security;

namespace UnitcareNetworkAdmin.UI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.RequiresStatelessAuth(new UnitcareSecureTokenValidator(new ConfigProvider()), new AuthOptions() {IgnorePaths = new List<string>(new []{"/login","/content/*.js"})}).UseNancy();
        }
    }
}
