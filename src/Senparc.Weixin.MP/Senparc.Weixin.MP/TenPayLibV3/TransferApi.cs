using Senparc.Weixin.HttpUtility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Senparc.Weixin.MP.TenPayLibV3
{
    public class TransferApi
    {
        private static string GetNewBillNo(string mchId)
        {
            return string.Format("{0}{1}{2}", mchId, DateTime.Now.ToString("yyyyMMdd"), TenPayV3Util.BuildRandomStr(10));
        }

        /// <summary>
        /// 企业向个人付款
        /// </summary>
        /// <param name="appId">公众账号AppID</param>
        /// <param name="mchId">商户MchID</param>
        /// <param name="tenPayKey">支付密钥，微信商户平台(pay.weixin.qq.com)-->账户设置-->API安全-->密钥设置</param>
        /// <param name="tenPayCertPath">证书地址（硬盘地址，形如E://cert//apiclient_cert.p12）</param>
        /// <param name="openId">要转账的用户的OpenID</param>
        /// <param name="reUserName">转账用户的实名认证的姓名</param>
        /// <param name="iP">调用接口的机器Ip地址</param>
        /// <param name="amount">企业付款金额，单位为分</param>
        /// <param name="desc">企业付款操作说明信息。必填。</param>
        /// <param name="nonceStr"></param>
        /// <returns></returns>
        public static TransfersResult Transfer(string appId, string mchId, string tenPayKey, string tenPayCertPath,
          string openId, string reUserName, string iP, int amount, string desc)
        {
            string mchbillno = GetNewBillNo(mchId);

            string nonceStr = TenPayV3Util.GetNoncestr();


            RequestHandler packageReqHandler = new RequestHandler();
            //设置package订单参数
            packageReqHandler.SetParameter("mch_appid", appId);		  //公众账号ID
            packageReqHandler.SetParameter("mchid", mchId);		  //商户号
            packageReqHandler.SetParameter("nonce_str", nonceStr);           //随机字符串
            packageReqHandler.SetParameter("partner_trade_no", mchbillno);		//商家订单号
            packageReqHandler.SetParameter("openid", openId);
            //校验用户姓名选项
            //NO_CHECK：不校验真实姓名
            //FORCE_CHECK：强校验真实姓名（未实名认证的用户会校验失败，无法转账）
            //OPTION_CHECK：针对已实名认证的用户才校验真实姓名（未实名认证用户不校验，可以转账成功）
            packageReqHandler.SetParameter("check_name", "OPTION_CHECK");
            packageReqHandler.SetParameter("re_user_name", reUserName);   //收款用户真实姓名。
            packageReqHandler.SetParameter("amount", amount.ToString());       //金额,以分为单位
            packageReqHandler.SetParameter("desc", desc);
            packageReqHandler.SetParameter("spbill_create_ip", iP);   //调用接口的机器Ip地址
            string sign = packageReqHandler.CreateMd5Sign("key", tenPayKey);
            packageReqHandler.SetParameter("sign", sign);	      //签名
   
            //发红包需要post的数据
            string data = packageReqHandler.ParseXML();

            //发红包接口地址
            string url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers";
            //本地或者服务器的证书位置（证书在微信支付申请成功发来的通知邮件中）
            string cert = tenPayCertPath;
            //私钥（在安装证书时设置）
            string password = mchId;

            //调用证书
            X509Certificate2 cer = new X509Certificate2(cert, password, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            //X509Certificate cer = new X509Certificate(cert, password);

            #region 发起post请求
            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(url);
            webrequest.ClientCertificates.Add(cer);
            webrequest.Method = "post";


            byte[] postdatabyte = Encoding.UTF8.GetBytes(data);
            webrequest.ContentLength = postdatabyte.Length;
            Stream stream = webrequest.GetRequestStream();
            stream.Write(postdatabyte, 0, postdatabyte.Length);
            stream.Close();

            HttpWebResponse httpWebResponse = (HttpWebResponse)webrequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();
            #endregion

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(responseContent);

            TransfersResult normalReturn = new TransfersResult(responseContent)
            {
                err_code = "",
                err_code_des = ""
            };

            if (doc.SelectSingleNode("/xml/return_code") != null)
            {
                normalReturn.return_code = doc.SelectSingleNode("/xml/return_code").InnerText.ToUpper(); //(doc.SelectSingleNode("/xml/return_code").InnerText.ToUpper() == "SUCCESS");
            }
            if (doc.SelectSingleNode("/xml/return_msg") != null)
            {
                normalReturn.return_msg = doc.SelectSingleNode("/xml/return_msg").InnerText;
            }

            if (normalReturn.IsReturnCodeSuccess())
            {
                if (doc.SelectSingleNode("/xml/result_code") != null)
                {
                    normalReturn.result_code = doc.SelectSingleNode("/xml/result_code").InnerText; //(doc.SelectSingleNode("/xml/result_code").InnerText.ToUpper() == "SUCCESS");
                }

                if (normalReturn.IsResultCodeSuccess())
                {
                    if (doc.SelectSingleNode("/xml/mch_appid") != null)
                    {
                        normalReturn.mch_appid = doc.SelectSingleNode("/xml/mch_appid").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/mchid") != null)
                    {
                        normalReturn.mchid = doc.SelectSingleNode("/xml/mchid").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/device_info") != null)
                    {
                        normalReturn.device_info = doc.SelectSingleNode("/xml/device_info").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/nonce_str") != null)
                    {
                        normalReturn.nonce_str = doc.SelectSingleNode("/xml/nonce_str").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/partner_trade_no") != null)
                    {
                        normalReturn.partner_trade_no = doc.SelectSingleNode("/xml/partner_trade_no").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/payment_no") != null)
                    {
                        normalReturn.payment_no = doc.SelectSingleNode("/xml/payment_no").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/payment_time") != null)
                    {
                        normalReturn.payment_time = doc.SelectSingleNode("/xml/payment_time").InnerText;
                    }
                }
                else
                {
                    if (doc.SelectSingleNode("/xml/err_code") != null)
                    {
                        normalReturn.err_code = doc.SelectSingleNode("/xml/err_code").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/err_code_des") != null)
                    {
                        normalReturn.err_code_des = doc.SelectSingleNode("/xml/err_code_des").InnerText;
                    }
                }
            }

            return normalReturn;
        }

        /// <summary>
        /// 用于商户的企业付款操作进行结果查询，返回付款操作详细结果。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static string GetTransferInfo(string data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/mmpaymkttransfers/gettransferinfo";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return RequestUtility.HttpPost(urlFormat, null, ms, timeOut: timeOut);
        }


        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            if (errors == SslPolicyErrors.None)
                return true;
            return false;
        }

    }
}
