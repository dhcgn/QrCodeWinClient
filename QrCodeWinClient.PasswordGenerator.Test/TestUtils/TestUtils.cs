using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QrCodeWinClient.Common;

namespace QrCodeWinClient.PasswordGenerator.Test
{
    public class TestUtils
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
}
