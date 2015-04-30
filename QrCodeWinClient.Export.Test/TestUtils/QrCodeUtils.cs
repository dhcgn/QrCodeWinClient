using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using ZXing;

namespace QrCodeWinClient.Export.Test
{
    public class QrCodeUtils
    {
        public static string GetStringFromQrCode(BitmapImage bitmapImage)
        {
            var bitmap = BitmapImage2Bitmap(bitmapImage);
            IBarcodeReader reader = new BarcodeReader();
            var decode = reader.Decode(bitmap);
            var output = decode.Text;

            return output;
        }

        private static Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                Bitmap bitmap = new Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }
    }
}