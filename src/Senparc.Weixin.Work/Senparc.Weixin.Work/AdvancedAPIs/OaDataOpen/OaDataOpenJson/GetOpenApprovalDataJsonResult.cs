/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetOpenApprovalDataJsonResult.cs
    文件功能描述：获取公费电话拨打记录返回结果
    
    
    创建标识：Senparc - 20181009

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.OaDataOpen
{
    /// <summary>
    /// 查询自建应用审批单当前状态返回结果
    /// </summary>
    public class GetOpenApprovalDataJsonResult : WorkJsonResult
    {
        public GetOpenApprovalData data { get; set; }
    }

    public class GetOpenApprovalData
    {
        public string ThirdNo { get; set; }
        public string OpenTemplateId { get; set; }
        public string OpenSpName { get; set; }
        public int OpenSpstatus { get; set; }
        public int ApplyTime { get; set; }
        public string ApplyUsername { get; set; }
        public string ApplyUserParty { get; set; }
        public string ApplyUserImage { get; set; }
        public string ApplyUserId { get; set; }
        public Approvalnodes ApprovalNodes { get; set; }
        public Notifynodes NotifyNodes { get; set; }
        public int approverstep { get; set; }
    }

    public class Approvalnodes
    {
        public Approvalnode[] ApprovalNode { get; set; }
    }

    public class Approvalnode
    {
        public int NodeStatus { get; set; }
        public int NodeAttr { get; set; }
        public int NodeType { get; set; }
        public Items Items { get; set; }
    }

    public class Items
    {
        public Item[] Item { get; set; }
    }

    public class Item
    {
        public string ItemName { get; set; }
        public string ItemParty { get; set; }
        public string ItemImage { get; set; }
        public string ItemUserId { get; set; }
        public int ItemStatus { get; set; }
        public string ItemSpeech { get; set; }
        public int ItemOpTime { get; set; }
    }

    public class Notifynodes
    {
        public Notifynode[] NotifyNode { get; set; }
    }

    public class Notifynode
    {
        public string ItemName { get; set; }
        public string ItemParty { get; set; }
        public string ItemImage { get; set; }
        public string ItemUserId { get; set; }
    }


}
