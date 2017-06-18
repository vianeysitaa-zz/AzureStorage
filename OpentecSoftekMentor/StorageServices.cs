using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.ServiceRuntime;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using System.Configuration;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.WebKey;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;
using System.Web.Configuration;


namespace OpentecSoftekMentor
{
    public class StorageServices
    {
        String connectionString;
        //Method to reference blob storage
        public CloudBlobContainer GetCloudBlobContainer()
        {
            GetSecret();

            //string connString = "DefaultEndpointsProtocol=https;AccountName=softektraining;AccountKey=ECDPy78Nw990HsPdnd8NEzudwTsSTXaYWINK/Yq2x7qIrLfb3mu0B0xaCV/qq1KOoMinfi2FcNxV29ZVSFOP/Q==;EndpointSuffix=core.windows.net;";
            string destContainer = "softektraining";
 
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(destContainer);
            if (blobContainer.CreateIfNotExists())
            {
                blobContainer.SetPermissions(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
        });

            }
            return blobContainer;

        }

        public async void GetSecret()
        {
            var keyVaultClient = new KeyVaultClient(AuthenticateVault);
            var result = await keyVaultClient.GetSecretAsync("https://opentec-softek.vault.azure.net/secrets/fileString/e294bxxxxxx1de1efce672f");
            connectionString = result.Value;
        }

        private async Task<string> AuthenticateVault(string authority, string resource, string scope)
        {
            var clientCredentials = new ClientCredential(WebConfigurationManager.AppSettings["ClientId"],
                WebConfigurationManager.AppSettings["ClientSecret"]);
            var authenticationContext = new AuthenticationContext(authority);
            var result = await authenticationContext.AcquireTokenAsync(resource, clientCredentials);
            return result.AccessToken;
        }


    }
}