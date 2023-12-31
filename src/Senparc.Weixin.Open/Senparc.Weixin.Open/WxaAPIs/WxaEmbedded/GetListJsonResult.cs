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
  
    文件名：GetListJsonResult.cs
    文件功能描述：半屏小程序列表结果
    
    
    创建标识：mc7246 - 20220706

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;

namespace Senparc.Weixin.Open.WxaAPIs.WxaEmbedded
{
    /// <summary>
    /// 半屏小程序列表
    /// https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/embedded-management/getEmbeddedList.html
    /// </summary>
    [Serializable]
    public class GetListJsonResult: WxJsonResult
    {
        /// <summary>
        /// 半屏小程序列表
        /// </summary>
        public List<WxaEmbeddedInfo> wxa_embedded_list { get; set; }

        /// <summary>
        /// 授权方式。0表示需要管理员确认，1表示自动通过，2表示自动拒绝
        /// </summary>
        public int embedded_flag { get; set; }
    }

    /// <summary>
    /// 半屏小程序详情
    /// </summary>
    [Serializable]
    public class WxaEmbeddedInfo
    {
        /// <summary>
        /// 半屏小程序appid
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public string create_time { get; set; }

        /// <summary>
        /// 头像url
        /// </summary>
        public string headimg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 半屏小程序昵称
        /// </summary>
        public string nickname { get; set; }

        /// <summary>
        /// 申请理由
        /// </summary>
        public string reason { get; set; }

        /// <summary>
        /// 申请状态
        /// </summary>
        public int status { get; set; }
    }
}
