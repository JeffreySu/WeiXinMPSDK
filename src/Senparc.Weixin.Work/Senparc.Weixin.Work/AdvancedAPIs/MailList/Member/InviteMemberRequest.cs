/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：MemberCreateRequest.cs
    文件功能描述：邀请成员接口 请求包
     
    
    创建标识：Senparc - 20181009

----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.AdvancedAPIs.MailList.Member
{
    /// <summary>
    /// 邀请成员数据
    /// </summary>
    public class InviteMemberData
    {
        public string[] user { get; set; }
        public string[] party { get; set; }
        public string[] tag { get; set; }
    }

}
