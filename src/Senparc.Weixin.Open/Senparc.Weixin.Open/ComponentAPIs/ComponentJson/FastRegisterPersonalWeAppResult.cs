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

    文件名：FastRegisterPersonalWeAppResult.cs
    文件功能描述：创建个人主体小程序接口返回结果


    创建标识： mc7246 - 20211107

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;

namespace Senparc.Weixin.Open.ComponentAPIs
{
    /// <summary>
    /// 创建个人主体小程序接口返回结果
    /// </summary>
    [Serializable]
    public class FastRegisterPersonalWeAppResult : WxJsonResult
    {
        /// <summary>
        /// 任务id，后面query查询需要用到
        /// </summary>
        public string taskid { get; set; }

        /// <summary>
        /// 给用户扫码认证的验证url
        /// </summary>
        public string authorize_url { get; set; }

        /// <summary>
        /// 任务的状态 0成功
        /// </summary>
        public int status { get; set; }
    }
}
