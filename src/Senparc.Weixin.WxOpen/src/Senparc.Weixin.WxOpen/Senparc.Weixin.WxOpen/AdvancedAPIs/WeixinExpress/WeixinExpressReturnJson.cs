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

    文件名：WeixinExpressReturnJson.cs
    文件功能描述：WeixinExpressReturnJson 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WeixinExpress
{
    /// <summary>
    /// 微信物流退货组件按退货 ID 操作的请求。
    /// </summary>
    public class WeixinExpressReturnIdRequest
    {
        /// <summary>
        /// 微信物流退货 ID。
        /// </summary>
        public string return_id { get; set; }
    }

    /// <summary>
    /// 创建微信物流退货 ID 请求。
    /// </summary>
    public class WeixinExpressAddReturnIdRequest
    {
        /// <summary>
        /// 商家内部系统使用的退货编号；与退货 ID 一一对应。
        /// </summary>
        public string shop_order_id { get; set; }

        /// <summary>
        /// 商家退货地址。
        /// </summary>
        public WeixinExpressReturnAddress biz_addr { get; set; }

        /// <summary>
        /// 用户购物时的收货地址。
        /// </summary>
        public WeixinExpressReturnAddress user_addr { get; set; }

        /// <summary>
        /// 退货用户 OpenId，用于向用户下发选择退货方式的消息。
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 退货订单在商家小程序中的页面路径。
        /// </summary>
        public string order_path { get; set; }

        /// <summary>
        /// 退货商品列表。
        /// </summary>
        public IList<WeixinExpressReturnGoodsItem> goods_list { get; set; }

        /// <summary>
        /// 退货订单价格，单位为元。
        /// </summary>
        public decimal order_price { get; set; }

        /// <summary>
        /// 已投保的微信支付交易单号。官方参数表标为必填，但当前请求示例未填写。
        /// </summary>
        public string wx_pay_id { get; set; }
    }

    /// <summary>
    /// 微信物流退货地址。
    /// </summary>
    public class WeixinExpressReturnAddress
    {
        /// <summary>
        /// 联系人姓名，不超过 64 字节。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 座机号码；与手机号至少填写一项，不超过 32 字节。
        /// </summary>
        public string tel { get; set; }

        /// <summary>
        /// 手机号码；与座机至少填写一项，不超过 32 字节。
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 公司名称，不超过 64 字节。
        /// </summary>
        public string company { get; set; }

        /// <summary>
        /// 邮政编码，不超过 10 字节。
        /// </summary>
        public string post_code { get; set; }

        /// <summary>
        /// 国家或地区，不超过 64 字节。
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// 省份，不超过 64 字节。
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// 城市或地区，不超过 64 字节。
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 区或县，不超过 64 字节。
        /// </summary>
        public string area { get; set; }

        /// <summary>
        /// 详细地址，不超过 512 字节。
        /// </summary>
        public string address { get; set; }
    }

    /// <summary>
    /// 微信物流退货商品。
    /// </summary>
    public class WeixinExpressReturnGoodsItem
    {
        /// <summary>
        /// 退货商品名称。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 退货商品图片 URL。
        /// </summary>
        public string url { get; set; }
    }

    /// <summary>
    /// 创建微信物流退货 ID 结果。
    /// </summary>
    public class WeixinExpressAddReturnIdJsonResult : WxJsonResult
    {
        /// <summary>
        /// 微信生成的退货 ID。
        /// </summary>
        public string return_id { get; set; }
    }

    /// <summary>
    /// 查询微信物流退货 ID 状态结果。
    /// </summary>
    public class WeixinExpressGetReturnIdJsonResult : WxJsonResult
    {
        /// <summary>
        /// 退货方式：0 用户未填写，1 预约上门取件，2 自行寄回。
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 退货运单号。
        /// </summary>
        public string waybill_id { get; set; }

        /// <summary>
        /// 物流状态：0 待揽件、1 已揽件、2 运输中、3 派件中、4 已签收、5 异常、6 代签收、7 揽收失败、8 签收失败、11 已取消、13 退件中、14 已退件、99 未知。
        /// </summary>
        public int order_status { get; set; }

        /// <summary>
        /// 运力公司编码。
        /// </summary>
        public string delivery_id { get; set; }

        /// <summary>
        /// 运力公司名称。
        /// </summary>
        public string delivery_name { get; set; }
    }
}
