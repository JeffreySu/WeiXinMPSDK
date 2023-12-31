/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：MiniResultJson.cs
    文件功能描述：企业微信小程序返回结果
     
    
    创建标识：Senparc - 20181009

    修改标识：Loongle - 20220424
    修改描述：v3.15.1 修复 LoginCheckResultJson 缺少 open_userid


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
        public string open_userid { get; set; }
    }

    /// <summary>
    /// 获取下级/下游企业小程序session返回结果
    /// </summary>
    public class TransferSessionResultJson : WorkJsonResult
    {
        public string userid { get; set; }
        public string session_key { get; set; }
    }
}
