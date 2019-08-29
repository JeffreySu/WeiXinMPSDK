/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageBase.cs
    文件功能描述：第三方应用授权回调消息服务
    
    
    创建标识：Senparc - 20150430

    修改标识：Senparc - 20181226
    修改描述：v4.3.3 修改 DateTime 为 DateTimeOffset
----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.Open
{
    /// <summary>
    /// 请求消息接口
    /// </summary>
    public interface IRequestMessageBase// : Senparc.NeuChar.Entities.IRequestMessageBase
    {
        string AppId { get; set; }
        DateTimeOffset CreateTime { get; set; }
        RequestInfoType InfoType { get; }
    }

    /// <summary>
    /// 请求消息
    /// </summary>
    public class RequestMessageBase : IRequestMessageBase
    {
        public string AppId { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public virtual RequestInfoType InfoType
        {
            get { return RequestInfoType.component_verify_ticket; }
        }
    }
}
