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
            //string expectedSubscriptionID = "{b44fdde2-1234-1234-a75b-24429617b2d9}";
            AzureServicePrincipal sp = new AzureServicePrincipal();
            string sandboxSubID = "b44fdde2-5322-4858-a75b-24429617b2d9";
            sp.subscriptionID = sp.ValidateGuidFormat(sandboxSubID);
        }
    }
}
