/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：GetExternalContactInfoBatchResult.cs
    文件功能描述：批量获取客户详情 返回结果
    
    
    创建标识：gokeiyou - 20201013

    修改标识：ShyUncle - 20210513
    修改描述：v3.9.102 修复“批量获取客户详情 返回结果”参数名称

    修改标识：ccccccmd - 20220620
    修改描述：v3.15.5 调整批量获取客户详情返回值
    
----------------------------------------------------------------*/


using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    /// <summary>
    /// 批量获取客户详情 返回结果
    /// </summary>
    public class GetExternalContactInfoBatchResult : WorkJsonResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ExternalContactList> external_contact_list { get; set; }

        /// <summary>
        /// 分页游标，再下次请求时填写以获取之后分页的记录，如果已经没有更多的数据则返回空
        /// </summary>
        public string next_cursor { get; set; }
    }

    public class ExternalContactList
    {
        public External_Contact external_contact { get; set; }
        public FollowUser follow_info { get; set; }
    }

    
    public class FollowUser
    {
        /// <summary>添加了此外部联系人的企业成员userid</summary>
        public string userid { get; set; }

        /// <summary>该成员对此外部联系人的备注</summary>
        public string remark { get; set; }

        /// <summary>该成员对此外部联系人的描述</summary>
        public string description { get; set; }

        /// <summary>该成员添加此外部联系人的时间</summary>
        public int createtime { get; set; }

        /// <summary>企业自定义的state参数，用于区分客户具体是通过哪个「联系我」添加，由企业通过创建「联系我」方式指定</summary>
        public string state { get; set; }

        /// <summary>
        /// 发起添加的userid，如果成员主动添加，为成员的userid；如果是客户主动添加，则为客户的外部联系人userid；如果是内部成员共享/管理员分配，则为对应的成员/管理员userid
        /// </summary>
        public string oper_userid { get; set; }

        /// <summary>该成员添加此客户的来源</summary>
        public int add_way { get; set; }

        /// <summary>该成员对此客户备注的企业名称</summary>
        public string remark_corp_name { get; set; }

        /// <summary>该成员对此客户备注的手机号码，第三方不可获取</summary>
        public string[] remark_mobiles { get; set; }

        public string[] tag_id { get; set; }
    }
}
