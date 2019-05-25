/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：MiniResultJson.cs
    文件功能描述：企业微信小程序返回结果
     
    
    创建标识：Senparc - 20181009

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 登录凭证校验返回结果
    /// </summary>
    public class LoginCheckResultJson : WorkJsonResult
    {
        public string corpid { get; set; }
        public string userid { get; set; }
        public string session_key { get; set; }
    }

}
