using Owin;

namespace UnitcareNetworkAdmin.Auth
{
    public static class AuthExtensions
    {
        public static IAppBuilder RequiresStatelessAuth(this IAppBuilder app, ITokenValidator tokenValidator, AuthOptions options = null)
        {
            return app.Use(typeof(AuthCoordinator), tokenValidator, options);
        }
    }
}
