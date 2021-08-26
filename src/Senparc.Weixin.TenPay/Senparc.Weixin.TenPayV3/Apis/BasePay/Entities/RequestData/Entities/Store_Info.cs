#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2021 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2021 Senparc
  
    文件名：Store_Info.cs
    文件功能描述：下单请求数据商户门店信息
    
    
    创建标识：Senparc - 20210825
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.BasePay.Entities.RequestData.Entities
{
    /// <summary>
    /// 商户门店信息
    /// </summary>
    public class Store_Info
    {
        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="id">商户侧门店编号</param>
        /// <param name="name">商户侧门店名称，可为null</param>
        /// <param name="area_code">地区编码，详细请见省市区编号对照表，可为null</param>
        /// <param name="address">详细地址，可为null</param>
        public Store_Info(string id, string name, string area_code, string address)
        {
            this.address = address;
            this.area_code = area_code;
            this.name = name;
            this.id = id;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public Store_Info()
        {
        }

        /// <summary>
        /// 详细地址
        /// 详细的商户门店地址
        /// 示例值：广东省深圳市南山区科技中一道10000号
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 地区编码	
        /// 地区编码，详细请见省市区编号对照表。
        /// 示例值：440305
        /// </summary>
        public string area_code { get; set; }

        /// <summary>
        /// 门店名称
        /// 商户侧门店名称
        /// 示例值：腾讯大厦分店
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 门店编号
        /// 商户侧门店编号
        /// 示例值：0001
        /// </summary>
        public string id { get; set; }
    }
}
