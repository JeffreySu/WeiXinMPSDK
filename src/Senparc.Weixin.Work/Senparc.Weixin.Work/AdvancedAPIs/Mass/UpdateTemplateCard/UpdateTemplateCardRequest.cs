using Senparc.Weixin.Work.AdvancedAPIs.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Mass.UpdateTemplateCard
{
    public class UpdateTemplateCardRequest
    {
        public string[] userids { get; set; }
        public int[] partyids { get; set; }

        public int[] tagids { get; set; }

        public int atall { get;set; }

        public int agentid { get; set; }

        public string response_code { get; set; }

        public int enable_id_trans { get; set; } = 0;

        public SendTemplateCard.Template_CardBase template_card { get; private set; }

        public Button button { get; set; }

        UpdateTemplateCardRequest() { 
        
        }

        public UpdateTemplateCardRequest(SendTemplateCard.Template_CardBase templateCard): this()
        {
            template_card = templateCard;
        }

        public class Button
        {
            public string replace_name { get; set; }
        }
    }
}
