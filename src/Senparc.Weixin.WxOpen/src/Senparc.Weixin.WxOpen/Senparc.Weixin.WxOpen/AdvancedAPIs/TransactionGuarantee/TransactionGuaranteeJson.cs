#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License
is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：TransactionGuaranteeJson.cs
    文件功能描述：TransactionGuaranteeJson 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.TransactionGuarantee
{
    #region 基础能力

    /// <summary>
    /// 小程序交易体验分违规记录。
    /// </summary>
    public class TransactionGuaranteePenaltyRecord
    {
        /// <summary>
        /// 扣分记录 ID。官方参数表标记为数字，但示例使用字符串，因此使用字符串避免精度损失。
        /// </summary>
        public string illegalOrderId { get; set; }

        /// <summary>
        /// 投诉单 ID。官方参数表标记为数字，但示例使用字符串，因此使用字符串避免精度损失。
        /// </summary>
        public string complaintOrderId { get; set; }

        /// <summary>
        /// 违规行为说明。
        /// </summary>
        public string illegalWording { get; set; }

        /// <summary>
        /// 扣分记录状态：2 扣分审批通过，4 申诉中，5 申诉驳回，6 申诉成功。
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 扣除分数。
        /// </summary>
        public int minusScore { get; set; }

        /// <summary>
        /// 关联订单号；可能包含字母和特殊前缀，不能按数字处理。
        /// </summary>
        public string orderId { get; set; }

        /// <summary>
        /// 扣分记录创建时间，Unix 秒级时间戳。
        /// </summary>
        public long illegalTime { get; set; }

        /// <summary>
        /// 记录更新时间，Unix 秒级时间戳。
        /// </summary>
        public long updateTime { get; set; }
    }

    /// <summary>
    /// 获取小程序交易体验分违规记录结果。
    /// </summary>
    public class TransactionGuaranteePenaltyListJsonResult : WxJsonResult
    {
        /// <summary>
        /// 违规和申诉记录列表。
        /// </summary>
        public IList<TransactionGuaranteePenaltyRecord> appealList { get; set; }

        /// <summary>
        /// 当前小程序交易体验分。
        /// </summary>
        public int currentScore { get; set; }

        /// <summary>
        /// 当前小程序扣分记录总数。
        /// </summary>
        public int totalNum { get; set; }
    }

    /// <summary>
    /// 获取小程序交易保障标状态结果。
    /// </summary>
    public class TransactionGuaranteeStatusJsonResult : WxJsonResult
    {
        /// <summary>
        /// 是否已经激活交易保障标。
        /// </summary>
        public bool isActived { get; set; }

        /// <summary>
        /// 保障标状态说明。
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 未能开通保障标的原因列表。
        /// </summary>
        public IList<string> reasons { get; set; }
    }

    #endregion

    #region 评价与评论请求

    /// <summary>
    /// 小程序交易评价列表查询条件。
    /// </summary>
    public class TransactionGuaranteeCommentListRequest
    {
        /// <summary>
        /// 查询开始时间，Unix 秒级时间戳。
        /// </summary>
        public long startTime { get; set; }

        /// <summary>
        /// 查询结束时间，Unix 秒级时间戳。
        /// </summary>
        public long endTime { get; set; }

        /// <summary>
        /// 可选过滤类型：1 全部差评，2 全部好评，3 待处理差评，4 待开发者回复，5 已改评差评，6 全部评价。
        /// </summary>
        public int? filterType { get; set; }

        /// <summary>
        /// 可选偏移量，默认值为 0。
        /// </summary>
        public int? offset { get; set; }

        /// <summary>
        /// 可选每页数量，默认值为 8。
        /// </summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 仅包含评价 ID 的请求。
    /// </summary>
    public class TransactionGuaranteeCommentIdRequest
    {
        /// <summary>
        /// 评价 ID。
        /// </summary>
        public string commentId { get; set; }
    }

    /// <summary>
    /// 创建商家评论请求。
    /// </summary>
    public class TransactionGuaranteeAddReplyRequest : TransactionGuaranteeCommentIdRequest
    {
        /// <summary>
        /// 评论内容。
        /// </summary>
        public string content { get; set; }
    }

    /// <summary>
    /// 删除商家评论请求。
    /// </summary>
    public class TransactionGuaranteeDeleteReplyRequest : TransactionGuaranteeCommentIdRequest
    {
        /// <summary>
        /// 需要删除的评论 ID。
        /// </summary>
        public string replyId { get; set; }
    }

    /// <summary>
    /// 创建评论回复请求。
    /// </summary>
    public class TransactionGuaranteeAddCommentReplyRequest : TransactionGuaranteeDeleteReplyRequest
    {
        /// <summary>
        /// 回复评论的内容。
        /// </summary>
        public string content { get; set; }
    }

    /// <summary>
    /// 删除评论回复请求。
    /// </summary>
    public class TransactionGuaranteeDeleteCommentReplyRequest : TransactionGuaranteeDeleteReplyRequest
    {
        /// <summary>
        /// 需要删除的评论回复 ID。
        /// </summary>
        public string commentReplyId { get; set; }
    }

    /// <summary>
    /// 确认差评和解请求。
    /// </summary>
    public class TransactionGuaranteeCompromiseRequest : TransactionGuaranteeCommentIdRequest
    {
        /// <summary>
        /// 和解图片的临时素材 media_id 列表。
        /// </summary>
        public IList<string> picList { get; set; }

        /// <summary>
        /// 和解说明文本。
        /// </summary>
        public string content { get; set; }
    }

    #endregion

    #region 评价与评论返回模型

    /// <summary>
    /// 商家订单信息。
    /// </summary>
    public class TransactionGuaranteeOrderInfo
    {
        /// <summary>
        /// 商户系统内部订单号，对应微信支付 out_trade_no。
        /// </summary>
        public string busiOrderId { get; set; }
    }

    /// <summary>
    /// 评价用户信息。
    /// </summary>
    public class TransactionGuaranteeUserInfo
    {
        /// <summary>
        /// 评价用户 OpenId。
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 用户头像地址。
        /// </summary>
        public string headImg { get; set; }

        /// <summary>
        /// 用户昵称。
        /// </summary>
        public string nickName { get; set; }
    }

    /// <summary>
    /// 被评价的小程序信息。
    /// </summary>
    public class TransactionGuaranteeBusinessInfo
    {
        /// <summary>
        /// 商家小程序 AppId。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 商家小程序头像地址。
        /// </summary>
        public string headImg { get; set; }

        /// <summary>
        /// 商家小程序昵称。
        /// </summary>
        public string nickName { get; set; }
    }

    /// <summary>
    /// 评价媒体信息。
    /// </summary>
    public class TransactionGuaranteeCommentMedia
    {
        /// <summary>
        /// 图片 CDN 地址；存在视频时通常为空。
        /// </summary>
        public string img { get; set; }

        /// <summary>
        /// 图片缩略图 CDN 地址。
        /// </summary>
        public string thumbImg { get; set; }

        /// <summary>
        /// 视频资源 CDN 地址；存在图片时通常为空。
        /// </summary>
        public string video { get; set; }

        /// <summary>
        /// 视频封面地址。
        /// </summary>
        public string videoCover { get; set; }

        /// <summary>
        /// 视频时长，单位为秒。
        /// </summary>
        public int? videoDuration { get; set; }
    }

    /// <summary>
    /// 评价正文和媒体内容。
    /// </summary>
    public class TransactionGuaranteeCommentContent
    {
        /// <summary>
        /// 评价文本。
        /// </summary>
        public string txt { get; set; }

        /// <summary>
        /// 评价图片或视频列表；视频和图片不会同时存在。
        /// </summary>
        public IList<TransactionGuaranteeCommentMedia> media { get; set; }
    }

    /// <summary>
    /// 评价额外信息。
    /// </summary>
    public class TransactionGuaranteeCommentExtraInfo
    {
        /// <summary>
        /// 是否已经发送过差评客服会话。
        /// </summary>
        public bool isAlreadySendTmpl { get; set; }
    }

    /// <summary>
    /// 评价商品条目。
    /// </summary>
    public class TransactionGuaranteeProduct
    {
        /// <summary>
        /// 商品名称。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 商品图片地址。
        /// </summary>
        public string picUrl { get; set; }
    }

    /// <summary>
    /// 评价关联的商品信息。
    /// </summary>
    public class TransactionGuaranteeProductInfo
    {
        /// <summary>
        /// 商品列表。
        /// </summary>
        public IList<TransactionGuaranteeProduct> productList { get; set; }
    }

    /// <summary>
    /// 小程序交易评价条目。
    /// </summary>
    public class TransactionGuaranteeCommentItem
    {
        /// <summary>
        /// 评价 ID。
        /// </summary>
        public string commentId { get; set; }

        /// <summary>
        /// 订单金额，单位为分。官方不同页面对类型标记不一致，使用长整型兼容数值示例。
        /// </summary>
        public long amount { get; set; }

        /// <summary>
        /// 微信侧订单 ID。
        /// </summary>
        public string orderId { get; set; }

        /// <summary>
        /// 评价创建时间，官方以秒级时间戳字符串返回。
        /// </summary>
        public string createTime { get; set; }

        /// <summary>
        /// 支付时间，官方以秒级时间戳字符串返回。
        /// </summary>
        public string payTime { get; set; }

        /// <summary>
        /// 微信支付交易单号，对应 transaction_id。
        /// </summary>
        public string wxPayId { get; set; }

        /// <summary>
        /// 商家订单信息。
        /// </summary>
        public TransactionGuaranteeOrderInfo orderInfo { get; set; }

        /// <summary>
        /// 评价用户信息。
        /// </summary>
        public TransactionGuaranteeUserInfo userInfo { get; set; }

        /// <summary>
        /// 商家小程序信息。
        /// </summary>
        public TransactionGuaranteeBusinessInfo bizInfo { get; set; }

        /// <summary>
        /// 评价分数，每 100 分对应一颗星。
        /// </summary>
        public int score { get; set; }

        /// <summary>
        /// 评价正文和媒体内容。
        /// </summary>
        public TransactionGuaranteeCommentContent content { get; set; }

        /// <summary>
        /// 评价额外信息。
        /// </summary>
        public TransactionGuaranteeCommentExtraInfo extInfo { get; set; }

        /// <summary>
        /// 评价关联商品信息。
        /// </summary>
        public TransactionGuaranteeProductInfo productInfo { get; set; }
    }

    /// <summary>
    /// 查询小程序交易评价列表结果。
    /// </summary>
    public class TransactionGuaranteeCommentListJsonResult : WxJsonResult
    {
        /// <summary>
        /// 请求是否成功。该字段存在于官方示例中，但未列入返回参数表。
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// 当前查询偏移量。
        /// </summary>
        public int offset { get; set; }

        /// <summary>
        /// 评价总数。
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 评价列表。
        /// </summary>
        public IList<TransactionGuaranteeCommentItem> commentList { get; set; }
    }

    /// <summary>
    /// 评论或回复的正文。
    /// </summary>
    public class TransactionGuaranteeReplyContent
    {
        /// <summary>
        /// 评论或回复文本。
        /// </summary>
        public string content { get; set; }
    }

    /// <summary>
    /// 评论或回复的发布者信息。
    /// </summary>
    public class TransactionGuaranteeReplyObject
    {
        /// <summary>
        /// 发布者昵称。
        /// </summary>
        public string nickname { get; set; }

        /// <summary>
        /// 发布者头像地址。
        /// </summary>
        public string imgUrl { get; set; }
    }

    /// <summary>
    /// 评价下的第一条商家评论。
    /// </summary>
    public class TransactionGuaranteeReply
    {
        /// <summary>
        /// 评价 ID。
        /// </summary>
        public string commentId { get; set; }

        /// <summary>
        /// 评论 ID。
        /// </summary>
        public string replyId { get; set; }

        /// <summary>
        /// 创建时间，Unix 秒级时间戳字符串。
        /// </summary>
        public string createTime { get; set; }

        /// <summary>
        /// 更新时间，Unix 秒级时间戳字符串。
        /// </summary>
        public string updateTime { get; set; }

        /// <summary>
        /// 评论正文。
        /// </summary>
        public TransactionGuaranteeReplyContent replyContent { get; set; }

        /// <summary>
        /// 评论发布者信息。
        /// </summary>
        public TransactionGuaranteeReplyObject replyObject { get; set; }
    }

    /// <summary>
    /// 对商家评论的后续回复。
    /// </summary>
    public class TransactionGuaranteeCommentReply
    {
        /// <summary>
        /// 评价 ID。
        /// </summary>
        public string commentId { get; set; }

        /// <summary>
        /// 评论回复 ID。
        /// </summary>
        public string commentReplyId { get; set; }

        /// <summary>
        /// 创建时间，Unix 秒级时间戳字符串。
        /// </summary>
        public string createTime { get; set; }

        /// <summary>
        /// 更新时间，Unix 秒级时间戳字符串。
        /// </summary>
        public string updateTime { get; set; }

        /// <summary>
        /// 回复正文。
        /// </summary>
        public TransactionGuaranteeReplyContent commentReplyContent { get; set; }

        /// <summary>
        /// 回复发布者信息。
        /// </summary>
        public TransactionGuaranteeReplyObject commentReplyObject { get; set; }
    }

    /// <summary>
    /// 评价下的评论和回复集合。
    /// </summary>
    public class TransactionGuaranteeReplyCollection
    {
        /// <summary>
        /// 第一条商家评论。
        /// </summary>
        public TransactionGuaranteeReply reply { get; set; }

        /// <summary>
        /// 第二条及之后的回复列表。
        /// </summary>
        public IList<TransactionGuaranteeCommentReply> commentReplyList { get; set; }
    }

    /// <summary>
    /// 查询评价评论和回复列表结果。
    /// </summary>
    public class TransactionGuaranteeReplyListJsonResult : WxJsonResult
    {
        /// <summary>
        /// 评论和回复集合。
        /// </summary>
        public TransactionGuaranteeReplyCollection list { get; set; }
    }

    /// <summary>
    /// 评价详情容器。
    /// </summary>
    public class TransactionGuaranteeCommentDetailInfo
    {
        /// <summary>
        /// 完整评价内容。
        /// </summary>
        public TransactionGuaranteeCommentItem content { get; set; }
    }

    /// <summary>
    /// 差评处理进度节点。
    /// </summary>
    public class TransactionGuaranteeCommentProcessAction
    {
        /// <summary>
        /// 进度类型：1 发表差评，2 开发者处理，3 用户调研，4 用户改评。
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 更新时间，Unix 秒级时间戳；未到达的节点可能没有此值。
        /// </summary>
        public long? updateTime { get; set; }
    }

    /// <summary>
    /// 差评处理进度。
    /// </summary>
    public class TransactionGuaranteeCommentProcessInfo
    {
        /// <summary>
        /// 进度节点列表；从后向前第一个含更新时间的节点为当前状态。
        /// </summary>
        public IList<TransactionGuaranteeCommentProcessAction> actionList { get; set; }

        /// <summary>
        /// 评价 ID。
        /// </summary>
        public string commentId { get; set; }
    }

    /// <summary>
    /// 改评前的旧评价内容。
    /// </summary>
    public class TransactionGuaranteeOldCommentContent
    {
        /// <summary>
        /// 旧评价文本。官方旧评价字段名为 ext，与当前评价的 txt 不同。
        /// </summary>
        public string ext { get; set; }

        /// <summary>
        /// 旧评价图片或视频列表。
        /// </summary>
        public IList<TransactionGuaranteeCommentMedia> media { get; set; }
    }

    /// <summary>
    /// 改评前的旧评价。
    /// </summary>
    public class TransactionGuaranteeOldComment
    {
        /// <summary>
        /// 旧评价 ID。
        /// </summary>
        public string commentId { get; set; }

        /// <summary>
        /// 创建时间，Unix 秒级时间戳字符串。
        /// </summary>
        public string createTime { get; set; }

        /// <summary>
        /// 评价分数，每 100 分对应一颗星。
        /// </summary>
        public int score { get; set; }

        /// <summary>
        /// 旧评价正文和媒体。
        /// </summary>
        public TransactionGuaranteeOldCommentContent content { get; set; }
    }

    /// <summary>
    /// 查询交易评价详情结果。
    /// </summary>
    public class TransactionGuaranteeCommentInfoJsonResult : WxJsonResult
    {
        /// <summary>
        /// 当前评价详情。
        /// </summary>
        public TransactionGuaranteeCommentDetailInfo info { get; set; }

        /// <summary>
        /// 差评处理进度；非差评时可能为空。
        /// </summary>
        public TransactionGuaranteeCommentProcessInfo processInfo { get; set; }

        /// <summary>
        /// 改评前的评价；未发生改评时可能为空。
        /// </summary>
        public TransactionGuaranteeOldComment oldComment { get; set; }
    }

    /// <summary>
    /// 评价、评论或回复操作结果。
    /// </summary>
    public class TransactionGuaranteeActionJsonResult : WxJsonResult
    {
        /// <summary>
        /// 请求是否成功。
        /// </summary>
        public bool success { get; set; }
    }

    #endregion

    #region 投诉请求

    /// <summary>
    /// 商家回应投诉请求。
    /// </summary>
    public class TransactionGuaranteeRespondComplaintRequest
    {
        /// <summary>
        /// 回应内容；与图片列表至少填写一项。
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 投诉单号。
        /// </summary>
        public long complaintOrderId { get; set; }

        /// <summary>
        /// 回应图片的临时素材 media_id 列表；与文字内容至少填写一项。
        /// </summary>
        public IList<string> mediaIdList { get; set; }

        /// <summary>
        /// 商家处理意见：1 同意和解，2 拒绝和解。
        /// </summary>
        public int bussiHandle { get; set; }
    }

    /// <summary>
    /// 投诉凭证或申诉材料请求。
    /// </summary>
    public class TransactionGuaranteeComplaintMaterialRequest
    {
        /// <summary>
        /// 材料文字内容；与图片列表至少填写一项。
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 投诉单号。
        /// </summary>
        public long complaintOrderId { get; set; }

        /// <summary>
        /// 材料图片的临时素材 media_id 列表；与文字内容至少填写一项。
        /// </summary>
        public IList<string> mediaIdList { get; set; }
    }

    /// <summary>
    /// 投诉退款处理凭证请求。
    /// </summary>
    public class TransactionGuaranteeRefundProofRequest : TransactionGuaranteeComplaintMaterialRequest
    {
        /// <summary>
        /// 是否确认收到退货；处于退货状态时必填，具体取值以投诉单状态要求为准。
        /// </summary>
        public int? acceptReturn { get; set; }

        /// <summary>
        /// 退货单号；处于退货状态时必填，可从投诉单详情获取。
        /// </summary>
        public long? returnId { get; set; }
    }

    #endregion

    #region 投诉返回模型

    /// <summary>
    /// 用户提交的投诉材料。
    /// </summary>
    public class TransactionGuaranteeCustomerMaterial
    {
        /// <summary>
        /// 投诉文字内容。
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 投诉图片 CDN 地址列表；地址有时效，应在查看前重新查询详情。
        /// </summary>
        public IList<string> mediaIdList { get; set; }
    }

    /// <summary>
    /// 小程序交易投诉单主体信息。
    /// </summary>
    public class TransactionGuaranteeComplaintOrder
    {
        /// <summary>
        /// 投诉单 ID。
        /// </summary>
        public string complaintOrderId { get; set; }

        /// <summary>
        /// 投诉用户 OpenId。
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 投诉发起时间，Unix 秒级时间戳。
        /// </summary>
        public long createTime { get; set; }

        /// <summary>
        /// 联系电话。官方参数表标记为数字，但电话号码不是计算值，因此使用字符串避免格式和精度损失。
        /// </summary>
        public string phoneNumber { get; set; }

        /// <summary>
        /// 投诉问题分类编码，取值见微信官方投诉类型枚举。
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 投诉单状态编码，取值见微信官方投诉状态枚举。
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 用户提交的投诉材料。
        /// </summary>
        public TransactionGuaranteeCustomerMaterial customerMaterial { get; set; }

        /// <summary>
        /// 微信支付订单号。
        /// </summary>
        public string orderId { get; set; }

        /// <summary>
        /// 商家订单号。
        /// </summary>
        public string outTradeNo { get; set; }

        /// <summary>
        /// 商品名称。
        /// </summary>
        public string productName { get; set; }

        /// <summary>
        /// 支付时间，Unix 秒级时间戳。
        /// </summary>
        public long payTime { get; set; }

        /// <summary>
        /// 交易金额。官方文档将类型标记为字符串。
        /// </summary>
        public string totalCost { get; set; }

        /// <summary>
        /// 当前投诉状态的到期时间，Unix 秒级时间戳；0 表示不存在。
        /// </summary>
        public long expireTime { get; set; }

        /// <summary>
        /// 投诉用户头像地址。
        /// </summary>
        public string headImgUrl { get; set; }

        /// <summary>
        /// 投诉用户微信昵称。
        /// </summary>
        public string nickName { get; set; }

        /// <summary>
        /// 申诉状态：0 未进入申诉，401 待申诉，402 已超时，403 申诉中，117 成功，118 失败。
        /// </summary>
        public int appealState { get; set; }
    }

    /// <summary>
    /// 投诉处理进度节点。
    /// </summary>
    public class TransactionGuaranteeComplaintProgressItem
    {
        /// <summary>
        /// 投诉节点状态编码，取值见微信官方 itemType 枚举。
        /// </summary>
        public int itemType { get; set; }

        /// <summary>
        /// 节点发生时间，Unix 秒级时间戳。
        /// </summary>
        public long time { get; set; }

        /// <summary>
        /// 节点内容文本。
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 节点图片 CDN 地址列表。
        /// </summary>
        public IList<string> mediaIdList { get; set; }

        /// <summary>
        /// 节点联系电话。官方参数表标记为数字，模型使用字符串避免格式和精度损失。
        /// </summary>
        public string phoneNumber { get; set; }

        /// <summary>
        /// 商责处理标记；节点类型为 31 或 32 时，1 表示待用户退货，0 表示待上传处理凭证。
        /// </summary>
        public int? blameResult { get; set; }

        /// <summary>
        /// 操作者昵称。
        /// </summary>
        public string nickName { get; set; }

        /// <summary>
        /// 当前节点的申诉状态编码。
        /// </summary>
        public int appealItemType { get; set; }
    }

    /// <summary>
    /// 投诉关联的退货运单。
    /// </summary>
    public class TransactionGuaranteeReturnBill
    {
        /// <summary>
        /// 退货单 ID。
        /// </summary>
        public string returnId { get; set; }

        /// <summary>
        /// 物流运单号。
        /// </summary>
        public string waybillId { get; set; }

        /// <summary>
        /// 运力公司名称。
        /// </summary>
        public string deliveryName { get; set; }

        /// <summary>
        /// 运单状态：0 待揽件、1 已揽件、2 运输中、3 派件中、4 已签收，其他取值见微信官方枚举。
        /// </summary>
        public int orderStatus { get; set; }
    }

    /// <summary>
    /// 查询小程序交易投诉单详情结果。
    /// </summary>
    public class TransactionGuaranteeComplaintDetailJsonResult : WxJsonResult
    {
        /// <summary>
        /// 投诉单主体信息。
        /// </summary>
        public TransactionGuaranteeComplaintOrder complaintOrder { get; set; }

        /// <summary>
        /// 投诉处理进度列表。官方参数表写作 object，代码示例实际返回数组，因此按数组建模。
        /// </summary>
        public IList<TransactionGuaranteeComplaintProgressItem> item { get; set; }

        /// <summary>
        /// 关联退货运单；不存在退货时可能为空。
        /// </summary>
        public TransactionGuaranteeReturnBill returnBill { get; set; }
    }

    #endregion
}
