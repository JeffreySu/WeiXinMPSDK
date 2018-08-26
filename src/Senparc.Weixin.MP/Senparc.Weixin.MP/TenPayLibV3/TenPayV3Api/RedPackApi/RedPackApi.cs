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
  
    文件名：RedPackApi.cs
    文件功能描述：普通红包发送和红包查询Api（暂缺裂变红包发送）
    
    
    创建标识：Yu XiaoChou - 20160107
        
    修改标识：Senparc - 20161024
    修改描述：v14.3.102 重新整理红包发送方法

    修改标识：Senparc - 20161112
    修改描述：v14.3.107 SearchRedPack方法修改证书初始化方法

    修改标识：Senparc - 20170110
    修改描述：v14.3.118  

    修改标识：Senparc - 20170810
    修改描述：v14.5.9 查询红包接口（SearchRedPack）添加refund_amount和remark两个参数获取

    修改标识：Senparc - 20170810
    修改描述：v14.6.10 添加接口：普通红包发送(服务商）
    
    修改标识：Senparc - 20170925
    修改描述：添加新规定提示：红包超过2000元必须提供scene_id参数：
              https://pay.weixin.qq.com/wiki/doc/api/tools/cash_coupon.php?chapter=13_4&index=3
                  
    修改标识：Senparc - 20171208
    修改描述：v14.8.10 修复红包接口 RedPackApi.SendNormalRedPack() 在.NET 4.6 下的XML解析问题

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Senparc.Weixin.Exceptions;

#if !NET35 && !NET40 && !NET45
using System.Net.Http;
#endif

namespace Senparc.Weixin.MP.TenPayLibV3
{
    /// <summary>
    /// 红包发送和查询Api（暂缺裂变红包发送）
    /// </summary>
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public partial class RedPackApi
    {
        private static string GetNewBillNo(string mchId)
        {
            //return string.Format("{0}{1}{2}", mchId, DateTime.Now.ToString("yyyyMMdd"), TenPayV3Util.BuildRandomStr(10));
            return string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmssfff"), TenPayV3Util.BuildRandomStr(3));
        }

        #region 错误码

        /*
错误码	描述	解决方案
NO_AUTH	发放失败，此请求可能存在风险，已被微信拦截	请提醒用户检查自身帐号是否异常。使用常用的活跃的微信号可避免这种情况。
SENDNUM_LIMIT	该用户今日领取红包个数超过限制	如有需要、请在微信支付商户平台【api安全】中重新配置 【每日同一用户领取本商户红包不允许超过的个数】。
CA_ERROR	请求未携带证书，或请求携带的证书出错	到商户平台下载证书，请求带上证书后重试。
ILLEGAL_APPID	错误传入了app的appid	接口传入的所有appid应该为公众号的appid（在mp.weixin.qq.com申请的），不能为APP的appid（在open.weixin.qq.com申请的）。
SIGN_ERROR	商户签名错误	按文档要求重新生成签名后再重试。
FREQ_LIMIT	受频率限制	请对请求做频率控制
XML_ERROR	请求的xml格式错误，或者post的数据为空	检查请求串，确认无误后重试
PARAM_ERROR	参数错误	请查看err_code_des，修改设置错误的参数
OPENID_ERROR	Openid错误	根据用户在商家公众账号上的openid，获取用户在红包公众账号上的openid 错误。请核对商户自身公众号appid和用户在此公众号下的openid。
NOTENOUGH	余额不足	商户账号余额不足，请登录微信支付商户平台充值
FATAL_ERROR	重复请求时，参数与原单不一致	使用相同商户单号进行重复请求时，参数与第一次请求时不一致，请检查并修改参数后再重试。
SECOND_OVER_LIMITED	企业红包的按分钟发放受限	每分钟发送红包数量不得超过1800个；（可联系微信支付wxhongbao@tencent.com调高额度）
DAY_ OVER_LIMITED	企业红包的按天日发放受限	单个商户日发送红包数量不大于10000个；（可联系微信支付wxhongbao@tencent.com调高额度）
MONEY_LIMIT	红包金额发放限制	每个红包金额必须大于1元，小于1000元（可联系微信支付wxhongbao@tencent.com调高额度至4999元）
SEND_FAILED	红包发放失败,请更换单号再重试	原商户单号已经失败，如果还要对同一个用户发放红包， 需要更换新的商户单号再试。
SYSTEMERROR	系统繁忙，请再试。	可用同一商户单号再次调用，只会发放一个红包
PROCESSING	请求已受理，请稍后使用原单号查询发放结果	二十分钟后查询,按照查询结果成功失败进行处理
             */

        #endregion

        /// <summary>
        /// 普通红包发送
        /// </summary>
        /// <param name="appId">公众账号AppID</param>
        /// <param name="mchId">商户MchID</param>
        /// <param name="tenPayKey">支付密钥，微信商户平台(pay.weixin.qq.com)-->账户设置-->API安全-->密钥设置</param>
        /// <param name="tenPayCertPath">证书地址（硬盘物理地址，形如E:\\cert\\apiclient_cert.p12）</param>
        /// <param name="openId">要发红包的用户的OpenID</param>
        /// <param name="senderName">红包发送者名称，会显示给接收红包的用户</param>
        /// <param name="iP">发送红包的服务器地址</param>
        /// <param name="redPackAmount">付款金额，单位分。红包金额大于200时，请求参数scene必传。</param>
        /// <param name="wishingWord">祝福语</param>
        /// <param name="actionName">活动名称（请注意活动名称长度，官方文档提示为32个字符，实际限制不足32个字符）</param>
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
        /// <param name="consumeMchId">资金授权商户号，服务商替特约商户发放时使用（非必填），String(32)。示例：1222000096</param>
        /// <returns></returns>
        public static NormalRedPackResult SendNormalRedPack(string appId, string mchId, string tenPayKey, string tenPayCertPath,
            string openId, string senderName,
            string iP, int redPackAmount, string wishingWord, string actionName, string remark,
            out string nonceStr, out string paySign,
            string mchBillNo, RedPack_Scene? scene = null, string riskInfo = null, string consumeMchId = null)
        {
            mchBillNo = mchBillNo ?? GetNewBillNo(mchId);

            nonceStr = TenPayV3Util.GetNoncestr();
            //RequestHandler packageReqHandler = new RequestHandler(null);

            //string accessToken = AccessTokenContainer.GetAccessToken(ConstantClass.AppID);
            //UserInfoJson userInforResult = UserApi.Info(accessToken, openID);

            RequestHandler packageReqHandler = new RequestHandler();
            //设置package订单参数
            packageReqHandler.SetParameter("nonce_str", nonceStr);              //随机字符串
            packageReqHandler.SetParameter("wxappid", appId);		  //公众账号ID
            packageReqHandler.SetParameter("mch_id", mchId);		  //商户号
            packageReqHandler.SetParameter("mch_billno", mchBillNo);                 //填入商家订单号
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

            XmlDocument doc = new Senparc.CO2NET.ExtensionEntities.XmlDocument_XxeFixed();

#if NET35 || NET40 || NET45 || NET461
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
            string response = streamReader.ReadToEnd();
            #endregion
            doc.LoadXml(response);
#else
            #region 发起post请求
            HttpClientHandler handler = new HttpClientHandler();
            handler.ClientCertificates.Add(cer);

            HttpClient client = new HttpClient(handler);
            HttpContent hc = new StringContent(data);
            var request = client.PostAsync(url, hc).Result;
            var response = request.Content.ReadAsStreamAsync().Result;
            #endregion
            doc.Load(response);

#endif

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

        #region v14.3.105中将发布

        /// <summary>
        /// 裂变红包发送
        /// <para>裂变红包：一次可以发放一组红包。首先领取的用户为种子用户，种子用户领取一组红包当中的一个，并可以通过社交分享将剩下的红包给其他用户。裂变红包充分利用了人际传播的优势。</para>
        /// </summary>
        /// <param name="appId">公众账号AppID</param>
        /// <param name="mchId">商户MchID</param>
        /// <param name="tenPayKey">支付密钥，微信商户平台(pay.weixin.qq.com)-->账户设置-->API安全-->密钥设置</param>
        /// <param name="tenPayCertPath">证书地址（硬盘物理地址，形如E:\\cert\\apiclient_cert.p12）</param>
        /// <param name="openId">要发红包的用户的OpenID</param>
        /// <param name="senderName">红包发送者名称，会显示给接收红包的用户</param>
        /// <param name="iP">发送红包的服务器地址</param>
        /// <param name="redPackAmount">付款金额，单位分。红包金额大于200时，请求参数scene必传。</param>
        /// <param name="wishingWord">祝福语</param>
        /// <param name="actionName">活动名称（请注意活动名称长度，官方文档提示为32个字符，实际限制不足32个字符）</param>
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
        /// <param name="consumeMchId">资金授权商户号，服务商替特约商户发放时使用（非必填），String(32)。示例：1222000096</param>
        /// <param name="amtType">红包金额设置方式，默认填写“ALL_RAND”，ALL_RAND—全部随机,商户指定总金额和红包发放总人数，由微信支付随机计算出各红包金额</param>
        /// <returns></returns>
        public static NormalRedPackResult SendNGroupRedPack(string appId, string mchId, string tenPayKey, string tenPayCertPath,
            string openId, string senderName,
            string iP, int redPackAmount, string wishingWord, string actionName, string remark,
            out string nonceStr, out string paySign, string mchBillNo, RedPack_Scene? scene = null, string riskInfo = null, string consumeMchId = null, string amtType = "ALL_RAND")
        {
            mchBillNo = mchBillNo ?? GetNewBillNo(mchId);

            nonceStr = TenPayV3Util.GetNoncestr();
            //RequestHandler packageReqHandler = new RequestHandler(null);

            //string accessToken = AccessTokenContainer.GetAccessToken(ConstantClass.AppID);
            //UserInfoJson userInforResult = UserApi.Info(accessToken, openID);

            RequestHandler packageReqHandler = new RequestHandler();
            //设置package订单参数
            packageReqHandler.SetParameter("nonce_str", nonceStr);              //随机字符串
            packageReqHandler.SetParameter("wxappid", appId);		  //公众账号ID
            packageReqHandler.SetParameter("mch_id", mchId);		  //商户号
            packageReqHandler.SetParameter("mch_billno", mchBillNo);                 //填入商家订单号
            packageReqHandler.SetParameter("send_name", senderName);                //红包发送者名称
            packageReqHandler.SetParameter("re_openid", openId);                 //接受收红包的用户的openId
            packageReqHandler.SetParameter("total_amount", redPackAmount.ToString());                //付款金额，单位分
            packageReqHandler.SetParameter("total_num", "1");               //红包发放总人数
            packageReqHandler.SetParameter("wishing", wishingWord);               //红包祝福语
            packageReqHandler.SetParameter("client_ip", iP);               //调用接口的机器Ip地址
            packageReqHandler.SetParameter("act_name", actionName);   //活动名称
            packageReqHandler.SetParameter("remark", remark);   //备注信息
            paySign = packageReqHandler.CreateMd5Sign("key", tenPayKey);
            packageReqHandler.SetParameter("sign", paySign);	                    //签名

            //比普通红包多的部分
            packageReqHandler.SetParameter("amt_type", amtType);	                    //签名

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

            XmlDocument doc = new Senparc.CO2NET.ExtensionEntities.XmlDocument_XxeFixed();

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

            //if (xDoc==null)
            //{
            //    throw new WeixinException("微信支付XML响应格式错误");
            //}

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


        /// <summary>
        /// 查询红包(包括普通红包和裂变红包)
        /// </summary>
        /// <param name="appId">公众账号AppID</param>
        /// <param name="mchId">商户MchID</param>
        /// <param name="tenPayKey">支付密钥，微信商户平台(pay.weixin.qq.com)-->账户设置-->API安全-->密钥设置</param>
        /// <param name="tenPayCertPath">证书地址（硬盘地址，形如E://cert//apiclient_cert.p12）</param>
        /// <param name="mchBillNo">商家订单号</param>
        /// <returns></returns>
        public static SearchRedPackResult SearchRedPack(string appId, string mchId, string tenPayKey, string tenPayCertPath, string mchBillNo)
        {
            string nonceStr = TenPayV3Util.GetNoncestr();
            RequestHandler packageReqHandler = new RequestHandler();

            packageReqHandler.SetParameter("nonce_str", nonceStr);              //随机字符串
            packageReqHandler.SetParameter("appid", appId);		  //公众账号ID
            packageReqHandler.SetParameter("mch_id", mchId);		  //商户号
            packageReqHandler.SetParameter("mch_billno", mchBillNo);                 //填入商家订单号
            packageReqHandler.SetParameter("bill_type", "MCHT");                 //MCHT:通过商户订单号获取红包信息。 
            string sign = packageReqHandler.CreateMd5Sign("key", tenPayKey);
            packageReqHandler.SetParameter("sign", sign);	                    //签名
            //发红包需要post的数据
            string data = packageReqHandler.ParseXML();

            //发红包接口地址
            string url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/gethbinfo";
            //本地或者服务器的证书位置（证书在微信支付申请成功发来的通知邮件中）
            string cert = tenPayCertPath;
            //私钥（在安装证书时设置）
            string password = mchId;

            //调用证书
            //X509Certificate cer = new X509Certificate(cert, password);
            X509Certificate2 cer = new X509Certificate2(cert, password, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);

            XmlDocument doc = new Senparc.CO2NET.ExtensionEntities.XmlDocument_XxeFixed();
            #region 发起post请求，载入到doc中

#if NET35 || NET40 || NET45 || NET461
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

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




            SearchRedPackResult searchReturn = new SearchRedPackResult
            {
                err_code = "",
                err_code_des = ""
            };
            if (doc.SelectSingleNode("/xml/return_code") != null)
            {
                searchReturn.return_code = (doc.SelectSingleNode("/xml/return_code").InnerText.ToUpper() == "SUCCESS");
            }
            if (doc.SelectSingleNode("/xml/return_msg") != null)
            {
                searchReturn.return_msg = doc.SelectSingleNode("/xml/return_msg").InnerText;
            }

            if (searchReturn.return_code == true)
            {
                //redReturn.sign = doc.SelectSingleNode("/xml/sign").InnerText;
                if (doc.SelectSingleNode("/xml/result_code") != null)
                {
                    searchReturn.result_code = (doc.SelectSingleNode("/xml/result_code").InnerText.ToUpper() == "SUCCESS");
                }

                if (searchReturn.result_code == true)
                {
                    if (doc.SelectSingleNode("/xml/mch_billno") != null)
                    {
                        searchReturn.mch_billno = doc.SelectSingleNode("/xml/mch_billno").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/mch_id") != null)
                    {
                        searchReturn.mch_id = doc.SelectSingleNode("/xml/mch_id").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/detail_id") != null)
                    {
                        searchReturn.detail_id = doc.SelectSingleNode("/xml/detail_id").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/status") != null)
                    {
                        searchReturn.status = doc.SelectSingleNode("/xml/status").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/send_type") != null)
                    {
                        searchReturn.send_type = doc.SelectSingleNode("/xml/send_type").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/hb_type") != null)
                    {
                        searchReturn.hb_type = doc.SelectSingleNode("/xml/hb_type").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/total_num") != null)
                    {
                        searchReturn.total_num = doc.SelectSingleNode("/xml/total_num").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/total_amount") != null)
                    {
                        searchReturn.total_amount = doc.SelectSingleNode("/xml/total_amount").InnerText;
                    }

                    if (doc.SelectSingleNode("/xml/reason") != null)
                    {
                        searchReturn.reason = doc.SelectSingleNode("/xml/reason").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/send_time") != null)
                    {
                        searchReturn.send_time = doc.SelectSingleNode("/xml/send_time").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/refund_time") != null)
                    {
                        searchReturn.refund_time = doc.SelectSingleNode("/xml/refund_time").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/refund_amount") != null)
                    {
                        searchReturn.refund_amount = doc.SelectSingleNode("/xml/refund_amount").InnerText;
                    }

                    if (doc.SelectSingleNode("/xml/wishing") != null)
                    {
                        searchReturn.wishing = doc.SelectSingleNode("/xml/wishing").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/remark") != null)
                    {
                        searchReturn.remark = doc.SelectSingleNode("/xml/remark").InnerText;
                    }

                    if (doc.SelectSingleNode("/xml/act_name") != null)
                    {
                        searchReturn.act_name = doc.SelectSingleNode("/xml/act_name").InnerText;
                    }

                    if (doc.SelectSingleNode("/xml/hblist") != null)
                    {
                        searchReturn.hblist = new List<RedPackHBInfo>();

                        foreach (XmlNode hbinfo in doc.SelectNodes("/xml/hblist/hbinfo"))
                        {
                            RedPackHBInfo wechatHBInfo = new RedPackHBInfo();
                            wechatHBInfo.openid = hbinfo.SelectSingleNode("openid").InnerText;
                            //wechatHBInfo.status = hbinfo.SelectSingleNode("status").InnerText;
                            wechatHBInfo.amount = hbinfo.SelectSingleNode("amount").InnerText;
                            wechatHBInfo.rcv_time = hbinfo.SelectSingleNode("rcv_time").InnerText;

                            searchReturn.hblist.Add(wechatHBInfo);
                        }
                    }
                }
                else
                {
                    if (doc.SelectSingleNode("/xml/err_code") != null)
                    {
                        searchReturn.err_code = doc.SelectSingleNode("/xml/err_code").InnerText;
                    }
                    if (doc.SelectSingleNode("/xml/err_code_des") != null)
                    {
                        searchReturn.err_code_des = doc.SelectSingleNode("/xml/err_code_des").InnerText;
                    }
                }
            }

            return searchReturn;
        }


        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            if (errors == SslPolicyErrors.None)
                return true;
            return false;
        }

    }
}
