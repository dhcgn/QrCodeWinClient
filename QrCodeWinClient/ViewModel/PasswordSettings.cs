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
                this.ForceEach = true;

                this.IncludeNumeric = true;
                this.IncludeAlphaLower = true;
                this.IncludeAlphaUpper = true;
            }
            else
            {
            }
        }

        private int length;

        public int Length
        {
            get { return this.length; }
            set { base.Set(ref this.length, value); }
        }

        private bool forceEach;

        public bool ForceEach
        {
            get { return this.forceEach; }
            set { base.Set(ref this.forceEach, value); }
        }

        private bool includeNumeric;

        public bool IncludeNumeric
        {
            get { return this.includeNumeric; }
            set { base.Set(ref this.includeNumeric, value); }
        }

        private bool includeAlphaLower;

        public bool IncludeAlphaLower
        {
            get { return this.includeAlphaLower; }
            set { base.Set(ref this.includeAlphaLower, value); }
        }

        private bool includeAlphaUpper;

        public bool IncludeAlphaUpper
        {
            get { return this.includeAlphaUpper; }
            set { base.Set(ref this.includeAlphaUpper, value); }
        }

        private bool includeSymbolSetNormal;

        public bool IncludeSymbolSetNormal
        {
            get { return this.includeSymbolSetNormal; }
            set { base.Set(ref this.includeSymbolSetNormal, value); }
        }

        private bool includeSymbolSetExtended;

        public bool IncludeSymbolSetExtended
        {
            get { return this.includeSymbolSetExtended; }
            set { base.Set(ref this.includeSymbolSetExtended, value); }
        }
    }
}