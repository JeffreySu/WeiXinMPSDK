/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：BatchJobInfo.cs
    文件功能描述：异步任务完成事件推送的BatchJob
    
    
    创建标识：Senparc - 20150507
----------------------------------------------------------------*/

namespace Senparc.Weixin.QY.Entities
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
