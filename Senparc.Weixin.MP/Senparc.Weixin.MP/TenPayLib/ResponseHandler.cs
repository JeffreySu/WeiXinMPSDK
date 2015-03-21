/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
 
    �ļ�����ResponseHandler.cs
    �ļ�����������΢��֧�� ��Ӧ����
    
    
    ������ʶ��Senparc - 20150211
    
    �޸ı�ʶ��Senparc - 20150303
    �޸�����������ӿ�
----------------------------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Xml;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.TenPayLib
{

    /** 
    '============================================================================
    'Api˵����
    'GetKey()/setKey(),��ȡ/������Կ
    'GetParameter()/setParameter(),��ȡ/���ò���ֵ
    'GetAllParameters(),��ȡ���в���
    'IsTenpaySign(),�Ƿ���ȷ��ǩ��,true:�� false:��
    'IsWXsign(),�Ƿ���ȷ��ǩ��,true:�� false:��
    ' * IsWXsignfeedback�ж�΢��άȨǩ��
    ' *GetDebugInfo(),��ȡdebug��Ϣ 
    '============================================================================
    */

    public class ResponseHandler
	{
		/// <summary>
        /// ��Կ 
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
        /// Ӧ��Ĳ���
		/// </summary>
		protected Hashtable Parameters;
		
		/// <summary>
        /// debug��Ϣ
		/// </summary>
		private string DebugInfo;
        /// <summary>
        /// ԭʼ����
        /// </summary>
        protected string Content;

        private string Charset = "gb2312";

        /// <summary>
        /// ����ǩ���Ĳ����б�
        /// </summary>
        private static string SignField = "appid,appkey,timestamp,openid,noncestr,issubscribe";

		protected HttpContext HttpContext;

        /// <summary>
        /// ��ʼ������
        /// </summary>
        public virtual void Init()
        {
        }

        /// <summary>
        /// ��ȡҳ���ύ��get��post����
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
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(this.HttpContext.Request.InputStream);
                XmlNode root = xmlDoc.SelectSingleNode("xml");
                XmlNodeList xnl = root.ChildNodes;

                foreach (XmlNode xnf in xnl)
                {
                    XmlMap.Add(xnf.Name, xnf.InnerText);
                }
            }
        }
    

		/// <summary>
        /// ��ȡ��Կ
		/// </summary>
		/// <returns></returns>
		public string GetKey() 
		{ return Key;}

		/// <summary>
        /// ������Կ
		/// </summary>
		/// <param name="key"></param>
		/// <param name="appkey"></param>
		public void SetKey(string key, string appkey) 
		{
            this.Key = key;
            this.Appkey = appkey;
        }

		/// <summary>
        /// ��ȡ����ֵ
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns></returns>
		public string GetParameter(string parameter) 
		{
			string s = (string)Parameters[parameter];
			return (null == s) ? "" : s;
		}

		/// <summary>
        /// ���ò���ֵ
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
		/// �Ƿ�Ƹ�ͨǩ��,������:����������a-z����,������ֵ�Ĳ������μ�ǩ����return boolean
		/// </summary>
		/// <returns></returns>
        public virtual Boolean IsTenpaySign() 
		{
			StringBuilder sb = new StringBuilder();

			ArrayList akeys=new ArrayList(Parameters.Keys); 
			akeys.Sort();

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
            string sign = MD5UtilHelper.GetMD5(sb.ToString(), GetCharset()).ToLower();
            this.SetDebugInfo(sb.ToString() + " &sign=" + sign);
			//debug��Ϣ
			return GetParameter("sign").ToLower().Equals(sign); 
		}

        /// <summary>
        /// �ж�΢��ǩ��
        /// </summary>
        /// <returns></returns>
        public virtual Boolean IsWXsign()
        {
            StringBuilder sb = new StringBuilder();
            Hashtable signMap = new Hashtable();

            foreach (string k in XmlMap.Keys)
            {
                if (k != "SignMethod" && k != "AppSignature")
                {
                    signMap.Add(k.ToLower(), XmlMap[k]);
                }
            }
            signMap.Add("appkey", this.Appkey);


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

            string sign = SHA1UtilHelper.GetSha1(sb.ToString()).ToString().ToLower();

            this.SetDebugInfo(sb.ToString() + " => SHA1 sign:" + sign);

            return sign.Equals(XmlMap["AppSignature"]);

        }

        /// <summary>
        /// �ж�΢��άȨǩ��
        /// </summary>
        /// <returns></returns>
        public virtual Boolean IsWXsignfeedback()
        {
            StringBuilder sb = new StringBuilder();
            Hashtable signMap = new Hashtable();
       
            foreach (string k in XmlMap.Keys)
            {
                if (SignField.IndexOf(k.ToLower()) != -1)
                {
                    signMap.Add(k.ToLower(), XmlMap[k]);
                }
            }
            signMap.Add("appkey", this.Appkey);
          

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

            string sign = SHA1UtilHelper.GetSha1(sb.ToString()).ToString().ToLower();
            
            this.SetDebugInfo(sb.ToString() + " => SHA1 sign:" + sign);

            return sign.Equals( XmlMap["AppSignature"] );

        }
   
		/// <summary>
        /// ��ȡdebug��Ϣ
		/// </summary>
		/// <returns></returns>
		public string GetDebugInfo() 
		{ return DebugInfo;}
				
		/// <summary>
        /// ����debug��Ϣ
		/// </summary>
		/// <param name="debugInfo"></param>
		protected void SetDebugInfo(String debugInfo)
		{ this.DebugInfo = debugInfo;}

		protected virtual string GetCharset()
		{
			return this.HttpContext.Request.ContentEncoding.BodyName;
			
		}

		
	}
}
