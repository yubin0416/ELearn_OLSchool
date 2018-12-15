using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace IdentityServer.API
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[] {
                new ApiResource("UserCenter","UserCenter API"){  ApiSecrets = {  new Secret("secret".Sha256())} },
                new ApiResource("OrderCenter","OrderCenter API"){  ApiSecrets = {  new Secret("secret".Sha256())} },
                new ApiResource("CurriculumCenter","Curriculum API"){  ApiSecrets = {  new Secret("secret".Sha256())} },
                new ApiResource("Payment","Payment API"){  ApiSecrets = {  new Secret("secret".Sha256())} }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[] {
                new Client(){
                    ClientId ="Client1",
                    AccessTokenType = AccessTokenType.Reference,
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes= {"stu_sms_code","tc_sms_code"},
                    AllowedScopes={"UserCenter","OrderCenter","CurriculumCenter","Payment"},
                }
            };
        }
    }
}
