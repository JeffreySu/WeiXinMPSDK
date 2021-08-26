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
  
    文件名：Settle_Info.cs
    文件功能描述：下单请求数据结算信息
    
    
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
    /// 结算信息
    /// </summary>
    public class Settle_Info
    {
        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="profit_sharing">是否指定分账，可为null</param>
        public Settle_Info(bool profit_sharing)
        {
            this.profit_sharing = profit_sharing;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public Settle_Info()
        {
        }

        /// <summary>
        /// 是否指定分账
        /// </summary>
        public bool profit_sharing { get; set; }
    }
}
