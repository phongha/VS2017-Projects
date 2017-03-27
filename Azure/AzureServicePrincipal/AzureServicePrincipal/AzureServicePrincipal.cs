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
        public string subscriptionID
        {
            get; set;
        }
        public string tenantID
        {
            get; set;
        }
        public string applicationID
        {
            get; set;
        }
        public string applicationSecret
        {
            get; set;
        }

        public AuthenticationResult securityToken
        {
            get; set;
        }

        public AuthenticationResult GetSecurityToken()
        {
            AuthenticationContext authContext = null;
           try
            {
                authContext = new AuthenticationContext("https://login.windows.net/" + tenantID);
            }
            catch (Exception e)
            {
                Trace.WriteLine("Failed to acquire authentication context %s.", e.Message);
                return null;
            }
            try
            {
                securityToken = authContext.AcquireToken("https://management.core.windows.net/", new ClientCredential(applicationID, applicationSecret));
            }
            catch (Exception e)
            {
                Trace.WriteLine("Failed to acquire token with authentication context %s", e.Message);
                return null;
            }
            return securityToken;
        }

        public bool CheckAllGuidsMembers()
        {
            try
            {
                if(subscriptionID == null)
                {
                    return false;
                }
                subscriptionID = ValidateGuidFormat(subscriptionID);
            }
            catch (FormatException e)
            {
                Trace.WriteLine("Invalid Subscription ID GUID: %s", e.Message);
                return false;
            }
            try
            {
                if (applicationID == null)
                {
                    return false;
                }
                applicationID = ValidateGuidFormat(applicationID);
            }
            catch (FormatException e)
            {
                Trace.WriteLine("Invalid application ID GUID: %s", e.Message);
                return false;
            }
            try
            {
                if (tenantID == null)
                {
                    return false;
                }
                tenantID = ValidateGuidFormat(tenantID);
            }
            catch (FormatException e)
            {
                Trace.WriteLine("Invalid tenant ID GUID: %s", e.Message);
                return false;
            }        
            return true;
        }


        public string ValidateGuidFormat(string v)
        {
            //trim leading and trailing blanks, and ensure that the GUID is bracketed by curly braces.
            v = v.Trim(' ');
            //Validate that v is of this format: "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
            Regex guidFormat = new Regex(@"^[{|\(]?[0-9a-fA-F]{8}[-]?([0-9a-fA-F]{4}[-]?){3}[0-9a-fA-F]{12}[\)|}]?$");
            Match match = guidFormat.Match(v);

            if (match.Success == false)
            {
                throw new System.FormatException("Value must be a GUID: xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx");
            }
            //strip leading curly braces, or brackets
            if (v[0] == '{')
            {
                v = v.TrimStart('{');
                v = v.TrimEnd('}');
            }
            else if (v[0] == '(')
            {
                v = v.TrimStart('(');
                v = v.TrimEnd(')');
            }

            return v;
        }
    }
}