#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc
 
    文件名：InvoiceApi.cs
    文件功能描述：电子发票接口
    
    
    创建标识：Senparc - 20180927

    修改标识：Senparc - 20180930
    修改描述：添加设置支付后开票信息接口,查询支付后开票信息接口,设置授权页字段信息接口,查询授权页字段信息接口,查询开票信息,获取授权页链接
              统一开票接口-开具蓝票,统一开票接口-发票冲红,统一开票接口-查询已开发票,设置商户联系方式,查询商户联系方式,获取自身的开票平台识别码
              创建发票卡券模板,上传PDF,查询已上传的PDF文件,将电子发票卡券插入用户卡包,更新发票卡券状态,查询报销发票信息,批量查询报销发票信息
              报销方更新发票状态，报销方批量更新发票状态

    修改标识：Senparc - 20180930
    修改描述：添加非税票据相关接口

    修改标识：Senparc - 20181016
    修改描述：修正批量查询报销发票信息接口返回值

    修改标识：Senparc - 20181018
    修改描述：【异步方法】批量查询报销发票信息接口去掉无意义参数，更新reimburseStatus为枚举值

    修改标识：Senparc - 20181023
    修改描述：修正发票信息实体，区分开票平台提交的发票信息实体与报销方获取的发票信息实体

----------------------------------------------------------------*/


