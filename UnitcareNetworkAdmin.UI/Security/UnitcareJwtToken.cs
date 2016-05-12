using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace UnitcareNetworkAdmin.UI.Security
{
    public class UnitcareJwtToken
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
        public DateTime Expiry { get; set; }
    }
}