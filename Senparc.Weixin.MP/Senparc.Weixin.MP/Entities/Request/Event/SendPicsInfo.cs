﻿/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：SendPicsInfo.cs
    文件功能描述：系统拍照发图中的SendPicsInfo
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 系统拍照发图中的SendPicsInfo
    /// </summary>
    public class SendPicsInfo
    {
        /// <summary>
        /// 发送的图片数量
        /// </summary>
        public string Count { get; set; }
        /// <summary>
        /// 图片列表
        /// </summary>
        public List<PicItem> PicList { get; set; }
    }

    public class PicItem
    {
        public Md5Sum item { get; set; }
    }

    public class Md5Sum
    {
        /// <summary>
        /// 图片的MD5值，开发者若需要，可用于验证接收到图片
        /// </summary>
        public string PicMd5Sum { get; set; }
    }
}
