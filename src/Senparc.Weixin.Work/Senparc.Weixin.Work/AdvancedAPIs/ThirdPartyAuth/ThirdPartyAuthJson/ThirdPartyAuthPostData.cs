/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ThirdPartyAuthPostData.cs
    文件功能描述：第三方应用授权需要post的数据
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.AdvancedAPIs.ThirdPartyAuth
{
    public class ThirdParty_AgentData
    {
        /// <summary>
        /// 企业应用的id
        /// </summary>
        public string agentid { get; set; }
        /// <summary>
        /// 企业应用是否打开地理位置上报 0：不上报；1：进入会话上报；2：持续上报
        /// </summary>
        public string report_location_flag { get; set; }
        /// <summary>
        /// 企业应用头像的mediaid，通过多媒体接口上传图片获得mediaid，上传后会自动裁剪成方形和圆形两个头像
        /// </summary>
        public string logo_mediaid { get; set; }
        /// <summary>
        /// 企业应用名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 企业应用详情
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 企业应用可信域名
        /// </summary>
        public string redirect_domain { get; set; }
        /// <summary>
        /// 是否接收用户变更通知。0：不接收；1：接收
        /// </summary>
        public SetAgent_IsReportUser isreportuser { get; set; }
    }

    /// <summary>
    /// 获取注册码数据
    /// </summary>
    public class GetRegisterCodeData
    {
        public string template_id { get; set; }
        public string corp_name { get; set; }
        public string admin_name { get; set; }
        public string admin_mobile { get; set; }
        public string state { get; set; }
        public string follow_user { get; set; }
    }

    /// <summary>
    /// 设置授权应用可见范围数据
    /// </summary>
    public class SetScopeData
    {
        public int agentid { get; set; }
        public string[] allow_user { get; set; }
        public int[] allow_party { get; set; }
        public int[] allow_tag { get; set; }
    }

}


