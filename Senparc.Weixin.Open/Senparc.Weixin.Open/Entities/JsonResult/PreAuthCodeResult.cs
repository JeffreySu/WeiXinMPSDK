﻿/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：PreAuthCodeResult.cs
    文件功能描述：获取预授权码返回结果
    
    
    创建标识：Senparc - 20150430
----------------------------------------------------------------*/

namespace Senparc.Weixin.Open.Entities
{
    /// <summary>
    /// 获取预授权码返回结果
    /// </summary>
    public class PreAuthCodeResult
    {
        /// <summary>
        /// 预授权码
        /// </summary>
        public string pre_auth_code { get; set; }
        /// <summary>
        /// 有效期，为20分钟
        /// </summary>
        public int expires_in { get; set; }
    }
}
