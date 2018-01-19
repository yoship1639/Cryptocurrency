using System.Linq;

namespace Cryptocurrency
{
    public class Address
    {
        public static readonly byte[] Pref_NotCompressed = new byte[] { 0x04 };
        public static readonly byte[] Pref_MainNet = new byte[] { 0x0F };

        public byte[] Bytes { get; set; }
        public string String => Convert.ToBase58String(Bytes);

        public Address(byte[] bytes) => Bytes = bytes;

        public static Address GenerateFromPublicKey(byte[] publicKey)
        {
            if (publicKey == null || publicKey.Length != 64) return null;

            // 2. 公開鍵の先頭に非圧縮を示すプレフィックス0x04を付加する
            var kPref = Pref_NotCompressed.Concat(publicKey).ToArray();

            // 3. 2をSHA-256でハッシュ化する
            var kHash = Hash.SHA256(kPref);

            // 4. 3をRIPEMD-160でハッシュ化する
            kHash = Hash.RIPEMD160(kHash);

            // 5. 4の先頭にアドレスのバージョンを示すプレフィックスを付加する
            var ad = Pref_MainNet.Concat(kHash).ToArray();

            // 6. 5をSHA-256でダブルハッシュ化する
            var adHash = Hash.SHA256(ad, 2);

            // 7. 5の末尾に6の先頭4バイトをチェックサムとして付加する
            return new Address(ad.Concat(adHash.Take(4)).ToArray());
        }
    }
}
