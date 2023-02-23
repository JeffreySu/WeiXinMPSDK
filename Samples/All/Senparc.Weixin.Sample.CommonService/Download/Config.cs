using System.Collections.Generic;

namespace Senparc.Weixin.Sample.CommonService.Download
{
    public class Config
    {
        public int QrCodeId { get; set; }
        /// <summary>
        /// chm版
        /// </summary>
        public List<string> Versions { get; set; }
        /// <summary>
        /// 网页版
        /// </summary>
        public List<string> WebVersions { get; set; }
        public int DownloadCount { get; set; }

    }
}
