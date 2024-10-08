#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2024 Senparc
    
    文件名：GetRecentAverageResultJson.cs
    文件功能描述：小程序 获取小程序某个付费能力的最近用量数据 返回结果
    
    
    创建标识：mc7246 - 20240831
    
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Charge
{
    /// <summary>
    /// 小程序 获取小程序某个付费能力的最近用量数据 返回结果
    /// </summary>
    public class GetRecentAverageResultJson: WxJsonResult
    {
        /// <summary>
        /// 最近月平均用量，经模糊化处理，非精确值
        /// 当averageData返回值为50时，语义为小程序最近平均用量小于等于50次/月，并不是特指精确等于每月50次。
        /// </summary>
        public int averageData { get; set; }
    }
}
