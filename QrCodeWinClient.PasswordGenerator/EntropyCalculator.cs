using System;
using System.Collections.Generic;
using System.Linq;
using QrCodeWinClient.Common;

namespace QrCodeWinClient.PasswordGenerator
{
    public class EntropyCalculator
    {
        public static int CalcEntropy(IPasswordSettings settings)
        {
            var digitTypes = GetDigitTypes(settings);
            return GetPasswordCardinality(settings.Length, digitTypes);
        }

        private static int GetPasswordCardinality(int length, IEnumerable<CharSets.DigitTypes> digitTypes)
        {
            var passwordCardinality = 0;
            foreach (var digitType in digitTypes)
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

            return (int) Math.Floor(length*Math.Log(passwordCardinality, 2));
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

            if (settings.IncludeNumeric)
                result.Add(CharSets.DigitTypes.Numeric);

            if (settings.IncludeAlphaLower)
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