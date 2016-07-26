/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：RequestMessageEvent_Batch_Job_Result.cs
    文件功能描述：异步任务完成事件推送(batch_job_result)
    
    
    创建标识：Senparc - 20150507
----------------------------------------------------------------*/

namespace Senparc.Weixin.QY.Entities
{
    /// <summary>
    /// 异步任务完成事件推送(batch_job_result)
    /// </summary>
    public class RequestMessageEvent_Batch_Job_Result : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.BATCH_JOB_RESULT; }
        }

        /// <summary>
        /// 异步任务完成事件推送BatchJob
        /// </summary>
        public BatchJobInfo BatchJob { get; set; }
    }
}
