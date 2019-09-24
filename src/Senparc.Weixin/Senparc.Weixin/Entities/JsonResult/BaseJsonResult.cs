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
    
    文件名：BaseJsonResult.cs
    文件功能描述：所有xxJsonResult（基类）的基类
    
    
    创建标识：Senparc - 20170702
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.Entities
{
    /// <summary>
    /// WxJsonResult 等 Json 结果的基类（抽象类），子类必须具有不带参数的构造函数
    /// </summary>
    [Serializable]
    public abstract class BaseJsonResult : IJsonResult
    {
        /// <summary>
        /// 返回结果信息
        /// </summary>
        public virtual string errmsg { get; set; }

        /// <summary>
        /// errcode的
        /// </summary>
        public abstract int ErrorCodeValue { get; }
        public virtual object P2PData { get; set; }
    }
}
