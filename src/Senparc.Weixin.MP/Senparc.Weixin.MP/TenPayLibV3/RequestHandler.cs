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
    文件功能描述：微信支付V3 请求处理
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Yu XiaoChou - 20160107
    修改描述：增加一个不需要HttpContext的初始化方法，避免使用这个类的时候，还要必须从页面初始化一个对象过来，可以在基类里直接使用这个类，相应的，将GetCharset方法中，不存在HttpContext时，默认使用UTF-8

    修改标识：Senparc - 20161112
    修改描述：为ParseXML()方法添加v==null的判断

    修改标识：Senparc - 20170115
    修改描述：v14.3.120 添加SetParameterWhenNotNull()方法

    修改标识：Senparc - 20170319
    修改描述：v14.3.134 修改RequestHandler构造函数

    修改标识：Senparc - 20180331
    修改描述：v14.10.12 更新过期方法

    修改标识：Senparc - 20180825
    修改描述：v15.2.4 微信支付 RequestHandler 增加 HMAC-SHA256 加密方式

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
using Senparc.Weixin.Helpers;


namespace Senparc.Weixin.MP.TenPayLibV3
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
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public class RequestHandler
    {

        public RequestHandler()
            : this(null)
        {
        }


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
        /// 当参数不为null或空字符串时，设置参数值
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="parameterValue"></param>
        public void SetParameterWhenNotNull(string parameter, string parameterValue)
        {
            if (!string.IsNullOrEmpty(parameterValue))
            {
                SetParameter(parameter, parameterValue);
            }
        }

        /// <summary>
        /// 创建md5摘要,规则是:按参数名称a-z排序,遇到空值的参数不参加签名
        /// </summary>
        /// <param name="key">参数名</param>
        /// <param name="value">参数值</param>
        /// key和value通常用于填充最后一组参数
        /// <returns></returns>
        public virtual string CreateMd5Sign(string key, string value)
        {
            StringBuilder sb = new StringBuilder();

            ArrayList akeys = new ArrayList(Parameters.Keys);
            akeys.Sort(ASCIISort.Create());

            foreach (string k in akeys)
            {
                string v = (string)Parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0
                    //&& "sign_type".CompareTo(k) != 0
                    && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }

            sb.Append(key + "=" + value);

            //string sign = EncryptHelper.GetMD5(sb.ToString(), GetCharset()).ToUpper();

            //编码强制使用UTF8：https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=4_1
            string sign = EncryptHelper.GetMD5(sb.ToString(), "UTF-8").ToUpper();

            return sign;
        }

        /// <summary>
        /// 创建sha256摘要,规则是:按参数名称a-z排序,遇到空值的参数不参加签名
        /// </summary>
        /// <param name="key">参数名</param>
        /// <param name="value">参数值</param>
        /// key和value通常用于填充最后一组参数
        /// <returns></returns>
        public virtual string CreateSha256Sign(string key, string value)
        {
            var sb = new StringBuilder();

            var akeys = new ArrayList(Parameters.Keys);
            akeys.Sort(ASCIISort.Create());

            foreach (string k in akeys)
            {
                string v = (string)Parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0
                    //&& "sign_type".CompareTo(k) != 0
                    && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }

            sb.Append(key + "=" + value);

            //string sign = EncryptHelper.GetMD5(sb.ToString(), GetCharset()).ToUpper();

            //编码强制使用UTF8：https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=4_1

            // 现支持HMAC-SHA256签名方式（EncryptHelper.GetHmacSha256方法留待实现）
            // https://pay.weixin.qq.com/wiki/doc/api/micropay.php?chapter=4_3
            string sign = EncryptHelper.GetHmacSha256(sb.ToString(), value).ToUpper();

            return sign;
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
                if (v != null && Regex.IsMatch(v, @"^[0-9.]$"))
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
#if NET35 || NET40 || NET45 || NET461
            if (this.HttpContext == null)//构造函数已经排除了这种可能，暂时保留
            {
                return Encoding.UTF8.BodyName;
            }

            return this.HttpContext.Request.ContentEncoding.BodyName;
#else
            if (this.HttpContext == null)//构造函数已经排除了这种可能，暂时保留
            {
                return Encoding.UTF8.WebName;
            }

            return this.HttpContext.Request.Headers["charset"];
#endif

        }
    }
}
