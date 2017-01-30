/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：EncryptHelper.cs
    文件功能描述：加密、解密处理类
    
    
    创建标识：Senparc - 20170130
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.WxOpen.Containers;

namespace Senparc.Weixin.WxOpen.Helpers
{
    /// <summary>
    /// EncryptHelper
    /// </summary>
    public class EncryptHelper
    {
        ///// <summary>
        ///// SHA1加密
        ///// </summary>
        ///// <param name="str"></param>
        ///// <returns></returns>
        //public static string EncryptToSHA1(string str)
        //{
        //    SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
        //    byte[] str1 = Encoding.UTF8.GetBytes(str);
        //    byte[] str2 = sha1.ComputeHash(str1);
        //    sha1.Clear();
        //    (sha1 as IDisposable).Dispose();
        //    return Convert.ToBase64String(str2);
        //}

        /// <summary>
        /// 获得签名
        /// </summary>
        /// <param name="rawData"></param>
        /// <param name="session_Key"></param>
        /// <returns></returns>
        public static string GetSignature(string rawData, string session_Key)
        {
            var signature = Senparc.Weixin.Helpers.EncryptHelper.SHA1_Encrypt(rawData + session_Key);
            return signature;
        }

        /// <summary>
        /// 比较签名是否正确
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="rawData"></param>
        /// <param name="compareSignature"></param>
        /// <exception cref="WxOpenException">当SessionId无效时抛出异常</exception>
        /// <returns></returns>
        public static bool CheckSignature(string sessionId, string rawData, string compareSignature)
        {
            var sessionBag = SessionContainer.GetSession(sessionId);
            if (sessionBag == null)
            {
                throw new WxOpenException("SessionId无效");
            }

            var signature = GetSignature(rawData, sessionBag.SessionKey);
            return signature == compareSignature;
        }
    }
}
