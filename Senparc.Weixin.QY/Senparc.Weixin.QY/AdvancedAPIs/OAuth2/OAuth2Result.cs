/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：OAuth2Result.cs
    文件功能描述：获取成员信息返回结果
    
    
    创建标识：Senparc - 20130313
    
    修改标识：Senparc - 20130313
    修改描述：整理接口
    
    修改标识：Senparc - 20130316
    修改描述：添加DeviceId字段
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
        /// <summary>
        /// 手机设备号(由微信在安装时随机生成)
        /// </summary>
        public string DeviceId { get; set; }
    }
}
