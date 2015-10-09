/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：WeixinUserInfoResult.cs
    文件功能描述：获取用户信息返回结果
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class WeixinUserInfoResult : WxJsonResult
    {
        /// <summary>
        /// 用户是否订阅该公众号标识，值为0时，拉取不到其余信息
        /// </summary>
        public int subscribe { get; set; }
        /// <summary>
        /// 普通用户的标识，对当前公众号唯一
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 普通用户的昵称
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 普通用户的头像链接
        /// </summary>
        public string headimgurl { get; set; }
        /// <summary>
        /// 普通用户的语言，简体中文为zh_CN
        /// </summary>
        public string language { get; set; }
    }
}
