using Cryptocurrency;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace AddressGenerateTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1.公開鍵と秘密鍵のペアをECDsaで自動生成する
            var dsa = ECDsa.Create("ECDsaCng");
            var curve = ECCurve.CreateFromFriendlyName("secp256k1");
            dsa.GenerateKey(curve);
            var param = dsa.ExportParameters(true);

            // 秘密鍵(32byte)
            var privateKey = param.D;

            // 公開鍵(64byte)
            var publicKey = param.Q.X.Concat(param.Q.Y).ToArray();

            // 公開鍵からアドレスを作成
            var address = Address.GenerateFromPublicKey(publicKey);

            // ランダムに作成したアドレスを出力
            Console.WriteLine($"Address: {address.String}");
            Console.ReadLine();
        }
    }
}
