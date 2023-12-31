/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：RequestMessageUnauthorized.cs
    文件功能描述：推送取消授权通知
    
    
    创建标识：Senparc - 20150430
----------------------------------------------------------------*/

namespace Senparc.Weixin.Open
{
    public class RequestMessageIcpFilingVerify : RequestMessageBase
    {
        public override RequestInfoType InfoType
        {
            get { return RequestInfoType.notify_icpfiling_verify_result; }
        }
        /// <summary>
        /// 人脸核验任务id
        /// </summary>
        public string task_id { get; set; }

        /// <summary>
        /// 小程序唯一id
        /// </summary>
        public string verify_appid { get; set; }

        /// <summary>
        /// 人脸核验结果： 2-核验失败；3-核验成功
        /// </summary>
        public int result { get; set; }
    }
}
