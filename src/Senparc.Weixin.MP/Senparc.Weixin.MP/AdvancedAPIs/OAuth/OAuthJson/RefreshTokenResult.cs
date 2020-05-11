#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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

using System;
using System.Collections.Generic;
using System.Linq;
/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RefreshTokenResult.cs
    文件功能描述：通过RefreshToken接口返回的结果
    
    
    创建标识：Senparc - 20161019
    
----------------------------------------------------------------*/

using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.OAuth
{
    /// <summary>
    /// 通过RefreshToken接口返回的结果
    /// </summary>
    public class RefreshTokenResult : OAuthAccessTokenResult
    {
        //public string access_token { get; set; }
        //public int expires_in { get; set; }
        //public  string refresh_token { get; set; }

        //public string  openId { get; set; }
    }
}
