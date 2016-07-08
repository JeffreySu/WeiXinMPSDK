/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：RequestMessageComponentVerifyTicket.cs
    文件功能描述：推送component_verify_ticket协议
    
    
    创建标识：Senparc - 20150430
----------------------------------------------------------------*/

namespace Senparc.Weixin.Open
{
    public class RequestMessageComponentVerifyTicket : RequestMessageBase
    {
        public override RequestInfoType InfoType
        {
            get { return RequestInfoType.component_verify_ticket; }
        }
        public string ComponentVerifyTicket { get; set; }
    }
}
