/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetCallBackIpResult.cs
    文件功能描述：获取微信服务器的ip段的JSON返回格式
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.Entities
{
    public class GetCallBackIpResult : WorkJsonResult
    {
        public string[] ip_list { get; set; }
    }
}
