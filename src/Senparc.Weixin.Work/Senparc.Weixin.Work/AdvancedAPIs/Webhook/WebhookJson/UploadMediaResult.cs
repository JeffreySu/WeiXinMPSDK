using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Webhook
{
    public class UploadMediaResult : WorkJsonResult
    {
        public string type { get; set; }

        public string media_id { get; set; }

        public string created_at { get; set; }
    }
}
