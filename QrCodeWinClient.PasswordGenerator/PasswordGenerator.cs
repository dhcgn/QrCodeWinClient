using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using QrCodeWinClient.Common;

namespace QrCodeWinClient.PasswordGenerator
{
    public class PasswordGenerator
    {
        public static string Generate(IPasswordSettings settings)
        {
            var combinedCharSet = GetCombinedCharSet(settings);

            var password = CreatePassword(combinedCharSet, settings.Length);

            return password;
        }

        private static string CreatePassword(string combinedCharSet, int length)
        {
            var random = new CryptoRandom();

            var sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                int rndIndex = random.Next(0, combinedCharSet.Length);
                sb.Append(combinedCharSet[rndIndex]);
            }

            return sb.ToString();
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
            return (int) (length*Math.Log(passwordCardinality));
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

            if (password.Any(c => CharSets.Numbers.Contains(c)))
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
    public class CryptoRandom : Random
    {
        private readonly RNGCryptoServiceProvider _rng =
            new RNGCryptoServiceProvider();

        private readonly byte[] _uint32Buffer = new byte[4];

        public CryptoRandom()
        {
        }

        public CryptoRandom(Int32 ignoredSeed)
        {
        }

        public override Int32 Next()
        {
            _rng.GetBytes(_uint32Buffer);
            return BitConverter.ToInt32(_uint32Buffer, 0) & 0x7FFFFFFF;
        }

        public override Int32 Next(Int32 maxValue)
        {
            if (maxValue < 0)
                throw new ArgumentOutOfRangeException("maxValue");
            return Next(0, maxValue);
        }

        public override Int32 Next(Int32 minValue, Int32 maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException("minValue");
            if (minValue == maxValue) return minValue;
            Int64 diff = maxValue - minValue;
            while (true)
            {
                _rng.GetBytes(_uint32Buffer);
                UInt32 rand = BitConverter.ToUInt32(_uint32Buffer, 0);

                const long max = (1 + (Int64)UInt32.MaxValue);
                Int64 remainder = max % diff;
                if (rand < max - remainder)
                {
                    return (Int32)(minValue + (rand % diff));
                }
            }
        }

        public override double NextDouble()
        {
            _rng.GetBytes(_uint32Buffer);
            UInt32 rand = BitConverter.ToUInt32(_uint32Buffer, 0);
            return rand / (1.0 + UInt32.MaxValue);
        }

        public override void NextBytes(byte[] buffer)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            _rng.GetBytes(buffer);
        }
    }
}
