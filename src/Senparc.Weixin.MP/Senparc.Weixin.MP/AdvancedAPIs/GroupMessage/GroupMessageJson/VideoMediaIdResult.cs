#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc
    
    文件名：VideoMediaIdResult.cs
    文件功能描述：群发视频文件调用接口获取视频群发用的MediaId
    
    
    创建标识：Senparc - 20150623
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.GroupMessage
{
    public class VideoMediaIdResult : WxJsonResult
    {
        /// <summary>
        /// mediaId
        /// </summary>
        public string media_id { get; set; }
        /// <summary>
        /// 类型（通常为video）
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 创建时间戳
        /// </summary>
        public long created_at { get; set; }
    }
}
