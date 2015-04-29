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

        [TestMethod]
        public void OnlyNumeric()
        {
            var result = PasswordGenerator.CalcRealEntropy("8242377558");

            Assert.AreEqual(33, result);
        }

        [TestMethod]
        public void OnlyLowerCase()
        {
            var result = PasswordGenerator.CalcRealEntropy("cxvquuclnh");

            Assert.AreEqual(47, result);
        }

        [TestMethod]
        public void OnlyUpperCase()
        {
            var result = PasswordGenerator.CalcRealEntropy("CXVQUUCLNH");

            Assert.AreEqual(47, result);
        }

        [TestMethod]
        public void LowerAndUpperCase()
        {
            var result = PasswordGenerator.CalcRealEntropy("lxDHNQCAFX");

            Assert.AreEqual(57, result);
        }

        [TestMethod]
        public void AllSymbols()
        {
            var result = PasswordGenerator.CalcRealEntropy("Ggb:3z/:T3");

            Assert.AreEqual(64, result);
        }
    }
}
