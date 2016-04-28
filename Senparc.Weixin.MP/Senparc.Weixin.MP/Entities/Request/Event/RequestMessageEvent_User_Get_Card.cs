/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

    文件名：RequestMessageEvent_UserGetCard.cs
    文件功能描述：事件之领取卡券


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20150323
    修改描述：添加OuterId字段

    修改标识：hello2008zj - 20160428
    修改描述：v13.7.7 添加IsRestoreMemberCard及OldUserCardCode字段。
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageEvent_User_Get_Card : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 领取卡券
        /// </summary>
        public override Event Event
        {
            get { return Event.user_get_card; }
        }

        /// <summary>
        /// 赠送方账号（一个OpenID），"IsGiveByFriend”为1时填写该参数
        /// </summary>
        public string FriendUserName { get; set; }

        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }

        /// <summary>
        /// 是否为转赠，1 代表是，0 代表否。
        /// </summary>
        public int IsGiveByFriend { get; set; }

        /// <summary>
        /// code 序列号。自定义code 及非自定义code的卡券被领取后都支持事件推送。
        /// </summary>
        public string UserCardCode { get; set; }

        /// <summary>
        /// 领取场景值，用于领取渠道数据统计。可在生成二维码接口及添加JS API 接口中自定义该字段的整型值。
        /// </summary>
        public int OuterId { get; set; }


        /// <summary>
        /// 是否为删除后重新领取卡，1 代表是，0 代表否。
        /// </summary>
        public int IsRestoreMemberCard { get; set; }

        /// <summary>
        /// 转赠前的code序列号
        /// </summary>
        public string OldUserCardCode { get; set; }
    }
}
