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
  
    文件名：BillData.cs
    文件功能描述：新微信支付V3账单数据
    
    
    创建标识：Senparc - 20210814
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.BasePay
{
    /// <summary>
    /// 账单数据
    /// </summary>
    public class BillReturnJson:ReturnJsonBase
    {
        /// <summary>
        /// 哈希类型
        /// 原始账单（gzip需要解压缩）的摘要值，用于校验文件的完整性。
        /// 示例值：SHA1
        /// </summary>
        public string hash_type { get; set; }

        /// <summary>
        /// 哈希值
        /// 原始账单（gzip需要解压缩）的摘要值，用于校验文件的完整性。
        /// 示例值：79bb0f45fc4c42234a918000b2668d689e2bde04
        /// </summary>
        public string hash_value { get; set; }

        /// <summary>
        /// 账单下载地址
        /// 供下一步请求账单文件的下载地址，该地址30s内有效。
        /// 示例值：https://api.mch.weixin.qq.com/v3/billdownload/file?token=xxx
        /// </summary>
        public string download_url { get; set; }
    }
}
