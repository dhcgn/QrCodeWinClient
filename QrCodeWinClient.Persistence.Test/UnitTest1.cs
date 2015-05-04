using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QrCodeWinClient.Common;

namespace QrCodeWinClient.Persistence.Test
{
    [TestClass]
    public class SaveAndLoad
    {
        [TestMethod]
        public void TestSaveAndLoad()
        {
            var saved = GetPasswordSettings();

            Persistence.Save(saved);

            PasswordSettingsTest loaded = Persistence.Load<PasswordSettingsTest>();

            Assert.IsNotNull(loaded);
            Assert.AreEqual(saved.Length, loaded.Length);
            Assert.AreEqual(saved.ForceEach, loaded.ForceEach);
        }

        [TestMethod]
        public void TestTrySaveAndTryLoad()
        {
            var saved = GetPasswordSettings();

            var saveResult = Persistence.TrySave(saved);
            Assert.IsTrue(saveResult);

            PasswordSettingsTest loaded;
            var loadResult = Persistence.TryLoad(out loaded);

            Assert.IsTrue(loadResult);
            Assert.IsNotNull(loaded);
            Assert.AreEqual(saved.Length, loaded.Length);
            Assert.AreEqual(saved.ForceEach, loaded.ForceEach);
        }

        private static PasswordSettingsTest GetPasswordSettings()
        {
            var result = new PasswordSettingsTest
            {
                Length = 12,
                ForceEach = true,
                IncludeNumeric = true,
                IncludeAlphaLower = true,
                IncludeAlphaUpper = true,
                IncludeSymbolSetNormal = true,
                IncludeSymbolSetExtended = true
            };

            return result;
        }

        public class PasswordSettingsTest
        {
            public int Length { get; set; }
            public bool ForceEach { get; set; }
            public bool IncludeNumeric { get; set; }
            public bool IncludeAlphaLower { get; set; }
            public bool IncludeAlphaUpper { get; set; }
            public bool IncludeSymbolSetNormal { get; set; }
            public bool IncludeSymbolSetExtended { get; set; }
        }
    }
}