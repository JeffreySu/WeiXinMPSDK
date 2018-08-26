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
 
    文件名：TenPayHttpClient.cs
    文件功能描述：微信支付http、https通信类
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

/**
 * http、https通信类
 * ============================================================================
 * api说明：
 * setReqContent($reqContent),设置请求内容，无论post和get，都用get方式提供
 * getResContent(), 获取应答内容
 * setMethod($method),设置请求方法,post或者get
 * getErrInfo(),获取错误信息
 * setCertInfo($certFile, $certPasswd, $certType="PEM"),设置证书，双向https时需要使用
 * setCaInfo($caFile), 设置CA，格式未pem，不设置则不检查
 * setTimeOut($timeOut)， 设置超时时间，单位秒
 * getResponseCode(), 取返回的http状态码
 * call(),真正调用接口
 * 
 * ============================================================================
 *
 */

namespace Senparc.Weixin.MP.TenPayLib
{
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V2 中的对应方法")]
    public class TenPayHttpClient
    {
        /// <summary>
        /// 请求内容，无论post和get，都用get方式提供
        /// </summary>
        private string ReqContent;

        /// <summary>
        /// 应答内容
        /// </summary>
        private string ResContent;

        /// <summary>
        /// 请求方法
        /// </summary>
        private string Method;

        /// <summary>
        /// 错误信息
        /// </summary>
        private string ErrInfo;

        /// <summary>
        /// 证书文件
        /// </summary>

        private string CertFile;

        /// <summary>
        /// 证书密码 
        /// </summary>
        private string CertPasswd;

        /// <summary>
        /// ca证书文件 
        /// </summary>
        private string CaFile;

        /// <summary>
        /// 超时时间,以秒为单位 
        /// </summary>
        private int TimeOut;

        /// <summary>
        /// http应答编码
        /// </summary>

        private int ResponseCode;

        /// <summary>
        /// 字符编码
        /// </summary>
        private string Charset;

        public TenPayHttpClient()
        {
            this.CaFile = "";
            this.CertFile = "";
            this.CertPasswd = "";

            this.ReqContent = "";
            this.ResContent = "";
            this.Method = "POST";
            this.ErrInfo = "";
            this.TimeOut = 1 * 60;//5分钟

            this.ResponseCode = 0;
            this.Charset = "gb2312";

        }

        /// <summary>
        /// 设置请求内容
        /// </summary>
        /// <param name="reqContent"></param>
        public void SetReqContent(string reqContent)
        {
            this.ReqContent = reqContent;
        }

        /// <summary>
        /// 获取结果内容
        /// </summary>
        /// <returns></returns>
        public string GetResContent()
        {
            return this.ResContent;
        }

        /// <summary>
        /// 设置请求方法post或者get
        /// </summary>
        /// <param name="method"></param>

        public void SetMethod(string method)
        {
            this.Method = method;
        }

        /// <summary>
        /// 获取错误信息
        /// </summary>
        /// <returns></returns>
        public string GetErrInfo()
        {
            return this.ErrInfo;
        }

        /// <summary>
        /// 设置证书信息
        /// </summary>
        /// <param name="certFile"></param>
        /// <param name="certPasswd"></param>
        public void SetCertInfo(string certFile, string certPasswd)
        {
            this.CertFile = certFile;
            this.CertPasswd = certPasswd;
        }

        /// <summary>
        /// 设置ca
        /// </summary>
        /// <param name="caFile"></param>
        public void SetCaInfo(string caFile)
        {
            this.CaFile = caFile;
        }

        /// <summary>
        /// 设置超时时间,以秒为单位
        /// </summary>
        /// <param name="timeOut"></param>
        public void SetTimeOut(int timeOut)
        {
            this.TimeOut = timeOut;
        }


        /// <summary>
        /// 获取http状态码
        /// </summary>
        /// <returns></returns>
        public int GetResponseCode()
        {
            return this.ResponseCode;
        }

        public void SetCharset(string charset)
        {
            this.Charset = charset;
        }

        /// <summary>
        /// 验证服务器证书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        /// <summary>
        /// 执行http调用
        /// </summary>
        /// <returns></returns>
        public bool Call()
        {
#if NET35 || NET40 || NET45 || NET461
                        StreamReader sr = null;
            HttpWebResponse wr = null;

            HttpWebRequest hp = null;
            try
            {
                string postData = null;
                if (this.Method.ToUpper() == "POST")
                {
                    string[] sArray = Regex.Split(this.ReqContent, "\\?");

                    hp = (HttpWebRequest)WebRequest.Create(sArray[0]);

                    if (sArray.Length >= 2)
                    {
                        postData = sArray[1];
                    }

                }
                else
                {
                    hp = (HttpWebRequest)WebRequest.Create(this.ReqContent);
                }


                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                if (this.CertFile != "")
                {
                    hp.ClientCertificates.Add(new X509Certificate2(this.CertFile, this.CertPasswd));
                }
                hp.Timeout = this.TimeOut * 1000;

                Encoding encoding = Encoding.GetEncoding(this.Charset);
                if (postData != null)
                {
                    byte[] data = encoding.GetBytes(postData);

                    hp.Method = "POST";

                    hp.ContentType = "application/x-www-form-urlencoded";

                    hp.ContentLength = data.Length;

                    Stream ws = hp.GetRequestStream();

                    // 发送数据

                    ws.Write(data, 0, data.Length);
                    ws.Close();


                }


                wr = (HttpWebResponse)hp.GetResponse();
                sr = new StreamReader(wr.GetResponseStream(), encoding);



                this.ResContent = sr.ReadToEnd();
                sr.Close();
                wr.Close();
            }
            catch (Exception exp)
            {
                this.ErrInfo += exp.Message;
                if (wr != null)
                {
                    this.ResponseCode = Convert.ToInt32(wr.StatusCode);
                }

                return false;
            }

            this.ResponseCode = Convert.ToInt32(wr.StatusCode);

#else

#endif
            return true;
        }
    }
}
