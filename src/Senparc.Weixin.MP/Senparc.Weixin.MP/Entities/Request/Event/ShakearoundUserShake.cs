#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc
    
    文件名：ShakearoundUserShake.cs
    文件功能描述：系统拍照发图中的SendPicsInfo
    
    
    创建标识：Senparc - 20150914
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// Beacon的参数以及距离
    /// </summary>
    public class BaseBeaconItem
    {
        public string Uuid { get; set; }
        public long Major { get; set; }
        public long Minor { get; set; }
        /// <summary>
        /// 设备与用户的距离（浮点数；单位：米）
        /// </summary>
        public double Distance { get; set; }
    }

    public class ChosenBeacon : BaseBeaconItem
    {
        
    }

    public class AroundBeacon : BaseBeaconItem
    {

    }
}
