/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：MiniResultJson.cs
    文件功能描述：互联企业消息推送接口返回结果
     
    
    创建标识：Senparc - 20181009

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Mass
{
    /// <summary>
    /// 发送消息返回结果
    /// 如果部分接收人无权限或不存在，发送仍然执行，但会返回无效的部分（即invaliduser或invalidparty），常见的原因是接收人不在应用的可见范围内。
    /// </summary>
    public class LinkedCorpMassResult : WorkJsonResult
    {
        public string[] invaliduser { get; set; }
        public string[] invalidparty { get; set; }
        public string[] invalidtag { get; set; }
    }
}

