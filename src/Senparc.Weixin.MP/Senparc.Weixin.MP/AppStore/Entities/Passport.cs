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
  
    文件名：Passport.cs
    文件功能描述：P2P通行证
    
    
    创建标识：Senparc - 20150319
----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.MP.AppStore
{
    /// <summary>
    /// P2P通行证
    /// </summary>
    public class Passport : IAppResultData
    {
        public string Token { get; set; }
        public string AppKey { get; set; }
        public string Secret { get; set; }
        /// <summary>
        /// API常规URL
        /// </summary>
        public string ApiUrl { get; set; }

        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 供微微嗨服务器记录唯一开发人员ID
        /// </summary>
        public int DeveloperId { get; set; }
    }
}
