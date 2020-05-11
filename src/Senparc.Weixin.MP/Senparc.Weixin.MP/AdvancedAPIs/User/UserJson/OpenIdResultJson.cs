/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：OpenIdResultJson.cs
    文件功能描述：获取关注者OpenId信息返回结果


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.User
{
    /// <summary>
    /// 获取关注者OpenId信息返回结果
    /// </summary>
    public class OpenIdResultJson : WxJsonResult
    {
        /// <summary>
        /// 关注该公众账号的总用户数
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 拉取的OPENID个数，最大值为10000
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 列表数据，OPENID的列表
        /// </summary>
        public OpenIdResultJson_Data data { get; set; }
        /// <summary>
        /// 拉取列表的最后一个用户的OPENID
        /// </summary>
        public string next_openid { get; set; }
    }

    public class OpenIdResultJson_Data
    {
        public List<string> openid { get; set; }
    }
}
