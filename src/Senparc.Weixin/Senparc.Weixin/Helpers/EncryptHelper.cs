/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
 
    创建标识：Senparc - 20160808
    创建描述：安全帮助类，提供SHA-1、AES算法等

    修改标识：Senparc - 20170130
    修改描述：v4.9.15 添加AES加密、解密算法
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Helpers
{
    ///
    /// 安全帮助类，提供SHA-1算法等
    ///
    public class EncryptHelper
    {
        ///
        /// 采用SHA-1算法加密字符串
        ///
        /// 要加密的字符串
        /// 返回加密后的字符串
        public static string SHA1_Encrypt(string sourceStr)
        {
            byte[] strRes = Encoding.Default.GetBytes(sourceStr);
            HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
            strRes = iSHA.ComputeHash(strRes);
            StringBuilder enText = new StringBuilder();
            foreach (byte iByte in strRes)
            {
                enText.AppendFormat("{0:x2}", iByte);
            }
            return enText.ToString();
        }

        #region AES

        /// AES加密
        /// </summary>
        /// <param name="inputdata">输入的数据</param>
        /// <param name="iv">向量128位</param>
        /// <param name="strKey">加密密钥</param>
        /// <returns></returns>
        public static byte[] AESEncrypt(byte[] inputdata, byte[] iv, string strKey)
        {
            //分组加密算法   
            SymmetricAlgorithm des = Rijndael.Create();
            byte[] inputByteArray = inputdata;//得到需要加密的字节数组       
                                              //设置密钥及密钥向量
            des.Key = Encoding.UTF8.GetBytes(strKey.Substring(0, 32));
            des.IV = iv;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    byte[] cipherBytes = ms.ToArray();//得到加密后的字节数组   
                    cs.Close();
                    ms.Close();
                    return cipherBytes;
                }
            }
        }


        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="inputdata">输入的数据</param>
        /// <param name="iv">向量128</param>
        /// <param name="strKey">key</param>
        /// <returns></returns>
        public static byte[] AESDecrypt(byte[] inputdata, byte[] iv, string strKey)
        {
            SymmetricAlgorithm des = Rijndael.Create();
            des.Key = Encoding.UTF8.GetBytes(strKey.Substring(0, 7));
            des.IV = iv;
            byte[] decryptBytes = new byte[inputdata.Length];
            using (MemoryStream ms = new MemoryStream(inputdata))
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    cs.Read(decryptBytes, 0, decryptBytes.Length);
                    cs.Close();
                    ms.Close();
                }
            }
            return decryptBytes;
        }

        /// <summary>
        /// Tencent官方方法
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="Iv"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static String AES_PKCS7_Encrypt(String Input, byte[] Iv, byte[] Key)
        {
            var aes = new RijndaelManaged();
            //秘钥的大小，以位为单位
            aes.KeySize = 256;
            //支持的块大小
            aes.BlockSize = 128;
            //填充模式
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            aes.Key = Key;
            aes.IV = Iv;
            var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] xBuff = null;

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Encoding.UTF8.GetBytes(Input);
                    cs.Write(xXml, 0, xXml.Length);
                }
                xBuff = ms.ToArray();
            }
            String Output = Convert.ToBase64String(xBuff);
            return Output;
        }


        #endregion
    }
}
