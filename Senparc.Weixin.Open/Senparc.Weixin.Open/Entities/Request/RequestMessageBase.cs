/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：RequestMessageBase.cs
    文件功能描述：第三方应用授权回调消息服务
    
    
    创建标识：Senparc - 20150430
----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.Open
{
    /// <summary>
    /// 请求消息接口
    /// </summary>
    public interface IRequestMessageBase// : Weixin.Entities.IRequestMessageBase
    {
        string AppId { get; set; }
        DateTime CreateTime { get; set; }
        RequestInfoType InfoType { get; }
    }

    /// <summary>
    /// 请求消息
    /// </summary>
    public class RequestMessageBase : IRequestMessageBase
    {
        public string AppId { get; set; }
        public DateTime CreateTime { get; set; }
        public virtual RequestInfoType InfoType
        {
            get { return RequestInfoType.component_verify_ticket; }
        }
    }
}
