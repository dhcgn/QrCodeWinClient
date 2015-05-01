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
        #region Commands

        public RelayCommand CopyQrCodeToClipboardCommand { get; set; }
        public RelayCommand SaveQrCodeToLibraryCommand { get; set; }
        public RelayCommand SaveQrCodeDialogCommand { get; set; }
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
                    this.MessengerInstance.Send(new QrCodeRequestMessage(value, this.QrSettingsViewModel));
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

        private QrCodeSettingsViewModel qrSettingsViewModel;

        public QrCodeSettingsViewModel QrSettingsViewModel
        {
            get { return this.qrSettingsViewModel; }
            set { this.Set(ref this.qrSettingsViewModel, value); }
        }

        private PasswordSettingsViewModel passwordSettingsViewModel;
        

        public PasswordSettingsViewModel PasswordSettingsViewModel
        {
            get { return this.passwordSettingsViewModel; }
            set { this.Set(ref this.passwordSettingsViewModel, value); }
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

        #region .ctor

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (this.IsInDesignMode)
            {
                this.QrSettingsViewModel = new QrCodeSettingsViewModel();
                this.PasswordSettingsViewModel = new PasswordSettingsViewModel();

                this.QrCodeImage = Application.Current.Resources["EmptyQrCodeImageSource"] as BitmapImage;

                this.Entropy = 128;
                this.RealEntropy = 121;

                this.InputText = "7mGwdDUhBafPMqwj13C7AFwM1JICXEko";
            }
            else
            {
                ExportInstance.Instance.Init();
                this.MessengerInstance.Register<QrCodeResponseMessage>(this, this.ReveiceQRCode);

                this.QrSettingsViewModel = new QrCodeSettingsViewModel();
                this.QrSettingsViewModel.PropertyChanged += (sender, args) => this.MessengerInstance.Send(new QrCodeRequestMessage(this.InputText, this.QrSettingsViewModel));

                this.PasswordSettingsViewModel = new PasswordSettingsViewModel();
                this.Entropy = PasswordGenerator.EntropyCalculator.CalcEntropy(this.PasswordSettingsViewModel);
                this.PasswordSettingsViewModel.PropertyChanged += (sender, args) =>
                {
                    this.GeneratePasswordCommand.RaiseCanExecuteChanged();
                    this.Entropy = PasswordGenerator.EntropyCalculator.CalcEntropy(this.PasswordSettingsViewModel);
                };

                this.QrCodeImage = Application.Current.Resources["EmptyQrCodeImageSource"] as BitmapImage;

                this.SaveQrCodeToLibraryCommand = new RelayCommand(SaveQRCodeToLibrary);
                this.SaveQrCodeDialogCommand = new RelayCommand(SaveQRCodeDialog);
                this.CopyQrCodeToClipboardCommand = new RelayCommand(CopyQRCodeToClipboard);
                GeneratePasswordCommand = new RelayCommand(GeneratePassword, ()=>this.PasswordSettingsViewModel.IsValid());
            }
        }

        #endregion

        #region Command Handling

        private void GeneratePassword()
        {
            this.InputText = PasswordGenerator.PasswordGenerator.Generate(this.PasswordSettingsViewModel);
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

        #endregion

        #region Message Handling

        private void ReveiceQRCode(QrCodeResponseMessage qrCodeResponseMessage)
        {
            this.QrCodeImage = qrCodeResponseMessage.QrCodeImage;
        }

        #endregion
    }
}