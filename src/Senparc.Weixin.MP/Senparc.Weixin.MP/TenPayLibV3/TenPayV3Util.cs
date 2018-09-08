#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc
 
    文件名：TenPayV3Util.cs
    文件功能描述：微信支付V3配置文件
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20161014
    修改描述：修改TenPayUtil.BuildRandomStr()方法

    修改标识：Senparc - 20170516
    修改描述：v14.4.8 1、完善TenPayLibV3.GetNoncestr()方法
                      2、优化BuildRandomStr()方法
             
    修改标识：Senparc - 20170522
    修改描述：v14.4.9 修改TenPayUtil.GetNoncestr()方法，将编码由GBK改为UTF8

    修改标识：Senparc - 20180331
    修改描述：v14.4.9 修改TenPayUtil.GetNoncestr()方法，将编码由GBK改为UTF8

----------------------------------------------------------------*/

using System;
using System.Text;
using System.Net;
using Senparc.Weixin.Helpers;
using Senparc.CO2NET.Helpers;

namespace Senparc.Weixin.MP.TenPayLibV3
{
    /// <summary>
    /// 微信支付工具类
    /// </summary>
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public class TenPayV3Util
    {
        public static Random random = new Random();

        /// <summary>
        /// 随机生成Noncestr
        /// </summary>
        /// <returns></returns>
        public static string GetNoncestr()
        {
            return EncryptHelper.GetMD5(Guid.NewGuid().ToString(), "UTF-8");
        }

        /// <summary>
        /// 获取微信时间格式
        /// </summary>
        /// <returns></returns>
        public static string GetTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// 对字符串进行URL编码
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

                //return res;
            }
        }

        /// <summary>
        /// 对字符串进行URL解码
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
                //return res;

            }
        }


        /// <summary>
        /// 取时间戳生成随即数,替换交易单号中的后10位流水号
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
        /// 取随机数
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
        /// 创建当天内不会重复的数字
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string BuildDailyRandomStr(int length)
        {
            var stringFormat = DateTime.Now.ToString("HHmmss0000");//共10位

            return stringFormat;
        }


        /// <summary>
        /// 对退款通知消息进行解密
        /// </summary>
        /// <param name="reqInfo"></param>
        /// <param name="mchKey"></param>
        /// <returns></returns>
        public static string DecodeRefundReqInfo(string reqInfo, string mchKey)
        {
            //参考文档：https://pay.weixin.qq.com/wiki/doc/api/native.php?chapter=9_16&index=11
            /*
               解密步骤如下： 
                （1）对加密串A做base64解码，得到加密串B
                （2）对商户key做md5，得到32位小写key* ( key设置路径：微信商户平台(pay.weixin.qq.com)-->账户设置-->API安全-->密钥设置 )

                （3）用key*对加密串B做AES-256-ECB解密（PKCS7Padding）
             */
            //var base64Encode = Encoding.UTF8.GetString(Convert.FromBase64String(reqInfo));//(1)
            var base64Encode = reqInfo;//(1) EncryptHelper.AESDecrypt 方法内部会进行一次base64解码，因此这里不再需要解码
            var md5Str = EncryptHelper.GetLowerMD5(mchKey, Encoding.UTF8);//(2)
            var result = EncryptHelper.AESDecrypt(base64Encode, md5Str);//(3)
            return result;
        }
    }
}
