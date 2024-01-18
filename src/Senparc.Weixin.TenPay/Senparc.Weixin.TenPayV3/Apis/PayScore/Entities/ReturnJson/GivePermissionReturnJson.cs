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
  
    文件名：GivePermissionReturnJson.cs
    文件功能描述：商户预授权Json类
    
    
    创建标识：Senparc - 20210915
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.PayScore
{
    /// <summary>
    /// 商户预授权Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_2.shtml </para>
    /// </summary>
    public class GivePermissionReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="apply_permissions_token">预授权token <para>用于跳转到微信侧小程序授权数据,跳转到微信侧小程序传入，时效性为1小时</para><para>示例值：apply_permissions_token</para></param>
        public GivePermissionReturnJson(string apply_permissions_token)
        {
            this.apply_permissions_token = apply_permissions_token;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public GivePermissionReturnJson()
        {
        }

        /// <summary>
        /// 预授权token
        /// <para>用于跳转到微信侧小程序授权数据,跳转到微信侧小程序传入，时效性为1小时 </para>
        /// <para>示例值：apply_permissions_token</para>
        /// </summary>
        public string apply_permissions_token { get; set; }

    }






}
