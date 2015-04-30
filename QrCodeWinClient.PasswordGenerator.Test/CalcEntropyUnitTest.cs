using Microsoft.VisualStudio.TestTools.UnitTesting;
using QrCodeWinClient.Common;

namespace QrCodeWinClient.PasswordGenerator.Test
{
    [TestClass]
    public class CalcEntropyUnitTest
    {
        [TestMethod]
        public void SimpleTest()
        {
            var result = PasswordGenerator.CalcEntropy(TestUtils.GetPasswordSettings());

            Assert.AreNotEqual(0, result);
        }

        [TestMethod]
        public void OnlyNumeric()
        {
            var passwordSettings = new PasswordSettings();
            passwordSettings.Length = 10;
            passwordSettings.IncludeNumeric = true;

            var result = PasswordGenerator.CalcEntropy(passwordSettings);

            // L = Number of symbols in the password
            // N = number of possible symbols
            // L  log 2   N
            // 10 Log[2, 10] = 33.219
            Assert.AreEqual(33, result);
        }

        [TestMethod]
        public void OnlyLowerCase()
        {
            var passwordSettings = new PasswordSettings();
            passwordSettings.Length = 10;
            passwordSettings.IncludeAlphaLower = true;

            var result = PasswordGenerator.CalcEntropy(passwordSettings);

            // L = Number of symbols in the password
            // N = number of possible symbols
            // L  log 2   N
            // 10 Log[2, 26] = 47.004
            Assert.AreEqual(47, result);
        }

        [TestMethod]
        public void OnlyUpperCase()
        {
            var passwordSettings = new PasswordSettings();
            passwordSettings.Length = 10;
            passwordSettings.IncludeAlphaUpper = true;

            var result = PasswordGenerator.CalcEntropy(passwordSettings);

            // L = Number of symbols in the password
            // N = number of possible symbols
            // L  log 2   N
            // 10 Log[2, 26] = 47.004
            Assert.AreEqual(47, result);
        }

        [TestMethod]
        public void LowerAndUpperCase()
        {
            var passwordSettings = new PasswordSettings();
            passwordSettings.Length = 10;
            passwordSettings.IncludeAlphaLower = true;
            passwordSettings.IncludeAlphaUpper = true;

            var result = PasswordGenerator.CalcEntropy(passwordSettings);

            // L = Number of symbols in the password
            // N = number of possible symbols
            // L  log 2   N
            // 10 Log[2, 52] = 57.004
            Assert.AreEqual(57, result);
        }

        [TestMethod]
        public void AllSymbols()
        {
            var passwordSettings = new PasswordSettings();
            passwordSettings.Length = 10;
            passwordSettings.IncludeNumeric = true;
            passwordSettings.IncludeAlphaLower = true;
            passwordSettings.IncludeAlphaUpper = true;
            passwordSettings.IncludeSymbolSetNormal = true;
            passwordSettings.IncludeSymbolSetExtended = true;

            var result = PasswordGenerator.CalcEntropy(passwordSettings);

            // L = Number of symbols in the password
            // N = number of possible symbols
            // L  log 2   N
            // 10 Log[2, 90] = 64.918
            Assert.AreEqual(64, result);
        }
    }
}