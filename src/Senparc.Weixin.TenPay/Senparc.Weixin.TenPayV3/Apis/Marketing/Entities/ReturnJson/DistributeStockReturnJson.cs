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
  
    文件名：DistributeStockReturnJson.cs
    文件功能描述：发放代金券批次返回Json类
    
    
    创建标识：Senparc - 20210823
    
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
    /// 发放代金券批次返回Json类
    /// <para><see href="https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_2.shtml">更多详细请参考微信支付官方文档</see></para>
    /// </summary>
    public class DistributeStockReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 代金券id
        /// <para>微信为代金券唯一分配的id<para>
        /// </summary>
        public string coupon_id { get; set; }
    }
}
