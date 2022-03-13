using System;
using System.Security.Cryptography;

namespace Client.TenPayHttpClient.Signer
{
    public class SHA256WithRSASigner : ISigner
    {
        public string GetAlgorithm()
        {
            return "SHA256-RSA2048";
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

            using (var rsa = System.Security.Cryptography.RSA.Create())
            {
                rsa.ImportPkcs8PrivateKey(keyData, out _);
                byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
                return Convert.ToBase64String(rsa.SignData(data, 0, data.Length, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
            }
        }
    }
}
