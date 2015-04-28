using System.Windows.Media;
using System.Xml.Serialization;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using QrCodeWinClient.Common;
using QrCodeWinClient.Export;
using System.Drawing;
using Brush = System.Drawing.Brush;
using Brushes = System.Drawing.Brushes;

namespace QrCodeWinClient.ViewModel
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

        private ImageSource qrCodeImage;

        public ImageSource QrCodeImage
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

        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (this.IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                ExportInstance.Instance.Init();
                this.MessengerInstance.Register<QrCodeResponseMessage>(this, this.ReveiceQRCode);

                this.Settings = new QrCodeSettings();
            }
        }

        private void ReveiceQRCode(QrCodeResponseMessage qrCodeResponseMessage)
        {
            this.QrCodeImage = qrCodeResponseMessage.QrCodeImage;
        }
    }

    public class QrCodeSettings : ViewModelBase, IQrCodeSettings
    {
        public QrCodeSettings()
        {
            this.ErrorCorrectionLevel = ErrorCorrectionLevel.M;
            this.ModuleSize = 1;
            this.LightBrush = Brushes.White;
            this.DarkBrush = Brushes.Black;
        }

        private ErrorCorrectionLevel errorCorrectionLevel;

        public ErrorCorrectionLevel ErrorCorrectionLevel
        {
            get { return this.errorCorrectionLevel; }
            set { this.Set(ref this.errorCorrectionLevel, value); }
        }

        private int moduleSize;

        public int ModuleSize
        {
            get { return this.moduleSize; }
            set { this.Set(ref this.moduleSize, value); }
        }

        private Brush lightBrush;

        public Brush LightBrush
        {
            get { return this.lightBrush; }
            set { this.Set(ref this.lightBrush, value); }
        }

        private Brush darkBrush;

        public Brush DarkBrush
        {
            get { return this.darkBrush; }
            set { this.Set(ref this.darkBrush, value); }
        }
    }
}