using System.Collections.Generic;
using System.Windows.Media;
using System.Xml.Serialization;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using QrCodeWinClient.Common;
using QrCodeWinClient.Export;
using System.Drawing;
using System.Diagnostics;
using System;
using System.Linq;
using System.IO;
using System.Windows.Media.Imaging;

namespace QrCodeWinClient
{
    public class MainViewModel : ViewModelBase
    {
        #region

        public RelayCommand CopyQRCodeToClipboardCommand { get; set; }
        public RelayCommand SaveQRCodeToLibraryCommand { get; set; }
        public RelayCommand SaveQRCodeDialogCommand { get; set; }

        #endregion

        #region Properties

        private string inputText;

        public string InputText
        {
            get { return this.inputText; }
            set
            {
                this.MessengerInstance.Send(new QrCodeRequestMessage(value, this.Settings));
                this.Set(ref this.inputText, value);
            }
        }

        private BitmapImage qrCodeImage;

        public BitmapImage QrCodeImage
        {
            get { return this.qrCodeImage; }
            set { this.Set(ref this.qrCodeImage, value); }
        }

        private QrCodeSettings settings;

        public QrCodeSettings Settings
        {
            get { return this.settings; }
            set { this.Set(ref this.settings, value); }
        }

        public IEnumerable<ErrorCorrectionLevel> ErrorCorrectionLevels
        {
            get { return Enum.GetValues(typeof (ErrorCorrectionLevel)).Cast<ErrorCorrectionLevel>(); }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (this.IsInDesignMode)
            {
                this.Settings = new QrCodeSettings();
            }
            else
            {
                ExportInstance.Instance.Init();
                this.MessengerInstance.Register<QrCodeResponseMessage>(this, this.ReveiceQRCode);

                this.Settings = new QrCodeSettings();

                this.Settings.PropertyChanged += (sender, args) => this.MessengerInstance.Send(new QrCodeRequestMessage(this.InputText, this.Settings));

                SaveQRCodeToLibraryCommand = new RelayCommand(SaveQRCodeToLibrary);
            }

            //this.RaisePropertyChanged("Settings");
            //this.RaisePropertyChanged("DarkBrush");
        }

        private void SaveQRCodeToLibrary()
        {
            ImageSaver.SaveQRCodeToLibrary(QrCodeImage);
        }



        private void ReveiceQRCode(QrCodeResponseMessage qrCodeResponseMessage)
        {
            this.QrCodeImage = qrCodeResponseMessage.QrCodeImage;
        }
    }

    public class ImageSaver
    {
        private static string GetFileName()
        {
            return DateTime.Now.ToString("yyyy-mm-dd - HH_mm_ss") + " QR_Code.png";
        }

        internal static void SaveQRCodeToLibrary(BitmapImage qrCodeImage)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            string combine = Path.Combine(path, GetFileName());
            SaveBitmapIamge(combine, qrCodeImage);
        }

        private static void SaveBitmapIamge(string fileName, BitmapImage qrCodeImage)
        {
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(qrCodeImage));

            using (var filestream = new FileStream(fileName, FileMode.Create))
                encoder.Save(filestream);
        }


    }
}