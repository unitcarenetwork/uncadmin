using System.Security.Claims;

namespace UnitcareNetworkAdmin.Auth
{
    public interface ITokenValidator
    {
        ClaimsPrincipal ValidateUser(string token);
    }
}