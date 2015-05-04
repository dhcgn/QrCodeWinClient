using GalaSoft.MvvmLight;
using QrCodeWinClient.Common;

namespace QrCodeWinClient
{
    public class PasswordSettingsViewModel : ViewModelBase, IPasswordSettings
    {
        public PasswordSettingsViewModel()
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
                this.Length = 12;
                this.ForceEach = true;

                this.IncludeNumeric = true;
                this.IncludeAlphaLower = true;
                this.IncludeAlphaUpper = true;
            }
        }

        private int length;

        public int Length
        {
            get { return this.length; }
            set { this.Set(ref this.length, value); }
        }

        private bool forceEach;

        public bool ForceEach
        {
            get { return this.forceEach; }
            set { this.Set(ref this.forceEach, value); }
        }

        private bool includeNumeric;

        public bool IncludeNumeric
        {
            get { return this.includeNumeric; }
            set { this.Set(ref this.includeNumeric, value); }
        }

        private bool includeAlphaLower;

        public bool IncludeAlphaLower
        {
            get { return this.includeAlphaLower; }
            set { this.Set(ref this.includeAlphaLower, value); }
        }

        private bool includeAlphaUpper;

        public bool IncludeAlphaUpper
        {
            get { return this.includeAlphaUpper; }
            set { this.Set(ref this.includeAlphaUpper, value); }
        }

        private bool includeSymbolSetNormal;

        public bool IncludeSymbolSetNormal
        {
            get { return this.includeSymbolSetNormal; }
            set { this.Set(ref this.includeSymbolSetNormal, value); }
        }

        private bool includeSymbolSetExtended;

        public bool IncludeSymbolSetExtended
        {
            get { return this.includeSymbolSetExtended; }
            set { this.Set(ref this.includeSymbolSetExtended, value); }
        }

        private bool includeUmlaute;

        public bool IncludeUmlaute
        {
            get { return this.includeUmlaute; }
            set { this.Set(ref this.includeUmlaute, value); }
        }
        private bool excludeSimilar;

        public bool ExcludeSimilar
        {
            get { return this.excludeSimilar; }
            set { this.Set(ref this.excludeSimilar, value); }
        }
    }
}