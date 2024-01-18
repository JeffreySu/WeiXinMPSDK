/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：RequestMessageUnauthorized.cs
    文件功能描述：推送取消授权通知
    
    
    创建标识：Senparc - 20150430
----------------------------------------------------------------*/

namespace Senparc.Weixin.Open
{
    public class RequestMessageIcpFilingApply : RequestMessageBase
    {
        public override RequestInfoType InfoType
        {
            get { return RequestInfoType.notify_apply_icpfiling_result; }
        }

        /// <summary>
        /// 小程序唯一id
        /// </summary>
        public string authorizer_appid { get; set; }

        /// <summary>
        /// 备案状态，参考“获取小程序备案状态及驳回原因”接口的备案状态枚举
        /// </summary>
        public int beian_status { get; set; }
    }
}
