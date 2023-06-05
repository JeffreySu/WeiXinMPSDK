using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.Entities.Request.Event
{
    public class RequestMessageEvent_Switch_WorkBench_Mode:RequestMessageEventBase,IRequestMessageEventBase
    {
        public override Work.Event Event
        {
            get { return Work.Event.switch_workbench_mode; }
        }
        /// <summary>
        /// 1表示开启工作台自定义模式，0表示关闭工作台自定义模式
        /// </summary>
        public int Mode { get; set; }
        /// <summary>
        /// 企业应用的id，整型。可在应用的设置页面查看
        /// </summary>
        public int AgentID { get; set; }
    }
}
