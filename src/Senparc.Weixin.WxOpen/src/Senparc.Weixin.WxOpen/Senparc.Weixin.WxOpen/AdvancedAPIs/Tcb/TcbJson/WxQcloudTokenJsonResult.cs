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
    
    文件名：WxQcloudTokenJsonResult.cs
    文件功能描述：获取腾讯云API调用凭证 返回结果
    
    
    创建标识：lishewen - 20190530
   
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Tcb
{
    /// <summary>
    /// 获取腾讯云API调用凭证 返回结果
    /// </summary>
    public class WxQcloudTokenJsonResult : WxJsonResult
    {
        public string secretid { get; set; }
        public string secretkey { get; set; }
        public string token { get; set; }
        /// <summary>
        /// 过期时间戳
        /// </summary>
        public int expired_time { get; set; }
    }
}
