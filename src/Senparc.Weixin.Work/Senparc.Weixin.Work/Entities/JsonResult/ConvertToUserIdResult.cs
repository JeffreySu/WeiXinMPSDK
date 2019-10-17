﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ConvertToUserIdResult.cs
    文件功能描述：openid转换成userid接口返回的Json结果
    
    
    创建标识：Senparc - 20150722
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// openid转换成userid接口返回的Json结果
    /// </summary>
    public class ConvertToUserIdResult : WorkJsonResult
    {
        /// <summary>
        /// 该openid在企业号中对应的成员userid
        /// </summary>
        public string userid { get; set; }
    }
}
