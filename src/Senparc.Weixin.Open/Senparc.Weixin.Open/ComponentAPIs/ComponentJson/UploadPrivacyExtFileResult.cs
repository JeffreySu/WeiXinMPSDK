/*----------------------------------------------------------------
    Copyright (C) 2021 Yaofeng

    文件名：UploadPrivacyExtFileResult.cs
    文件功能描述：上传小程序用户隐私保护指引


    创建标识：Yaofeng - 20211111

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;

namespace Senparc.Weixin.Open.ComponentAPIs
{
    /// <summary>
    /// 上传小程序用户隐私保护指引
    /// </summary>
    [Serializable]
    public class UploadPrivacyExtFileResult : WxJsonResult
    {
        /// <summary>
        /// 文件的media_id
        /// </summary>
        public string ext_file_media_id { get; set; }
    }
}