using QrCodeWinClient.Common;

namespace QrCodeWinClient.PasswordGenerator.Test
{
    public class PasswordSettings : IPasswordSettings
    {
        public int Length { get; set; }
        public bool ForceEach { get; set; }
        public bool IncludeNumeric { get; set; }
        public bool IncludeAlphaLower { get; set; }
        public bool IncludeAlphaUpper { get; set; }
        public bool IncludeSymbolSetNormal { get; set; }
        public bool IncludeSymbolSetExtended { get; set; }
    }
}