using System;
using Gma.QrCodeNet.Encoding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Media;
using System.Drawing;
using Color = System.Drawing.Color;

namespace QrCodeWinClient.Export.Test
{
    [TestClass]
    public class SimpleQrCodeTests
    {
        [TestMethod]
        public void GenerateQrCodeWithAlpha()
        {
            var input = "Hello World";

            var bitmapImage = QrCodeExporter.Export(Common.ErrorCorrectionLevel.M, input, 12, new SolidBrush(Color.Black), new SolidBrush(Color.White));

            var output = QrCodeUtils.GetStringFromQrCode(bitmapImage);

            Assert.AreEqual(input, output);
        }

        [TestMethod]
        public void GenerateQrCodeWithNumeric()
        {
            var input = "65494981";

            var bitmapImage = QrCodeExporter.Export(Common.ErrorCorrectionLevel.M, input, 12, new SolidBrush(Color.Black), new SolidBrush(Color.White));

            var output = QrCodeUtils.GetStringFromQrCode(bitmapImage);

            Assert.AreEqual(input, output);
        }


        [TestMethod]
        public void GenerateQrCodeWitUmlaute()
        {
            var input = "Köln Düsseldorf";

            var bitmapImage = QrCodeExporter.Export(Common.ErrorCorrectionLevel.M, input, 12, new SolidBrush(Color.Black), new SolidBrush(Color.White));

            var output = QrCodeUtils.GetStringFromQrCode(bitmapImage);

            Assert.AreEqual(input, output);
        }
    
        [TestMethod]
        public void GenerateQrCodeComplex()
        {
            var input = @"Köln Düsseldorf 7m5*847OT6%YqMJGU#|,v\4w#e!\dp öäüÄÖÜ \@€";

            var bitmapImage = QrCodeExporter.Export(Common.ErrorCorrectionLevel.M, input, 12, new SolidBrush(Color.Black), new SolidBrush(Color.White));

            var output = QrCodeUtils.GetStringFromQrCode(bitmapImage);

            Assert.AreEqual(input, output);
        }
    
    }
}