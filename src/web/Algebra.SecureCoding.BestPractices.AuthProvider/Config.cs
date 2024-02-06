using Duende.IdentityServer.Models;
using IdentityModel;

namespace Algebra.SecureCoding.BestPractices.AuthProvider
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("api.read"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
               
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "https://localhost:44374/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:44374/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "api.read","role" }
                },
            };
    }
}
