using System.Security.Cryptography;
using System.Text;

namespace Cryptocurrency
{
    public static class Hash
    {
        #region SHA-256
        private static SHA256 sha256 = new SHA256CryptoServiceProvider();

        public static byte[] SHA256(string plane, uint times = 1)
        {
            return SHA256(Encoding.UTF8.GetBytes(plane), times);
        }

        public static byte[] SHA256(byte[] buf, uint times = 1)
        {
            if (buf == null) return null;
            byte[] hash = buf;
            for (uint i = 0; i < times; i++)
            {
                hash = sha256.ComputeHash(hash);
            }
            return hash;
        }
        #endregion

        #region RIPEMD-160
        private static RIPEMD160 ripemd160 = new RIPEMD160Managed();

        public static byte[] RIPEMD160(string plane, uint times = 1)
        {
            return RIPEMD160(Encoding.UTF8.GetBytes(plane), times);
        }

        public static byte[] RIPEMD160(byte[] buf, uint times = 1)
        {
            if (buf == null) return null;
            byte[] hash = buf;
            for (uint i = 0; i < times; i++)
            {
                hash = ripemd160.ComputeHash(hash);
            }
            return hash;
        }
        #endregion
    }
}
