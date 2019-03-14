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
    
    文件名：GetShakeInfoResultJson.cs
    文件功能描述：获取摇周边的设备及用户信息返回结果
    
    
    创建标识：Senparc - 20150512
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.ShakeAround
{
    /// <summary>
    /// 获取摇周边的设备及用户信息返回结果
    /// </summary>
    public class GetShakeInfoResultJson : WxJsonResult
    {
        /// <summary>
        /// 获取摇周边的设备及用户信息返回数据
        /// </summary>
        public ShakeInfo_Data data { get; set; }
    }

    public class ShakeInfo_Data
    {
        /// <summary>
        /// 摇周边页面唯一ID
        /// </summary>
        public string page_id { get; set; }
        /// <summary>
        /// 设备信息，包括UUID、major、minor，以及距离
        /// </summary>
        public ShakeInfo_Data_Beacon_Info beacon_info { get; set; }
        /// <summary>
        /// 商户AppID下用户的唯一标识
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 门店ID，有的话则返回，反之不会在JSON格式内
        /// </summary>
        public long poi_id { get; set; }
    }

    public class ShakeInfo_Data_Beacon_Info
    {
        /// <summary>
        /// Beacon信号与手机的距离，单位为米
        /// </summary>
        public double distance { get; set; }
        public long major { get; set; }
        public long minor { get; set; }
        public string uuid { get; set; }
    }
}