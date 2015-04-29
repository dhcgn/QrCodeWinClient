namespace QrCodeWinClient.Common
{
    public interface IPasswordSettings
    {
        int Length { get; set; }
        int Entropy { get; set; }
        bool ForceEach { get; set; }
        bool IncludeNumeric { get; set; }
        bool IncludeAlphaLower { get; set; }
        bool IncludeAlphaUpper { get; set; }
        bool IncludeSymbolSetNormal { get; set; }
        bool IncludeSymbolSetExtended { get; set; }
    }
}