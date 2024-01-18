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

    文件名：FastRegisterBetaWeAppResult.cs
    文件功能描述：创建试用小程序接口返回结果


    创建标识： mc7246 - 20220329

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;

namespace Senparc.Weixin.Open.ComponentAPIs
{
    /// <summary>
    /// 创建试用小程序接口返回结果
    /// </summary>
    [Serializable]
    public class FastRegisterBetaWeAppResult : WxJsonResult
    {
        /// <summary>
        /// 该请求的唯一标识符，用于关联微信用户和后面产生的appid
        /// </summary>
        public string unique_id { get; set; }

        /// <summary>
        /// 用户授权确认url，需将该url发送给用户，用户进入授权页面完成授权方可创建小程序
        /// </summary>
        public string authorize_url { get; set; }

    }
}
