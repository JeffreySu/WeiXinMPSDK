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
  
    文件名：RedPackApi.Via.cs
    文件功能描述：RedPackApi.cs 的部分类，用于存放服务商的接口

    创建标识：Senparc - 20170925

    修改标识：Senparc - 20170925
    修改描述：添加新规定提示：红包超过2000元必须提供scene_id参数：
    https://pay.weixin.qq.com/wiki/doc/api/tools/cash_coupon.php?chapter=13_4&index=3

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;

#if !NET35 && !NET40 && !NET45
using System.Net.Http;
# endif

namespace Senparc.Weixin.MP.TenPayLibV3
{
    public partial class RedPackApi
    {
        #region 同步方法


        #region 服务商

        /// <summary>
        /// 普通红包发送(服务商）
        /// </summary>
        /// <param name="wxAppId">公众账号appid</param>
        /// <param name="msgAppId">触达用户appid</param>
        /// <param name="mchId">商户号</param>
        /// <param name="subMchId">子商户号</param>
        /// <param name="consumeMchId">资金授权商户号，服务商替特约商户发放时使用（非必填），String(32)。示例：1222000096</param>
        /// <param name="tenPayKey">支付密钥，微信商户平台(pay.weixin.qq.com)-->账户设置-->API安全-->密钥设置</param>
        /// <param name="tenPayCertPath">证书地址（硬盘物理地址，形如E:\\cert\\apiclient_cert.p12）</param>
        /// <param name="openId">要发红包的用户的OpenID</param>
        /// <param name="senderName">商户名称</param>
        /// <param name="iP">发送红包的服务器地址</param>
        /// <param name="redPackAmount">付款金额，单位分。红包金额大于200时，请求参数scene必传。</param>
        /// <param name="wishingWord">红包发放总人数</param>
        /// <param name="actionName">红包祝福语（请注意活动名称长度，官方文档提示为32个字符，实际限制不足32个字符）</param>
        /// <param name="remark">活动描述，用于低版本微信显示</param>
        /// <param name="nonceStr">将nonceStr随机字符串返回，开发者可以存到数据库用于校验</param>
        /// <param name="paySign">将支付签名返回，开发者可以存到数据库用于校验</param>
        /// <param name="mchBillNo">商户订单号，新的订单号可以从RedPackApi.GetNewBillNo(mchId)方法获得，如果传入null，则系统自动生成</param>
        /// <param name="scene">场景id（非必填），红包金额大于200时，请求参数scene必传</param>
        /// <param name="riskInfo">活动信息（非必填）,String(128)posttime:用户操作的时间戳。
        /// <para>示例：posttime%3d123123412%26clientversion%3d234134%26mobile%3d122344545%26deviceid%3dIOS</para>
        /// <para>mobile:业务系统账号的手机号，国家代码-手机号。不需要+号</para>
        /// <para>deviceid :mac 地址或者设备唯一标识</para>
        /// <para>clientversion :用户操作的客户端版本</para>
        /// <para>把值为非空的信息用key = value进行拼接，再进行urlencode</para>
        /// <para>urlencode(posttime= xx & mobile = xx & deviceid = xx)</para>
        /// </param>
        /// <returns></returns>
        public static NormalRedPackResult SendNormalRedPackViaProvider(string wxAppId, string msgAppId, string mchId, string subMchId, string consumeMchId, string tenPayKey, string tenPayCertPath,
            string openId, string senderName,
            string iP, int redPackAmount, string wishingWord, string actionName, string remark,
            out string nonceStr, out string paySign,
            string mchBillNo, RedPack_Scene? scene = null, string riskInfo = null)
        {
            mchBillNo = mchBillNo ?? GetNewBillNo(mchId);

            nonceStr = TenPayV3Util.GetNoncestr();
            //RequestHandler packageReqHandler = new RequestHandler(null);

            //string accessToken = AccessTokenContainer.GetAccessToken(ConstantClass.AppID);
            //UserInfoJson userInforResult = UserApi.Info(accessToken, openID);

            RequestHandler packageReqHandler = new RequestHandler();
            //设置package订单参数
            packageReqHandler.SetParameter("nonce_str", nonceStr);              //随机字符串
            packageReqHandler.SetParameter("mch_billno", mchBillNo);                 //填入商家订单号
            packageReqHandler.SetParameter("mch_id", mchId);          //商户号
            if (subMchId != null)
            {
                packageReqHandler.SetParameter("sub_mch_id", subMchId);          //商户号
            }
            packageReqHandler.SetParameter("wxappid", wxAppId);       //公众账号ID
            packageReqHandler.SetParameter("msgappid", msgAppId);       //触达用户appid  
            packageReqHandler.SetParameter("send_name", senderName);                //红包发送者名称
            packageReqHandler.SetParameter("re_openid", openId);                 //接受收红包的用户的openId
            packageReqHandler.SetParameter("total_amount", redPackAmount.ToString());                //付款金额，单位分
            packageReqHandler.SetParameter("total_num", "1");               //红包发放总人数
            packageReqHandler.SetParameter("wishing", wishingWord);               //红包祝福语
            packageReqHandler.SetParameter("client_ip", iP);               //调用接口的机器Ip地址
            packageReqHandler.SetParameter("act_name", actionName);   //活动名称
            packageReqHandler.SetParameter("remark", remark);   //备注信息
            if (scene.HasValue)
            {
                packageReqHandler.SetParameter("scene_id", scene.Value.ToString());//场景id
            }
            if (riskInfo != null)
            {
                packageReqHandler.SetParameter("risk_info", riskInfo);//活动信息	
            }
            if (consumeMchId != null)
            {
                packageReqHandler.SetParameter("consume_mch_id", consumeMchId);//活动信息	
            }

            paySign = packageReqHandler.CreateMd5Sign("key", tenPayKey);
            packageReqHandler.SetParameter("sign", paySign);	                    //签名


            //最新的官方文档中将以下三个字段去除了
            //packageReqHandler.SetParameter("nick_name", "提供方名称");                 //提供方名称
            //packageReqHandler.SetParameter("max_value", "100");                //最大红包金额，单位分
            //packageReqHandler.SetParameter("min_value", "100");                //最小红包金额，单位分

            //发红包需要post的数据
            string data = packageReqHandler.ParseXML();

            //发红包接口地址
            string url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/sendredpack";
            //本地或者服务器的证书位置（证书在微信支付申请成功发来的通知邮件中）
            string cert = tenPayCertPath;
            //私钥（在安装证书时设置）
            string password = mchId;

            //调用证书
            X509Certificate2 cer = new X509Certificate2(cert, password, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);

            XmlDocument doc = new XmlDocument();

            #region 发起post请求，载入到doc中

#if NET35 || NET40 || NET45 || NET461
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            //X509Certificate cer = new X509Certificate(cert, password);
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
            string response = streamReader.ReadToEnd();
            doc.LoadXml(response);
#else
            HttpClientHandler handler = new HttpClientHandler();
            handler.ClientCertificates.Add(cer);

            HttpClient client = new HttpClient(handler);
            HttpContent hc = new StringContent(data);
            var request = client.PostAsync(url, hc).Result;
            var response = request.Content.ReadAsStreamAsync().Result;
            doc.Load(response);
#endif
            #endregion


            //XDocument xDoc = XDocument.Load(responseContent);

            NormalRedPackResult normalReturn = new NormalRedPackResult
            {
                err_code = "",
                err_code_des = ""
            };

            if (doc.SelectSingleNode("/xml/return_code") != null)
            {
                normalReturn.return_code = doc.SelectSingleNode("/xml/return_code").InnerText;
            }
            if (doc.SelectSingleNode("/xml/return_msg") != null)
            {
                normalReturn.return_msg = doc.SelectSingleNode("/xml/return_msg").InnerText;
            }

            if (normalReturn.ReturnCodeSuccess)
            {
                //redReturn.sign = doc.SelectSingleNode("/xml/sign").InnerText;
                if (doc.SelectSingleNode("/xml/result_code") != null)
                {
                    normalReturn.result_code = doc.SelectSingleNode("/xml/result_code").InnerText;
                }

                if (normalReturn.ResultCodeSuccess)
                {
                    if (doc.SelectSingleNode("/xml/mch_billno") != null)
                    {
                        normalReturn.mch_billno = doc.SelectSingleNode("/xml/mch_billno").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/mch_id") != null)
                    {
                        normalReturn.mch_id = doc.SelectSingleNode("/xml/mch_id").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/wxappid") != null)
                    {
                        normalReturn.wxappid = doc.SelectSingleNode("/xml/wxappid").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/re_openid") != null)
                    {
                        normalReturn.re_openid = doc.SelectSingleNode("/xml/re_openid").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/total_amount") != null)
                    {
                        normalReturn.total_amount = doc.SelectSingleNode("/xml/total_amount").InnerText;
                    }

                    //裂变红包才有
                    if (doc.SelectSingleNode("/xml/send_time") != null)
                    {
                        normalReturn.send_time = doc.SelectSingleNode("/xml/send_time").InnerText;
                    }
                    //裂变红包才有
                    if (doc.SelectSingleNode("/xml/send_listid") != null)
                    {
                        normalReturn.send_listid = doc.SelectSingleNode("/xml/send_listid").InnerText;
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
                    if (doc.SelectSingleNode("/xml/mch_billno") != null)
                    {
                        normalReturn.mch_billno = doc.SelectSingleNode("/xml/mch_billno").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/mch_id") != null)
                    {
                        normalReturn.mch_id = doc.SelectSingleNode("/xml/mch_id").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/wxappid") != null)
                    {
                        normalReturn.wxappid = doc.SelectSingleNode("/xml/wxappid").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/re_openid") != null)
                    {
                        normalReturn.re_openid = doc.SelectSingleNode("/xml/re_openid").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/total_amount") != null)
                    {
                        normalReturn.total_amount = doc.SelectSingleNode("/xml/total_amount").InnerText;
                    }

                }
            }

            return normalReturn;
        }

        #endregion

        #endregion

#if !NET35 && !NET40
        #region 异步方法


        #region 服务商

        //暂未提供异步方法

        #endregion

        #endregion
#endif
    }
}
