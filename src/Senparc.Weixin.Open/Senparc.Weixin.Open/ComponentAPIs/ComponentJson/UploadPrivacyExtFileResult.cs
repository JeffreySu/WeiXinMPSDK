#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2021 Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

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