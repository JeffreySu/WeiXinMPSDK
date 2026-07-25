/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：CustomerAcquisitionP1Json.cs
    文件功能描述：CustomerAcquisitionP1Json 强类型数据模型


    创建标识：Senparc - 20260723

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐企业微信通讯录、安全、智能机器人、微信客服和获客助手接口

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.CustomerAcquisition.CustomerAcquisitionJson
{
    /// <summary>
    /// GetCustomerAcquisitionCustomer 接口请求参数。
    /// </summary>
    public class GetCustomerAcquisitionCustomerRequest
    {
        public string link_id { get; set; }
        public int? limit { get; set; }
        public string cursor { get; set; }
    }

    /// <summary>
    /// GetCustomerAcquisitionCustomer 接口返回结果。
    /// </summary>
    public class GetCustomerAcquisitionCustomerResult : WorkJsonResult
    {
        public IList<CustomerAcquisitionCustomer> customer_list { get; set; }
        public string next_cursor { get; set; }
    }

    /// <summary>
    /// CustomerAcquisitionCustomer 微信接口数据模型。
    /// </summary>
    public class CustomerAcquisitionCustomer
    {
        public string external_userid { get; set; }
        public string userid { get; set; }
        public int chat_status { get; set; }
        public string state { get; set; }
    }

    /// <summary>
    /// GetCustomerAcquisitionQuota 接口返回结果。
    /// </summary>
    public class GetCustomerAcquisitionQuotaResult : WorkJsonResult
    {
        public long total { get; set; }
        public long balance { get; set; }
        public IList<CustomerAcquisitionQuota> quota_list { get; set; }
    }

    /// <summary>
    /// CustomerAcquisitionQuota 微信接口数据模型。
    /// </summary>
    public class CustomerAcquisitionQuota
    {
        public long expire_date { get; set; }
        public long balance { get; set; }
    }

    /// <summary>
    /// GetCustomerAcquisitionStatistic 接口请求参数。
    /// </summary>
    public class GetCustomerAcquisitionStatisticRequest
    {
        public string link_id { get; set; }
        public long start_time { get; set; }
        public long end_time { get; set; }
    }

    /// <summary>
    /// GetCustomerAcquisitionStatistic 接口返回结果。
    /// </summary>
    public class GetCustomerAcquisitionStatisticResult : WorkJsonResult
    {
        public long click_link_customer_cnt { get; set; }
        public long new_customer_cnt { get; set; }
    }

    /// <summary>
    /// GetCustomerAcquisitionChatInfo 接口请求参数。
    /// </summary>
    public class GetCustomerAcquisitionChatInfoRequest
    {
        public string chat_key { get; set; }
    }

    /// <summary>
    /// GetCustomerAcquisitionChatInfo 接口返回结果。
    /// </summary>
    public class GetCustomerAcquisitionChatInfoResult : WorkJsonResult
    {
        public string userid { get; set; }
        public string external_userid { get; set; }
        public CustomerAcquisitionChatInfo chat_info { get; set; }
    }

    /// <summary>
    /// CustomerAcquisitionChat 信息。
    /// </summary>
    public class CustomerAcquisitionChatInfo
    {
        public int recv_msg_cnt { get; set; }
        public string link_id { get; set; }
        public string state { get; set; }
    }
}
