﻿#region Apache License Version 2.0
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
    
    文件名：WiFiConnectUrlResultJson.cs
    文件功能描述：获取公众号连网URL返回结果
    
    
    创建标识：Senparc - 20160506
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.WiFi
{
    /// <summary>
    /// WiFiConnectUrlResultJson
    /// </summary>
    public class WiFiConnectUrlResultJson : WxJsonResult
    {
        /// <summary>
        /// data
        /// </summary>
        public WiFiConnectUrl_Data data { get; set; }
    }

    /// <summary>
    /// WiFiConnectUrl_Data
    /// </summary>
    public class WiFiConnectUrl_Data
    {
        public string connect_url { get; set; }
    }
}
