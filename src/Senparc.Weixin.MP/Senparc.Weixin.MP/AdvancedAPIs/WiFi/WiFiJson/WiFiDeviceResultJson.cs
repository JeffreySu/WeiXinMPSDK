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
    
    文件名：WiFiDeviceResultJson.cs
    文件功能描述：WiFi设备接口返回结果
    
    
    创建标识：Senparc - 20150709
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.WiFi
{
    /// <summary>
    /// 查询设备返回结果
    /// </summary>
    public class GetDeviceListResult : WxJsonResult
    {
        public GetDeviceList_Data data { get; set; }
    }

    public class GetDeviceList_Data
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int recordcount { get; set; }
        /// <summary>
        /// 分页下标
        /// </summary>
        public int pageindex { get; set; }
        /// <summary>
        /// 分页页数
        /// </summary>
        public int pagecount { get; set; }
        /// <summary>
        /// 当前页列表数组
        /// </summary>
        public List<GetDeviceList_Data_Record> records { get; set; }
    }

    public class GetDeviceList_Data_Record
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long shop_id { get; set; }
        /// <summary>
        /// 连网设备ssid
        /// </summary>
        public string ssid { get; set; }
        /// <summary>
        /// 无线MAC地址
        /// </summary>
        public string bssid { get; set; }
    }
}
