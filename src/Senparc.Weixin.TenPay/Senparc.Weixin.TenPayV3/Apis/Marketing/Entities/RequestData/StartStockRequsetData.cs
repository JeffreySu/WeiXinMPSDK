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
  
    文件名：StartStockRequsetData.cs
    文件功能描述：激活代金券批次请求数据
    
    
    创建标识：Senparc - 20210831
    
----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    /// <summary>
    /// 激活代金券批次API请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_3.shtml </para>
    /// </summary>
    public class StartStockRequsetData
    {
        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="stock_creator_mchid">创建批次的商户号</param>
        public StartStockRequsetData(string stock_creator_mchid)
        {
            this.stock_creator_mchid = stock_creator_mchid;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public StartStockRequsetData()
        {
        }

        /// <summary>
        /// 创建批次的商户号
        /// <para>批次创建方商户号</para>
        /// </summary>
        public string stock_creator_mchid { get; set; }
    }
}
