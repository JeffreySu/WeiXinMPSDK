using System;
using System.Security.Cryptography;
using System.Text;

namespace Senparc.Weixin.MP.WeixinPayLib
{
	/// <summary>
	/// MD5Util 的摘要说明。
	/// </summary>
	public class MD5Util
	{
		public MD5Util()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
        /// 获取大写的MD5签名结果
		/// </summary>
		/// <param name="encypStr"></param>
		/// <param name="charset"></param>
		/// <returns></returns>
		public static string GetMD5(string encypStr, string charset)
		{
			string retStr;
			MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

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
				inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
			}
			outputBye = m5.ComputeHash(inputBye);

			retStr = System.BitConverter.ToString(outputBye);
			retStr = retStr.Replace("-", "").ToUpper();
			return retStr;
		}
	}
}
