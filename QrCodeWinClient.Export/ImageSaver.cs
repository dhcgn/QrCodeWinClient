using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace QrCodeWinClient.Export
{
    public class ImageSaver
    {
        public static string GetFileName()
        {
            return DateTime.Now.ToString("yyyy-mm-dd - HH_mm_ss") + " QR_Code.png";
        }

        public static void SaveQRCodeToLibrary(BitmapImage qrCodeImage)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            string combine = Path.Combine(path, GetFileName());
            SaveBitmapIamge(combine, qrCodeImage);
        }

        public static void SaveBitmapIamge(string fileName, BitmapImage qrCodeImage)
        {
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(qrCodeImage));

            using (var filestream = new FileStream(fileName, FileMode.Create))
                encoder.Save(filestream);
        }

        public static void SaveQRCodeToLibrary(BitmapImage qrCodeImage, string fileName)
        {
            SaveBitmapIamge(fileName, qrCodeImage);
        }
    }
}