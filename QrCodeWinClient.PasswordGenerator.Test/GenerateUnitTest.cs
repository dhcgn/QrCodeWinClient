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

        [TestMethod]
        public void OnlyNumeric()
        {
            var passwordSettings = new PasswordSettings();
            passwordSettings.Length = 10;
            passwordSettings.IncludeNumeric = true;
            passwordSettings.ForceEach = true;

            var pwd = PasswordGenerator.Generate(passwordSettings);

            Assert.AreEqual(10, pwd.Length);
            
            var entropy = PasswordGenerator.CalcRealEntropy(pwd);

            // L = Number of symbols in the password
            // N = number of possible symbols
            // L  log 2   N
            // 10 Log[2, 10] = 33.219
            Assert.IsTrue(entropy>=33, "Entropy was {0}, but exptected higher than {1}", entropy, 64);
        }

        [TestMethod]
        public void OnlyLowerCase()
        {
            var passwordSettings = new PasswordSettings();
            passwordSettings.Length = 10;
            passwordSettings.IncludeAlphaLower = true;
            passwordSettings.ForceEach = true;

            var pwd = PasswordGenerator.Generate(passwordSettings);

            Assert.AreEqual(10, pwd.Length);

            var entropy = PasswordGenerator.CalcRealEntropy(pwd);

            // L = Number of symbols in the password
            // N = number of possible symbols
            // L  log 2   N
            // 10 Log[2, 26] = 47.004
            Assert.IsTrue(entropy >= 47, "Entropy was {0}, but exptected higher than {1}", entropy, 64);
        }

        [TestMethod]
        public void OnlyUpperCase()
        {
            var passwordSettings = new PasswordSettings();
            passwordSettings.Length = 10;
            passwordSettings.IncludeAlphaUpper = true;
            passwordSettings.ForceEach = true;

            var pwd = PasswordGenerator.Generate(passwordSettings);

            Assert.AreEqual(10, pwd.Length);

            var entropy = PasswordGenerator.CalcRealEntropy(pwd);

            // L = Number of symbols in the password
            // N = number of possible symbols
            // L  log 2   N
            // 10 Log[2, 26] = 47.004
            Assert.IsTrue(entropy >= 47, "Entropy was {0}, but exptected higher than {1}", entropy, 64);
        }

        [TestMethod]
        public void LowerAndUpperCase()
        {
            var passwordSettings = new PasswordSettings();
            passwordSettings.Length = 10;
            passwordSettings.IncludeAlphaLower = true;
            passwordSettings.IncludeAlphaUpper = true;
            passwordSettings.ForceEach = true;

            var pwd = PasswordGenerator.Generate(passwordSettings);

            Assert.AreEqual(10, pwd.Length);

            var entropy = PasswordGenerator.CalcRealEntropy(pwd);

            // L = Number of symbols in the password
            // N = number of possible symbols
            // L  log 2   N
            // 10 Log[2, 52] = 57.004
            Assert.IsTrue(entropy >= 57, "Entropy was {0}, but exptected higher than {1}", entropy, 64);
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
            passwordSettings.ForceEach = true;

            var pwd = PasswordGenerator.Generate(passwordSettings);

            Assert.AreEqual(10, pwd.Length);

            var entropy = PasswordGenerator.CalcRealEntropy(pwd);

            // L = Number of symbols in the password
            // N = number of possible symbols
            // L  log 2   N
            // 10 Log[2, 90] = 64.918
            Assert.IsTrue(entropy >= 64, "Entropy was {0}, but exptected higher than {1}", entropy, 64);
        }

        [TestMethod]
        public void Generate_Length_Fail()
        {
            var passwordSettings = new PasswordSettings();
            passwordSettings.Length = 4;
            passwordSettings.IncludeNumeric = true;
            passwordSettings.IncludeAlphaLower = true;
            passwordSettings.IncludeAlphaUpper = true;
            passwordSettings.IncludeSymbolSetNormal = true;
            passwordSettings.IncludeSymbolSetExtended = true;
            passwordSettings.ForceEach = true;

            var pwd = PasswordGenerator.Generate(passwordSettings);

            Assert.AreEqual(null, pwd);
        }

        [TestMethod]
        public void Generate_NoIncludes_Fail()
        {
            var passwordSettings = new PasswordSettings();
            passwordSettings.Length = 4;

            var pwd = PasswordGenerator.Generate(passwordSettings);

            Assert.AreEqual(null, pwd);
        }

        [TestMethod]
        public void Generate_LengthZero_Fail()
        {
            var passwordSettings = new PasswordSettings();
            passwordSettings.Length = 0;

            var pwd = PasswordGenerator.Generate(passwordSettings);

            Assert.AreEqual(null, pwd);
        }
    }
}