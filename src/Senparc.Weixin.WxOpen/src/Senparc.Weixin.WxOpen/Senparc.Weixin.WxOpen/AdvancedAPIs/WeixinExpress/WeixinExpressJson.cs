#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2026 Senparc

    文件名：WeixinExpressJson.cs
    文件功能描述：WeixinExpressJson 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WeixinExpress
{
    /// <summary>
    /// 微信物流服务上传运单请求。
    /// </summary>
    public class WeixinExpressTraceWaybillRequest
    {
        /// <summary>
        /// 用户 OpenId。
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 寄件人手机号。
        /// </summary>
        public string sender_phone { get; set; }

        /// <summary>
        /// 收件人手机号；部分运力使用手机号作为查单依据。
        /// </summary>
        public string receiver_phone { get; set; }

        /// <summary>
        /// 运单号。
        /// </summary>
        public string waybill_id { get; set; }

        /// <summary>
        /// 商品信息。
        /// </summary>
        public WeixinExpressGoodsInfo goods_info { get; set; }

        /// <summary>
        /// 微信支付交易单号，通常以 420 开头。官方参数表标为必填，但当前示例未填写。
        /// </summary>
        public string trans_id { get; set; }

        /// <summary>
        /// 用户点击物流商品卡片后的跳转路径，建议填写订单详情页。官方参数表标为必填，说明中又允许不传时跳转首页。
        /// </summary>
        public string order_detail_path { get; set; }

        /// <summary>
        /// 运力公司 ID，可通过获取运力 ID 列表接口获得；非主流快递建议填写以提高识别准确度。
        /// </summary>
        public string delivery_id { get; set; }
    }

    /// <summary>
    /// 微信物流服务查询运单请求。
    /// </summary>
    public class WeixinExpressQueryTraceRequest
    {
        /// <summary>
        /// 上传运单接口返回的查询 Token。
        /// </summary>
        public string waybill_token { get; set; }

        /// <summary>
        /// 用户 OpenId。
        /// </summary>
        public string openid { get; set; }
    }

    /// <summary>
    /// 微信物流服务更新运单商品请求。
    /// </summary>
    public class WeixinExpressUpdateGoodsRequest
    {
        /// <summary>
        /// 上传运单接口返回的查询 Token。
        /// </summary>
        public string waybill_token { get; set; }

        /// <summary>
        /// 用户 OpenId。官方请求示例包含该字段，当前参数表未单独列出。
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 更新后的商品信息。
        /// </summary>
        public WeixinExpressGoodsInfo goods_info { get; set; }
    }

    /// <summary>
    /// 微信物流服务商品信息。
    /// </summary>
    public class WeixinExpressGoodsInfo
    {
        /// <summary>
        /// 商品明细列表。
        /// </summary>
        public IList<WeixinExpressGoodsItem> detail_list { get; set; }
    }

    /// <summary>
    /// 微信物流服务商品明细。
    /// </summary>
    public class WeixinExpressGoodsItem
    {
        /// <summary>
        /// 商品名称；查询组件要求 UTF-8 编码后不超过 60 个字符。
        /// </summary>
        public string goods_name { get; set; }

        /// <summary>
        /// 商品图片 URL。
        /// </summary>
        public string goods_img_url { get; set; }

        /// <summary>
        /// 商品详情描述，最多 40 个汉字；消息组件不填写时默认使用商品名称。
        /// </summary>
        public string goods_desc { get; set; }
    }

    /// <summary>
    /// 微信物流服务上传运单结果。
    /// </summary>
    public class WeixinExpressTraceWaybillJsonResult : WxJsonResult
    {
        /// <summary>
        /// 后续查询或更新运单时使用的 Token。
        /// </summary>
        public string waybill_token { get; set; }
    }

    /// <summary>
    /// 微信物流服务运力公司信息。
    /// </summary>
    public class WeixinExpressDeliveryInfo
    {
        /// <summary>
        /// 运力公司 ID。
        /// </summary>
        public string delivery_id { get; set; }

        /// <summary>
        /// 运力公司名称。
        /// </summary>
        public string delivery_name { get; set; }
    }

    /// <summary>
    /// 微信物流服务运力 ID 列表结果。
    /// </summary>
    public class WeixinExpressDeliveryListJsonResult : WxJsonResult
    {
        /// <summary>
        /// 运力公司列表。
        /// </summary>
        public IList<WeixinExpressDeliveryInfo> delivery_list { get; set; }

        /// <summary>
        /// 运力公司数量。
        /// </summary>
        public int count { get; set; }
    }

    /// <summary>
    /// 微信物流服务运单信息。
    /// </summary>
    public class WeixinExpressWaybillInfo
    {
        /// <summary>
        /// 运单状态：0 不存在或未揽收、1 已揽件、2 运输中、3 派件中、4 已签收、5 异常、6 代签收。
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 运单号。
        /// </summary>
        public string waybill_id { get; set; }
    }

    /// <summary>
    /// 微信物流服务商品展示信息。
    /// </summary>
    public class WeixinExpressShopInfo
    {
        /// <summary>
        /// 商品信息。
        /// </summary>
        public WeixinExpressGoodsInfo goods_info { get; set; }
    }

    /// <summary>
    /// 微信物流服务查询运单结果。
    /// </summary>
    public class WeixinExpressQueryTraceJsonResult : WxJsonResult
    {
        /// <summary>
        /// 运单号及当前状态。
        /// </summary>
        public WeixinExpressWaybillInfo waybill_info { get; set; }

        /// <summary>
        /// 商品展示信息。
        /// </summary>
        public WeixinExpressShopInfo shop_info { get; set; }

        /// <summary>
        /// 运力公司信息。
        /// </summary>
        public WeixinExpressDeliveryInfo delivery_info { get; set; }
    }
}
