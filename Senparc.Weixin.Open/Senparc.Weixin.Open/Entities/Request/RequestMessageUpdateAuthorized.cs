/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：RequestMessageUnauthorized.cs
    文件功能描述：推送取消授权通知
    
    
    创建标识：Senparc - 20150430
----------------------------------------------------------------*/

namespace Senparc.Weixin.Open
{
    public class RequestMessageUpdateAuthorized : RequestMessageBase
    {
        public override RequestInfoType InfoType
        {
            get { return RequestInfoType.updateauthorized; }
        }
        public string AuthorizerAppid { get; set; }

        public string AuthorizationCode { get; set; }

        public int AuthorizationCodeExpiredTime { get; set; }
    }
}
