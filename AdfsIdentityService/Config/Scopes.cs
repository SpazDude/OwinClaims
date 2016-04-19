using System.Collections.Generic;
using IdentityServer3.Core.Models;

namespace IdentityService.Config
{
    public static class Scopes
    {
        private static readonly List<Scope> _scopes = new List<Scope>();

        internal static IEnumerable<Scope> Get()
        {
            return _scopes;
        }
    }
}
