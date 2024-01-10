/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：RequestMessageNicknameAudit.cs
    文件功能描述：小程序昵称审核事件
    
    
    创建标识：mc7246 - 20190603

----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.Open
{
    /// <summary>
    /// 小程序昵称审核事件
    /// 该事件已移入小程序SDK,请在小程序SDK内处理
    /// </summary>
    [Obsolete("此事件请在小程序SDK处理")]
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
