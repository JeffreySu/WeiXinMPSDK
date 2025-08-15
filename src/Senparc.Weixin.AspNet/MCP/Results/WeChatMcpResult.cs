using Senparc.CO2NET.HttpUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.AspNet.MCP
{
    public class WeChatMcpResult<T>
    {
        [Description("下一步需要执行的操作")]
        public string NextRoundTip { get; set; }
        public T Result { get; set; }
    }
}
