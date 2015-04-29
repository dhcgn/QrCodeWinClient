using System;
using System.Security.Cryptography;

namespace QrCodeWinClient.PasswordGenerator
{
    public class CryptoRandom : Random
    {
        private readonly RNGCryptoServiceProvider rng =
            new RNGCryptoServiceProvider();

        private readonly byte[] uint32Buffer = new byte[4];

        public CryptoRandom()
        {
        }

        public CryptoRandom(int ignoredSeed)
        {
        }

        public override int Next()
        {
            this.rng.GetBytes(this.uint32Buffer);
            return BitConverter.ToInt32(this.uint32Buffer, 0) & 0x7FFFFFFF;
        }

        public override int Next(int maxValue)
        {
            if (maxValue < 0)
                throw new ArgumentOutOfRangeException("maxValue");
            return this.Next(0, maxValue);
        }

        public override int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException("minValue");
            if (minValue == maxValue) return minValue;
            long diff = maxValue - minValue;
            while (true)
            {
                this.rng.GetBytes(this.uint32Buffer);
                var rand = BitConverter.ToUInt32(this.uint32Buffer, 0);

                const long max = (1 + (long) uint.MaxValue);
                var remainder = max%diff;
                if (rand < max - remainder)
                {
                    return (int) (minValue + (rand%diff));
                }
            }
        }

        public override double NextDouble()
        {
            this.rng.GetBytes(this.uint32Buffer);
            var rand = BitConverter.ToUInt32(this.uint32Buffer, 0);
            return rand/(1.0 + uint.MaxValue);
        }

        public override void NextBytes(byte[] buffer)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            this.rng.GetBytes(buffer);
        }
    }
}