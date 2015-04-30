namespace QrCodeWinClient.PasswordGenerator
{
    public class CharSets
    {
        /// <summary>
        /// This similar charecters should only be used if you use copy/paste
        /// </summary>
        public const string SimiliarChars = "iIlO0o";

        public const string Numerics = "0123456789";
        public const string AlphaLower = "abcdefghijklmnopqrstuvwxyz";
        public const string AlphaUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public const string Umlaute = "‰ˆ¸ƒ÷‹";

        public const string SymbolCommon = ",.-;:_#+*?=)(&%$!<>@";
        public const string SymbolUnCommon = "∞^ß/\\'~|";

        public enum DigitTypes
        {
            Undefiend = 0,
            Numerics,
            AlphaLower,
            AlphaUpper,
            Umlaute,
            SymbolCommon,
            SymbolUnCommon
        }
    }
}