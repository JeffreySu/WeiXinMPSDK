/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
 
    文件名：RequestHandler.cs
    文件功能描述：微信支付 请求处理
    
    
    创建标识：Senparc - 20150722
    
    修改标识：Senparc - 20170623
    修改描述：使用 ASCII 字典排序
----------------------------------------------------------------*/

using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using Senparc.Weixin.Work.Helpers;
using Senparc.CO2NET.Helpers;

#if NET35 || NET40 || NET45 || NET461
using System.Web;
#else
using Microsoft.AspNetCore.Http;
#endif



namespace Senparc.Weixin.Work.TenPayLib
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
        {
            Parameters = new Hashtable();
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
                    && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }

            sb.Append(key + "=" + value);
            string sign = EncryptHelper.GetMD5(sb.ToString(), GetCharset()).ToUpper();

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
#if NET35 || NET40 || NET45 || NET461
            return this.HttpContext.Request.ContentEncoding.BodyName;
#else
            return Encoding.UTF8.WebName;
#endif
        }
    }
}
