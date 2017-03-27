/*
 * This example code is based off the follow article, modified extensively to showcase the use of a new class 
 * calls AzureServicePrincipal to encapsulate what is unique about the use Azure service principal. A collection
 * of this new class may also be used to handle the various environments or separation of duty for tighter 
 * security handling.
 * 
 * https://github.com/Microsoft/azure-docs/blob/master/articles/sql-database/sql-database-get-started-csharp.md
 * 
 * Also see,
 * 
 * https://azure.microsoft.com/documentation/articles/resource-group-authenticate-service-principal
 * 
 * Prerequisite packages:
 *  Tools --> NuGet Package Manager --> Package Manager Console
 *      Install-Package Microsoft.Azure.Management.ResourceManager -Pre
 *      Install-Package Microsoft.Azure.Common.Authentication -Pre
 *      Install-Package Microsoft.Azure.Management.Sql -Pre
 *  
 */

using Microsoft.Azure;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Models;
using Microsoft.Azure.Management.Sql;
using Microsoft.Azure.Management.Sql.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.ServicePrincipal;

namespace TestAzureServicePrincipal
{
    class Program
    {
        static void Main(string[] args)
        {
            //string expectedSubscriptionID = "{b44fdde2-1234-1234-a75b-24429617b2d9}";
            AzureServicePrincipal sp = new AzureServicePrincipal();
            AuthenticationResult token = null;

            sp.subscriptionID = "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx";
            sp.tenantID = "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx";
            sp.applicationID = "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx";
            sp.applicationSecret = "xxxxxxxxxxxxxxxx";

            if (sp.CheckAllGuidsMembers() == true)
            {
                token = sp.GetSecurityToken();
                Console.WriteLine("Token acquired. Expires on:" + token.ExpiresOn);

            }

            Console.ReadLine();

        }
    }
}
