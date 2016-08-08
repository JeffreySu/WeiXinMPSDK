/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
 
    创建标识：Senparc - 20160808
    创建描述：安全帮助类，提供SHA-1算法等
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
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
    }
}
