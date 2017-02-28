/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：MD5UtilHelper.cs
    文件功能描述：获取大写的MD5签名结果
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20161015
    修改描述：修改GB2312编码为936

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
		public static string GetMD5(string encypStr, string charset)
		{
			string retStr;
			var m5 = MD5.Create();

			//创建md5对象
			byte[] inputBye;
			byte[] outputBye;

			//使用GB2312编码方式把字符串转化为字节数组．
			try
			{
				inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
			}
			catch (Exception ex)
			{
                //inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
                inputBye = Encoding.GetEncoding(936).GetBytes(encypStr);
            }
            outputBye = m5.ComputeHash(inputBye);

			retStr = BitConverter.ToString(outputBye);
			retStr = retStr.Replace("-", "").ToUpper();
			return retStr;
		}
	}
}
