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
    
    文件名：DecodeEntityBase.cs
    文件功能描述：所有解密类的基类，提供watermark属性
    
    
    创建标识：Senparc - 20170131

    修改标识：Senparc - 20170522
    修改描述：v3.3.2 修改 DateTime 为 DateTimeOffset

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.CO2NET.Helpers;
//using Senparc.Weixin.Helpers;

namespace Senparc.Weixin.WxOpen.Entities
{
    [Serializable]
    public class DecodeEntityBase
    {
        public Watermark watermark { get; set; }
    }

    /// <summary>
    /// 水印
    /// </summary>
    [Serializable]
    public class Watermark
    {
        public string appid { get; set; }
        public long timestamp { get; set; }

        public DateTimeOffset DateTimeStamp
        {
            get { return DateTimeHelper.GetDateTimeFromXml(timestamp); }
        }
    }
}
