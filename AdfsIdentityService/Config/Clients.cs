using System.Collections.Generic;
using IdentityServer3.Core.Models;

namespace IdentityService.Config
{
    public static class Clients
    {
        private static List<Client> _clients = new List<Client>(); 

        internal static IEnumerable<Client> Get()
        {
            return _clients;
        }
    }
}
