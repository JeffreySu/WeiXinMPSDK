/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：MemberCreateRequest.cs
    文件功能描述：创建成员接口 请求包
     
    
    创建标识：Senparc - 20180728

----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.AdvancedAPIs.MailList.Member
{
    /// <summary>
    /// 创建成员接口 请求包
    /// <para>文档：<see cref="http://work.weixin.qq.com/api/doc#10018"/></para>
    /// </summary>
    public class MemberCreateRequest : MemberBase
    {
        /// <summary>
        /// 是否邀请该成员使用企业微信（将通过微信服务通知或短信或邮件下发邀请，每天自动下发一次，最多持续3个工作日），默认值为true。
        /// </summary>
        public bool to_invite { get; set; }
    }
}
