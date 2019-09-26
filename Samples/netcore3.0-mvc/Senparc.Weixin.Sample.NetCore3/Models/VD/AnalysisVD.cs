using System;
using System.Collections.Generic;
using System.Linq;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.CoreSample.Controllers;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Sample.NetCore3.Models.VD
{
    public class Analysis_IndexVD
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public AnalysisType AnalysisType { get; set; }
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public object Result { get; set; }
        public WxJsonResult WxJsonResult { get; set; }
    }
}