using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.HttpUtility;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs.Media;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 电子发票接口
    /// </summary>
    public static class InvoiceApi
    {
        #region 同步方法

        #region 支付相关
        /// <summary>
        /// 设置支付后开票信息/关联商户号与开票平台
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="payMchInfo"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.SetPayMch", true)]
        public static WxJsonResult SetPayMch(string accessTokenOrAppId, PayMchInfoData payMchInfo, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/setbizattr?action=set_pay_mch&access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    paymch_info = payMchInfo
                };
                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询支付后开票信息接口/查询商户号与开票平台关联情况
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.GetPayMch", true)]
        public static GetPayMchResultJson GetPayMch(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/setbizattr?action=get_pay_mch&access_token={0}", accessToken.AsUrlData());
                var data = new { };
                return CommonJsonSend.Send<GetPayMchResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        #endregion

        #region 授权相关 

        /// <summary>
        /// 设置授权页字段信息接口
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="authField"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.SetAuthField", true)]
        public static WxJsonResult SetAuthField(string accessTokenOrAppId, AuthFieldData authField, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/setbizattr?action=set_auth_field&access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    auth_field = authField
                };
                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询授权页字段信息接口
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.GetAuthField", true)]
        public static AuthFieldResultJson GetAuthField(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/setbizattr?action=set_auth_field&access_token={0}", accessToken.AsUrlData());
                var data = new { };
                return CommonJsonSend.Send<AuthFieldResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询开票信息/查询授权完成状态
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="orderId"></param>
        /// <param name="s_appId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.GetAuthData", true)]
        public static AuthDataResultJson GetAuthData(string accessTokenOrAppId, string orderId, string s_appId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/getauthdata?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    order_id = orderId,
                    s_appid = s_appId
                };
                return CommonJsonSend.Send<AuthDataResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取授权页链接
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.GetAuthUrl", true)]
        public static AuthUrlResultJson GetAuthUrl(string accessTokenOrAppId, GetAuthUrlData data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/getauthurl?access_token={0}", accessToken.AsUrlData());

                return CommonJsonSend.Send<AuthUrlResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

        #region 统一开票接口

        /// <summary>
        /// 拒绝开票
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="s_pappId"></param>
        /// <param name="orderId"></param>
        /// <param name="reason"></param>
        /// <param name="url"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.RejectInsert", true)]
        public static WxJsonResult RejectInsert(string accessTokenOrAppId, string s_pappId, string orderId, string reason, string url, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/rejectinsert?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    s_pappid = s_pappId,
                    orderid = orderId,
                    reason = reason,
                    url = url
                };
                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 开具蓝票
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="InvoiceInfo"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.MakeOutInvoice", true)]
        public static WxJsonResult MakeOutInvoice(string accessTokenOrAppId, MakeOutInvoiceData InvoiceInfo, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/makeoutinvoice?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    invoiceinfo = InvoiceInfo
                };
                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发票冲红
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="InvoiceInfo"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.ClearOutInvoice", true)]
        public static WxJsonResult ClearOutInvoice(string accessTokenOrAppId, ClearOutInvoiceData InvoiceInfo, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/clearoutinvoice?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    invoiceinfo = InvoiceInfo
                };
                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询已开发票
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="fpqqlsh"></param>
        /// <param name="nsrsbh"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.QueryInvoice", true)]
        public static QueryInvoiceResultJson QueryInvoice(string accessTokenOrAppId, string fpqqlsh, string nsrsbh, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/queryinvoceinfo?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    fpqqlsh = fpqqlsh,
                    nsrsbh = nsrsbh
                };
                return CommonJsonSend.Send<QueryInvoiceResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

        #region 非税票据 https://mp.weixin.qq.com/wiki?t=resource/res_main&id=21530623533CgUdj

        /// <summary>
        /// 将财政电子票据添加到用户微信卡包 
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="info"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.InsertBill", true)]
        public static InsertCardResultJson InsertBill(string accessTokenOrAppId, InsertCardToBagData info, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/nontax/insertbill?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    info
                };

                return CommonJsonSend.Send<InsertCardResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 创建财政电子票据接口
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="invoiceInfo"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.CreateBillCard", true)]
        public static CreateCardResultJson CreateBillCard(string accessTokenOrAppId, InvoiceInfo invoiceInfo, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "nontax/createbillcard?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    invoice_info = invoiceInfo
                };

                return CommonJsonSend.Send<CreateCardResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取授权页链接
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.GetBillAuthUrl", true)]
        public static GetBillAuthUrlResultJson GetBillAuthUrl(string accessTokenOrAppId, GetBillAuthUrlData data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/nontax/getbillauthurl?access_token={0}", accessToken.AsUrlData());

                return CommonJsonSend.Send<GetBillAuthUrlResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 更新电子票据状态
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId"></param>
        /// <param name="code"></param>
        /// <param name="reimburseStatus"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.UpdateStatus", true)]
        public static WxJsonResult UpdateStatus(string accessTokenOrAppId, string cardId, string code, Reimburse_Status reimburseStatus, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/platform/updatestatus?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId,
                    code = code,
                    reimburse_status = reimburseStatus.ToString()
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

        /// <summary>
        /// 设置商户联系方式
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="phone"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.SetContact", true)]
        public static WxJsonResult SetContact(string accessTokenOrAppId, string phone, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/setbizattr?action=set_contact&access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    contact = new
                    {
                        phone = phone,
                        time_out = timeOut
                    }
                };
                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询商户联系方式
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.GetContact", true)]
        public static GetContactResultJson GetContact(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/setbizattr?action=get_contact&access_token={0}", accessToken.AsUrlData());
                var data = new { };

                return CommonJsonSend.Send<GetContactResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询报销发票信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId"></param>
        /// <param name="encryptCode"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.GetInvoiceInfo", true)]
        public static GetInvoiceInfoResultJson GetInvoiceInfo(string accessTokenOrAppId, string cardId, string encryptCode, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/reimburse/getinvoiceinfo?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    card_id = cardId,
                    encrypt_code = encryptCode
                };

                return CommonJsonSend.Send<GetInvoiceInfoResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 批量查询报销发票信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="itemList"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.GetInvoiceListInfo", true)]
        public static GetInvoiceListResultJson GetInvoiceListInfo(string accessTokenOrAppId, List<InvoiceItem> itemList, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/reimburse/getinvoicebatch?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    item_list = itemList
                };

                return CommonJsonSend.Send<GetInvoiceListResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 报销方更新发票状态
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId"></param>
        /// <param name="encryptCode"></param>
        /// <param name="reimburseStatus"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.UpdateInvoiceStatus", true)]
        public static WxJsonResult UpdateInvoiceStatus(string accessTokenOrAppId, string cardId, string encryptCode, Reimburse_Status reimburseStatus, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/reimburse/updateinvoicestatus?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    card_id = cardId,
                    encrypt_code = encryptCode,
                    reimburse_status = reimburseStatus.ToString()
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 报销方批量更新发票状态
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="reimburseStatus"></param>
        /// <param name="itemList"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.UpdateInvoiceListStatus", true)]
        public static WxJsonResult UpdateInvoiceListStatus(string accessTokenOrAppId, string openId, Reimburse_Status reimburseStatus, List<InvoiceItem> itemList, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/reimburse/updatestatusbatch?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    openid = openId,
                    reimburse_status = reimburseStatus.ToString(),
                    invoice_list = itemList
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #region 开票平台接口

        /// <summary>
        /// 获取自身的开票平台识别码/获取财政局s_pappid
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.SetUrl", true)]
        public static SetUrlResultJson SetUrl(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/seturl?access_token={0}", accessToken.AsUrlData());
                var data = new { };

                return CommonJsonSend.Send<SetUrlResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 创建发票卡券模板
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="invoiceInfo"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.CreateCard", true)]
        public static CreateCardResultJson CreateCard(string accessTokenOrAppId, InvoiceInfo invoiceInfo, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/platform/createcard?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    invoice_info = invoiceInfo
                };

                return CommonJsonSend.Send<CreateCardResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 上传PDF 
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="file"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.SetPdf", true)]
        public static SetPDFResultJson SetPdf(string accessTokenOrAppId, string file, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/platform/setpdf?access_token={0}", accessToken.AsUrlData());
                var fileDictionary = new Dictionary<string, string>();
                fileDictionary["pdf"] = file;

                return Post.PostFileGetJson<SetPDFResultJson>(urlFormat, null, fileDictionary, null, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询已上传的PDF文件 
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="s_mediaId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.GetPdf", true)]
        public static GetPDFResultJson GetPdf(string accessTokenOrAppId, string s_mediaId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/platform/getpdf?action=get_url&access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    action = "get_url",
                    s_media_id = s_mediaId
                };

                return CommonJsonSend.Send<GetPDFResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 将电子发票卡券插入用户卡包 
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="info"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.InsertCardToBag", true)]
        public static InsertCardResultJson InsertCardToBag(string accessTokenOrAppId, InsertCardToBagData info, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/insert?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    info
                };

                return CommonJsonSend.Send<InsertCardResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

        #region 微信极速开发票

        /// <summary>
        /// 将发票抬头信息录入到用户微信中
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.GetUserTitleUrl", true)]
        public static GetUserTitleUrlResultJson GetUserTitleUrl(string accessTokenOrAppId, GetUserTitleUrlData data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/biz/getusertitleurl?access_token={0}", accessToken.AsUrlData());

                return CommonJsonSend.Send<GetUserTitleUrlResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取用户抬头（方式一）:获取商户专属二维码，立在收银台
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="attach"></param>
        /// <param name="bizName"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.GetSelectTitleUrl", true)]
        public static GetUserTitleUrlResultJson GetSelectTitleUrl(string accessTokenOrAppId, string attach, string bizName, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/biz/getselecttitleurl?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    attach = attach,
                    biz_name = bizName
                };

                return CommonJsonSend.Send<GetUserTitleUrlResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取用户抬头（方式二）:商户扫描用户的发票抬头二维码
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="scanText"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.ScanTitle", true)]
        public static ScanTitleResultJson ScanTitle(string accessTokenOrAppId, string scanText, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/scantitle?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    scan_text = scanText
                };

                return CommonJsonSend.Send<ScanTitleResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

        #endregion

        #region 异步方法

        #region 支付相关
        /// <summary>
        /// 【异步方法】设置支付后开票信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="payMchInfo"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.SetPayMchAsync", true)]
        public static async Task<WxJsonResult> SetPayMchAsync(string accessTokenOrAppId, PayMchInfoData payMchInfo, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/setbizattr?action=set_pay_mch&access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    paymch_info = payMchInfo
                };
                return await CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】查询支付后开票信息接口
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.GetPayMchAsync", true)]
        public static async Task<GetPayMchResultJson> GetPayMchAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/setbizattr?action=get_pay_mch&access_token={0}", accessToken.AsUrlData());
                var data = new { };
                return await CommonJsonSend.SendAsync<GetPayMchResultJson>(null, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        #endregion

        #region 授权相关

        /// <summary>
        /// 【异步方法】设置授权页字段信息接口
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="authField"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.SetAuthFieldAsync", true)]
        public static async Task<WxJsonResult> SetAuthFieldAsync(string accessTokenOrAppId, AuthFieldData authField, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/setbizattr?action=set_auth_field&access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    auth_field = authField
                };
                return await CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】查询授权页字段信息接口
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.GetAuthFieldAsync", true)]
        public static async Task<AuthFieldResultJson> GetAuthFieldAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/setbizattr?action=set_auth_field&access_token={0}", accessToken.AsUrlData());
                var data = new { };
                return await CommonJsonSend.SendAsync<AuthFieldResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】查询开票信息/查询授权完成状态
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="orderId"></param>
        /// <param name="s_appId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.GetAuthDataAsync", true)]
        public static async Task<AuthDataResultJson> GetAuthDataAsync(string accessTokenOrAppId, string orderId, string s_appId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/getauthdata?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    order_id = orderId,
                    s_appid = s_appId
                };
                return await CommonJsonSend.SendAsync<AuthDataResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】获取授权页链接
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.GetAuthUrlAsync", true)]
        public static async Task<AuthUrlResultJson> GetAuthUrlAsync(string accessTokenOrAppId, GetAuthUrlData data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/getauthurl?access_token={0}", accessToken.AsUrlData());

                return await CommonJsonSend.SendAsync<AuthUrlResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

        #region 统一开票接口

        /// <summary>
        /// 【异步方法】拒绝开票
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="s_pappId"></param>
        /// <param name="orderId"></param>
        /// <param name="reason"></param>
        /// <param name="url"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.RejectInsertAsync", true)]
        public static async Task<WxJsonResult> RejectInsertAsync(string accessTokenOrAppId, string s_pappId, string orderId, string reason, string url, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/rejectinsert?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    s_pappid = s_pappId,
                    orderid = orderId,
                    reason = reason,
                    url = url
                };
                return await CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】开具蓝票
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="InvoiceInfo"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.MakeOutInvoiceAsync", true)]
        public static async Task<WxJsonResult> MakeOutInvoiceAsync(string accessTokenOrAppId, MakeOutInvoiceData InvoiceInfo, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/makeoutinvoice?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    invoiceinfo = InvoiceInfo
                };
                return await CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】发票冲红
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="InvoiceInfo"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.ClearOutInvoiceAsync", true)]
        public static async Task<WxJsonResult> ClearOutInvoiceAsync(string accessTokenOrAppId, ClearOutInvoiceData InvoiceInfo, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/clearoutinvoice?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    invoiceinfo = InvoiceInfo
                };
                return await CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】查询已开发票
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="fpqqlsh"></param>
        /// <param name="nsrsbh"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.QueryInvoiceAsync", true)]
        public static async Task<QueryInvoiceResultJson> QueryInvoiceAsync(string accessTokenOrAppId, string fpqqlsh, string nsrsbh, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/queryinvoceinfo?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    fpqqlsh = fpqqlsh,
                    nsrsbh = nsrsbh
                };
                return await CommonJsonSend.SendAsync<QueryInvoiceResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

        #region 非税票据 https://mp.weixin.qq.com/wiki?t=resource/res_main&id=21530623533CgUdj

        /// <summary>
        /// 【异步方法】将财政电子票据添加到用户微信卡包 
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="info"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.InsertBillAsync", true)]
        public static async Task<InsertCardResultJson> InsertBillAsync(string accessTokenOrAppId, InsertCardToBagData info, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/nontax/insertbill?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    info
                };

                return await CommonJsonSend.SendAsync<InsertCardResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】创建财政电子票据接口
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="invoiceInfo"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.CreateBillCardAsync", true)]
        public static async Task<CreateCardResultJson> CreateBillCardAsync(string accessTokenOrAppId, InvoiceInfo invoiceInfo, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/nontax/createbillcard?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    invoice_info = invoiceInfo
                };

                return await CommonJsonSend.SendAsync<CreateCardResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】获取授权页链接
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.GetBillAuthUrlAsync", true)]
        public static async Task<GetBillAuthUrlResultJson> GetBillAuthUrlAsync(string accessTokenOrAppId, GetBillAuthUrlData data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/nontax/getbillauthurl?access_token={0}", accessToken.AsUrlData());

                return await CommonJsonSend.SendAsync<GetBillAuthUrlResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】更新电子票据状态
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId"></param>
        /// <param name="code"></param>
        /// <param name="reimburseStatus"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CardApi.UpdateStatusAsync", true)]
        public static async Task<WxJsonResult> UpdateStatusAsync(string accessTokenOrAppId, string cardId, string code, Reimburse_Status reimburseStatus, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/platform/updatestatus?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId,
                    code = code,
                    reimburse_status = reimburseStatus.ToString()
                };

                return await CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

        /// <summary>
        /// 【异步方法】设置商户联系方式
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="phone"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.SetContactAsync", true)]
        public static async Task<WxJsonResult> SetContactAsync(string accessTokenOrAppId, string phone, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/setbizattr?action=set_contact&access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    contact = new
                    {
                        phone = phone,
                        time_out = timeOut
                    }
                };
                return await CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】查询商户联系方式
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.GetContactAsync", true)]
        public static async Task<GetContactResultJson> GetContactAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/setbizattr?action=get_contact&access_token={0}", accessToken.AsUrlData());
                var data = new { };
                return await CommonJsonSend.SendAsync<GetContactResultJson>(null, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】查询报销发票信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId"></param>
        /// <param name="encryptCode"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.GetInvoiceInfoAsync", true)]
        public static async Task<GetInvoiceInfoResultJson> GetInvoiceInfoAsync(string accessTokenOrAppId, string cardId, string encryptCode, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/reimburse/getinvoiceinfo?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    card_id = cardId,
                    encrypt_code = encryptCode
                };

                return await CommonJsonSend.SendAsync<GetInvoiceInfoResultJson>(null, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】批量查询报销发票信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="itemList"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.GetInvoiceListInfoAsync", true)]
        public static async Task<GetInvoiceListResultJson> GetInvoiceListInfoAsync(string accessTokenOrAppId, List<InvoiceItem> itemList, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/reimburse/getinvoicebatch?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    item_list = itemList
                };

                return await CommonJsonSend.SendAsync<GetInvoiceListResultJson>(null, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】报销方更新发票状态
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId"></param>
        /// <param name="encryptCode"></param>
        /// <param name="reimburseStatus"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.UpdateInvoiceStatusAsync", true)]
        public static async Task<WxJsonResult> UpdateInvoiceStatusAsync(string accessTokenOrAppId, string cardId, string encryptCode, Reimburse_Status reimburseStatus, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/reimburse/updateinvoicestatus?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    card_id = cardId,
                    encrypt_code = encryptCode,
                    reimburse_status = reimburseStatus.ToString()
                };

                return await CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】报销方批量更新发票状态
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="reimburseStatus"></param>
        /// <param name="itemList"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.UpdateInvoiceListStatusAsync", true)]
        public static async Task<WxJsonResult> UpdateInvoiceListStatusAsync(string accessTokenOrAppId, string openId, Reimburse_Status reimburseStatus, List<InvoiceItem> itemList, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/reimburse/updatestatusbatch?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    openid = openId,
                    reimburse_status = reimburseStatus.ToString(),
                    invoice_list = itemList
                };

                return await CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        #region 开票平台接口

        /// <summary>
        /// 【异步方法】获取自身的开票平台识别码
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.SetUrlAsync", true)]
        public static async Task<SetUrlResultJson> SetUrlAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/seturl?access_token={0}", accessToken.AsUrlData());
                var data = new { };

                return await CommonJsonSend.SendAsync<SetUrlResultJson>(null, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】创建发票卡券模板
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="invoiceInfo"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.CreateCardAsync", true)]
        public static async Task<CreateCardResultJson> CreateCardAsync(string accessTokenOrAppId, InvoiceInfo invoiceInfo, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/platform/createcard?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    invoice_info = invoiceInfo
                };

                return await CommonJsonSend.SendAsync<CreateCardResultJson>(null, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】上传PDF 
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="file"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.SetPdfAsync", true)]
        public static async Task<SetPDFResultJson> SetPdfAsync(string accessTokenOrAppId, string file, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/platform/setpdf?access_token={0}", accessToken.AsUrlData());
                var fileDictionary = new Dictionary<string, string>();
                fileDictionary["pdf"] = file;

                return await Post.PostFileGetJsonAsync<SetPDFResultJson>(urlFormat, null, fileDictionary, null, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】查询已上传的PDF文件 
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="s_mediaId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.GetPdfAsync", true)]
        public static async Task<GetPDFResultJson> GetPdfAsync(string accessTokenOrAppId, string s_mediaId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/platform/getpdf?action=get_url&access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    action = "get_url",
                    s_media_id = s_mediaId
                };

                return await CommonJsonSend.SendAsync<GetPDFResultJson>(null, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】将电子发票卡券插入用户卡包 
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="info"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.InsertCardToBagAsync", true)]
        public static async Task<InsertCardResultJson> InsertCardToBagAsync(string accessTokenOrAppId, InsertCardToBagData info, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/insert?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    info
                };

                return await CommonJsonSend.SendAsync<InsertCardResultJson>(null, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        #endregion

        #region 微信极速开发票

        /// <summary>
        /// 【异步方法】将发票抬头信息录入到用户微信中
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.GetUserTitleUrlAsync", true)]
        public static async Task<GetUserTitleUrlResultJson> GetUserTitleUrlAsync(string accessTokenOrAppId, GetUserTitleUrlData data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/biz/getusertitleurl?access_token={0}", accessToken.AsUrlData());

                return await CommonJsonSend.SendAsync<GetUserTitleUrlResultJson>(null, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取用户抬头（方式一）:获取商户专属二维码，立在收银台
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="attach"></param>
        /// <param name="bizName"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.GetSelectTitleUrlAsync", true)]
        public static async Task<GetUserTitleUrlResultJson> GetSelectTitleUrlAsync(string accessTokenOrAppId, string attach, string bizName, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/biz/getselecttitleurl?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    attach = attach,
                    biz_name = bizName
                };

                return await CommonJsonSend.SendAsync<GetUserTitleUrlResultJson>(null, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取用户抬头（方式二）:商户扫描用户的发票抬头二维码
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="scanText"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "InvoiceApi.ScanTitleAsync", true)]
        public static async Task<ScanTitleResultJson> ScanTitleAsync(string accessTokenOrAppId, string scanText, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/invoice/scantitle?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    scan_text = scanText
                };

                return await CommonJsonSend.SendAsync<ScanTitleResultJson>(null, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        #endregion

        #endregion

    }
}
