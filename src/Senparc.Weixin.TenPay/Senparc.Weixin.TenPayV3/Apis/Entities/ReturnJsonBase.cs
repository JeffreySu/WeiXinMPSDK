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
  
    文件名：ReturnJsonBase.cs
    文件功能描述：ReturnJson 的基类
    
    
    创建标识：Senparc - 20210804
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Entities
{
    /// <summary>
    /// ReturnJson 的基类（预留）
    /// </summary>
    public class ReturnJsonBase
    {
        /// <summary>
        /// 回复状态码
        /// </summary>
        public TenPayApiResultCode ResultCode { get; set; } = new TenPayApiResultCode();

        /// <summary>
        /// 回复签名是否正确 在有错误的情况下，或不要求验证签名时 为null
        /// <para>通常情况下，必须为 true 才表明签名通过</para>
        /// <para>注意：在 204（NoContent） 的 <see cref="System.Net.HttpStatusCode"/> 下，此属性始终为 true</para>
        /// </summary>
        public bool? VerifySignSuccess { get; set; } = null; 
    }
}
