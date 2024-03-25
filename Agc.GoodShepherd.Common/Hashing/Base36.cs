using System.Numerics;

namespace Agc.GoodShepherd.Common.Hashing
{
    public static class Base36
    {
        private const string DIGITS = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static long Decode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Empty value.");
            value = value.ToUpper();
            bool negative = false;
            if (value[0] == '-')
            {
                negative = true;
                value = value.Substring(1, value.Length - 1);
            }

            if (value.Any(c => !DIGITS.Contains(c)))
                throw new ArgumentException("Invalid value: \"" + value + "\".");
            var decoded = 0L;
            for (var i = 0; i < value.Length; ++i)
                decoded += DIGITS.IndexOf(value[i]) * (long)BigInteger.Pow(DIGITS.Length, value.Length - i - 1);
            return negative ? decoded * -1 : decoded;
        }

        public static string Encode(long value)
        {
            if (value == long.MinValue)
            {
                return "-1Y2P0IJ32E8E8";
            }

            bool negative = value < 0;
            value = Math.Abs(value);
            string encoded = string.Empty;
            do
                encoded = DIGITS[(int)(value % DIGITS.Length)] + encoded;
            while ((value /= DIGITS.Length) != 0);
            return negative ? "-" + encoded : encoded;
        }
    }
}