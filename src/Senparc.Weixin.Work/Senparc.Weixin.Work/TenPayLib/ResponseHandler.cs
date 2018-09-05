/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
 
    文件名：ResponseHandler.cs
    文件功能描述：微信支付 响应处理
    
    
    创建标识：Senparc - 20150722
    
    修改标识：Senparc - 20170623
    修改描述：使用 ASCII 字典排序
----------------------------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

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
    '============================================================================
    'Api说明：
    'GetKey()/setKey(),获取/设置密钥
    'GetParameter()/setParameter(),获取/设置参数值
    'GetAllParameters(),获取所有参数
    'IsTenpaySign(),是否正确的签名,true:是 false:否
    'IsWXsign(),是否正确的签名,true:是 false:否
    ' * IsWXsignfeedback判断微信维权签名
    ' *GetDebugInfo(),获取debug信息 
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
        /// xmlMap
        /// </summary>
        private Hashtable XmlMap;

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

#if NET35 || NET40 || NET45 || NET461
        //private string Charset = "gb2312";
#else
        private int Charset = 936;
#endif

        protected HttpContext HttpContext;

        /// <summary>
        /// 初始化函数
        /// </summary>
        public virtual void Init()
        {
        }

#if NET35 || NET40 || NET45 || NET461
		/// <summary>
		/// 获取页面提交的get和post参数
		/// </summary>
		/// <param name="httpContext"></param>
		public ResponseHandler(HttpContext httpContext)
		{
			Parameters = new Hashtable();
			XmlMap = new Hashtable();

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
				xmlDoc.Load(this.HttpContext.Request.InputStream);
				XmlNode root = xmlDoc.SelectSingleNode("xml");
				XmlNodeList xnl = root.ChildNodes;

				foreach (XmlNode xnf in xnl)
				{
					XmlMap.Add(xnf.Name, xnf.InnerText);
					this.SetParameter(xnf.Name, xnf.InnerText);
				}
			}
		}
#else
        /// <summary>
        /// 获取页面提交的get和post参数
        /// </summary>
        /// <param name="httpContext"></param>
        public ResponseHandler(HttpContext httpContext)
        {
            Parameters = new Hashtable();
            XmlMap = new Hashtable();

            this.HttpContext = httpContext ?? new DefaultHttpContext();
            IFormCollection collection;
            //post data
            if (this.HttpContext.Request.Method.ToUpper() == "POST" && this.HttpContext.Request.HasFormContentType)
            {
                collection = this.HttpContext.Request.Form;
                foreach (var k in collection)
                {
                    this.SetParameter(k.Key, k.Value[0]);
                }
            }
            //query string
            var coll = this.HttpContext.Request.Query;
            foreach (var k in coll)
            {
                this.SetParameter(k.Key, k.Value[0]);
            }
            if (this.HttpContext.Request.Body.Length > 0)
            {
                XmlDocument xmlDoc = new Senparc.CO2NET.ExtensionEntities.XmlDocument_XxeFixed();
                xmlDoc.Load(this.HttpContext.Request.Body);
                XmlNode root = xmlDoc.SelectSingleNode("xml");
                XmlNodeList xnl = root.ChildNodes;

                foreach (XmlNode xnf in xnl)
                {
                    XmlMap.Add(xnf.Name, xnf.InnerText);
                }
            }
        }
#endif

        /// <summary>
        /// 获取密钥
        /// </summary>
        /// <returns></returns>
        public string GetKey() 
		{ return Key;}

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
		public void SetParameter(string parameter,string parameterValue) 
		{
			if(parameter != null && parameter != "")
			{
				if(Parameters.Contains(parameter))
				{
					Parameters.Remove(parameter);
				}
	
				Parameters.Add(parameter,parameterValue);		
			}
		}

		/// <summary>
		/// 是否财付通签名,规则是:按参数名称a-z排序,遇到空值的参数不参加签名。return boolean
		/// </summary>
		/// <returns></returns>
        public virtual Boolean IsTenpaySign() 
		{
			StringBuilder sb = new StringBuilder();

			ArrayList akeys=new ArrayList(Parameters.Keys); 
			akeys.Sort(ASCIISort.Create());

			foreach(string k in akeys)
			{
				string v = (string)Parameters[k];
				if(null != v && "".CompareTo(v) != 0
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
		{ return DebugInfo;}
				
		/// <summary>
        /// 设置debug信息
		/// </summary>
		/// <param name="debugInfo"></param>
		protected void SetDebugInfo(String debugInfo)
		{ this.DebugInfo = debugInfo;}

		protected virtual string GetCharset()
		{
#if NET35 || NET40 || NET45 || NET461
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
