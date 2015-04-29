using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QrCodeWinClient.Export.Test
{
    [TestClass]
    public class QrCodeErrorCorrectionLevelTest
    {
        private const string InputText = @"test";
        

        [TestMethod]
        public void ErrorCorrectionLevel_Low_ModuleSize_1_Black_White()
        {
            var bitmapImage = QrCodeExporter.Export(Common.ErrorCorrectionLevel.L, InputText, 1, new SolidBrush(Color.Black), new SolidBrush(Color.White));

            var output = QrCodeUtils.GetStringFromQrCode(bitmapImage);

            Assert.AreEqual(InputText, output);
        }

        [TestMethod]
        public void ErrorCorrectionLevel_Medium_ModuleSize_1_Black_White()
        {
            var bitmapImage = QrCodeExporter.Export(Common.ErrorCorrectionLevel.M, InputText, 1, new SolidBrush(Color.Black), new SolidBrush(Color.White));

            var output = QrCodeUtils.GetStringFromQrCode(bitmapImage);

            Assert.AreEqual(InputText, output);
        }

        [TestMethod]
        public void ErrorCorrectionLevel_High_ModuleSize_1_Black_White()
        {
            var bitmapImage = QrCodeExporter.Export(Common.ErrorCorrectionLevel.H, InputText, 1, new SolidBrush(Color.Black), new SolidBrush(Color.White));

            var output = QrCodeUtils.GetStringFromQrCode(bitmapImage);

            Assert.AreEqual(InputText, output);
        }
        [TestMethod]

        public void ErrorCorrectionLevel_Quality_ModuleSize_1_Black_White()
        {
            var bitmapImage = QrCodeExporter.Export(Common.ErrorCorrectionLevel.Q, InputText, 1, new SolidBrush(Color.Black), new SolidBrush(Color.White));

            var output = QrCodeUtils.GetStringFromQrCode(bitmapImage);

            Assert.AreEqual(InputText, output);
        }
       
    }
}