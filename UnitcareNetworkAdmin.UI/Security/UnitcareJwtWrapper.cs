using JWT;

namespace UnitcareNetworkAdmin.UI.Security
{
    public interface IUnitcareJwtWrapper
    {
        string Encode(object payload, string key, JwtHashAlgorithm algorithm);
    }
    public class UnitcareJwtWrapper : IUnitcareJwtWrapper
    {
        public string Encode(object payload, string key, JwtHashAlgorithm algorithm)
        {
            return JsonWebToken.Encode(payload, key, algorithm);
        }
    }
}