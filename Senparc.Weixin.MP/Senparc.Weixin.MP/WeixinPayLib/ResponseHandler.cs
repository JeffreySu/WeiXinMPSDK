using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Xml;

namespace Senparc.Weixin.MP.WeixinPayLib
{

    /** 
    '============================================================================
    'api说明：
    'getKey()/setKey(),获取/设置密钥
    'getParameter()/setParameter(),获取/设置参数值
    'getAllParameters(),获取所有参数
    'isTenpaySign(),是否正确的签名,true:是 false:否
    'isWXsign(),是否正确的签名,true:是 false:否
    ' * isWXsignfeedback判断微信维权签名
    ' *getDebugInfo(),获取debug信息
    '============================================================================
    */

    public class ResponseHandler
	{
		/// <summary>
        /// 密钥 
		/// </summary>
		private string key;

        /// <summary>
        /// appkey
        /// </summary>
        private string appkey;

        /// <summary>
        /// xmlMap
        /// </summary>
        private Hashtable xmlMap;

		/// <summary>
        /// 应答的参数
		/// </summary>
		protected Hashtable parameters;
		
		/// <summary>
        /// debug信息
		/// </summary>
		private string debugInfo;
        /// <summary>
        /// 原始内容
        /// </summary>
        protected string content;

        private string charset = "gb2312";

        /// <summary>
        /// 参与签名的参数列表
        /// </summary>
        private static string SignField = "appid,appkey,timestamp,openid,noncestr,issubscribe";

		protected HttpContext httpContext;

        /// <summary>
        /// 初始化函数
        /// </summary>
        public virtual void Init()
        {
        }

        /// <summary>
        /// 获取页面提交的get和post参数
        /// </summary>
        /// <param name="httpContext"></param>
        public ResponseHandler(HttpContext httpContext)
        {
            parameters = new Hashtable();
            xmlMap = new Hashtable();

            this.httpContext = httpContext;
            NameValueCollection collection;
            //post data
            if (this.httpContext.Request.HttpMethod == "POST")
            {
                collection = this.httpContext.Request.Form;
                foreach (string k in collection)
                {
                    string v = (string)collection[k];
                    this.SetParameter(k, v);
                }
            }
            //query string
            collection = this.httpContext.Request.QueryString;
            foreach (string k in collection)
            {
                string v = (string)collection[k];
                this.SetParameter(k, v);
            }
            if (this.httpContext.Request.InputStream.Length > 0)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(this.httpContext.Request.InputStream);
                XmlNode root = xmlDoc.SelectSingleNode("xml");
                XmlNodeList xnl = root.ChildNodes;

                foreach (XmlNode xnf in xnl)
                {
                    xmlMap.Add(xnf.Name, xnf.InnerText);
                }
            }
        }
    

		/// <summary>
        /// 获取密钥
		/// </summary>
		/// <returns></returns>
		public string GetKey() 
		{ return key;}

		/// <summary>
        /// 设置密钥
		/// </summary>
		/// <param name="key"></param>
		/// <param name="appkey"></param>
		public void SetKey(string key, string appkey) 
		{
            this.key = key;
            this.appkey = appkey;
        }

		/// <summary>
        /// 获取参数值
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns></returns>
		public string GetParameter(string parameter) 
		{
			string s = (string)parameters[parameter];
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
				if(parameters.Contains(parameter))
				{
					parameters.Remove(parameter);
				}
	
				parameters.Add(parameter,parameterValue);		
			}
		}

		/// <summary>
		/// 是否财付通签名,规则是:按参数名称a-z排序,遇到空值的参数不参加签名。return boolean
		/// </summary>
		/// <returns></returns>
        public virtual Boolean IsTenpaySign() 
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
            string sign = MD5Util.GetMD5(sb.ToString(), GetCharset()).ToLower();
            this.SetDebugInfo(sb.ToString() + " => sign:" + sign);
			//debug信息
			return GetParameter("sign").ToLower().Equals(sign); 
		}

        /// <summary>
        /// 判断微信签名
        /// </summary>
        /// <returns></returns>
        public virtual Boolean IsWXsign()
        {
            StringBuilder sb = new StringBuilder();
            Hashtable signMap = new Hashtable();

            foreach (string k in xmlMap.Keys)
            {
                if (k != "SignMethod" && k != "AppSignature")
                {
                    signMap.Add(k.ToLower(), xmlMap[k]);
                }
            }
            signMap.Add("appkey", this.appkey);


            ArrayList akeys = new ArrayList(signMap.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = (string)signMap[k];
                if (sb.Length == 0)
                {
                    sb.Append(k + "=" + v);
                }
                else
                {
                    sb.Append("&" + k + "=" + v);
                }
            }

            string sign = SHA1Util.GetSha1(sb.ToString()).ToString().ToLower();

            this.SetDebugInfo(sb.ToString() + " => SHA1 sign:" + sign);

            return sign.Equals(xmlMap["AppSignature"]);

        }

        /// <summary>
        /// 判断微信维权签名
        /// </summary>
        /// <returns></returns>
        public virtual Boolean IsWXsignfeedback()
        {
            StringBuilder sb = new StringBuilder();
            Hashtable signMap = new Hashtable();
       
            foreach (string k in xmlMap.Keys)
            {
                if (SignField.IndexOf(k.ToLower()) != -1)
                {
                    signMap.Add(k.ToLower(), xmlMap[k]);
                }
            }
            signMap.Add("appkey", this.appkey);
          

            ArrayList akeys = new ArrayList(signMap.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = (string)signMap[k];
                if ( sb.Length == 0 )
                {
                    sb.Append(k + "=" + v);
                }
                else
                {
                    sb.Append("&" + k + "=" + v);
                }
            }
            
            string sign = SHA1Util.GetSha1(sb.ToString()).ToString().ToLower();
            
            this.SetDebugInfo(sb.ToString() + " => SHA1 sign:" + sign);

            return sign.Equals( xmlMap["AppSignature"] );

        }
   
		/// <summary>
        /// 获取debug信息
		/// </summary>
		/// <returns></returns>
		public string GetDebugInfo() 
		{ return debugInfo;}
				
		/// <summary>
        /// 设置debug信息
		/// </summary>
		/// <param name="debugInfo"></param>
		protected void SetDebugInfo(String debugInfo)
		{ this.debugInfo = debugInfo;}

		protected virtual string GetCharset()
		{
			return this.httpContext.Request.ContentEncoding.BodyName;
			
		}

		
	}
}
