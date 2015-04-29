using System;
using System.Security.Cryptography;

namespace QrCodeWinClient.PasswordGenerator
{
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
            this._rng.GetBytes(this._uint32Buffer);
            return BitConverter.ToInt32(this._uint32Buffer, 0) & 0x7FFFFFFF;
        }

        public override Int32 Next(Int32 maxValue)
        {
            if (maxValue < 0)
                throw new ArgumentOutOfRangeException("maxValue");
            return this.Next(0, maxValue);
        }

        public override Int32 Next(Int32 minValue, Int32 maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException("minValue");
            if (minValue == maxValue) return minValue;
            Int64 diff = maxValue - minValue;
            while (true)
            {
                this._rng.GetBytes(this._uint32Buffer);
                UInt32 rand = BitConverter.ToUInt32(this._uint32Buffer, 0);

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
            this._rng.GetBytes(this._uint32Buffer);
            UInt32 rand = BitConverter.ToUInt32(this._uint32Buffer, 0);
            return rand / (1.0 + UInt32.MaxValue);
        }

        public override void NextBytes(byte[] buffer)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            this._rng.GetBytes(buffer);
        }
    }
}