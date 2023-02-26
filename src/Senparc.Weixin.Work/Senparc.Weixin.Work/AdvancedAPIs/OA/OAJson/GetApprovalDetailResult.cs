

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.OA.OAJson
{
    /// <summary>
    /// 
    /// </summary>
    public class GetApprovalDetailResult : WorkJsonResult
    {
        /// <summary>
        /// 
        /// </summary>
        public GetApprovalDetailResult_Info info { get; set; }
    }

    public class GetApprovalDetailResult_Info
    {
        /// <summary>
        /// 
        /// </summary>
        public string sp_no { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sp_name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int sp_status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string template_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long apply_time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public GetApprovalDetailResult_Info_Applyer applyer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<GetApprovalDetailResult_Info_SpRecord> sp_record { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public List<GetApprovalDetailResult_Info_Notifyer> notifyer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public GetApprovalDetailResult_Info_ApplyData apply_data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<GetApprovalDetailResult_Info_Comments> comments { get; set; }
    }

    public class GetApprovalDetailResult_TextLang
    {
        /// <summary>
        /// 摘要行显示文字，用于记录列表和消息通知的显示，不要超过20个字符
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 摘要行显示语言，中文：zh_CN（注意不是zh-CN），英文：en。
        /// </summary>
        public string lang { get; set; }
    }

    public class GetApprovalDetailResult_Info_Applyer
    {
        /// <summary>
        /// 
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string partyid { get; set; }
    }

    public class GetApprovalDetailResult_Info_SpRecord
    {
        /// <summary>
        /// 
        /// </summary>
        public int sp_status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int approverattr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<GetApprovalDetailResult_Info_SpRecord_Details> details { get; set; }
    }

    public class GetApprovalDetailResult_Info_SpRecord_Details
    {
        /// <summary>
        /// 
        /// </summary>
        public GetApprovalDetailResult_Info_SpRecord_Details_Approver approver { get; set; }

        public string speech { get; set; }

        public int sp_status { get; set; }

        public long sptime { get; set; }

        public List<string> media_id { get; set; }

    }

    public class GetApprovalDetailResult_Info_SpRecord_Details_Approver
    {
        /// <summary>
        /// 
        /// </summary>
        public string userid { get; set; }

    }


    public class GetApprovalDetailResult_Info_Notifyer
    {
        /// <summary>
        /// 
        /// </summary>
        public string userid { get; set; }

    }

    public class GetApprovalDetailResult_Info_ApplyData
    {
        /// <summary>
        /// 
        /// </summary>
        public List<GetApprovalDetailResult_Info_ApplyData_Contents> contents { get; set; }

    }

    public class GetApprovalDetailResult_Info_ApplyData_Contents
    {
        /// <summary>
        /// 
        /// </summary>
        public string control { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<GetApprovalDetailResult_TextLang> title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public GetApprovalDetailResult_Info_ApplyData_Contents_Value value { get; set; }
    }

    public class GetApprovalDetailResult_Info_ApplyData_Contents_Value
    {
        /// <summary>
        /// 
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> tips { get; set; }
    }

    public class GetApprovalDetailResult_Info_Comments
    {
        /// <summary>
        /// 
        /// </summary>
        public GetApprovalDetailResult_Info_Comments_CommentUserInfo commentUserInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long commenttime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string commentcontent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string commentid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> media_id { get; set; }
    }

    public class GetApprovalDetailResult_Info_Comments_CommentUserInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string userid { get; set; }
    }
}
