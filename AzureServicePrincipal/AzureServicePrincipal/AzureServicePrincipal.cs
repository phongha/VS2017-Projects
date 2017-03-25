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
 *  
 */

using Microsoft.Azure;

using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Azure.ServicePrincipal
{
    public class AzureServicePrincipal
    {
        // private string subscriptionID;
        // private string tenantID;
        //private string applicationID;
        //private string applicationSecret;

        public string subscriptionID
        {
            get { return subscriptionID; }
            set
            {
                try
                {
                    subscriptionID = validateGuidFormat(value);
                }
                catch (System.FormatException e)
                {
                    Trace.WriteLine(e.ToString());
                }
            }
        }
        public string tenantID
        {
            get { return tenantID; }
            set
            {
                try
                {
                    tenantID = validateGuidFormat(value);
                }
                catch (System.FormatException e)
                {
                    Trace.WriteLine(e.ToString());
                }
            }
        }
        public string applicationID
        {
            get { return applicationID; }
            set
            {
                try
                {
                    applicationID = validateGuidFormat(value);
                }
                catch (System.FormatException e)
                {
                    Trace.WriteLine(e.ToString());
                }
            }

        }
        public string applicationSecret
        {
            get; set;
        }

        public AuthenticationResult securityToken
        {
            get; set;
        }

        private AuthenticationResult GetSecurityToken(string tenantId, string applicationId, string applicationSecret)
        {
            AuthenticationContext authContext = new AuthenticationContext("https://login.windows.net/" + tenantId);
            securityToken = authContext.AcquireToken("https://management.core.windows.net/", new ClientCredential(applicationId, applicationSecret));
            return securityToken;
        }

        private string validateGuidFormat(string v)
        {
            //Validate that v is of this format: "{xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}"
            Regex guidFormat = new Regex(@"^[{|\(]?[0-9a-fA-F]{8}[-]?([0-9a-fA-F]{4}[-]?){3}[0-9a-fA-F]{12}[\)|}]?$");
            Match match = guidFormat.Match(v);

            if (match.Success == false)
            {
                throw new System.FormatException("Value must be a GUID: {xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}");
            }
            else
            {
                if (v[0] != '{')
                {
                    v = "{" + v;
                }

                if (v[v.Length - 1] != '}')
                {
                    v = v + "}";
                }
            }

            return v;
        }
    }
}