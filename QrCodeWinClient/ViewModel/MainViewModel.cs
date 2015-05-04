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
                this.MessengerInstance.Register<QrCodeResponseMessage>(this, this.ReveiceQRCode);
                this.MessengerInstance.Register<LoadResponseMessage>(this, this.ProcessLoadMessage);


                this.QrSettingsViewModel = new QrCodeSettingsViewModel();
                this.QrSettingsViewModel.PropertyChanged += (sender, args) =>
                {
                    this.MessengerInstance.Send(new QrCodeRequestMessage(this.InputText, this.QrSettingsViewModel));

                    if (this.loadedTypes.Contains(typeof (PersistQrCodeSettings)))
                    {
                        var persistQrCodeSettings = new PersistQrCodeSettings();
                        persistQrCodeSettings.OverrideFrom(this.QrSettingsViewModel);

                        this.MessengerInstance.Send(new SaveRequestMessage(persistQrCodeSettings, typeof (PersistQrCodeSettings)));
                    }

                };

                this.PasswordSettingsViewModel = new PasswordSettingsViewModel();
                this.Entropy = PasswordGenerator.EntropyCalculator.CalcEntropy(this.PasswordSettingsViewModel);
                this.PasswordSettingsViewModel.PropertyChanged += (sender, args) =>
                {
                    this.GeneratePasswordCommand.RaiseCanExecuteChanged();
                    this.Entropy = PasswordGenerator.EntropyCalculator.CalcEntropy(this.PasswordSettingsViewModel);

                    if (this.loadedTypes.Contains(typeof (PersistPasswordSettings)))
                    {
                        var persistSettings = new PersistPasswordSettings();
                        persistSettings.OverrideFrom(this.PasswordSettingsViewModel);

                        this.MessengerInstance.Send(new SaveRequestMessage(persistSettings, typeof (PersistPasswordSettings)));
                    }
                };

                this.QrCodeImage = Application.Current.Resources["EmptyQrCodeImageSource"] as BitmapImage;

                this.SaveQrCodeToLibraryCommand = new RelayCommand(this.SaveQRCodeToLibrary);
                this.SaveQrCodeDialogCommand = new RelayCommand(this.SaveQRCodeDialog);
                this.CopyQrCodeToClipboardCommand = new RelayCommand(this.CopyQRCodeToClipboard);
                this.GeneratePasswordCommand = new RelayCommand(this.GeneratePassword, () => this.PasswordSettingsViewModel.IsValid());

                this.MessengerInstance.Send(new LoadRequestMessage(typeof (PersistPasswordSettings)));
                this.MessengerInstance.Send(new LoadRequestMessage(typeof (PersistQrCodeSettings)));
            }
        }

        private readonly HashSet<Type> loadedTypes = new HashSet<Type>();

        private void ProcessLoadMessage(LoadResponseMessage obj)
        {
            if (obj.MyProperty != null)
            {
                if (obj.LoadType == typeof (PersistQrCodeSettings))
                {
                    this.QrSettingsViewModel.OverrideFrom(obj.MyProperty as PersistQrCodeSettings);
                }
                else if (obj.LoadType == typeof (PersistPasswordSettings))
                {
                    this.PasswordSettingsViewModel.OverrideFrom(obj.MyProperty as PersistPasswordSettings);
                }
            }

            if (!this.loadedTypes.Contains(obj.LoadType))
                this.loadedTypes.Add(obj.LoadType);
        }

        #endregion

        #region Command Handling

        private void GeneratePassword()
        {
            this.InputText = PasswordGenerator.PasswordGenerator.Generate(this.PasswordSettingsViewModel);
        }

        private void SaveQRCodeToLibrary()
        {
            ImageSaver.SaveQRCodeToLibrary(this.QrCodeImage);
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
                ImageSaver.SaveQRCodeToLibrary(this.QrCodeImage, dlg.FileName);
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
            var bitmapImage = qrCodeResponseMessage.QrCodeImage;
            if (bitmapImage != null)
            {
                this.QrCodeImage = bitmapImage;
            }
            else
            {
                this.QrCodeImage = Application.Current.Resources["EmptyQrCodeImageSource"] as BitmapImage;
            }
        }

        #endregion
    }
}