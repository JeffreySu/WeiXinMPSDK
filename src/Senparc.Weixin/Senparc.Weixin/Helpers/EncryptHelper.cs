#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc
 
    创建标识：Senparc - 20160808
    创建描述：安全帮助类，提供SHA-1、AES算法等

    修改标识：Senparc - 20170130
    修改描述：v4.9.15 添加AES加密、解密算法
    
    修改标识：Senparc - 20170313
    修改描述：v4.11.4 修改EncryptHelper.GetSha1(string encypStr)方法算法
      
    修改标识：Senparc - 20170313
    修改描述：v4.14.3 重构MD5生成方法，并提供小写MD5方法
    
    修改标识：Senparc - 20180101
    修改描述：v4.18.10 添加 EncryptHelper.GetHmacSha256() 方法，为“小游戏”签名提供支持


    
    ----  CO2NET   ----

    修改标识：Senparc - 20180601
    修改描述：v5.0.0 引入 Senparc.CO2NET

----------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Senparc.Weixin.Helpers
{
    /// <summary>
    /// 安全帮助类，提供SHA-1算法等
    /// </summary>
    [Obsolete("请使用 Senparc.CO2NET.Helpers.EncryptHelper 类")]
    public class EncryptHelper
    {

    }
}
