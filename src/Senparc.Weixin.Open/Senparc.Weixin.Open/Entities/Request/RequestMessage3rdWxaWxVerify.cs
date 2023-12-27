/*----------------------------------------------------------------
    Copyright (C) 2023 Senparc
    
    文件名：RequestMessage3rdWxaAuth.cs
    文件功能描述：小程序认证年审和过期能力限制提醒（过期当天&过期30天&过期60天）
    
    
    创建标识：Senparc - 20231211
----------------------------------------------------------------*/

namespace Senparc.Weixin.Open
{
    public class RequestMessage3rdWxaWxVerify : RequestMessageBase
    {
        public override RequestInfoType InfoType
        {
            get { return RequestInfoType.notify_3rd_wxa_wxverify; }
        }
        /// <summary>
        /// 小程序appid
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 认证过期时间戳（秒）
        /// </summary>
        public long expired { get; set; }

        /// <summary>
        /// 提醒消息内容
        /// </summary>
        public string message { get; set; }

    }
}
