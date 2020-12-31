using Senparc.Weixin.Work.Entities.Models;

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    public class SendWelcomeMessageRequest
    {
        public string welcome_code { get; set; }
        public MessageText text { get; set; }
        public MessageImage image { get; set; }
        public MessageLink link { get; set; }
        public MessageMiniprogram miniprogram { get; set; }
    }
}