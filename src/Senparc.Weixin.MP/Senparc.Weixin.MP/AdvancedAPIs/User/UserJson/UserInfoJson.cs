/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：UserInfoJson.cs
    文件功能描述：获取用户信息返回结果


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：jsionr - 20150321
    修改描述：添加remark属性

    修改标识：Senparc - 20150321
    修改描述：添加unionid属性

    修改标识：jsionr - 20150513
    修改描述：增加个人分组信息

    修改标识：Senparc - 20150526
    修改描述：修改备注

    修改标识：Senparc - 20150727
    修改描述：添加批量获取用户基本信息返回结果

    修改标识：Senparc - 20150727
    修改描述：添加批量获取用户基本信息返回结果

    修改标识：Senparc - 20190601
    修改描述：qr_scene 属性改为 uint 类型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.User
{
    /// <summary>
    /// 高级接口获取的用户信息
    /// </summary>
    public class UserInfoJson : WxJsonResult
    {
        /// <summary>
        /// 用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息。
        /// </summary>
        public int subscribe { get; set; }
        /// <summary>
        /// 用户的标识，对当前公众号唯一
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 用户的昵称
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public int sex { get; set; }
        /// <summary>
        ///用户的语言，简体中文为zh_CN
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// 用户所在城市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 用户所在省份
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 用户所在国家
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空。若用户更换头像，原有头像URL将失效。
        /// </summary>
        public string headimgurl { get; set; }
        /// <summary>
        /// 用户关注时间，为时间戳。如果用户曾多次关注，则取最后关注时间
        /// </summary>
        public long subscribe_time { get; set; }
        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。
        /// </summary>
        public string unionid { get; set; }
        /// <summary>
        /// 公众号运营者对粉丝的备注，公众号运营者可在微信公众平台用户管理界面对粉丝添加备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 用户所在的分组ID（兼容旧的用户分组接口）
        /// </summary>
        public int groupid { get; set; }
        /// <summary>
        /// 用户标签
        /// </summary>
        public int[] tagid_list { get; set; }
        /// <summary>
        /// 返回用户关注的渠道来源，ADD_SCENE_SEARCH 公众号搜索，ADD_SCENE_ACCOUNT_MIGRATION 公众号迁移，ADD_SCENE_PROFILE_CARD 名片分享，ADD_SCENE_QR_CODE 扫描二维码，ADD_SCENEPROFILE LINK 图文页内名称点击，ADD_SCENE_PROFILE_ITEM 图文页右上角菜单，ADD_SCENE_PAID 支付后关注，ADD_SCENE_OTHERS 其他
        /// </summary>
        public string subscribe_scene { get; set; }
        /// <summary>
        /// 二维码扫码场景（开发者自定义）
        /// </summary>
        public uint qr_scene { get; set; }
        /// <summary>
        /// 二维码扫码场景描述（开发者自定义）
        /// </summary>
        public string qr_scene_str { get; set; }
    }

    /// <summary>
    /// 批量获取用户基本信息返回结果
    /// </summary>
    public class BatchGetUserInfoJsonResult : WxJsonResult
    {
        public List<UserInfoJson> user_info_list { get; set; }
    }
}
