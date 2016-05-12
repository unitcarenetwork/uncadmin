using System.Collections.Generic;
using Nancy.Security;

namespace UnitcareNetworkAdmin.UI.Security
{
    public class UnitcareUserIdentity : IUserIdentity
    {
        public string UserName { get; set; }
        public IEnumerable<string> Claims { get; set; }
    }
}