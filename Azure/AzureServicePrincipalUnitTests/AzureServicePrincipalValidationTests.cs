using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Azure.ServicePrincipal;

namespace AzureServicePrincipalUnitTests
{
    [TestClass]
    public class AzureServicePrincipalValidationTests
    {
        [TestMethod]
        public void ValidateGuidFormat()
        {
            AzureServicePrincipal sp = new AzureServicePrincipal();
            string expectedSubscriptionID = "{b44fdde2-1234-1234-a75b-24429617b2d9}";
            
            sp.subscriptionID = sp.validateGuidFormat("b44fdde2-1234-1234-a75b-24429617b2d9");
            //The expected value should match with curly braces
            Assert.AreEqual(expectedSubscriptionID, sp.subscriptionID, true);

            sp.subscriptionID = sp.validateGuidFormat("{b44fdde2-1234-1234-a75b-24429617b2d9}");
            //The expected value should match with curly braces
            Assert.AreEqual(expectedSubscriptionID, sp.subscriptionID, true);

            sp.subscriptionID = sp.validateGuidFormat("b44fdde2-1234-1234-a75b-24429617b2d9}");
            //The expected value should match with curly braces
            Assert.AreEqual(expectedSubscriptionID, sp.subscriptionID, true);

            sp.subscriptionID = sp.validateGuidFormat("{b44fdde2-1234-1234-a75b-24429617b2d9");
            //The expected value should match with curly braces
            Assert.AreEqual(expectedSubscriptionID, sp.subscriptionID, true);

        }
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ValidateGuidFormat2()
        {
            AzureServicePrincipal sp = new AzureServicePrincipal();
            //Assert will be handled by the exception
            sp.subscriptionID = sp.validateGuidFormat("b44fdde2-1234-1234-a75b-1234}");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ValidateGuidFormat3()
        {
            AzureServicePrincipal sp = new AzureServicePrincipal();
            //Assert will be handled by the exception
            sp.subscriptionID = sp.validateGuidFormat("{b44fdde2-1234- -a75b-24429617b2d9}");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ValidateGuidFormat4()
        {
            AzureServicePrincipal sp = new AzureServicePrincipal();
            //Assert will be handled by the exception
            sp.subscriptionID = sp.validateGuidFormat("{1234567-1234-1234-a75b-24429617b2d9}");
        }
    }
}
