using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QrCodeWinClient.Common;

namespace QrCodeWinClient.PasswordGenerator
{
    public class PasswordGenerator
    {
        public static string Generate(IPasswordSettings settings)
        {
            if (!settings.IsValid()) return null;

            var combinedCharSet = GetCombinedCharSet(settings);

            var password = CreatePassword(combinedCharSet, settings.Length, settings);

            return password;
        }

        private static string CreatePassword(string combinedCharSet, int length, IPasswordSettings settings)
        {
            var random = new CryptoRandom();

            for (int i = 0; i < Int32.MaxValue; i++)
            {
                var sb = new StringBuilder();
                for (int pwdIndex = 0; pwdIndex < length; pwdIndex++)
                {
                    int rndIndex = random.Next(0, combinedCharSet.Length);
                    sb.Append(combinedCharSet[rndIndex]);
                }

                var password = sb.ToString();
                if(settings.ForceEach && ContainsAllCharSets(settings, password))
                    return password;
                else if(!settings.ForceEach)
                    return password;
            }

            return null;
        }

        private static bool ContainsAllCharSets(IPasswordSettings settings, string password)
        {
            if(settings.IncludeNumeric && !password.Any(x=> CharSets.Numerics.Contains(x)))
                return false;

            if(settings.IncludeAlphaLower && !password.Any(x=> CharSets.AlphaLower.Contains(x)))
                return false;

            if(settings.IncludeAlphaUpper && !password.Any(x=> CharSets.AlphaUpper.Contains(x)))
                return false;

            if(settings.IncludeSymbolSetNormal && !password.Any(x=> CharSets.SymbolCommon.Contains(x)))
                return false;

            if(settings.IncludeSymbolSetExtended && !password.Any(x=> CharSets.SymbolUnCommon.Contains(x)))
                return false;

            return true;
        }

        private static string GetCombinedCharSet(IPasswordSettings settings)
        {
            string result = String.Empty;

            if (settings.IncludeNumeric)
                result += CharSets.Numerics;

            if (settings.IncludeAlphaLower)
                result += CharSets.AlphaLower;

            if (settings.IncludeAlphaUpper)
                result += CharSets.AlphaUpper;

            if (settings.IncludeSymbolSetNormal)
                result += CharSets.SymbolCommon;

            if (settings.IncludeSymbolSetExtended)
                result += CharSets.SymbolUnCommon;

            return result;
        }

        public static int CalcEntropy(IPasswordSettings settings)
        {
            return EntropyCalculator.CalcEntropy(settings);
        }

        public static int CalcRealEntropy(string password)
        {
            return EntropyCalculator.CalcRealEntropy(password);
        }
    }
}
