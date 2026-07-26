/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc
    
    文件名：BatchJobInfo.cs
    文件功能描述：异步任务完成事件推送的BatchJob
    
    
    创建标识：Senparc - 20150507

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补充导入上下游联系人异步任务类型说明
----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 异步任务完成事件推送的BatchJob
    /// </summary>
    public class BatchJobInfo
    {
        /// <summary>
        /// 异步任务id，最大长度为64字符
        /// </summary>
        public string JobId { get; set; }

        /// <summary>
        /// 操作类型，字符串，目前分别有：
        /// 1. sync_user(增量更新成员)
        /// 2. replace_user(全量覆盖成员)
        /// 3. invite_user(邀请成员关注)
        /// 4. replace_party(全量覆盖部门)
        /// 5. import_chain_contact(导入上下游联系人)
        /// 6. export_user(导出成员详情)
        /// 7. export_simple_user(导出成员)
        /// 8. export_department(导出部门)
        /// 9. export_taguser(导出标签成员)
        /// </summary>
        public string JobType { get; set; }

        /// <summary>
        /// 返回码
        /// </summary>
        public int ErrCode { get; set; }

        /// <summary>
        /// 对返回码的文本描述内容
        /// </summary>
        public string ErrMsg { get; set; }
    }
}
