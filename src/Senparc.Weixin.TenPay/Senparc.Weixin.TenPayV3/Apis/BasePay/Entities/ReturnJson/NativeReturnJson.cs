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
  
    文件名：NativeReturnJson.cs
    文件功能描述：Native下单返回Json类
    
    
    创建标识：Senparc - 20210804
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.BasePay
{
    public class NativeReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 二维码链接
        /// 此URL用于生成支付二维码，然后提供给用户扫码支付。
        /// 注意：code_url并非固定值，使用时按照URL格式转成二维码即可。
        /// 示例值：weixin://wxpay/bizpayurl/up?pr=NwY5Mz9&amp;groupid=00
        /// </summary>
        public string code_url { get; set; }
    }
}
