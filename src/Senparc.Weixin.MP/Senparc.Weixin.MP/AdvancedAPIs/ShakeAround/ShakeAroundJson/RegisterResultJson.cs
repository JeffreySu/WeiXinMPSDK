/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

    文件名：RegisterResultJson.cs
    文件功能描述：返回空的数据

    创建标识：Senparc - 20160520
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.ShakeAround
{
    /// <summary>
    /// 申请开通功能的返回结果
    /// </summary>
    public class RegisterResultJson : WxJsonResult
    {
        public object data { get; set; }
    }
}