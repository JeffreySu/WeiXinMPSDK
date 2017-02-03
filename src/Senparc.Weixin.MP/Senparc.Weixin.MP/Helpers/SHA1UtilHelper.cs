/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：SHA1UtilHelper.cs
    文件功能描述：SHA1签名算法
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20170203
    修改描述：v14.3.123  废除MD5UtilHelper，方法合并到
       Senparc.Weixin.Helpers.EncryptHelper下（Senparc.Weixin.dll中）

----------------------------------------------------------------*/

using System;
using System.Security.Cryptography;
using System.Text;

namespace Senparc.Weixin.MP.Helpers
{
    public class SHA1UtilHelper
    {
        /// <summary>
        /// 签名算法
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [Obsolete("此方法已过期，请使用Senparc.Weixin.Helpers.EncryptHelper.GetSha1(str)")]
        public static string GetSha1(string str)
        {
            return Senparc.Weixin.Helpers.EncryptHelper.GetSha1(str);

            ////建立SHA1对象
            //SHA1 sha = new SHA1CryptoServiceProvider();
            ////将mystr转换成byte[] 
            //ASCIIEncoding enc = new ASCIIEncoding();
            //byte[] dataToHash = enc.GetBytes(str);
            ////Hash运算
            //byte[] dataHashed = sha.ComputeHash(dataToHash);
            ////将运算结果转换成string
            //string hash = BitConverter.ToString(dataHashed).Replace("-", "");
            //return hash;
        }
    }
}