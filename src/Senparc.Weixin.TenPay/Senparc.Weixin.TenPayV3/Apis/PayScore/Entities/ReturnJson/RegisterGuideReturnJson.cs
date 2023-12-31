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
  
    文件名：RegisterGuideReturnJson.cs
    文件功能描述：服务人员注册Json类
    
    
    创建标识：Senparc - 20210925
    
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
    /// 服务人员注册Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_4_1.shtml </para>
    /// </summary>
    public class RegisterGuideReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="guide_id">服务人员ID <para>服务人员在服务人员系统中的唯一标识</para><para>示例值：LLA3WJ6DSZUfiaZDS79FH5Wm5m4X69TBic</para></param>
        public RegisterGuideReturnJson(string guide_id)
        {
            this.guide_id = guide_id;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public RegisterGuideReturnJson()
        {
        }

        /// <summary>
        /// 服务人员ID
        /// <para>服务人员在服务人员系统中的唯一标识 </para>
        /// <para>示例值：LLA3WJ6DSZUfiaZDS79FH5Wm5m4X69TBic</para>
        /// </summary>
        public string guide_id { get; set; }

    }






}
