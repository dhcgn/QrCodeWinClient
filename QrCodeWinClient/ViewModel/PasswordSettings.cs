using GalaSoft.MvvmLight;
using QrCodeWinClient.Common;

namespace QrCodeWinClient
{
    public class PasswordSettings : ViewModelBase, IPasswordSettings
    {
        public PasswordSettings()
        {
            if (this.IsInDesignMode)
            {
                this.Length = 12;
                this.Entropy = 128;
                this.ForceEach = true;

                this.IncludeNumeric = true;
                this.IncludeAlphaLower = true;
                this.IncludeAlphaUpper = true;
            }
            else
            {
                
            }
        }

        public int Length { get; set; }

        public int Entropy { get; set; }

        public bool ForceEach { get; set; }

        public bool IncludeNumeric { get; set; }

        public bool IncludeAlphaLower { get; set; }

        public bool IncludeAlphaUpper { get; set; }

        public bool IncludeSymbolSetNormal { get; set; }

        public bool IncludeSymbolSetExtended { get; set; }
    }
}