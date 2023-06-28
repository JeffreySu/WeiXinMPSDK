namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 事件之推送发布结果
    /// </summary>
    public class RequestMessageEvent_Publish_Job_Finish : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.publish_job_finish; }
        }

        public NeuChar.Entities.Publish_Event_Info PublishEventInfo { get; set; }
    }
}
