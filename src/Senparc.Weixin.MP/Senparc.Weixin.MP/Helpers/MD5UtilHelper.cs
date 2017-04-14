#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    �ļ�����MD5UtilHelper.cs
    �ļ�������������ȡ��д��MD5ǩ�����
    
    
    ������ʶ��Senparc - 20150211
    
    �޸ı�ʶ��Senparc - 20150303
    �޸�����������ӿ�
    
    �޸ı�ʶ��Senparc - 20161015
    �޸��������޸�GB2312����Ϊ936

    �޸ı�ʶ��Senparc - 20170203
    �޸�������v14.3.123  �ϳ�MD5UtilHelper�������ϲ���
       Senparc.Weixin.Helpers.EncryptHelper�£�Senparc.Weixin.dll�У�

----------------------------------------------------------------*/

using System;
using System.Security.Cryptography;
using System.Text;

namespace Senparc.Weixin.MP.Helpers
{
	/// <summary>
    /// MD5UtilHelper ��ժҪ˵����
	/// </summary>
	public class MD5UtilHelper
	{
        /// <summary>
        /// ��ȡ��д��MD5ǩ�����
		/// </summary>
		/// <param name="encypStr"></param>
		/// <param name="charset"></param>
		/// <returns></returns>
		public static string GetMD5(string encypStr, string charset)
		{
			string retStr;
			var m5 = MD5.Create();

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
