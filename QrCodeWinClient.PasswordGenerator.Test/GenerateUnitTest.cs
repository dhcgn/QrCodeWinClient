using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QrCodeWinClient.PasswordGenerator.Test
{
    [TestClass]
    public class GenerateUnitTest
    {
        [TestMethod]
        public void SimpleTest()
        {
            var result = PasswordGenerator.Generate(Helper.GetPasswordSettings());

            Assert.IsFalse(String.IsNullOrEmpty(result));
        }
    }
}