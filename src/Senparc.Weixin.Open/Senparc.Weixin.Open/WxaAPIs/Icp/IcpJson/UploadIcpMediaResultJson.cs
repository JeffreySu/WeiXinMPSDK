/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：UploadIcpMediaResultJson.cs
    文件功能描述：上传小程序备案媒体材料结果 接口返回消息
    
    
    创建标识：Senparc - 20230905

----------------------------------------------------------------*/

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
    public class UploadIcpMediaResultJson : WxJsonResult
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
