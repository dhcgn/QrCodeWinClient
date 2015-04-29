using System;
using System.Collections.Generic;
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
            if(settings.IncludeNumeric && !password.Any(x=> CharSets.Numbers.Contains(x)))
                return false;

            if(settings.IncludeAlphaLower && !password.Any(x=> CharSets.AlphaLower.Contains(x)))
                return false;

            if(settings.IncludeAlphaUpper && !password.Any(x=> CharSets.AlphaUpper.Contains(x)))
                return false;

            if(settings.IncludeSymbolSetNormal && !password.Any(x=> CharSets.CommonSymbols.Contains(x)))
                return false;

            if(settings.IncludeSymbolSetExtended && !password.Any(x=> CharSets.UnCommonSymbols.Contains(x)))
                return false;

            return true;
        }

        private static string GetCombinedCharSet(IPasswordSettings settings)
        {
            string result = String.Empty;

            if (settings.IncludeNumeric)
                result += CharSets.Numbers;

            if (settings.IncludeAlphaLower)
                result += CharSets.AlphaLower;

            if (settings.IncludeAlphaUpper)
                result += CharSets.AlphaUpper;

            if (settings.IncludeSymbolSetNormal)
                result += CharSets.CommonSymbols;

            if (settings.IncludeSymbolSetExtended)
                result += CharSets.UnCommonSymbols;

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
    

    public class CharSets
    {
        public const string Numbers = "0123456789";
        public const string AlphaLower = "abcdefghijklmnopqrstuvwxyz";
        public const string AlphaUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string CommonSymbols = ",.-;:_#+*?=)(&%$!<>@";
        public const string UnCommonSymbols = "°^§/\\'~|";

        public enum DigitTypes
        {
            Undefiend = 0,
            Numeric,
            LowerCase,
            UpperCase,
            SymbolCommon,
            SymbolUnCommon
        }
    }

    public class EntropyCalculator
    {

        public static int CalcEntropy(IPasswordSettings settings)
        {
            var digitTypes = GetDigitTypes(settings);
            return GetPasswordCardinality(settings.Length, digitTypes);
        }

        private static int GetPasswordCardinality(int length, IEnumerable<CharSets.DigitTypes> digitTypes)
        {
            int passwordCardinality = 0;
            foreach (CharSets.DigitTypes digitType in digitTypes)
            {
                switch (digitType)
                {
                    case CharSets.DigitTypes.Numeric:
                        passwordCardinality += CharSets.Numbers.Length;
                        break;
                    case CharSets.DigitTypes.LowerCase:
                    case CharSets.DigitTypes.UpperCase:
                        passwordCardinality += CharSets.AlphaUpper.Length;
                        break;
                    case CharSets.DigitTypes.SymbolCommon:
                        passwordCardinality += CharSets.CommonSymbols.Length;
                        break;
                    case CharSets.DigitTypes.SymbolUnCommon:
                        passwordCardinality += CharSets.UnCommonSymbols.Length;
                        break;
                }
            }

            return (int)Math.Floor(length*Math.Log(passwordCardinality, 2));
        }

        public static int CalcRealEntropy(string password)
        {
            var digitTypes = GetDigitTypes(password);
            return GetPasswordCardinality(password.Length, digitTypes);
        }

        private static IEnumerable<CharSets.DigitTypes> GetDigitTypes(string password)
        {
            var result = new List<CharSets.DigitTypes>();

            if (password.Any(c => CharSets.Numbers.Contains(c)))
                result.Add(CharSets.DigitTypes.Numeric);

            if (password.Any(c => CharSets.AlphaLower.Contains(c)))
                result.Add(CharSets.DigitTypes.LowerCase);

            if (password.Any(c => CharSets.AlphaUpper.Contains(c)))
                result.Add(CharSets.DigitTypes.UpperCase);

            if (password.Any(c => CharSets.CommonSymbols.Contains(c)))
                result.Add(CharSets.DigitTypes.SymbolCommon);

            if (password.Any(c => CharSets.UnCommonSymbols.Contains(c)))
                result.Add(CharSets.DigitTypes.SymbolUnCommon);

            return result;
        }

        private static IEnumerable<CharSets.DigitTypes> GetDigitTypes(IPasswordSettings settings)
        {
            var result = new List<CharSets.DigitTypes>();

            if(settings.IncludeNumeric)
                result.Add(CharSets.DigitTypes.Numeric);

            if(settings.IncludeAlphaLower)
                result.Add(CharSets.DigitTypes.LowerCase);

            if (settings.IncludeAlphaUpper)
                result.Add(CharSets.DigitTypes.UpperCase);

            if (settings.IncludeSymbolSetNormal)
                result.Add(CharSets.DigitTypes.SymbolCommon);

            if (settings.IncludeSymbolSetExtended)
                result.Add(CharSets.DigitTypes.SymbolUnCommon);

            return result;
        }
    }
}
