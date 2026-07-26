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

    文件名：LiveBroadcastGoodsJson.cs
    文件功能描述：LiveBroadcastGoodsJson 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.LiveBroadcast
{
    /// <summary>直播商品新增或更新信息。</summary>
    public class LiveBroadcastGoodsInfoRequest
    {
        /// <summary>商品封面图临时素材 mediaId。</summary>
        public string coverImgUrl { get; set; }

        /// <summary>商品名称，最长 14 个汉字。</summary>
        public string name { get; set; }

        /// <summary>价格类型：1 一口价，2 价格区间，3 折扣价。</summary>
        public int? priceType { get; set; }

        /// <summary>第一价格，单位元，最多两位小数。</summary>
        public decimal? price { get; set; }

        /// <summary>第二价格，价格区间或折扣价时使用。</summary>
        public decimal? price2 { get; set; }

        /// <summary>商品详情页小程序路径。</summary>
        public string url { get; set; }

        /// <summary>可选第三方商品小程序 AppId。</summary>
        public string thirdPartyAppid { get; set; }

        /// <summary>更新商品时必填的商品 ID；新增商品时不设置。</summary>
        public long? goodsId { get; set; }
    }

    /// <summary>新增或更新直播商品请求。</summary>
    public class LiveBroadcastGoodsRequest
    {
        /// <summary>商品信息。</summary>
        public LiveBroadcastGoodsInfoRequest goodsInfo { get; set; }
    }

    /// <summary>新增直播商品结果。</summary>
    public class LiveBroadcastAddGoodsJsonResult : WxJsonResult
    {
        /// <summary>商品 ID。</summary>
        public long goodsId { get; set; }

        /// <summary>审核单 ID。</summary>
        public long auditId { get; set; }
    }

    /// <summary>包含商品 ID 的请求。</summary>
    public class LiveBroadcastGoodsIdRequest
    {
        /// <summary>商品 ID。</summary>
        public long goodsId { get; set; }
    }

    /// <summary>重新提交商品审核结果。</summary>
    public class LiveBroadcastAuditIdJsonResult : WxJsonResult
    {
        /// <summary>审核单 ID。</summary>
        public long auditId { get; set; }
    }

    /// <summary>批量查询商品信息和审核状态请求。</summary>
    public class LiveBroadcastGetGoodsWarehouseRequest
    {
        /// <summary>商品 ID 列表，单次最多 20 个。</summary>
        public IList<long> goods_ids { get; set; }
    }

    /// <summary>直播商品信息。</summary>
    /// <remarks>
    /// 微信的商品仓库接口返回 snake_case，商品列表接口返回 camelCase；模型同时保留两套官方字段名。
    /// </remarks>
    public class LiveBroadcastGoodsInfo
    {
        /// <summary>商品仓库接口返回的商品 ID；官方表标为 string，示例也可能返回 number。</summary>
        public string goods_id { get; set; }

        /// <summary>商品列表接口返回的商品 ID。</summary>
        public string goodsId { get; set; }

        /// <summary>商品名称。</summary>
        public string name { get; set; }

        /// <summary>商品仓库接口返回的封面图 URL。</summary>
        public string cover_img_url { get; set; }

        /// <summary>商品列表接口返回的封面图 URL。</summary>
        public string coverImgUrl { get; set; }

        /// <summary>商品详情页小程序路径。</summary>
        public string url { get; set; }

        /// <summary>商品仓库接口返回的价格类型。</summary>
        public int price_type { get; set; }

        /// <summary>商品列表接口返回的价格类型。</summary>
        public int priceType { get; set; }

        /// <summary>第一价格，单位元。</summary>
        public decimal price { get; set; }

        /// <summary>第二价格。</summary>
        public decimal price2 { get; set; }

        /// <summary>审核状态；官方表标为 string，示例返回 number，因此使用可空整数。</summary>
        public int? audit_status { get; set; }

        /// <summary>商品仓库接口返回的第三方来源标记。</summary>
        public int third_party_tag { get; set; }

        /// <summary>商品列表接口返回的第三方来源标记。</summary>
        public int thirdPartyTag { get; set; }

        /// <summary>第三方商品小程序 AppId。</summary>
        public string thirdPartyAppid { get; set; }
    }

    /// <summary>商品信息或商品列表结果。</summary>
    public class LiveBroadcastGoodsListJsonResult : WxJsonResult
    {
        /// <summary>商品列表。</summary>
        public IList<LiveBroadcastGoodsInfo> goods { get; set; }

        /// <summary>商品数量。</summary>
        public int total { get; set; }
    }

    /// <summary>撤回商品审核请求。</summary>
    public class LiveBroadcastResetGoodsAuditRequest
    {
        /// <summary>商品 ID。</summary>
        public long goodsId { get; set; }

        /// <summary>审核单 ID。</summary>
        public long auditId { get; set; }
    }
}
