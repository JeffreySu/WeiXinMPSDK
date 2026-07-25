#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WorkRedPackApi.Async.cs
    文件功能描述：企业红包异步发送与查询接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v1.19.0 新增企业红包异步发送和查询入口及取消传播

----------------------------------------------------------------*/

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Senparc.Weixin.TenPay.V3
{
    public partial class WorkRedPackApi
    {
        private sealed class WorkRedPackSendRequest
        {
            public string Data { get; set; }
            public string NonceStr { get; set; }
            public string PaySign { get; set; }
            public string WorkpaySign { get; set; }
            public string MchBillNo { get; set; }
        }

        private static WorkRedPackSendRequest CreateSendWorkRedPackRequest(
            string appId,
            string mchId,
            string tenPayKey,
            string senderName,
            int redPackAmount,
            string wishingWord,
            string actionName,
            string remark,
            int agentId,
            string openId,
            string senderHeader,
            string sceneId,
            string mchBillNo)
        {
            mchBillNo = mchBillNo ?? GetNewBillNo(mchId);
            var nonceStr = TenPayV3Util.GetNoncestr();
            var packageReqHandler = new RequestHandler();

            packageReqHandler.SetParameter("nonce_str", nonceStr);
            packageReqHandler.SetParameter("wxappid", appId);
            packageReqHandler.SetParameter("mch_id", mchId);
            packageReqHandler.SetParameter("mch_billno", mchBillNo);
            packageReqHandler.SetParameter("sender_name", senderName);
            packageReqHandler.SetParameter("agentid", agentId.ToString());
            packageReqHandler.SetParameter("sender_header_media_id", senderHeader);
            packageReqHandler.SetParameter("re_openid", openId);
            packageReqHandler.SetParameter("total_amount", redPackAmount.ToString());
            packageReqHandler.SetParameter("wishing", wishingWord);
            packageReqHandler.SetParameter("act_name", actionName);
            packageReqHandler.SetParameter("remark", remark);
            packageReqHandler.SetParameter("scene_id", sceneId);

            var workpaySign = packageReqHandler.CreateMd5Sign("key", tenPayKey);
            packageReqHandler.SetParameter("workwx_sign", workpaySign);
            var paySign = packageReqHandler.CreateMd5Sign("key", tenPayKey);
            packageReqHandler.SetParameter("sign", paySign);

            return new WorkRedPackSendRequest
            {
                Data = packageReqHandler.ParseXML(),
                NonceStr = nonceStr,
                PaySign = paySign,
                WorkpaySign = workpaySign,
                MchBillNo = mchBillNo
            };
        }

        private static string CreateSearchWorkRedPackRequest(
            string appId,
            string mchId,
            string tenPayKey,
            string mchBillNo)
        {
            var packageReqHandler = new RequestHandler();
            packageReqHandler.SetParameter("nonce_str", TenPayV3Util.GetNoncestr());
            packageReqHandler.SetParameter("appid", appId);
            packageReqHandler.SetParameter("mch_id", mchId);
            packageReqHandler.SetParameter("mch_billno", mchBillNo);
            packageReqHandler.SetParameter("sign", packageReqHandler.CreateMd5Sign("key", tenPayKey));
            return packageReqHandler.ParseXML();
        }

        private static string GetNodeText(XmlDocument document, string path)
        {
            return document.SelectSingleNode(path)?.InnerText;
        }

        private static NormalRedPackResult ParseSendWorkRedPackResult(XmlDocument document)
        {
            var result = new NormalRedPackResult
            {
                err_code = string.Empty,
                err_code_des = string.Empty,
                return_code = GetNodeText(document, "/xml/return_code"),
                return_msg = GetNodeText(document, "/xml/return_msg")
            };

            if (!string.Equals(result.return_code, "SUCCESS", StringComparison.OrdinalIgnoreCase))
            {
                return result;
            }

            result.result_code = GetNodeText(document, "/xml/result_code");
            result.mch_billno = GetNodeText(document, "/xml/mch_billno");
            result.mch_id = GetNodeText(document, "/xml/mch_id");
            result.wxappid = GetNodeText(document, "/xml/wxappid");
            result.re_openid = GetNodeText(document, "/xml/re_openid");
            result.total_amount = GetNodeText(document, "/xml/total_amount");

            if (!string.Equals(result.result_code, "SUCCESS", StringComparison.OrdinalIgnoreCase))
            {
                result.err_code = GetNodeText(document, "/xml/err_code");
                result.err_code_des = GetNodeText(document, "/xml/err_code_des");
                result.send_listid = GetNodeText(document, "/xml/send_listid");
            }

            return result;
        }

        private static SearchRedPackResult ParseSearchWorkRedPackResult(XmlDocument document)
        {
            var result = new SearchRedPackResult
            {
                err_code = string.Empty,
                err_code_des = string.Empty,
                return_code = string.Equals(GetNodeText(document, "/xml/return_code"), "SUCCESS",
                    StringComparison.OrdinalIgnoreCase),
                return_msg = GetNodeText(document, "/xml/return_msg")
            };

            if (!result.return_code)
            {
                return result;
            }

            result.result_code = string.Equals(GetNodeText(document, "/xml/result_code"), "SUCCESS",
                StringComparison.OrdinalIgnoreCase);
            if (!result.result_code)
            {
                result.err_code = GetNodeText(document, "/xml/err_code");
                result.err_code_des = GetNodeText(document, "/xml/err_code_des");
                return result;
            }

            result.mch_billno = GetNodeText(document, "/xml/mch_billno");
            result.mch_id = GetNodeText(document, "/xml/mch_id");
            result.detail_id = GetNodeText(document, "/xml/detail_id");
            result.status = GetNodeText(document, "/xml/status");
            result.send_type = GetNodeText(document, "/xml/send_type");
            result.total_amount = GetNodeText(document, "/xml/total_amount");
            result.reason = GetNodeText(document, "/xml/reason");
            result.send_time = GetNodeText(document, "/xml/send_time");
            result.wishing = GetNodeText(document, "/xml/wishing");
            result.remark = GetNodeText(document, "/xml/remark");
            result.act_name = GetNodeText(document, "/xml/act_name");
            return result;
        }

        /// <summary>
        /// 【异步方法】发放企业红包。
        /// </summary>
        /// <param name="appId">公众账号 AppId。</param>
        /// <param name="mchId">商户号。</param>
        /// <param name="tenPayKey">支付密钥。</param>
        /// <param name="tenPayCertPath">证书绝对路径。</param>
        /// <param name="senderName">红包发送者名称。</param>
        /// <param name="redPackAmount">付款金额，单位为分。</param>
        /// <param name="wishingWord">祝福语。</param>
        /// <param name="actionName">活动名称。</param>
        /// <param name="remark">活动描述。</param>
        /// <param name="agentId">发送红包的应用 ID。</param>
        /// <param name="openId">接收人的 OpenId。</param>
        /// <param name="amtType">金额类型；为兼容同步入口保留。</param>
        /// <param name="senderHeader">发送者头像素材 ID。</param>
        /// <param name="sceneId">红包场景 ID。</param>
        /// <param name="mchBillNo">商户订单号；为空时自动生成。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>接口响应以及请求签名信息。</returns>
        public static async Task<SendWorkRedPackResult> SendWorkRedPackAsync(
            string appId,
            string mchId,
            string tenPayKey,
            string tenPayCertPath,
            string senderName,
            int redPackAmount,
            string wishingWord,
            string actionName,
            string remark,
            int agentId,
            string openId,
            string amtType,
            string senderHeader,
            string sceneId,
            string mchBillNo,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = CreateSendWorkRedPackRequest(appId, mchId, tenPayKey, senderName,
                redPackAmount, wishingWord, actionName, remark, agentId, openId, senderHeader,
                sceneId, mchBillNo);
            var url = Senparc.Weixin.Config.TenPayV3Host + "/mmpaymkttransfers/sendworkwxredpack";

            XmlDocument document;
            using (var certificate = LoadCertificate(tenPayCertPath, mchId))
            {
                document = await RedPackHttpUtility.PostXmlAsync(
                    url, request.Data, certificate, cancellationToken).ConfigureAwait(false);
            }

            return new SendWorkRedPackResult(ParseSendWorkRedPackResult(document), request.NonceStr,
                request.PaySign, request.WorkpaySign, request.MchBillNo);
        }

        /// <summary>
        /// 【异步方法】查询企业红包记录。
        /// </summary>
        /// <param name="appId">公众账号 AppId。</param>
        /// <param name="mchId">商户号。</param>
        /// <param name="tenPayKey">支付密钥。</param>
        /// <param name="tenPayCertPath">证书绝对路径。</param>
        /// <param name="mchBillNo">商户订单号。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>企业红包查询结果。</returns>
        public static async Task<SearchRedPackResult> SearchRedPackAsync(
            string appId,
            string mchId,
            string tenPayKey,
            string tenPayCertPath,
            string mchBillNo,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = CreateSearchWorkRedPackRequest(appId, mchId, tenPayKey, mchBillNo);
            var url = Senparc.Weixin.Config.TenPayV3Host + "/mmpaymkttransfers/queryworkwxredpack";

            XmlDocument document;
            using (var certificate = LoadCertificate(tenPayCertPath, mchId))
            {
                document = await RedPackHttpUtility.PostXmlAsync(
                    url, data, certificate, cancellationToken).ConfigureAwait(false);
            }

            return ParseSearchWorkRedPackResult(document);
        }
    }
}
