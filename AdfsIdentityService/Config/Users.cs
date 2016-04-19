using System;
using System.Collections.Generic;
using IdentityServer3.Core.Services.InMemory;
using System.Security.Claims;

namespace IdentityService.Config
{
    public static class Users
    {
        private static readonly List<InMemoryUser> _users = new List<InMemoryUser>{
                new InMemoryUser()
                {
                    Username = "Username",
                    Password = "Password",
                    Enabled = true,
                    Provider = "provider",
                    ProviderId = "providerId",
                    Subject = "users",
                    Claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString("N")),
                        new Claim(ClaimTypes.Email, "testuser@testdomain.com"),
                        new Claim(ClaimTypes.Name, "Test User"),
                        new Claim(ClaimTypes.GivenName, "Test"),
                        new Claim(ClaimTypes.Surname, "User"),
                        new Claim(ClaimTypes.Role,"Student")
                    }
                }
            };

        public static List<InMemoryUser> Get()
        {
            return _users;
        }
    }
}
