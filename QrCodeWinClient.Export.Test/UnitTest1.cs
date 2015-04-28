using System;
using Gma.QrCodeNet.Encoding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Media;
using System.Drawing;
using Color = System.Drawing.Color;

namespace QrCodeWinClient.Export.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var temp = QrCodeExporter.Export(Common.ErrorCorrectionLevel.M, "Hello World", 12, new SolidBrush(Color.Black), new SolidBrush(Color.White));
        }
    }
}