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

    文件名：LiveBroadcastRoomJson.cs
    文件功能描述：LiveBroadcastRoomJson 强类型数据模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.LiveBroadcast
{
    /// <summary>创建小程序直播间请求。</summary>
    public class LiveBroadcastCreateRoomRequest
    {
        /// <summary>直播间名字，最短 3 个汉字、最长 17 个汉字。</summary>
        public string name { get; set; }

        /// <summary>直播间背景图临时素材 mediaId。</summary>
        public string coverImg { get; set; }

        /// <summary>计划开始 Unix 时间戳，需晚于当前时间至少 10 分钟。</summary>
        public long startTime { get; set; }

        /// <summary>计划结束 Unix 时间戳，与开始时间间隔 30 分钟至 24 小时。</summary>
        public long endTime { get; set; }

        /// <summary>主播昵称。</summary>
        public string anchorName { get; set; }

        /// <summary>已实名认证的主播微信号。</summary>
        public string anchorWechat { get; set; }

        /// <summary>可选已实名认证的主播副号微信号。</summary>
        public string subAnchorWechat { get; set; }

        /// <summary>可选创建者微信号；设置后直播间仅指定成员可见。</summary>
        public string createrWechat { get; set; }

        /// <summary>直播间分享图临时素材 mediaId。</summary>
        public string shareImg { get; set; }

        /// <summary>购物直播频道封面图临时素材 mediaId。</summary>
        public string feedsImg { get; set; }

        /// <summary>是否开启官方收录：1 开启，0 关闭；默认开启。</summary>
        public int? isFeedsPublic { get; set; }

        /// <summary>直播类型：1 推流，0 手机直播。</summary>
        public int type { get; set; }

        /// <summary>是否关闭点赞：0 开启，1 关闭。</summary>
        public int closeLike { get; set; }

        /// <summary>是否关闭货架：0 开启，1 关闭。</summary>
        public int closeGoods { get; set; }

        /// <summary>是否关闭评论：0 开启，1 关闭。</summary>
        public int closeComment { get; set; }

        /// <summary>是否关闭回放：0 开启，1 关闭。</summary>
        public int? closeReplay { get; set; }

        /// <summary>是否关闭分享：0 开启，1 关闭。</summary>
        public int? closeShare { get; set; }

        /// <summary>是否关闭客服：0 开启，1 关闭。</summary>
        public int? closeKf { get; set; }
    }

    /// <summary>编辑小程序直播间请求。</summary>
    public class LiveBroadcastEditRoomRequest
    {
        /// <summary>直播间 ID。</summary>
        public long id { get; set; }

        /// <summary>直播间名字。</summary>
        public string name { get; set; }

        /// <summary>直播间背景图临时素材 mediaId。</summary>
        public string coverImg { get; set; }

        /// <summary>计划开始 Unix 时间戳。</summary>
        public long startTime { get; set; }

        /// <summary>计划结束 Unix 时间戳。</summary>
        public long endTime { get; set; }

        /// <summary>主播昵称。</summary>
        public string anchorName { get; set; }

        /// <summary>主播微信号。</summary>
        public string anchorWechat { get; set; }

        /// <summary>分享图临时素材 mediaId。</summary>
        public string shareImg { get; set; }

        /// <summary>购物直播频道封面图临时素材 mediaId。</summary>
        public string feedsImg { get; set; }

        /// <summary>是否开启官方收录：1 开启，0 关闭。</summary>
        public int? isFeedsPublic { get; set; }

        /// <summary>是否关闭点赞：0 开启，1 关闭。</summary>
        public int closeLike { get; set; }

        /// <summary>是否关闭货架：0 开启，1 关闭。</summary>
        public int closeGoods { get; set; }

        /// <summary>是否关闭评论：0 开启，1 关闭。</summary>
        public int closeComment { get; set; }

        /// <summary>是否关闭回放：0 开启，1 关闭。</summary>
        public int? closeReplay { get; set; }

        /// <summary>是否关闭分享：0 开启，1 关闭。</summary>
        public int? closeShare { get; set; }

        /// <summary>是否关闭客服：0 开启，1 关闭。</summary>
        public int? closeKf { get; set; }
    }

    /// <summary>创建直播间结果。</summary>
    public class LiveBroadcastCreateRoomJsonResult : WxJsonResult
    {
        /// <summary>直播间 ID。</summary>
        public long roomId { get; set; }

        /// <summary>小程序直播小程序码 URL。</summary>
        public string qrcode_url { get; set; }
    }

    /// <summary>查询直播间列表或回放请求。</summary>
    public class LiveBroadcastGetLiveInfoRequest
    {
        /// <summary>起始偏移量，0 表示从第一条开始。</summary>
        public int start { get; set; }

        /// <summary>拉取数量，建议不超过 100。</summary>
        public int limit { get; set; }

        /// <summary>获取回放时填写 get_replay。</summary>
        public string action { get; set; }

        /// <summary>获取回放时必填的直播间 ID。</summary>
        public long? room_id { get; set; }
    }

    /// <summary>直播间中的商品信息。</summary>
    public class LiveBroadcastRoomGoodsInfo
    {
        /// <summary>商品名称。</summary>
        public string name { get; set; }

        /// <summary>商品封面图 URL。</summary>
        public string cover_img { get; set; }

        /// <summary>商品详情小程序路径。</summary>
        public string url { get; set; }

        /// <summary>商品价格，单位分。</summary>
        public long price { get; set; }

        /// <summary>第二价格，含义由 price_type 决定。</summary>
        public long price2 { get; set; }

        /// <summary>价格类型：1 一口价，2 价格区间，3 折扣价。</summary>
        public int price_type { get; set; }

        /// <summary>商品 ID。</summary>
        public long goods_id { get; set; }

        /// <summary>第三方商品小程序 AppId；当前小程序商品为空。</summary>
        public string third_party_appid { get; set; }
    }

    /// <summary>直播间信息。</summary>
    public class LiveBroadcastRoomInfo
    {
        /// <summary>直播间名称。</summary>
        public string name { get; set; }

        /// <summary>直播间背景图 URL。</summary>
        public string cover_img { get; set; }

        /// <summary>计划开始 Unix 时间戳。</summary>
        public long start_time { get; set; }

        /// <summary>计划结束 Unix 时间戳。</summary>
        public long end_time { get; set; }

        /// <summary>主播名称。</summary>
        public string anchor_name { get; set; }

        /// <summary>直播间 ID。</summary>
        public long roomid { get; set; }

        /// <summary>直播间商品列表。</summary>
        public IList<LiveBroadcastRoomGoodsInfo> goods { get; set; }

        /// <summary>直播状态：101 直播中、102 未开始、103 已结束等。</summary>
        public int live_status { get; set; }

        /// <summary>分享图 URL。</summary>
        public string share_img { get; set; }

        /// <summary>直播类型：1 推流，0 手机直播。</summary>
        public int live_type { get; set; }

        /// <summary>是否关闭点赞。</summary>
        public int close_like { get; set; }

        /// <summary>是否关闭货架。</summary>
        public int close_goods { get; set; }

        /// <summary>是否关闭评论。</summary>
        public int close_comment { get; set; }

        /// <summary>是否关闭客服。</summary>
        public int close_kf { get; set; }

        /// <summary>是否关闭回放。</summary>
        public int close_replay { get; set; }

        /// <summary>是否开启官方收录。</summary>
        public int is_feeds_public { get; set; }

        /// <summary>创建者 OpenId。</summary>
        public string creater_openid { get; set; }

        /// <summary>官方收录封面图 URL。</summary>
        public string feeds_img { get; set; }
    }

    /// <summary>直播回放信息。</summary>
    public class LiveBroadcastReplayInfo
    {
        /// <summary>回放视频创建时间。</summary>
        public string create_time { get; set; }

        /// <summary>回放视频 URL 过期时间。</summary>
        public string expire_time { get; set; }

        /// <summary>回放视频 URL。</summary>
        public string media_url { get; set; }
    }

    /// <summary>直播间列表或回放结果。</summary>
    public class LiveBroadcastGetLiveInfoJsonResult : WxJsonResult
    {
        /// <summary>直播间列表；获取回放时不返回。</summary>
        public IList<LiveBroadcastRoomInfo> room_info { get; set; }

        /// <summary>直播间总数。</summary>
        public int total { get; set; }

        /// <summary>回放列表；action=get_replay 时返回。</summary>
        public IList<LiveBroadcastReplayInfo> live_replay { get; set; }
    }

    /// <summary>包含直播间 id 字段的请求。</summary>
    public class LiveBroadcastIdRequest
    {
        /// <summary>直播间 ID。</summary>
        public long id { get; set; }
    }

    /// <summary>包含直播间 roomId 字段的请求。</summary>
    public class LiveBroadcastRoomIdRequest
    {
        /// <summary>直播间 ID。</summary>
        public long roomId { get; set; }
    }

    /// <summary>向直播间导入商品请求。</summary>
    public class LiveBroadcastImportGoodsRequest
    {
        /// <summary>商品 ID 列表。</summary>
        public IList<long> ids { get; set; }

        /// <summary>直播间 ID。</summary>
        public long roomId { get; set; }
    }

    /// <summary>直播间推流地址结果。</summary>
    public class LiveBroadcastPushUrlJsonResult : WxJsonResult
    {
        /// <summary>直播间 RTMP 推流地址。</summary>
        public string pushAddr { get; set; }
    }

    /// <summary>直播间分享二维码结果。</summary>
    public class LiveBroadcastSharedCodeJsonResult : WxJsonResult
    {
        /// <summary>分享二维码 CDN URL。</summary>
        public string cdnUrl { get; set; }

        /// <summary>分享页面路径。</summary>
        public string pagePath { get; set; }

        /// <summary>分享海报 URL。</summary>
        public string posterUrl { get; set; }
    }

    /// <summary>主播副号查询结果。</summary>
    public class LiveBroadcastSubAnchorJsonResult : WxJsonResult
    {
        /// <summary>主播副号微信号。</summary>
        public string username { get; set; }
    }

    /// <summary>直播间用户请求。</summary>
    public class LiveBroadcastRoomUserRequest
    {
        /// <summary>直播间 ID。</summary>
        public long roomId { get; set; }

        /// <summary>用户微信号。</summary>
        public string username { get; set; }
    }

    /// <summary>直播间商品操作请求。</summary>
    public class LiveBroadcastRoomGoodsRequest
    {
        /// <summary>直播间 ID。</summary>
        public long roomId { get; set; }

        /// <summary>商品 ID。</summary>
        public long goodsId { get; set; }
    }

    /// <summary>直播间商品上下架请求。</summary>
    public class LiveBroadcastGoodsOnSaleRequest : LiveBroadcastRoomGoodsRequest
    {
        /// <summary>上下架状态：0 下架，1 上架。</summary>
        public int onSale { get; set; }
    }

    /// <summary>直播间商品 ID 项。</summary>
    public class LiveBroadcastGoodsId
    {
        /// <summary>商品 ID。官方参数表为 number，示例同时出现字符串表示。</summary>
        public long goodsId { get; set; }
    }

    /// <summary>直播间商品排序请求。</summary>
    public class LiveBroadcastSortGoodsRequest
    {
        /// <summary>直播间 ID。</summary>
        public long roomId { get; set; }

        /// <summary>按预期顺序排列的商品 ID 列表。</summary>
        public IList<LiveBroadcastGoodsId> goods { get; set; }
    }

    /// <summary>直播间小助手新增或修改项。</summary>
    public class LiveBroadcastAssistantUser
    {
        /// <summary>用户微信号。</summary>
        public string username { get; set; }

        /// <summary>用户昵称。</summary>
        public string nickname { get; set; }
    }

    /// <summary>修改单个直播间小助手请求。</summary>
    public class LiveBroadcastModifyAssistantRequest : LiveBroadcastAssistantUser
    {
        /// <summary>直播间 ID。</summary>
        public long roomId { get; set; }
    }

    /// <summary>批量添加直播间小助手请求。</summary>
    public class LiveBroadcastAddAssistantsRequest
    {
        /// <summary>直播间 ID。</summary>
        public long roomId { get; set; }

        /// <summary>小助手用户列表。</summary>
        public IList<LiveBroadcastAssistantUser> users { get; set; }
    }

    /// <summary>直播间小助手信息。</summary>
    public class LiveBroadcastAssistantInfo
    {
        /// <summary>修改 Unix 时间戳。</summary>
        public long timestamp { get; set; }

        /// <summary>头像 URL。</summary>
        public string headimg { get; set; }

        /// <summary>昵称。</summary>
        public string nickname { get; set; }

        /// <summary>微信号。</summary>
        public string alias { get; set; }

        /// <summary>用户 OpenId。</summary>
        public string openid { get; set; }
    }

    /// <summary>直播间小助手列表结果。</summary>
    public class LiveBroadcastAssistantListJsonResult : WxJsonResult
    {
        /// <summary>小助手列表。</summary>
        public IList<LiveBroadcastAssistantInfo> list { get; set; }

        /// <summary>当前小助手数量。</summary>
        public int count { get; set; }

        /// <summary>小助手数量上限。</summary>
        public int maxCount { get; set; }
    }

    /// <summary>直播间禁言设置请求。</summary>
    public class LiveBroadcastUpdateCommentRequest
    {
        /// <summary>直播间 ID。官方参数表使用 id，示例误写为 roomId。</summary>
        public long id { get; set; }

        /// <summary>禁言状态：1 禁言，0 取消禁言。</summary>
        public int banComment { get; set; }
    }

    /// <summary>直播间官方收录设置请求。</summary>
    public class LiveBroadcastUpdateFeedPublicRequest
    {
        /// <summary>直播间 ID。</summary>
        public long roomId { get; set; }

        /// <summary>是否开启官方收录：1 开启，0 关闭。</summary>
        public int isFeedsPublic { get; set; }
    }

    /// <summary>直播间客服设置请求。</summary>
    public class LiveBroadcastUpdateCustomerServiceRequest
    {
        /// <summary>直播间 ID。</summary>
        public long roomId { get; set; }

        /// <summary>是否关闭客服：0 开启，1 关闭。</summary>
        public int closeKf { get; set; }
    }

    /// <summary>直播间回放设置请求。</summary>
    public class LiveBroadcastUpdateReplayRequest
    {
        /// <summary>直播间 ID。</summary>
        public long roomId { get; set; }

        /// <summary>是否关闭回放：0 开启，1 关闭。</summary>
        public int closeReplay { get; set; }
    }

    /// <summary>商品讲解视频结果。</summary>
    public class LiveBroadcastGoodsVideoJsonResult : WxJsonResult
    {
        /// <summary>商品讲解视频下载 URL。</summary>
        public string url { get; set; }
    }

    /// <summary>设置直播挂件全局商品 Key 请求。</summary>
    public class LiveBroadcastSetGoodsKeyRequest
    {
        /// <summary>全局商品 Key 字段列表。</summary>
        public IList<string> goodsKey { get; set; }
    }

    /// <summary>直播挂件全局商品 Key 结果。</summary>
    public class LiveBroadcastGoodsKeyJsonResult : WxJsonResult
    {
        /// <summary>当前全局商品 Key 字段列表。</summary>
        public IList<string> vendorGoodsKey { get; set; }
    }
}
