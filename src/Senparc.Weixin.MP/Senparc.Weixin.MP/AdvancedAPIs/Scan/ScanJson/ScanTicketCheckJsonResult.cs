﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2020 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2020 Senparc
    
    文件名：ScanTicketCheckJsonResult.cs
    文件功能描述： 检查wxticket参数的返回结果
    
    
    创建标识：Senparc - 20160520
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Scan
{
    /// <summary>
    /// 检查wxticket参数的返回结果
    /// </summary>
    public class ScanTicketCheckJsonResult : WxJsonResult 
    {
        /// <summary>
        /// 商品编码标准。
        /// </summary>
        public string keystandard { get; set; }
        /// <summary>
        /// 商品编码内容。
        /// </summary>
        public string keystr { get; set; }
        /// <summary>
        /// 当前访问者的openid，可唯一标识用户。
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 打开商品主页的场景，scan为扫码，others为其他场景，可能是会话、收藏或朋友圈。
        /// </summary>
        public string scene { get; set; }
        /// <summary>
        /// 该条码（二维码）是否被扫描，true为是，false为否。
        /// </summary>
        public string is_check { get; set; }
        /// <summary>
        /// 是否关注公众号，true为已关注，false为未关注。
        /// </summary>
        public string is_contact { get; set; }
    }
}
