#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2025 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2025 Senparc
  
    文件名：UploadVideoReturnJson.cs
    文件功能描述：视频上传返回结果
    
    
    创建标识：Senparc - 20250813
    
----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    /// <summary>
    /// 视频上传 返回结果
    /// </summary>
    public class UploadVideoReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 视频MediaID
        /// <para>微信返回的视频MediaID</para>
        /// <para>示例值：6uqyGjGrCf2GtyXP8bxrblWMLIsMaAaY</para>
        /// </summary>
        public string media_id { get; set; }
    }
}


