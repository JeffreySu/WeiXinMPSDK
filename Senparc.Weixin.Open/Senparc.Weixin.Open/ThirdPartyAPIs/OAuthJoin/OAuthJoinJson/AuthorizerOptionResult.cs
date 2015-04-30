/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：AuthorizerOptionResult.cs
    文件功能描述：获取授权方的选项设置信息返回结果
    
    
    创建标识：Senparc - 20150430
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Open.Entities;

namespace Senparc.Weixin.Open.OAuthJoin
{
    /// <summary>
    /// 获取授权方的选项设置信息返回结果
    /// </summary>
    public class AuthorizerOptionResult : WxJsonResult
    {
        /// <summary>
        /// 第三方平台appid
        /// </summary>
        public string authorizer_appid { get; set; }
        /// <summary>
        /// 授权公众号appid
        /// </summary>
        public string option_name { get; set; }
        /// <summary>
        /// 选项名称
        /// </summary>
        public string option_value { get; set; }
    }
}