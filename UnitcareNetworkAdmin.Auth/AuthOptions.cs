using System.Collections.Generic;

namespace UnitcareNetworkAdmin.Auth
{
    public class AuthOptions
    {
        public IEnumerable<string> IgnorePaths { get; set; }
        public string WWWAuthenticateChallenge { get; set; }
        public bool PassThroughUnauthorizedRequests { get; set; }
    }
}
