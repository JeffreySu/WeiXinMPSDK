/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：MD5UtilHelper.cs
    文件功能描述：获取大写的MD5签名结果
    
    
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
	/// <summary>
    /// MD5UtilHelper 的摘要说明。
	/// </summary>
	public class MD5UtilHelper
	{
        /// <summary>
        /// 获取大写的MD5签名结果
        /// </summary>
        /// <param name="encypStr"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        [Obsolete("此方法已过期，请使用Senparc.Weixin.Helpers.EncryptHelper.GetMD5(encypStr,charset)")]
        public static string GetMD5(string encypStr, string charset)
        {
            return Senparc.Weixin.Helpers.EncryptHelper.GetMD5(encypStr, charset);
        }
	}
}
