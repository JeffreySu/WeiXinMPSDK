/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
 
    �ļ�����TenPayUtil.cs
    �ļ�����������΢��֧�������ļ�
    
    
    ������ʶ��Senparc - 20150722

    �޸ı�ʶ��Senparc - 20161014
    �޸��������޸�TenPayUtil.BuildRandomStr()����

    �޸ı�ʶ��Senparc - 20161014
    �޸��������޸�TenPayUtil.BuildRandomStr()����

    �޸ı�ʶ��Senparc - 20170522
    �޸�������v4.2.5 �޸�TenPayUtil.GetNoncestr()��������������GBK��ΪUTF8
    
----------------------------------------------------------------*/

using System;
using System.Text;
using Senparc.Weixin.Helpers;
using System.Net;
using Senparc.CO2NET.Helpers;

namespace Senparc.Weixin.Work.TenPayLib
{
    /// <summary>
    /// TenpayUtil ��ժҪ˵����
    /// �����ļ�
    /// </summary>
    [Obsolete("��ʹ�� Senparc.Weixin.TenPay.dll��Senparc.Weixin.TenPay.V3 �еĶ�Ӧ����")]
    public class TenPayUtil
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

        public static string GetTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
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
                string res;

                try
                {
#if NET35 || NET40 || NET45 || NET461
                    return System.Web.HttpUtility.UrlEncode(instr, Encoding.GetEncoding(charset));
#else
                    return WebUtility.UrlEncode(instr);
#endif
                }
                catch (Exception ex)
                {
#if NET35 || NET40 || NET45 || NET461
                    return System.Web.HttpUtility.UrlEncode(instr, Encoding.GetEncoding("GB2312"));
#else
                    return WebUtility.UrlEncode(instr);
#endif
                }


                return res;
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
                string res;

                try
                {
#if NET35 || NET40 || NET45 || NET461
                    return System.Web.HttpUtility.UrlDecode(instr, Encoding.GetEncoding(charset));
#else
                    return WebUtility.UrlDecode(instr);
#endif
                }
                catch (Exception ex)
                {
#if NET35 || NET40 || NET45 || NET461
                    return System.Web.HttpUtility.UrlDecode(instr, Encoding.GetEncoding("GB2312"));
#else
                    return WebUtility.UrlDecode(instr);
#endif
                }


                return res;

            }
        }


        /// <summary>
        /// ȡʱ��������漴��,�滻���׵����еĺ�10λ��ˮ��
        /// </summary>
        /// <returns></returns>
        public static UInt32 UnixStamp()
        {
#if NET35 || NET40 || NET45 || NET461
            TimeSpan ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
#else
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1);
#endif
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

    }
}