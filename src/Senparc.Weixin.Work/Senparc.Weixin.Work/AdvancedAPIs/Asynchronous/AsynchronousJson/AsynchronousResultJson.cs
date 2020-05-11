/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：AsynchronousPostData.cs
    文件功能描述：异步任务接口提交数据Json
    
    
    创建标识：Senparc - 20150408

    修改标识：Senparc - 20150313
    修改描述：修改 AsynchronousReplaceUserResult 参数类型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Asynchronous
{
    /// <summary>
    /// 异步任务Id返回结果
    /// </summary>
    public class AsynchronousJobId : WorkJsonResult
    {
        /// <summary>
        /// 异步任务id，最大长度为64字符
        /// </summary>
        public string jobid { get; set; }
    }

    public class BaseAsynchronousResult : WorkJsonResult
    {
        /// <summary>
        /// 任务状态，整型，1表示任务开始，2表示任务进行中，3表示任务已完成
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 操作类型，字符串，目前分别有：
        /// 1. sync_user(增量更新成员)
        /// 2. replace_user(全量覆盖成员)
        /// 3. invite_user(邀请成员关注)
        /// 4. replace_party(全量覆盖部门)
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 任务运行总条数
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 目前运行百分比，当任务完成时为100
        /// </summary>
        public int percentage { get; set; }
        /// <summary>
        /// 预估剩余时间（单位：分钟），当任务完成时为0
        /// </summary>
        public int remaintime { get; set; }
    }

    /// <summary>
    /// 异步邀请成员返回结果
    /// </summary>
    public class AsynchronousInviteUserResult : BaseAsynchronousResult
    {
        public List<AsynchronousInviteUserItem> result { get; set; }
    }

    public class AsynchronousInviteUserItem : WorkJsonResult
    {
        /// <summary>
        /// 成员UserID。对应管理端的帐号
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 邀请类型：0 没有邀请方式或未邀请 1 微信邀请 2 邮件邀请
        /// </summary>
        public int invitetype { get; set; }
    }

    /// <summary>
    /// 异步新增或更新成员返回结果
    /// </summary>
    public class AsynchronousReplaceUserResult : BaseAsynchronousResult
    {
        public List<AsynchronousReplaceUserItem> result { get; set; }
    }

    /// <summary>
    /// 异步新增或更新成员返回结果 - result
    /// </summary>
    public class AsynchronousReplaceUserItem : WorkJsonResult
    {
        /// <summary>
        /// 成员UserID。对应管理端的帐号
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 操作类型（按位或）：1 表示修改，2 表示新增
        /// </summary>
        public int action { get; set; }
    }

    /// <summary>
    /// 异步覆盖部门返回结果
    /// </summary>
    public class AsynchronousReplacePartyResult : BaseAsynchronousResult
    {
        public List<AsynchronousReplacePartyItem> result { get; set; }
    }

    public class AsynchronousReplacePartyItem : WorkJsonResult
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public string partyid { get; set; }
        /// <summary>
        /// 操作类型（按位或）：1 新建部门 ，2 更改部门名称， 4 移动部门， 8 修改部门排序
        /// </summary>
        public int action { get; set; }
    }
}
