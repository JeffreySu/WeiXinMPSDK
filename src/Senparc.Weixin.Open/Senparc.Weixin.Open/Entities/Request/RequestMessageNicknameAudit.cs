/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageNicknameAudit.cs
    文件功能描述：推送取消授权通知
    
    
    创建标识：mc7246 - 20190603

----------------------------------------------------------------*/

namespace Senparc.Weixin.Open
{
    public class RequestMessageNicknameAudit : RequestMessageBase
    {
        public override RequestInfoType InfoType
        {
            get { return RequestInfoType.unauthorized; }
        }

        public string ToUserName { get; set; }

        public string FromUserName { get; set; }

        /// <summary>
        /// 结果2失败，3成功
        /// </summary>
        public int ret { get; set; }

        /// <summary>
        /// 小程序名称
        /// </summary>
        public string nickname { get; set; }

        /// <summary>
        /// 驳回原因
        /// </summary>
        public string reason { get; set; }
    }
}
