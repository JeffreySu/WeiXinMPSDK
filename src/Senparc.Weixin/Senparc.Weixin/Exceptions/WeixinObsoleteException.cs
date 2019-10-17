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

/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：WeixinObsoleteException.cs
    文件功能描述：v4.18.11 接口或方法过期异常
    
    
    创建标识：Senparc - 20180107
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.Exceptions
{
    /// <summary>
    /// 接口或方法过期异常
    /// </summary>
    public class WeixinObsoleteException : WeixinException
    {
        public WeixinObsoleteException(string message, bool logged = false) : base(message, logged)
        {
        }

        public WeixinObsoleteException(string message, Exception inner, bool logged = false) : base(message, inner, logged)
        {
        }
    }
}
