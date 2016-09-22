/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：GetCallBackIpResult.cs
    文件功能描述：获取微信服务器的ip段的JSON返回格式
    
    
    创建标识：Senparc - 20150917
    
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.Entities
{
    public class GetCallBackIpResult : WxJsonResult
    {
        public string[] ip_list { get; set; }
    }
}
