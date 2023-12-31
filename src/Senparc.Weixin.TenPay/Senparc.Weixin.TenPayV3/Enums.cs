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
  
    文件名：Enums.cs
    文件功能描述：应答的语种的枚举类
    
    
    创建标识：Senparc - 20210804
    
----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.TenPayV3
{
    /// <summary>
    /// 应答的语种
    /// </summary>
    public class AcceptLanguage
    {
        public const string EN = "en";
        public const string ZH_CN = "zh-CN";
        public const string ZH_HK = "zh-HK";
        public const string ZH_TW = "zh-TW";
    }

    /// <summary>
    /// API 请求方法
    /// </summary>
    public enum ApiRequestMethod
    {
        GET,
        POST,
        PUT,
        PATCH,
        DELETE
    }

    /// <summary>
    /// 支付请求签名算法类型
    /// </summary>
    public enum CertType
    {
        RSA,
        SM
    }
}
