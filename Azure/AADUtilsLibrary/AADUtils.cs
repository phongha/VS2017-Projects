/*
 * This class library AADUtils add support for working with AAD, and Azure KeyVault
 * 
 * Reference package required:
 * Install-Package Microsoft.IdentityModel.Clients.ActiveDirectory
 * Install-Package Microsoft.Azure.KeyVault
 * 
 */


using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Azure.KeyVault;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.AADUtilsLibrary
{
    class AADUtils
    {

        public async static Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);

            ClientCredential clientCred = new ClientCredential("<AzureADApplicationClientID>", "<AzureADApplicationClientKey>");

            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)

                throw new InvalidOperationException("Failed to obtain the JWT token");

            return result.AccessToken;
        }


    }
}
