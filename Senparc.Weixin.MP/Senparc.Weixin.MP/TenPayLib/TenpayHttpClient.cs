using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;


using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

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
    public class TenpayHttpClient
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

        public TenpayHttpClient()
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
            StreamReader sr = null;
            HttpWebResponse wr = null;

            HttpWebRequest hp = null;
            try
            {
                string postData = null;
                if (this.Method.ToUpper() == "POST")
                {
                    string[] sArray = System.Text.RegularExpressions.Regex.Split(this.ReqContent, "\\?");

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


                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
                if (this.CertFile != "")
                {
                    hp.ClientCertificates.Add(new X509Certificate2(this.CertFile, this.CertPasswd));
                }
                hp.Timeout = this.TimeOut * 1000;

                System.Text.Encoding encoding = System.Text.Encoding.GetEncoding(this.Charset);
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

            return true;
        }
    }
}
