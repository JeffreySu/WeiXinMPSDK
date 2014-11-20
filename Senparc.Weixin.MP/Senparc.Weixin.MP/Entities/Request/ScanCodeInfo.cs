using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public class ScanCodeInfo
    {
        /// <summary>
        /// 扫描类型
        /// </summary>
        public string ScanType { get; set; }

        /// <summary>
        /// 扫描结果，即二维码或条码对应的字符串信息
        /// </summary>
        public string ScanResult { get; set; }
    }
}
