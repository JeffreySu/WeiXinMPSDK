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

    文件名：WeixinExpressProviderJson.cs
    文件功能描述：WeixinExpressProviderJson 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WeixinExpress
{
    /// <summary>
    /// 运力方查询用户手机绑定状态请求。
    /// </summary>
    public class WeixinExpressUserQueryRequest
    {
        /// <summary>
        /// 待查询的手机号码。
        /// </summary>
        public string phone { get; set; }
    }

    /// <summary>
    /// 运力方查询用户绑定状态或推送轨迹的结果。
    /// </summary>
    public class WeixinExpressUserBindingJsonResult : WxJsonResult
    {
        /// <summary>
        /// 用户是否已绑定该手机号：0 未绑定，1 已绑定。
        /// </summary>
        public int exist { get; set; }
    }

    /// <summary>
    /// 运力方推送已绑定物流轨迹请求。
    /// </summary>
    public class WeixinExpressPathNotifyRequest
    {
        /// <summary>
        /// 寄件人信息。
        /// </summary>
        public WeixinExpressPathContact sender { get; set; }

        /// <summary>
        /// 收件人信息。
        /// </summary>
        public WeixinExpressPathContact receiver { get; set; }

        /// <summary>
        /// 运单号。
        /// </summary>
        public string waybill_id { get; set; }

        /// <summary>
        /// 本次需要推送的轨迹节点。
        /// </summary>
        public WeixinExpressPathNode path { get; set; }

        /// <summary>
        /// 运单创建时间，Unix 秒级时间戳。官方参数表标为必填，但当前请求示例未填写。
        /// </summary>
        public long create_time { get; set; }
    }

    /// <summary>
    /// 运力方物流轨迹联系人地址。
    /// </summary>
    public class WeixinExpressPathContact
    {
        /// <summary>
        /// 姓名；收件人必填，寄件人可不填。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 电话；收件人必填，寄件人可不填。
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 省份。
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// 城市。
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 区或县。
        /// </summary>
        public string area { get; set; }

        /// <summary>
        /// 街道。
        /// </summary>
        public string street { get; set; }

        /// <summary>
        /// 详细地址。
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 地址 ID。
        /// </summary>
        public string id { get; set; }
    }

    /// <summary>
    /// 运力方物流轨迹节点。
    /// </summary>
    public class WeixinExpressPathNode
    {
        /// <summary>
        /// 轨迹变化时间，Unix 秒级时间戳。
        /// </summary>
        public long action_time { get; set; }

        /// <summary>
        /// 轨迹类型，例如 100001 揽件成功、200001 运输轨迹、300003 签收成功、400001 订单取消。
        /// </summary>
        public int action_type { get; set; }

        /// <summary>
        /// 展示在快递轨迹详情页中的节点说明。
        /// </summary>
        public string action_msg { get; set; }

        /// <summary>
        /// 取件员姓名。
        /// </summary>
        public string pickup_courier_name { get; set; }

        /// <summary>
        /// 取件员电话。
        /// </summary>
        public string pickup_courier_phone { get; set; }

        /// <summary>
        /// 派件员姓名。
        /// </summary>
        public string delivery_courier_name { get; set; }

        /// <summary>
        /// 派件员电话。
        /// </summary>
        public string delivery_courier_phone { get; set; }
    }
}
