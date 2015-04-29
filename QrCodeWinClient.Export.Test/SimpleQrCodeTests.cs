using System;
using Gma.QrCodeNet.Encoding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Media;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using ZXing;
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

            var bitmap = BitmapImage2Bitmap(bitmapImage);
            IBarcodeReader reader = new BarcodeReader();
            var output = reader.Decode(bitmap).Text;

            Assert.AreEqual(input, output);
        }

        [TestMethod]
        public void GenerateQrCodeWithNumeric()
        {
            var input = "65494981";

            var bitmapImage = QrCodeExporter.Export(Common.ErrorCorrectionLevel.M, input, 12, new SolidBrush(Color.Black), new SolidBrush(Color.White));

            var bitmap = BitmapImage2Bitmap(bitmapImage);
            IBarcodeReader reader = new BarcodeReader();
            var output = reader.Decode(bitmap).Text;

            Assert.AreEqual(input, output);
        }


        [TestMethod]
        public void GenerateQrCodeWitUmlaute()
        {
            var input = "Köln Düsseldorf";

            var bitmapImage = QrCodeExporter.Export(Common.ErrorCorrectionLevel.M, input, 12, new SolidBrush(Color.Black), new SolidBrush(Color.White));

            var bitmap = BitmapImage2Bitmap(bitmapImage);
            IBarcodeReader reader = new BarcodeReader();
            var output = reader.Decode(bitmap).Text;

            Assert.AreEqual(input, output);
        }


        private Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }
    }
}