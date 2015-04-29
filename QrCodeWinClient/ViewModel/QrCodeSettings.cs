using System.Drawing;
using GalaSoft.MvvmLight;
using QrCodeWinClient.Common;

namespace QrCodeWinClient
{
    public class QrCodeSettings : ViewModelBase, IQrCodeSettings
    {
        public QrCodeSettings()
        {
            this.ErrorCorrectionLevel = ErrorCorrectionLevel.M;
            this.ModuleSize = 1;
            this.ModuleSize = 1;
            this.LightBrush = new SolidBrush(Color.White);
            this.DarkBrush = new SolidBrush(Color.Black);
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

        private SolidBrush lightBrush;

        public SolidBrush LightBrush
        {
            get { return this.lightBrush; }
            set { this.Set(ref this.lightBrush, value); }
        }

        private SolidBrush darkBrush;

        public SolidBrush DarkBrush
        {
            get { return this.darkBrush; }
            set { this.Set(ref this.darkBrush, value); }
        }
    }
}