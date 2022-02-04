using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.CLI
{
    /// <summary>
    /// SDK QQ群状态
    /// </summary>
    public class SdkQQGroup
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string GroupNumber { get; set; }
        public bool Open { get; set; }
        public string Code { get; set; }
        public string Note { get; set; }
    }
}
