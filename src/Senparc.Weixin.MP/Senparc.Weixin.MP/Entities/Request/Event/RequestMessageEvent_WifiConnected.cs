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
    
    文件名：RequestMessageEvent_WifiConnected.cs
    文件功能描述：事件之Wi-Fi连网成功
    
    
    创建标识：Senparc - 20150709
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 事件之Wi-Fi连网成功
    /// </summary>
    public class RequestMessageEvent_WifiConnected : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.WifiConnected; }
        }

        /// <summary>
        /// 连网时间
        /// </summary>
        public int ConnectTime { get; set; }
        /// <summary>
        /// 系统保留字段，固定值
        /// </summary>
        public int ExpireTime { get; set; }
        /// <summary>
        /// 系统保留字段，固定值
        /// </summary>
        public string VendorId { get; set; }
        /// <summary>
        /// 连网的门店id
        /// </summary>
        public string PlaceId { get; set; }
        /// <summary>
        /// 连网的设备无线mac地址，对应bssid
        /// </summary>
        public string DeviceNo { get; set; }
    }
}
