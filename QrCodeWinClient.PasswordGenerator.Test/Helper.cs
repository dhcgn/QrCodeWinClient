using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QrCodeWinClient.Common;

namespace QrCodeWinClient.PasswordGenerator.Test
{
    public class Helper
    {
        public static IPasswordSettings GetPasswordSettings()
        {
            var settings = new PasswordSettings
            {
                Length = 12,
                ForceEach = true,
                IncludeNumeric = true,
                IncludeAlphaLower = true,
                IncludeAlphaUpper = true
            };

            return settings;
        }
    }

   public class PasswordSettings : IPasswordSettings
    {
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
