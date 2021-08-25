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
  
    文件名：Payer.cs
    文件功能描述：下单请求支付者信息
    
    
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
    /// 支付者信息
    /// </summary>
    public class Payer
    {
        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="openid">用户在直连商户appid下的唯一标识</param>
        public Payer(string openid)
        {
            this.openid = openid;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public Payer()
        {
        }

        /// <summary>
        /// 用户标识	
        /// 用户在直连商户appid下的唯一标识
        /// 示例值：oUpF8uMuAJO_M2pxb1Q9zNjWeS6o
        /// </summary>
        public string openid { get; set; }
    }
}

