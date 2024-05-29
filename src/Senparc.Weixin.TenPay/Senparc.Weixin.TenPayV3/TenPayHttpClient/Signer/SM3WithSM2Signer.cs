using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Text;

namespace Client.TenPayHttpClient.Signer
{
    public class SM3WithSM2Signer : ISigner
    {
        public string GetAlgorithm()
        {
            return "SM2-WITH-SM3";
        }

        public string Sign(string message, string privateKey = null)
        {
            //privateKey ??= Senparc.Weixin.Config.SenparcWeixinSetting.TenPayV3_PrivateKey;

            byte[] keyData = Convert.FromBase64String(privateKey);

            #region 以下方法不兼容 Linux
            //using (CngKey cngKey = CngKey.Import(keyData, CngKeyBlobFormat.Pkcs8PrivateBlob))
            //using (RSACng rsa = new RSACng(cngKey))
            //{
            //    byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            //    return Convert.ToBase64String(rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
            //}
            #endregion

            //using (var rsa = System.Security.Cryptography.RSA.Create())
            //{
            //    rsa.ImportPkcs8PrivateKey(keyData, out _);
            //    byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            //    return Convert.ToBase64String(rsa.SignData(data, 0, data.Length, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
            //}


            byte[] msg = Encoding.Default.GetBytes(privateKey);
            byte[] md = new byte[32];
            SM3Digest sm3 = new SM3Digest();
            sm3.BlockUpdate(msg, 0, msg.Length);
            sm3.DoFinal(md, 0);
            return new UTF8Encoding().GetString(Hex.Encode(md));
        }
    }
}
