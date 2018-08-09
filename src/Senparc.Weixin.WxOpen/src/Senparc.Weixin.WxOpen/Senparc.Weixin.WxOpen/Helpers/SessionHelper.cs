﻿#region Apache License Version 2.0
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
    
    文件名：SessionHelper.cs
    文件功能描述：Session帮助类
    
    
    创建标识：Senparc - 20170129
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.Helpers
{
    /// <summary>
    /// Session帮助类
    /// </summary>
    public class SessionHelper
    {
        /// <summary>
        /// 获取新的3rdSession名称
        /// </summary>
        /// <param name="bSize">Session名称长度，单位：B，建议为16的倍数，通常情况下16B已经够用（32位GUID字符串）</param>
        /// <returns></returns>
        public static string GetNewThirdSessionName(int bSize = 16)
        {
            string key = null;
            for (int i = 0; i < bSize / 16; i++)
            {
                key += Guid.NewGuid().ToString("n");
            }
            return key;
        }
    }
}
