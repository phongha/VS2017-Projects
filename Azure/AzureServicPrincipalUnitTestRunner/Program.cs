using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.ServicePrincipal;

namespace AzureServicPrincipalUnitTestRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            string subscriptionID = "b44fdde2-1234-1234-a75b-24429617b2d";
            //string expectedSubscriptionID = "{b44fdde2-1234-1234-a75b-24429617b2d9}";
            AzureServicePrincipal sp = new AzureServicePrincipal();
            sp.subscriptionID = subscriptionID;
        }
    }
}
