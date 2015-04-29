using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;

namespace QrCodeWinClient.Export
{
    public class QrCodeExporter
    {
        public static BitmapImage Export(string inputText)
        {
            return Export(Common.ErrorCorrectionLevel.M, inputText, 12, new SolidBrush(Color.Black), new SolidBrush(Color.White));
        }


        public static BitmapImage Export(Common.ErrorCorrectionLevel errorCorrectionLevel, string inputText, int moduleSize, Brush darkBrush, Brush lightBrush)
        {
            BitmapImage bitmapImage;
            using (var ms = CreateQrCode(errorCorrectionLevel, inputText, moduleSize, darkBrush, lightBrush))
            {
                bitmapImage = CreateBitmapFromStream(ms);
            }

            return bitmapImage;
        }

        private static BitmapImage CreateBitmapFromStream(MemoryStream ms)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = ms;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();
            bitmapImage.Freeze();
            return bitmapImage;
        }

        private static MemoryStream CreateQrCode(Common.ErrorCorrectionLevel errorCorrectionLevel, string inputText, int moduleSize, Brush darkBrush, Brush lightBrush)
        {
            var encoder = new QrEncoder((ErrorCorrectionLevel) errorCorrectionLevel);

            QrCode qrCode;
            encoder.TryEncode(inputText, out qrCode);

            var gRenderer = new GraphicsRenderer(new FixedModuleSize(moduleSize, QuietZoneModules.Two), darkBrush, lightBrush);

            var ms = new MemoryStream();
            gRenderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            return ms;
        }
    }
}