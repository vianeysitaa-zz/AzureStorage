using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.WebKey;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace OpentecSoftekMentor
{
    public class Utils
    {

        public async void Test()
        {
            var keyVaultClient = new KeyVaultClient(AuthenticateVault);
            var result = await keyVaultClient.GetSecretAsync("https://opentec-softek.vault.azure.net/secrets/fileString/e294bxxxxxx1de1efce672f");
            var connectionString = result.Value;
        }
        private async Task<string> AuthenticateVault(string authority, string resource, string scope)
        {
            var clientCredentials = new ClientCredential("d75a9f8b - xxxxxxx", "4QupfP1Bq5KekdXuEJuQoUxxxxxxxxxkhuUOpGWE =");
            var authenticationContext = new AuthenticationContext(authority);
            var result = await authenticationContext.AcquireTokenAsync(resource, clientCredentials);
            return result.AccessToken;
        }




    }
}