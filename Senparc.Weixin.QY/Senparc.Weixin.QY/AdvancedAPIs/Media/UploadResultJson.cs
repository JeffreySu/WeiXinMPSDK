﻿/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：UploadResultJson.cs
    文件功能描述：上传媒体文件返回结果
    
    
    创建标识：Senparc - 20130313
    
    修改标识：Senparc - 20130313
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.QY.AdvancedAPIs.Media
{
    /// <summary>
    /// 上传媒体文件返回结果
    /// </summary>
    public class UploadResultJson : WxJsonResult
    {
        public UploadMediaFileType type { get; set; }
        public string media_id { get; set; }
        public long created_at { get; set; }
    }
}
