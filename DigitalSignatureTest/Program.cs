using Cryptocurrency;
using System;
using System.Text;

namespace DigitalSignatureTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // 公開鍵と秘密鍵を自動生成
            var ds = DigitalSignature.Generate();

            // 送金データを作成
            var txA = Encoding.UTF8.GetBytes("アドレスAからアドレスBに50枚送ります");

            // 送金データの署名を作成(秘密鍵で署名)
            var signA = ds.Sign(txA);

            // --- 誰かが受け取ったとする ---

            // 検証用のデジタル署名インスタンスを作成
            var ds2 = DigitalSignature.FromKey(ds.PublicKey);

            // 送金データの検証(公開鍵で検証)
            var res = ds2.Verify(txA, signA);
            Console.WriteLine($"検証結果: {res}"); // true

            // ためしに改ざん送金データを作成
            var txZ = Encoding.UTF8.GetBytes("アドレスAからアドレスZに100枚送ります");

            // これは失敗
            res = ds2.Verify(txZ, signA);
            Console.WriteLine($"検証結果: {res}"); // false
            Console.ReadLine();
        }
    }
}
