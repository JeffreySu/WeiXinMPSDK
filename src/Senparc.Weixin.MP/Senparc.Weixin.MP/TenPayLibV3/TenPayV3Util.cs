﻿#region Apache License Version 2.0
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
 
    文件名：TenPayV3Util.cs
    文件功能描述：微信支付V3配置文件
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20161014
    修改描述：修改TenPayUtil.BuildRandomStr()方法
----------------------------------------------------------------*/

using System;
using System.Text;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.TenPayLibV3
{
    /// <summary>
    /// 微信支付工具类
    /// </summary>
    public class TenPayV3Util
    {
        /// <summary>
        /// 随机生成Noncestr
        /// </summary>
        /// <returns></returns>
        public static string GetNoncestr()
        {
            Random random = new Random();
            return MD5UtilHelper.GetMD5(random.Next(1000).ToString(), "936");
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

        ///// <summary>
        ///// 对字符串进行URL编码
        ///// </summary>
        ///// <param name="instr"></param>
        ///// <param name="charset"></param>
        ///// <returns></returns>
        //public static string UrlEncode(string instr, string charset)
        //{
        //    //return instr;
        //    if (instr == null || instr.Trim() == "")
        //        return "";
        //    else
        //    {
        //        string res;

        //        try
        //        {
        //            res = System.Web.HttpUtility.UrlEncode(instr, Encoding.GetEncoding(charset));

        //        }
        //        catch (Exception ex)
        //        {
        //            res = System.Web.HttpUtility.UrlEncode(instr, Encoding.GetEncoding("GB2312"));
        //        }


        //        return res;
        //    }
        //}

        ///// <summary>
        ///// 对字符串进行URL解码
        ///// </summary>
        ///// <param name="instr"></param>
        ///// <param name="charset"></param>
        ///// <returns></returns>
        //public static string UrlDecode(string instr, string charset)
        //{
        //    if (instr == null || instr.Trim() == "")
        //        return "";
        //    else
        //    {
        //        string res;

        //        try
        //        {
        //            res = System.Web.HttpUtility.UrlDecode(instr, Encoding.GetEncoding(charset));

        //        }
        //        catch (Exception ex)
        //        {
        //            res = System.Web.HttpUtility.UrlDecode(instr, Encoding.GetEncoding("GB2312"));
        //        }


        //        return res;

        //    }
        //}


        ///// <summary>
        ///// 取时间戳生成随即数,替换交易单号中的后10位流水号
        ///// </summary>
        ///// <returns></returns>
        //public static UInt32 UnixStamp()
        //{
        //    TimeSpan ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        //    return Convert.ToUInt32(ts.TotalSeconds);
        //}

        /// <summary>
        /// 取随机数
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string BuildRandomStr(int length)
        {
            Random rand = new Random();

            int num = rand.Next();

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

    }
}