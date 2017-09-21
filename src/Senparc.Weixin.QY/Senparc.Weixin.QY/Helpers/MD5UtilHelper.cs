/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    �ļ�����MD5UtilHelper.cs
    �ļ�������������ȡ��д��MD5ǩ�����
    
    
    ������ʶ��Senparc - 20150313
    
    �޸ı�ʶ��Senparc - 20150313
    �޸�����������ӿ�
----------------------------------------------------------------*/

using System;
using System.Security.Cryptography;
using System.Text;

namespace Senparc.Weixin.QY.Helpers
{
	/// <summary>
	/// MD5UtilHelper ��ժҪ˵����
	/// </summary>
	public class MD5UtilHelper
	{
		public MD5UtilHelper()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ��ȡ��д��MD5ǩ�����
		/// </summary>
		/// <param name="encypStr"></param>
		/// <param name="charset"></param>
		/// <returns></returns>
		public static string GetMD5(string encypStr, string charset)
		{
			string retStr;
#if NET45
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();
#else
            var m5 = MD5.Create();
#endif

            //����md5����
            byte[] inputBye;
			byte[] outputBye;

			//ʹ��GB2312���뷽ʽ���ַ���ת��Ϊ�ֽ����飮
			try
			{
				inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
			}
			catch (Exception ex)
			{
				inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
			}
			outputBye = m5.ComputeHash(inputBye);

			retStr = BitConverter.ToString(outputBye);
			retStr = retStr.Replace("-", "").ToUpper();
			return retStr;
		}
	}
}
