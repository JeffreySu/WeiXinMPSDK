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
    
    文件名：ReauthResultJson.cs
    文件功能描述：小程序认证重新提审
    
    
    创建标识：Yaofeng - 20231130

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Open.WxaAPIs.Sec
{
    /// <summary>
    /// 小程序认证重新提审
    /// </summary>
    public class ReauthJsonResult : WxJsonResult
    {
        /// <summary>
        /// 认证任务id
        /// </summary>
        public string taskid { get; set; }

        /// <summary>
        /// 小程序管理员授权链接
        /// </summary>
        public string auth_url { get; set; }
    }
}