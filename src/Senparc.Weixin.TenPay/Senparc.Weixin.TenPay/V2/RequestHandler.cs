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
 
    文件名：RequestHandler.cs
    文件功能描述：微信支付 请求处理
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20170623
    修改描述：使用 ASCII 字典排序
----------------------------------------------------------------*/

using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using Senparc.CO2NET.Helpers;

#if NET35 || NET40 || NET45 || NET461
using System.Web;
#else
using Microsoft.AspNetCore.Http;
#endif

namespace Senparc.Weixin.TenPay.V2
{
    /**
    '签名工具类
     ============================================================================/// <summary>
    'api说明：
    'Init();
    '初始化函数，默认给一些参数赋值。
    'SetKey(key_)'设置商户密钥
    'CreateMd5Sign(signParams);字典生成Md5签名
    'GenPackage(packageParams);获取package包
    'CreateSHA1Sign(signParams);创建签名SHA1
    'ParseXML();输出xml
    'GetDebugInfo(),获取debug信息
     * 
     * ============================================================================
     */
    public class RequestHandler
    {

        public RequestHandler(HttpContext httpContext)
        {
            Parameters = new Hashtable();

#if NET35 || NET40 || NET45 || NET461
			this.HttpContext = httpContext ?? HttpContext.Current;
#else
            this.HttpContext = httpContext ?? new DefaultHttpContext();
#endif
        }
        /// <summary>
        /// 密钥
        /// </summary>
        private string Key;

        protected HttpContext HttpContext;

        /// <summary>
        /// 请求的参数
        /// </summary>
        protected Hashtable Parameters;

        /// <summary>
        /// debug信息
        /// </summary>
        private string DebugInfo;

        /// <summary>
        /// 初始化函数
        /// </summary>
        public virtual void Init()
        {
        }
        /// <summary>
        /// 获取debug信息
        /// </summary>
        /// <returns></returns>
        public String GetDebugInfo()
        {
            return DebugInfo;
        }
        /// <summary>
        /// 获取密钥
        /// </summary>
        /// <returns></returns>
        public string GetKey()
        {
            return Key;
        }
        /// <summary>
        /// 设置密钥
        /// </summary>
        /// <param name="key"></param>
        public void SetKey(string key)
        {
            this.Key = key;
        }

        /// <summary>
        /// 设置参数值
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="parameterValue"></param>
        public void SetParameter(string parameter, string parameterValue)
        {
            if (parameter != null && parameter != "")
            {
                if (Parameters.Contains(parameter))
                {
                    Parameters.Remove(parameter);
                }

                Parameters.Add(parameter, parameterValue);
            }
        }


        /// <summary>
        /// 获取package带参数的签名包
        /// </summary>
        /// <returns></returns>
        public string GetRequestURL()
        {
            this.CreateSign();
            StringBuilder sb = new StringBuilder();
            ArrayList akeys = new ArrayList(Parameters.Keys);
            akeys.Sort(ASCIISort.Create());
            foreach (string k in akeys)
            {
                string v = (string)Parameters[k];
                if (null != v && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + TenPayUtil.UrlEncode(v, GetCharset()) + "&");
                }
            }

            //去掉最后一个&
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }


            return sb.ToString();

        }

        /// <summary>
        /// 创建md5摘要,规则是:按参数名称a-z排序,遇到空值的参数不参加签名
        /// </summary>
        protected virtual void CreateSign()
        {
            StringBuilder sb = new StringBuilder();

            ArrayList akeys = new ArrayList(Parameters.Keys);
            akeys.Sort(ASCIISort.Create());

            foreach (string k in akeys)
            {
                string v = (string)Parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }

            sb.Append("key=" + this.GetKey());
            string sign = EncryptHelper.GetMD5(sb.ToString(), GetCharset()).ToUpper();

            this.SetParameter("sign", sign);

            //debug信息
            this.SetDebugInfo(sb.ToString() + " => sign:" + sign);
        }


        /// <summary>
        /// 创建package签名
        /// </summary>
        /// <returns></returns>
        public virtual string CreateMd5Sign()
        {
            StringBuilder sb = new StringBuilder();
            ArrayList akeys = new ArrayList(Parameters.Keys);
            akeys.Sort(ASCIISort.Create());

            foreach (string k in akeys)
            {
                string v = (string)Parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "".CompareTo(v) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }
            string sign = EncryptHelper.GetMD5(sb.ToString(), GetCharset()).ToLower();

            this.SetParameter("sign", sign);
            return sign;
        }


        /// <summary>
        /// 创建sha1签名
        /// </summary>
        /// <returns></returns>
        public string CreateSHA1Sign()
        {
            StringBuilder sb = new StringBuilder();
            ArrayList akeys = new ArrayList(Parameters.Keys);
            akeys.Sort(ASCIISort.Create());

            foreach (string k in akeys)
            {
                string v = (string)Parameters[k];
                if (null != v && "".CompareTo(v) != 0
                       && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    if (sb.Length == 0)
                    {
                        sb.Append(k + "=" + v);
                    }
                    else
                    {
                        sb.Append("&" + k + "=" + v);
                    }
                }
            }
            string paySign = EncryptHelper.GetSha1(sb.ToString()).ToString().ToLower();

            //debug信息
            this.SetDebugInfo(sb.ToString() + " => sign:" + paySign);
            return paySign;
        }


        /// <summary>
        /// 输出XML
        /// </summary>
        /// <returns></returns>
        public string ParseXML()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<xml>");
            foreach (string k in Parameters.Keys)
            {
                string v = (string)Parameters[k];
                if (Regex.IsMatch(v, @"^[0-9.]$"))
                {

                    sb.Append("<" + k + ">" + v + "</" + k + ">");
                }
                else
                {
                    sb.Append("<" + k + "><![CDATA[" + v + "]]></" + k + ">");
                }

            }
            sb.Append("</xml>");
            return sb.ToString();
        }



        /// <summary>
        /// 设置debug信息
        /// </summary>
        /// <param name="debugInfo"></param>
        public void SetDebugInfo(String debugInfo)
        {
            this.DebugInfo = debugInfo;
        }

        public Hashtable GetAllParameters()
        {
            return this.Parameters;
        }

        protected virtual string GetCharset()
        {
#if NET35 || NET40 || NET45
            return this.HttpContext.Request.ContentEncoding.BodyName;
#else
            return Encoding.UTF8.WebName;
#endif
        }
    }
}
