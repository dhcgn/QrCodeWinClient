using System;

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

    public class PersistPasswordSettings : IPasswordSettings
    {
        public int Length { get; set; }
        public bool ForceEach { get; set; }
        public bool IncludeNumeric { get; set; }
        public bool IncludeAlphaLower { get; set; }
        public bool IncludeAlphaUpper { get; set; }
        public bool IncludeSymbolSetNormal { get; set; }
        public bool IncludeSymbolSetExtended { get; set; }
    }

    public static class MyExtensions
    {
        public static void OverrideFrom(this IPasswordSettings settings, IPasswordSettings source)
        {
            if (settings == null || source == null) return;

            settings.Length = source.Length;

            settings.ForceEach = source.ForceEach;
            settings.IncludeNumeric = source.IncludeNumeric;
            settings.IncludeAlphaLower = source.IncludeAlphaLower;
            settings.IncludeAlphaUpper = source.IncludeAlphaUpper;
            settings.IncludeSymbolSetNormal = source.IncludeSymbolSetNormal;
            settings.IncludeSymbolSetExtended = source.IncludeSymbolSetExtended;
        }


        public static void OverrideFrom(this IQrCodeSettings settings, IQrCodeSettings source)
        {
            if (settings == null || source == null) return;

            settings.ModuleSize = source.ModuleSize;
            settings.ErrorCorrectionLevel = source.ErrorCorrectionLevel;
            settings.DarkBrush = source.DarkBrush;
            settings.LightBrush = source.LightBrush;
        }


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