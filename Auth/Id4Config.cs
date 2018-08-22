using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace Auth
{
    public class Id4Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1_scope","My APID")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId="ConsoleApp_id",
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    ClientSecrets={new Secret("ConsoleApp_key".Sha256())},
                    AllowedScopes={ "api1_scope" }
                },

                new Client
                {
                    ClientId="ro_client_id",
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    ClientSecrets={new Secret("ro_client_key".Sha256())},
                    AllowedScopes={ "api1_scope" }
                },

                new Client
                {
                    ClientId="WebMvc_id",
                    ClientName="web mvc client",
                    ClientSecrets={new Secret("mvckey".Sha256())},
                    AllowedGrantTypes=GrantTypes.Implicit,
                    RequireConsent=false,//取消同意页面

                    RedirectUris ={ "http://localhost:5052/signin-oidc"},
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },

                new Client
                {
                    ClientId="js",
                    ClientName="js client",
                    AllowedGrantTypes=GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser=true,
                    RequireConsent=false,//取消同意页面
                    RedirectUris={ "http://localhost:5053/callback.html" },
                    PostLogoutRedirectUris={ "http://localhost:5053/index.html" },
                    AllowedCorsOrigins={ "http://localhost:5053" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1_scope"
                    }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId="user_id_1",
                    Username="user1",
                    Password="user_pwd_1"
                },
                new TestUser
                {
                    SubjectId="user_id_2",
                    Username="user2",
                    Password="user_pwd_2"
                }
            };
        }
    }
}
