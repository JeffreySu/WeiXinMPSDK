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
    
    文件名：EntityHelper.cs
    文件功能描述：实体与xml相互转换
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20170810
    修改描述：v14.5.9 提取EntityHelper.FillClassValue()方法，优化FillEntityWithXml()方法

    修改标识：Senparc - 20180901
    修改描述：优化FillEntityWithXml()方法


----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.Entities;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Utilities;

namespace Senparc.Weixin.MP.Helpers
{
    /// <summary>
    /// 实体帮助类
    /// </summary>
    public static class EntityHelper
    {
        /// <summary>
        /// 检查是否是通过场景二维码扫入
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public static bool IsFromScene(this RequestMessageEvent_Subscribe requestMessage)
        {
            return !string.IsNullOrEmpty(requestMessage.EventKey);
        }
    }
}
