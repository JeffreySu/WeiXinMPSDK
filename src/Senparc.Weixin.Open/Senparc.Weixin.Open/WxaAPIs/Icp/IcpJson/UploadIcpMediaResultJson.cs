using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs.Icp
{
    /// <summary>
    /// 上传小程序备案媒体材料结果
    /// </summary>
    public class UploadIcpMediaResultJson :WxJsonResult
    {
        /// <summary>
        /// 媒体材料类型。目前支持两种：图片("image")和视频("video")
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 媒体id
        /// </summary>
        public string media_id { get; set; }

        /// <summary>
        /// 创建时间，UNIX时间戳
        /// </summary>
        public long create_at { get; set; }

    }
}
