namespace QrCodeWinClient.PasswordGenerator
{
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
}