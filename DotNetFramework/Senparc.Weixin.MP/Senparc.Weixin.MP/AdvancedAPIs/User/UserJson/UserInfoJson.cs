/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

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
        public string openid { get; set; }
        public string nickname { get; set; }
        public int sex { get; set; }
        public string language { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string headimgurl { get; set; }
        public long subscribe_time { get; set; }
        public string unionid { get; set; }
        public string remark { get; set; }
        public int groupid { get; set; }
    }

    /// <summary>
    /// 批量获取用户基本信息返回结果
    /// </summary>
    public class BatchGetUserInfoJsonResult : WxJsonResult
    {
        public List<UserInfoJson> user_info_list { get; set; }
    }
}
