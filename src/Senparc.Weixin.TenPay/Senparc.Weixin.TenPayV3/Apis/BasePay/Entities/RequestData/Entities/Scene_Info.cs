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
  
    文件名：Scene_Info.cs
    文件功能描述：下单请求数据场景信息
    
    
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
    /// 场景信息
    /// </summary>
    public class Scene_Info
    {
        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="payer_client_ip">用户终端IP</param>
        /// <param name="device_id">商户端设备号，可为null</param>
        /// <param name="store_info">商户门店信息，下单个订单可为null，下合单必须为null</param>
        /// <param name="h5_info">H5场景信息，H5下单必填，其它支付方式必须为null</param>
        public Scene_Info(string payer_client_ip, string device_id, Store_Info store_info, H5_Info h5_info = null)
        {
            this.store_info = store_info;
            this.device_id = device_id;
            this.payer_client_ip = payer_client_ip;
            this.h5_info = h5_info;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public Scene_Info()
        {
        }

        /// <summary>
        /// 商户门店信息
        /// </summary>
        public Store_Info store_info { get; set; }

        /// <summary>
        /// 商户端设备号
        /// 商户端设备号（门店号或收银设备ID）。
        /// 示例值：013467007045764
        /// </summary>
        public string device_id { get; set; }

        /// <summary>
        /// 用户终端IP
        /// 用户的客户端IP，支持IPv4和IPv6两种格式的IP地址。
        /// 示例值：14.23.150.211
        /// </summary>
        public string payer_client_ip { get; set; }

        /// <summary>
        /// H5场景信息
        /// </summary>
        public H5_Info h5_info { get; set; }
    }
}
