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
 
    文件名：ResponseHandler.cs
    文件功能描述：微信支付V3 响应处理
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20170623
    修改描述：v14.4.14 排序规则统一为字典排序（ASCII）

    修改标识：Senparc - 20180517
    修改描述：v4.21.5-rc1 优化 .net core 环境下 TenPayLibV3.ResponseHandler 的默认 HttpContext获取

----------------------------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.Exceptions;
using Senparc.CO2NET.Helpers;

#if NET35 || NET40 || NET45 || NET461
using System.Web;
#else
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
#endif

namespace Senparc.Weixin.MP.TenPayLibV3
{

    /** 
    '============================================================================
    'Api说明：
    'GetKey()/setKey(),获取/设置密钥
    'GetParameter()/setParameter(),获取/设置参数值
    'GetAllParameters(),获取所有参数
    'IsTenpaySign(),是否正确的签名,true:是 false:否
    'IsWXsign(),是否正确的签名,true:是 false:否
    ' * IsWXsignfeedback判断微信维权签名
    ' * GetDebugInfo(),获取debug信息 
    '============================================================================
    */

    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public class ResponseHandler
    {
        /// <summary>
        /// 密钥 
        /// </summary>
        private string Key;

        /// <summary>
        /// appkey
        /// </summary>
        private string Appkey;

        /// <summary>
        /// 应答的参数
        /// </summary>
        protected Hashtable Parameters;

        /// <summary>
        /// debug信息
        /// </summary>
        private string DebugInfo;
        /// <summary>
        /// 原始内容
        /// </summary>
        protected string Content;

        private string Charset = "gb2312";

        protected HttpContext HttpContext;

        /// <summary>
        /// 初始化函数
        /// </summary>
        public virtual void Init()
        {
        }

        /// <summary>
        /// 获取页面提交的get和post参数
        /// 注意:.NetCore环境必须传入HttpContext实例，不能传Null，这个接口调试特别困难，千万别出错！
        /// </summary>
        /// <param name="httpContext"></param>
        public ResponseHandler(HttpContext httpContext)
        {
#if NET35 || NET40 || NET45 || NET461
            Parameters = new Hashtable();

            this.HttpContext = httpContext ?? HttpContext.Current;
            NameValueCollection collection;
            //post data
            if (this.HttpContext.Request.HttpMethod == "POST")
            {
                collection = this.HttpContext.Request.Form;
                foreach (string k in collection)
                {
                    string v = (string)collection[k];
                    this.SetParameter(k, v);
                }
            }
            //query string
            collection = this.HttpContext.Request.QueryString;
            foreach (string k in collection)
            {
                string v = (string)collection[k];
                this.SetParameter(k, v);
            }
            if (this.HttpContext.Request.InputStream.Length > 0)
            {
                XmlDocument xmlDoc = new Senparc.CO2NET.ExtensionEntities.XmlDocument_XxeFixed();
                xmlDoc.XmlResolver = null;
                xmlDoc.Load(this.HttpContext.Request.InputStream);
                XmlNode root = xmlDoc.SelectSingleNode("xml");
                XmlNodeList xnl = root.ChildNodes;

                foreach (XmlNode xnf in xnl)
                {
                    this.SetParameter(xnf.Name, xnf.InnerText);
                }
            }
#else
            Parameters = new Hashtable();

#if NETSTANDARD2_0
            HttpContext = httpContext ?? throw new WeixinException(".net standard 2.0 环境必须传入HttpContext的实例");
#else
            HttpContext = httpContext ?? CO2NET.SenparcDI.GetService<IHttpContextAccessor>()?.HttpContext;
#endif

            //post data
            if (HttpContext.Request.Method.ToUpper() == "POST" && HttpContext.Request.HasFormContentType)
            {
                foreach (var k in HttpContext.Request.Form)
                {
                    SetParameter(k.Key, k.Value[0]);
                }
            }
            //query string
            foreach (var k in HttpContext.Request.Query)
            {
                SetParameter(k.Key, k.Value[0]);
            }
            if (HttpContext.Request.ContentLength > 0)
            {
                var xmlDoc = new Senparc.CO2NET.ExtensionEntities.XmlDocument_XxeFixed();
                xmlDoc.XmlResolver = null;
                //xmlDoc.Load(HttpContext.Request.Body);
                using (var reader = new System.IO.StreamReader(HttpContext.Request.Body))
                {
                    xmlDoc.Load(reader);
                }
                var root = xmlDoc.SelectSingleNode("xml");

                foreach (XmlNode xnf in root.ChildNodes)
                {
                    SetParameter(xnf.Name, xnf.InnerText);
                }
            }
#endif
        }


        /// <summary>
        /// 获取密钥
        /// </summary>
        /// <returns></returns>
        public string GetKey()
        { return Key; }

        /// <summary>
        /// 设置密钥
        /// </summary>
        /// <param name="key"></param>
        public void SetKey(string key)
        {
            this.Key = key;
        }

        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public string GetParameter(string parameter)
        {
            string s = (string)Parameters[parameter];
            return (null == s) ? "" : s;
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
        /// 是否财付通签名,规则是:按参数名称a-z排序,遇到空值的参数不参加签名。return boolean
        /// </summary>
        /// <returns></returns>
        public virtual Boolean IsTenpaySign()
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
            string sign = EncryptHelper.GetMD5(sb.ToString(), GetCharset()).ToLower();
            this.SetDebugInfo(sb.ToString() + " &sign=" + sign);
            //debug信息
            return GetParameter("sign").ToLower().Equals(sign);
        }

        /// <summary>
        /// 获取debug信息
        /// </summary>
        /// <returns></returns>
        public string GetDebugInfo()
        { return DebugInfo; }

        /// <summary>
        /// 设置debug信息
        /// </summary>
        /// <param name="debugInfo"></param>
        protected void SetDebugInfo(String debugInfo)
        { this.DebugInfo = debugInfo; }

        protected virtual string GetCharset()
        {
#if NET35 || NET40 || NET45
            return this.HttpContext.Request.ContentEncoding.BodyName;
#else
            return Encoding.UTF8.WebName;
#endif
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
    }
}
