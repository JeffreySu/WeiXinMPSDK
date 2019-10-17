/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：BatchGetUserInfoData.cs
    文件功能描述：批量获取用户基本信息数据
    
    
    创建标识：Senparc - 20150727

    修改标识：Senparc - 20170114
    修改描述：v14.3.119 暂时修改lang属性为int类型

    修改标识：Senparc - 20170204
    修改描述：v14.3.124 修改lang属性为string类型，并添加LangEnum作为枚举

----------------------------------------------------------------*/

using System;

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
        /// 国家地区语言版本，请使用Language范围内的值：zh_CN 简体，zh_TW 繁体，en 英语，默认为zh-CN
        /// 非必填
        /// </summary>
        public string lang { get; set; }

        /// <summary>
        /// lang属性的枚举转换
        /// </summary>
        public Language LangEnum {
            get { return (Language)Enum.Parse(typeof (Language), lang); }
            set { lang = value.ToString(); }
        }
    }
}
