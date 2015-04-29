using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QrCodeWinClient.Export.Test
{
    [TestClass]
    public class QrCodeTechnicalTestsSimpleInput
    {
        private const string InputText = @"test";

        [TestMethod]
        public void ErrorCorrectionLevel_Low_ModuleSize_12_Black_White()
        {
            var bitmapImage = QrCodeExporter.Export(Common.ErrorCorrectionLevel.L, InputText, 12, new SolidBrush(Color.Black), new SolidBrush(Color.White));

            var output = QrCodeUtils.GetStringFromQrCode(bitmapImage);

            Assert.AreEqual(InputText, output);
        }

        [TestMethod]
        public void ErrorCorrectionLevel_Medium_ModuleSize_12_Black_White()
        {
            var bitmapImage = QrCodeExporter.Export(Common.ErrorCorrectionLevel.M, InputText, 12, new SolidBrush(Color.Black), new SolidBrush(Color.White));

            var output = QrCodeUtils.GetStringFromQrCode(bitmapImage);

            Assert.AreEqual(InputText, output);
        }

        [TestMethod]
        public void ErrorCorrectionLevel_High_ModuleSize_12_Black_White()
        {
            var bitmapImage = QrCodeExporter.Export(Common.ErrorCorrectionLevel.H, InputText, 12, new SolidBrush(Color.Black), new SolidBrush(Color.White));

            var output = QrCodeUtils.GetStringFromQrCode(bitmapImage);

            Assert.AreEqual(InputText, output);
        }
        [TestMethod]

        public void ErrorCorrectionLevel_Quality_ModuleSize_12_Black_White()
        {
            var bitmapImage = QrCodeExporter.Export(Common.ErrorCorrectionLevel.Q, InputText, 12, new SolidBrush(Color.Black), new SolidBrush(Color.White));

            var output = QrCodeUtils.GetStringFromQrCode(bitmapImage);

            Assert.AreEqual(InputText, output);
        }
    }

    [TestClass]
    public class QrCodeTechnicalTestsComplexInput
    {
        private const string InputText = @"Köln Düsseldorf 7m5*847OT6%YqMJGU#|,v\4w#e!\dp öäüÄÖÜ \@€";

        [TestMethod]
        public void ErrorCorrectionLevel_Low_ModuleSize_12_Black_White()
        {
            var bitmapImage = QrCodeExporter.Export(Common.ErrorCorrectionLevel.L, InputText, 12, new SolidBrush(Color.Black), new SolidBrush(Color.White));

            var output = QrCodeUtils.GetStringFromQrCode(bitmapImage);

            Assert.AreEqual(InputText, output);
        }

        [TestMethod]
        public void ErrorCorrectionLevel_Medium_ModuleSize_12_Black_White()
        {
            var bitmapImage = QrCodeExporter.Export(Common.ErrorCorrectionLevel.M, InputText, 12, new SolidBrush(Color.Black), new SolidBrush(Color.White));

            var output = QrCodeUtils.GetStringFromQrCode(bitmapImage);

            Assert.AreEqual(InputText, output);
        }

        [TestMethod]
        public void ErrorCorrectionLevel_High_ModuleSize_12_Black_White()
        {
            var bitmapImage = QrCodeExporter.Export(Common.ErrorCorrectionLevel.H, InputText, 12, new SolidBrush(Color.Black), new SolidBrush(Color.White));

            var output = QrCodeUtils.GetStringFromQrCode(bitmapImage);

            Assert.AreEqual(InputText, output);
        }
        [TestMethod]

        public void ErrorCorrectionLevel_Quality_ModuleSize_12_Black_White()
        {
            var bitmapImage = QrCodeExporter.Export(Common.ErrorCorrectionLevel.Q, InputText, 12, new SolidBrush(Color.Black), new SolidBrush(Color.White));

            var output = QrCodeUtils.GetStringFromQrCode(bitmapImage);

            Assert.AreEqual(InputText, output);
        }
    }
}