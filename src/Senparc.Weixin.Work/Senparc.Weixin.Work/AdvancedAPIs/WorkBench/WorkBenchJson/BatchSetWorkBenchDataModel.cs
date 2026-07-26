/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：BatchSetWorkBenchDataModel.cs
    文件功能描述：企业微信批量设置成员工作台数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 增加批量工作台数据强类型模型

----------------------------------------------------------------*/

using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.WorkBench.WorkBenchJson
{
    /// <summary>批量设置成员工作台数据请求。</summary>
    public class BatchSetWorkBenchDataModel
    {
        /// <summary>企业应用 ID。</summary>
        public int agentid { get; set; }

        /// <summary>待设置工作台数据的成员 UserID 列表。</summary>
        public IList<string> userid_list { get; set; }

        /// <summary>成员共用的工作台展示数据。</summary>
        public BatchWorkBenchData data { get; set; }
    }

    /// <summary>批量工作台展示数据。</summary>
    public class BatchWorkBenchData
    {
        /// <summary>模板类型：keydata、image、list 或 webview。</summary>
        public string type { get; set; }

        /// <summary>关键数据型模板数据。</summary>
        public WorkBenchKeyDataModel keydata { get; set; }

        /// <summary>图片型模板数据。</summary>
        public WorkBenchImageModel image { get; set; }

        /// <summary>列表型模板数据。</summary>
        public WorkBenchListModel list { get; set; }

        /// <summary>网页型模板数据。</summary>
        public WorkBenchWebViewModel webview { get; set; }
    }
}
