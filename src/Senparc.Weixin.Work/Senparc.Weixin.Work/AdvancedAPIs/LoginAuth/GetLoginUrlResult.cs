/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetLoginInfoResult.cs
    文件功能描述：获取企业号管理员登录信息返回结果
    
    
    创建标识：Senparc - 20150325
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.LoginAuth
{
    public class GetLoginUrlResult : WorkJsonResult
    {
        /// <summary>
        /// 登录跳转的url，一次性有效，不可多次使用
        /// </summary>
        public string login_url { get; set; }
        /// <summary>
        /// url有效时长，单位为秒
        /// </summary>
        public int expires_in { get; set; }
    }
}
