using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 会员信息
    /// </summary>
    public class UserinfoGetResult : WxJsonResult
    {
        /// <summary>
        /// 用户在本公众号内唯一识别码
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// membership_number
        /// </summary>
        public string membership_number { get; set; }
        /// <summary>
        /// 积分信息
        /// </summary>
        public int bonus { get; set; }
        /// <summary>
        /// 用户性别，如MALE
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// 会员信息
        /// </summary>
        public UserinfoGetResult_UserInfo user_info { get; set; }
        /// <summary>
        /// 开发者设置的会员卡会员信息类目，如等级。
        /// </summary>
        public List<string> custom_field_list { get; set; }
        /// <summary>
        /// 当前用户的会员卡状态
        /// </summary>
        public UserCardStatus user_card_status { get; set; }
    }

    public class UserinfoGetResult_UserInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public List<UserinfoGetResult_UserInfo_Item> common_field_list { get; set; }
    }

    public class UserinfoGetResult_UserInfo_Item
    {
        /// <summary>
        /// 会员信息类目名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 会员卡信息类目值，比如等级值等
        /// </summary>
        public string value { get; set; }
    }
}
