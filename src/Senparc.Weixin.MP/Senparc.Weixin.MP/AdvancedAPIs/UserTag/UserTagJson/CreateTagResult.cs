using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.UserTag
{
    public class CreateTagResult : WxJsonResult
    {
        public TagJson_Tag tag { get; set; }
    }
}