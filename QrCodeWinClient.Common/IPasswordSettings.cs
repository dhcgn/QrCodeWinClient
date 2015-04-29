namespace QrCodeWinClient.Common
{
    public interface IPasswordSettings
    {
        int Length { get; set; }
        bool ForceEach { get; set; }
        bool IncludeNumeric { get; set; }
        bool IncludeAlphaLower { get; set; }
        bool IncludeAlphaUpper { get; set; }
        bool IncludeSymbolSetNormal { get; set; }
        bool IncludeSymbolSetExtended { get; set; }
    }

    public static class MyExtensions
    {
        public static bool IsValid(this IPasswordSettings settings)
        {
            if (settings.Length < 1) return false;

            if (!settings.IncludeNumeric
                && !settings.IncludeAlphaLower
                && !settings.IncludeAlphaUpper
                && !settings.IncludeSymbolSetNormal
                && !settings.IncludeSymbolSetExtended)
            {
                return false;
            }

            if (settings.ForceEach)
            {
                int sets = 0;

                if (settings.IncludeNumeric) sets++;
                if (settings.IncludeAlphaLower) sets++;
                if (settings.IncludeAlphaUpper) sets++;
                if (settings.IncludeSymbolSetNormal) sets++;
                if (settings.IncludeSymbolSetExtended) sets++;
                if (settings.Length < sets) return false;
            }

            return true;
        }
    }
}