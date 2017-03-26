﻿/*
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
        //true if the private GUID members are set to well formed GUIDs
        private bool checkGuidFormat = false;
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

        private AuthenticationResult GetSecurityToken()
        {
            bool isValid = false;

            if (checkGuidFormat == false)
            {
                isValid = CheckAllGuidsMembers();
            }
            AuthenticationContext authContext = new AuthenticationContext("https://login.windows.net/" + tenantID);
            securityToken = authContext.AcquireToken("https://management.core.windows.net/", new ClientCredential(applicationID, applicationSecret));
            return securityToken;
        }

        public bool CheckAllGuidsMembers()
        {
            //Do a GUID format validation check if it has not been done
            if (checkGuidFormat == false)
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
            }
            checkGuidFormat = true;
            return true;
        }


        public string ValidateGuidFormat(string v)
        {
            //trim leading and trailing blanks, and ensure that the GUID is bracketed by curly braces.
            v = v.Trim(' ');
            if (v[0] != '{')
            {
                v = "{" + v;
            }

            if (v[v.Length - 1] != '}')
            {
                v = v + "}";
            }
            //Validate that v is of this format: "{xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}"
            Regex guidFormat = new Regex(@"^[{|\(]?[0-9a-fA-F]{8}[-]?([0-9a-fA-F]{4}[-]?){3}[0-9a-fA-F]{12}[\)|}]?$");
            Match match = guidFormat.Match(v);

            if (match.Success == false)
            {
                throw new System.FormatException("Value must be a GUID: {xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}");
            }

            return v;
        }
    }
}