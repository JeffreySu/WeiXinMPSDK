/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
 
    文件名：RefundResponseHandler.cs
    文件功能描述：微信支付退款 响应处理
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.TenPayLib
{
    /// <summary>
    /// ResponseHandler 的摘要说明。
    /// </summary>
    public class RefundResponseHandler
    {
        /// <summary>
        /// 密钥
        /// </summary>
        private string Key;

        /// <summary>
        /// 应答的参数
        /// </summary>
        protected Hashtable Parameters;

        /// <summary>
        /// debug信息
        /// </summary>
        private string DebugInfo;

        protected HttpContext HttpContext;

        /// <summary>
        /// 获取服务器通知数据方式，进行参数获取
        /// </summary>
        /// <param name="httpContext"></param>
        public RefundResponseHandler(HttpContext httpContext)
        {
            Parameters = new Hashtable();

            this.HttpContext = httpContext ?? HttpContext.Current;
            NameValueCollection collection;
            if (this.HttpContext.Request.HttpMethod == "POST")
            {
                collection = this.HttpContext.Request.Form;
            }
            else
            {
                collection = this.HttpContext.Request.QueryString;
            }

            foreach (string k in collection)
            {
                string v = (string)collection[k];
                this.SetParameter(k, v);
            }
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
        { this.Key = key; }

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
        /// 是否财付通签名,规则是:按参数名称a-z排序,遇到空值的参数不参加签名 @return boolean 
        /// </summary>
        /// <returns></returns>
        public virtual Boolean IsTenpaySign()
        {
            StringBuilder sb = new StringBuilder();

            ArrayList akeys = new ArrayList(Parameters.Keys);
            akeys.Sort();

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
            string sign = MD5UtilHelper.GetMD5(sb.ToString(), getCharset()).ToLower();

            //debug信息
            this.SetDebugInfo(sb.ToString() + " => sign:" + sign);
            return GetParameter("sign").ToLower().Equals(sign);
        }

        /// <summary>
        /// 显示处理结果。
        /// @param show_url 显示处</summary>
        /// @throws IOException 
        /// <param name="show_url"></param>
        public void DoShow(string show_url)
        {
            string strHtml = "<html><head>\r\n" +
                "<meta name=\"TENCENT_ONLINE_PAYMENT\" content=\"China TENCENT\">\r\n" +
                "<script language=\"javascript\">\r\n" +
                "window.location.href='" + show_url + "';\r\n" +
                "</script>\r\n" +
                "</head><body></body></html>";

            this.HttpContext.Response.Write(strHtml);

            this.HttpContext.Response.End();
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
        protected void SetDebugInfo(string debugInfo)
        { this.DebugInfo = debugInfo; }

        protected virtual string getCharset()
        {
            return this.HttpContext.Request.ContentEncoding.BodyName;

        }

        /// <summary>
        /// 是否财付通签名,规则是:按参数名称a-z排序,遇到空值的参数不参加签名 @return boolean
        /// </summary>
        /// <param name="aKeys"></param>
        /// <returns></returns>
        public virtual Boolean IsTenpaySign(ArrayList aKeys)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string k in aKeys)
            {
                string v = (string)Parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }

            sb.Append("key=" + this.GetKey());
            string sign = MD5UtilHelper.GetMD5(sb.ToString(), getCharset()).ToLower();

            //debug信息
            this.SetDebugInfo(sb.ToString() + " => sign:" + sign);
            return GetParameter("sign").ToLower().Equals(sign);
        }
    }
}
