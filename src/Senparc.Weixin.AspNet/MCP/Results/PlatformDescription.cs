using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.AspNet.MCP
{
    public class PlatformDescription
    {
        public PlatformDescription(string platformName, string description)
        {
            PlatformName = platformName;
            Description = description;
        }

        public string PlatformName { get; set; }
        public string Description { get; set; }
    }
}
