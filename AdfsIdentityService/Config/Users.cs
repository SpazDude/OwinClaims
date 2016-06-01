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
                    Username = "Username1",
                    Password = "Password",
                    Enabled = true,
                    Provider = "provider",
                    ProviderId = "providerId",
                    Subject = "user1",
                    Claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, new Guid("{4ACD3D76-8AD4-41FD-99D7-0D066548F221}").ToString("N")),
                        new Claim(ClaimTypes.Email, "testuser@testdomain.com"),
                        new Claim(ClaimTypes.Name, "Test User"),
                        new Claim(ClaimTypes.GivenName, "Test"),
                        new Claim(ClaimTypes.Surname, "User"),
                        new Claim(ClaimTypes.Role,"Student")
                    }
                },
                new InMemoryUser()
                {
                    Username = "Username2",
                    Password = "Password",
                    Enabled = true,
                    Provider = "Provider",
                    ProviderId = "providerId",
                    Subject = "user2",
                    Claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, new Guid("{E9BC5C3B-F573-48C9-AA91-3D27674DB8DF}").ToString("N")),
                        new Claim(ClaimTypes.Email, "user2@testdomain.com"),
                        new Claim(ClaimTypes.Name, "Test User2"),
                        new Claim(ClaimTypes.GivenName, "Test"),
                        new Claim(ClaimTypes.Surname, "User2"),
                        new Claim(ClaimTypes.Role,"Staff")
                    }
                }
            };

        public static List<InMemoryUser> Get()
        {
            return _users;
        }
    }
}
