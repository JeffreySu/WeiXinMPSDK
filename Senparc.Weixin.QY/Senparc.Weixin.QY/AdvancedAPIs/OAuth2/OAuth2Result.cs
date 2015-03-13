/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：OAuth2Result.cs
    文件功能描述：获取成员信息返回结果
    
    
    创建标识：Senparc - 20130313
    
    修改标识：Senparc - 20130313
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.QY.AdvancedAPIs.OAuth2
{
    /// <summary>
    /// 获取成员信息返回结果
    /// </summary>
    public class GetUserIdResult : WxJsonResult
    {
        /// <summary>
        /// 员工UserID
        /// </summary>
        public string UserId { get; set; }
    }
}
