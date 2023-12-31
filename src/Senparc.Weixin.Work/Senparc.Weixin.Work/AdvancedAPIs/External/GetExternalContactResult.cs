/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：GetExternalContactResult.cs
    文件功能描述：获取外部联系人详情返回结果
     
    
    创建标识：Senparc - 20181009

    修改标识：ccccccmd - 20210513
    修改描述：v3.9.102 补充获取客户群详情结果字段

    修改标识：ccccccmd - 20210529
    修改描述：v3.9.102.2 补充企微获取客户详情接口返回值字段

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.External
{
    public class GetExternalContactResultJson : WorkJsonResult
    {
        public External_Contact external_contact { get; set; }
        public Follow_User[] follow_user { get; set; }

        /// <summary>
        /// 分页的cursor，当跟进人多于500人时返回
        /// </summary>
        public string next_cursor { get; set; }
    }

    public class External_Contact
    {
        /// <summary>
        /// 外部联系人的userid
        /// </summary>
        public string external_userid { get; set; }

        /// <summary>
        /// 外部联系人的名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 外部联系人的职位，如果外部企业或用户选择隐藏职位，则不返回，仅当联系人类型是企业微信用户时有此字段
        /// </summary>
        public string position { get; set; }

        /// <summary>
        /// 外部联系人头像，第三方不可获取
        /// </summary>
        public string avatar { get; set; }

        /// <summary>
        /// 外部联系人所在企业的简称，仅当联系人类型是企业微信用户时有此字段
        /// </summary>
        public string corp_name { get; set; }

        /// <summary>
        /// 外部联系人所在企业的主体名称，仅当联系人类型是企业微信用户时有此字段
        /// </summary>
        public string corp_full_name { get; set; }

        /// <summary>
        /// 外部联系人的类型，1表示该外部联系人是微信用户，2表示该外部联系人是企业微信用户
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 外部联系人性别 0-未知 1-男性 2-女性
        /// </summary>
        public int gender { get; set; }

        /// <summary>
        /// 外部联系人在微信开放平台的唯一身份标识（微信unionid），通过此字段企业可将外部联系人与公众号/小程序用户关联起来。仅当联系人类型是微信用户，且企业或第三方服务商绑定了微信开发者ID有此字段
        /// </summary>
        public string unionid { get; set; }

        /// <summary>
        /// 外部联系人的自定义展示信息
        /// </summary>
        public External_Profile external_profile { get; set; }
    }

    public class External_Profile
    {
        /// <summary>
        /// 属性列表，目前支持文本、网页、小程序三种类型
        /// </summary>
        public External_Attr[] external_attr { get; set; }
    }

    public class External_Attr
    {
        /// <summary>
        /// 属性类型: 0-文本 1-网页 2-小程序
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 属性名称： 需要先确保在管理端有创建该属性，否则会忽略
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 文本类型的属性
        /// </summary>
        public Text text { get; set; }
        /// <summary>
        /// 网页类型的属性，url和title字段要么同时为空表示清除该属性，要么同时不为空
        /// </summary>
        public Web web { get; set; }
        /// <summary>
        /// 小程序类型的属性，appid和title字段要么同时为空表示清除该属性，要么同时不为空
        /// </summary>
        public Miniprogram miniprogram { get; set; }
    }

    public class Text
    {
        public string value { get; set; }
    }

    public class Web
    {
        public string url { get; set; }
        public string title { get; set; }
    }

    public class Miniprogram
    {
        public string appid { get; set; }
        public string pagepath { get; set; }
        public string title { get; set; }
    }

    public class Follow_User
    {
        /// <summary>
        /// 添加了此外部联系人的企业成员userid
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 该成员对此外部联系人的备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 该成员对此外部联系人的描述
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 该成员添加此外部联系人的时间
        /// </summary>
        public int createtime { get; set; }

        /// <summary>
        /// 企业自定义的state参数，用于区分客户具体是通过哪个「联系我」添加，由企业通过创建「联系我」方式指定
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 发起添加的userid，如果成员主动添加，为成员的userid；如果是客户主动添加，则为客户的外部联系人userid；如果是内部成员共享/管理员分配，则为对应的成员/管理员userid
        /// </summary>
        public string oper_userid { get; set; }

        /// <summary>
        /// 该成员添加此客户的来源
        /// </summary>
        public int add_way { get; set; }

        /// <summary>
        /// 该成员对此客户备注的企业名称
        /// </summary>
        public string remark_corp_name { get; set; }

        /// <summary>
        /// 该成员对此客户备注的手机号码，第三方不可获取
        /// </summary>
        public string[] remark_mobiles { get; set; }

        public Follow_User_Tags[] tags { get; set; }
    }
    
    public class Follow_User_Tag
    {
        public string group_name { get; set; }
        public string tag_name { get; set; }
        public string tag_id { get; set; }
        public int type { get; set; }
    }

    public class Follow_User_Tags
    {
        /// <summary>
        /// 该成员添加此外部联系人所打标签的分组名称（标签功能需要企业微信升级到2.7.5及以上版本）
        /// </summary>
        public string group_name { get; set; }

        /// <summary>
        /// 该成员添加此外部联系人所打标签名称
        /// </summary>
        public string tag_name { get; set; }

        /// <summary>
        /// 该成员添加此外部联系人所打企业标签的id，仅企业设置（type为1）的标签返回
        /// </summary>
        public string tag_id { get; set; }

        /// <summary>
        /// 该成员添加此外部联系人所打标签类型, 1-企业设置, 2-用户自定义
        /// </summary>
        public int type { get; set; }
    }
}