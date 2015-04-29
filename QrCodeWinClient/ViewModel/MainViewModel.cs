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
using System.Windows.Media.Imaging;
using System.Windows;
using Microsoft.Win32;

namespace QrCodeWinClient
{
    public class MainViewModel : ViewModelBase
    {
        #region

        public RelayCommand CopyQRCodeToClipboardCommand { get; set; }
        public RelayCommand SaveQRCodeToLibraryCommand { get; set; }
        public RelayCommand SaveQRCodeDialogCommand { get; set; }
        public RelayCommand GeneratePasswordCommand { get; set; }

        #endregion

        #region Properties

        private string inputText;

        public string InputText
        {
            get { return this.inputText; }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.QrCodeImage = Application.Current.Resources["EmptyQrCodeImageSource"] as BitmapImage;
                    this.RealEntropy = 0;
                }
                else
                {
                    this.MessengerInstance.Send(new QrCodeRequestMessage(value, this.QrSettings));
                    this.RealEntropy = PasswordGenerator.EntropyCalculator.CalcRealEntropy(value);
                }

                this.Set(ref this.inputText, value);
            }
        }

        private BitmapImage qrCodeImage;

        public BitmapImage QrCodeImage
        {
            get { return this.qrCodeImage; }
            set { this.Set(ref this.qrCodeImage, value); }
        }

        private QrCodeSettings qrSettings;

        public QrCodeSettings QrSettings
        {
            get { return this.qrSettings; }
            set { this.Set(ref this.qrSettings, value); }
        }

        private PasswordSettings passwordSettings;
        

        public PasswordSettings PasswordSettings
        {
            get { return this.passwordSettings; }
            set { this.Set(ref this.passwordSettings, value); }
        }

        public IEnumerable<ErrorCorrectionLevel> ErrorCorrectionLevels
        {
            get { return Enum.GetValues(typeof (ErrorCorrectionLevel)).Cast<ErrorCorrectionLevel>(); }
        }

        private int entropy;
        public int Entropy
        {
            get { return this.entropy; }
            set { this.Set(ref this.entropy, value); }
        }

        private int realEntropy;
        public int RealEntropy
        {
            get { return this.realEntropy; }
            set { this.Set(ref this.realEntropy, value); }
        }



        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (this.IsInDesignMode)
            {
                this.QrSettings = new QrCodeSettings();
                this.PasswordSettings = new PasswordSettings();

                this.QrCodeImage = Application.Current.Resources["EmptyQrCodeImageSource"] as BitmapImage;

                this.Entropy = 128;
                this.RealEntropy = 121;

                this.InputText = "7mGwdDUhBafPMqwj13C7AFwM1JICXEko";
            }
            else
            {
                ExportInstance.Instance.Init();
                this.MessengerInstance.Register<QrCodeResponseMessage>(this, this.ReveiceQRCode);

                this.QrSettings = new QrCodeSettings();
                this.QrSettings.PropertyChanged += (sender, args) => this.MessengerInstance.Send(new QrCodeRequestMessage(this.InputText, this.QrSettings));

                this.PasswordSettings = new PasswordSettings();
                this.Entropy = PasswordGenerator.EntropyCalculator.CalcEntropy(this.PasswordSettings);
                this.PasswordSettings.PropertyChanged += (sender, args) =>
                {
                    this.GeneratePasswordCommand.RaiseCanExecuteChanged();
                    this.Entropy = PasswordGenerator.EntropyCalculator.CalcEntropy(this.PasswordSettings);
                };

                this.QrCodeImage = Application.Current.Resources["EmptyQrCodeImageSource"] as BitmapImage;

                SaveQRCodeToLibraryCommand = new RelayCommand(SaveQRCodeToLibrary);
                SaveQRCodeDialogCommand = new RelayCommand(SaveQRCodeDialog);
                CopyQRCodeToClipboardCommand = new RelayCommand(CopyQRCodeToClipboard);
                GeneratePasswordCommand = new RelayCommand(GeneratePassword, ()=>PasswordSettings.IsValid());
            }
        }

        private void GeneratePassword()
        {
            this.InputText = PasswordGenerator.PasswordGenerator.Generate(this.PasswordSettings);
        }

        private void SaveQRCodeToLibrary()
        {
            ImageSaver.SaveQRCodeToLibrary(QrCodeImage);
        }

        private void SaveQRCodeDialog()
        {
            var dlg = new SaveFileDialog
            {
                FileName = "QrCode.png",
                DefaultExt = ".png",
                Filter = "PNG (.png)|*.png"
            };

            if (dlg.ShowDialog().Value)
            {
                ImageSaver.SaveQRCodeToLibrary(QrCodeImage, dlg.FileName);
            }
        }

        private void CopyQRCodeToClipboard()
        {
            Clipboard.SetImage(this.QrCodeImage);
        }


        private void ReveiceQRCode(QrCodeResponseMessage qrCodeResponseMessage)
        {
            this.QrCodeImage = qrCodeResponseMessage.QrCodeImage;
        }
    }
}