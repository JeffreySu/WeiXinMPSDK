/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：BatchGetUserInfoData.cs
    文件功能描述：批量获取用户基本信息数据
    
    
    创建标识：Senparc - 20150727
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.User
{
    /// <summary>
    /// 批量获取用户基本信息数据
    /// </summary>
    public class BatchGetUserInfoData
    {
        /// <summary>
        /// 用户的标识，对当前公众号唯一
        /// 必填
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语，默认为zh-CN
        /// 非必填
        /// </summary>
        public Language lang { get; set; }
    }
}
