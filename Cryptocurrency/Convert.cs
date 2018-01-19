using System;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Cryptocurrency
{
    public static class Convert
    {
        private static readonly string Base58Digits = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";

        public static string ToBase58String(byte[] array)
        {
            BigInteger data = 0;
            for (var i = 0; i < array.Length; i++)
            {
                data = data * 256 + array[i];
            }

            var sb = new StringBuilder();
            while (data > 0)
            {
                var index = (int)(data % 58);
                data /= 58;
                sb.Insert(0, Base58Digits[index]);
            }

            for (var i = 0; i < array.Length && array[i] == 0; i++)
            {
                sb.Insert(0, Base58Digits[0]);
            }

            return sb.ToString();
        }

        public static byte[] FromBase58String(string str)
        {
            BigInteger data = 0;
            for (var i = 0; i < str.Length; i++)
            {
                var index = Base58Digits.IndexOf(str[i]);
                if (index < 0)
                    throw new FormatException(string.Format("Invalid Base58 character `{0}` at position {1}", str[i], i));
                data = data * 58 + index;
            }

            var zeroCount = str.TakeWhile(c => c == Base58Digits[0]).Count();
            var zeros = Enumerable.Repeat((byte)0, zeroCount);
            var bytes = data.ToByteArray().Reverse().SkipWhile(b => b == 0);

            return zeros.Concat(bytes).ToArray();
        }

    }
}
