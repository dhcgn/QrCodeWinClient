using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QrCodeWinClient.PasswordGenerator.Test
{
    [TestClass]
    public class CalcEntropyUnitTest
    {
        [TestMethod]
        public void SimpleTest()
        {
            var result = PasswordGenerator.CalcEntropy(Helper.GetPasswordSettings());

            Assert.AreNotEqual(0, result);
        }
    }
}