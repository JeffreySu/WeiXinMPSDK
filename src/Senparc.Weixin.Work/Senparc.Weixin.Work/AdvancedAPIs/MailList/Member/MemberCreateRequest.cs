/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：MemberCreateRequest.cs
    文件功能描述：创建成员接口 请求包
     
    
    创建标识：Senparc - 20180728

    修改标识：Senparc - 20190214
    修改描述：v3.3.7 添加 alias 属性

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

        /// <summary>
        /// 成员别名。长度1~32个utf8（非必须） ### https://work.weixin.qq.com/api/doc#90000/90135/90195
        /// </summary>
        public string alias { get; set; }
    }
}
