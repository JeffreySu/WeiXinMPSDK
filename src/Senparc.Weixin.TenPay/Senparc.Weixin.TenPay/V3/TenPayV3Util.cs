#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2020 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2020 Senparc
 
    �ļ�����TenPayV3Util.cs
    �ļ�����������΢��֧��V3�����ļ�
    
    
    ������ʶ��Senparc - 20150211
    
    �޸ı�ʶ��Senparc - 20150303
    �޸�����������ӿ�

    �޸ı�ʶ��Senparc - 20161014
    �޸��������޸�TenPayUtil.BuildRandomStr()����

    �޸ı�ʶ��Senparc - 20170516
    �޸�������v14.4.8 1������TenPayLibV3.GetNoncestr()����
                      2���Ż�BuildRandomStr()����
             
    �޸ı�ʶ��Senparc - 20170522
    �޸�������v14.4.9 �޸�TenPayUtil.GetNoncestr()��������������GBK��ΪUTF8

    �޸ı�ʶ��Senparc - 20180331
    �޸�������v14.4.9 �޸�TenPayUtil.GetNoncestr()��������������GBK��ΪUTF8

----------------------------------------------------------------*/

using System;
using System.Text;
using System.Net;
using Senparc.Weixin.Helpers;
using Senparc.CO2NET.Helpers;

namespace Senparc.Weixin.TenPay.V3
{
    /// <summary>
    /// ΢��֧��������
    /// </summary>
    public class TenPayV3Util
    {
        public static Random random = new Random();

        /// <summary>
        /// �������Noncestr
        /// </summary>
        /// <returns></returns>
        public static string GetNoncestr()
        {
            return EncryptHelper.GetMD5(Guid.NewGuid().ToString(), "UTF-8");
        }

        /// <summary>
        /// ��ȡ΢��ʱ���ʽ
        /// </summary>
        /// <returns></returns>
        public static string GetTimestamp()
        {
            TimeSpan ts = SystemTime.Now - new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// ���ַ�������URL����
        /// </summary>
        /// <param name="instr"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string UrlEncode(string instr, string charset)
        {
            //return instr;
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                //string res;

                try
                {
#if NET45
                    return System.Web.HttpUtility.UrlEncode(instr, Encoding.GetEncoding(charset));
#else
                    return WebUtility.UrlEncode(instr);
#endif
                }
                catch (Exception ex)
                {
#if NET45
                    return System.Web.HttpUtility.UrlEncode(instr, Encoding.GetEncoding("GB2312"));
#else
                    return WebUtility.UrlEncode(instr);
#endif
                }

                //return res;
            }
        }

        /// <summary>
        /// ���ַ�������URL����
        /// </summary>
        /// <param name="instr"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string UrlDecode(string instr, string charset)
        {
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                //string res;

                try
                {
#if NET45
                    return System.Web.HttpUtility.UrlDecode(instr, Encoding.GetEncoding(charset));
#else
                    return WebUtility.UrlDecode(instr);
#endif
                }
                catch (Exception ex)
                {
#if NET45
                    return System.Web.HttpUtility.UrlDecode(instr, Encoding.GetEncoding("GB2312"));
#else
                    return WebUtility.UrlDecode(instr);
#endif
                }
                //return res;

            }
        }


        /// <summary>
        /// ȡʱ��������漴��,�滻���׵����еĺ�10λ��ˮ��
        /// </summary>
        /// <returns></returns>
        public static UInt32 UnixStamp()
        {
//#if NET45
//            TimeSpan ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
//#else
            TimeSpan ts = SystemTime.Now - new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);
//#endif
            return Convert.ToUInt32(ts.TotalSeconds);
        }

        /// <summary>
        /// ȡ�����
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string BuildRandomStr(int length)
        {
            int num;

            lock (random)
            {
                num = random.Next();
            }

            string str = num.ToString();

            if (str.Length > length)
            {
                str = str.Substring(0, length);
            }
            else if (str.Length < length)
            {
                int n = length - str.Length;
                while (n > 0)
                {
                    str = str.Insert(0, "0");
                    n--;
                }
            }

            return str;
        }

        /// <summary>
        /// ���������ڲ����ظ�������
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string BuildDailyRandomStr(int length)
        {
            var stringFormat = SystemTime.Now.ToString("HHmmss0000");//��10λ

            return stringFormat;
        }


        /// <summary>
        /// ���˿�֪ͨ��Ϣ���н���
        /// </summary>
        /// <param name="reqInfo"></param>
        /// <param name="mchKey"></param>
        /// <returns></returns>
        public static string DecodeRefundReqInfo(string reqInfo, string mchKey)
        {
            //�ο��ĵ���https://pay.weixin.qq.com/wiki/doc/api/native.php?chapter=9_16&index=11
            /*
               ���ܲ������£� 
                ��1���Լ��ܴ�A��base64���룬�õ����ܴ�B
                ��2�����̻�key��md5���õ�32λСдkey* ( key����·����΢���̻�ƽ̨(pay.weixin.qq.com)-->�˻�����-->API��ȫ-->��Կ���� )

                ��3����key*�Լ��ܴ�B��AES-256-ECB���ܣ�PKCS7Padding��
             */
            //var base64Encode = Encoding.UTF8.GetString(Convert.FromBase64String(reqInfo));//(1)
            var base64Encode = reqInfo;//(1) EncryptHelper.AESDecrypt �����ڲ������һ��base64���룬������ﲻ����Ҫ����
            var md5Str = EncryptHelper.GetLowerMD5(mchKey, Encoding.UTF8);//(2)
            var result = EncryptHelper.AESDecrypt(base64Encode, md5Str);//(3)
            return result;
        }
    }
}
