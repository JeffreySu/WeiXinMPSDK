using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.NeuChar
{
    /// <summary>
    /// 用于传入设置的基础模型
    /// </summary>
    public class ConfigRoot
    {
        public string Version { get; set; }
        public string Name { get; set; }
        public string Guid { get; set; }
        public string Key { get; set; }

        public string Configs { get; set; }
    }

}
