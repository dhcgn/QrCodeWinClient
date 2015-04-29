using Microsoft.VisualStudio.TestTools.UnitTesting;
using QrCodeWinClient.Common;

namespace QrCodeWinClient.PasswordGenerator.Test
{
    [TestClass]
    public class CalcRealEntropyUnitTest
    {
        [TestMethod]
        public void SimpleTest()
        {
            var result = PasswordGenerator.CalcRealEntropy("qwert");

            Assert.AreNotEqual(0, result);
        }
    }
}
