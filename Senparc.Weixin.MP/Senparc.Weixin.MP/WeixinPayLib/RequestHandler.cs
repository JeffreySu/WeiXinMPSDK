using System;
using System.Collections;
using System.Text;
using System.Web;
using System.Xml;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace Senparc.Weixin.MP.WeixinPayLib
{
    /**
    '签名工具类
     ============================================================================/// <summary>
    'api说明：
    'init();
    '初始化函数，默认给一些参数赋值。
    'setKey(key_)'设置商户密钥
    'createMd5Sign(signParams);字典生成Md5签名
    'genPackage(packageParams);获取package包
    'createSHA1Sign(signParams);创建签名SHA1
    'parseXML();输出xml
    'getDebugInfo(),获取debug信息
     * 
     * ============================================================================
     */
    public class RequestHandler
	{
    
        public RequestHandler(HttpContext httpContext)
        {
            parameters = new Hashtable();

            this.httpContext = httpContext;
           
        }
        /// <summary>
        /// 密钥
        /// </summary>
        private string key;

        protected HttpContext httpContext;

		/// <summary>
        /// 请求的参数
		/// </summary>
		protected Hashtable parameters;
		
		/// <summary>
        /// debug信息
		/// </summary>
		private string debugInfo;
		
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
			return debugInfo;
		}
		/// <summary>
        /// 获取密钥
		/// </summary>
		/// <returns></returns>
		public string GetKey() 
		{
			return key;
		}
        /// <summary>
        /// 设置密钥
        /// </summary>
        /// <param name="key"></param>
		public void SetKey(string key) 
		{
			this.key = key;
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
                if (parameters.Contains(parameter))
                {
                    parameters.Remove(parameter);
                }

                parameters.Add(parameter, parameterValue);
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
            ArrayList akeys=new ArrayList(parameters.Keys); 
            akeys.Sort();
            foreach(string k in akeys)
            {
                string v = (string)parameters[k];
                if(null != v && "key".CompareTo(k) != 0) 
                {
                    sb.Append(k + "=" + WeixinPayUtil.UrlEncode(v, getCharset()) + "&");
                }
            }

            //去掉最后一个&
            if(sb.Length > 0)
            {
                sb.Remove(sb.Length-1, 1);
            }

         
           return sb.ToString();
           
        }
       
		/// <summary>
        /// 创建md5摘要,规则是:按参数名称a-z排序,遇到空值的参数不参加签名
		/// </summary>
        protected virtual void  CreateSign() 
        {
            StringBuilder sb = new StringBuilder();

            ArrayList akeys=new ArrayList(parameters.Keys); 
            akeys.Sort();

            foreach(string k in akeys)
            {
                string v = (string)parameters[k];
                if(null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0) 
                {
                    sb.Append(k + "=" + v + "&");
                }
            }

            sb.Append("key=" + this.GetKey());
            string sign = MD5Util.GetMD5(sb.ToString(), getCharset()).ToUpper();

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
            ArrayList akeys=new ArrayList(parameters.Keys); 
            akeys.Sort();

            foreach(string k in akeys)
            {
                string v = (string)parameters[k];
                if(null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "".CompareTo(v) != 0) 
                {
                    sb.Append(k + "=" + v + "&");
                }
            }
            string sign = MD5Util.GetMD5(sb.ToString(), getCharset()).ToLower();

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
            ArrayList akeys = new ArrayList(parameters.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = (string)parameters[k];
              if (null != v && "".CompareTo(v) != 0
                     && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    if(sb.Length==0)
                    {
                    sb.Append(k + "=" + v);
                    }
                    else{
                     sb.Append("&" + k + "=" + v);
                    }
                }
            }
            string paySign = SHA1Util.GetSha1(sb.ToString()).ToString().ToLower();
       
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
            foreach (string k in parameters.Keys)
            {
                string v = (string)parameters[k];
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
			this.debugInfo = debugInfo;
		}

		public Hashtable getAllParameters()
		{
			return this.parameters;
		}

         protected virtual string getCharset()
      {
          return this.httpContext.Request.ContentEncoding.BodyName;
      } 
    }
}
