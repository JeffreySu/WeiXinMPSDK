using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Mass
{
    /// <summary>
    /// 发送消息返回结果
    /// 如果对应用或收件人、部门、标签任何一个无权限，则本次发送失败；如果收件人、部门或标签不存在，发送仍然执行，但返回无效的部分。
    /// </summary>
    public class MassResult : WorkJsonResult
    {
        public string invaliduser { get; set; }
        public string invalidparty { get; set; }
        public string invalidtag { get; set; }
    }


    public class SendMiniProgramNoticeData
    {
        public string touser { get; set; }
        public string toparty { get; set; }
        public string totag { get; set; }
        public string msgtype { get; set; }
        public Miniprogram_Notice miniprogram_notice { get; set; }
    }

    public class SendTaskcardNoticeData
    {
        public string touser { get; set; }
        public string toparty { get; set; }
        public string totag { get; set; }
        public string msgtype { get; set; }
        public int agentid { get; set; }
        public Taskcard_Notice taskcard { get; set; }
    }


    public class UpdateTaskcardData
    {
        public string[] userids { get; set; }
        public int agentid { get; set; }
        public string task_id { get; set; }
        public string clicked_key { get; set; }
    }
}
