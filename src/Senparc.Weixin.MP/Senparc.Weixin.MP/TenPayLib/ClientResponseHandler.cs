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

using System;
using System.Collections;
using System.Text;
using System.Xml;
using Senparc.CO2NET.Helpers;
using Senparc.Weixin.MP.Helpers;

/**
 * 后台应答类
 * ============================================================================
 * api说明：
 * getKey()/setKey(),获取/设置密钥
 * getContent() / setContent(), 获取/设置原始内容
 * getParameter()/setParameter(),获取/设置参数值
 * getAllParameters(),获取所有参数
 * isTenpaySign(),是否财付通签名,true:是 false:否
 * getDebugInfo(),获取debug信息
 * 
 * ============================================================================
 *
 */

namespace Senparc.Weixin.MP.TenPayLib
{
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V2 中的对应方法")]
    public class ClientResponseHandler
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

        /// <summary>
        /// 原始内容
        /// </summary>
        protected string Content;

        private string Charset = "gb2312";


        /// <summary>
        /// 获取服务器通知数据方式，进行参数获取
        /// </summary>
        public ClientResponseHandler()
        {
            Parameters = new Hashtable();
        }

        public string GetContent()
        {
            return this.Content;
        }

        public virtual void SetContent(string content)
        {
            this.Content = content;
            XmlDocument xmlDoc = new Senparc.CO2NET.ExtensionEntities.XmlDocument_XxeFixed();
            xmlDoc.LoadXml(content);
            XmlNode root = xmlDoc.SelectSingleNode("root");
            XmlNodeList xnl = root.ChildNodes;

            foreach (XmlNode xnf in xnl)
            {
                this.SetParameter(xnf.Name, xnf.InnerXml);
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
            string sign = EncryptHelper.GetMD5(sb.ToString(), getCharset()).ToLower();

            //debug信息
            this.SetDebugInfo(sb.ToString() + " => sign:" + sign);
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
        protected void SetDebugInfo(string debugInfo)
        { this.DebugInfo = debugInfo; }

        protected virtual string getCharset()
        {
            return this.Charset;
        }

        public void SetCharset(string charset)
        {
            this.Charset = charset;
        }

        /// <summary>
        /// 是否财付通签名,规则是:按参数名称a-z排序,遇到空值的参数不参加签名  @return boolean
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
            string sign = EncryptHelper.GetMD5(sb.ToString(), getCharset()).ToLower();

            //debug信息
            this.SetDebugInfo(sb.ToString() + " => sign:" + sign);
            return GetParameter("sign").ToLower().Equals(sign);
        }
    }
}
