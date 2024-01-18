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
  
    文件名：DownloadStockUseFlowReturnJson.cs
    文件功能描述：下载批次核销明细返回Json
    
    
    创建标识：Senparc - 20210901
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    /// <summary>
    /// 下载批次核销明细返回Json
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_10.shtml </para>
    /// </summary>
    public class DownloadStockUseFlowReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 流水文件下载链接，30s内有效
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 文件内容的哈希值，防止篡改
        /// </summary>
        public string hash_value { get; set; }
 
        /// <summary>
        /// 哈希算法类型，目前只支持sha1
        /// </summary>
        public string hash_type { get; set; }
    }
}
