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
    
    文件名：WeixinNullReferenceException.cs
    文件功能描述：对象为null的异常
    
    
    创建标识：Senparc - 20170912
    
----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.Exceptions
{
    /// <summary>
    /// Null异常
    /// </summary>
    public class WeixinNullReferenceException : WeixinException
    {
        /// <summary>
        /// 上一级不为null的对象（或对调试很重要的对象）。
        /// 如果需要调试多个对象，可以传入数组，如：new {obj1, obj2}
        /// </summary>
        public object ParentObject { get; set; }
        public WeixinNullReferenceException(string message)
            : this(message, null, null)
        {
        }

        public WeixinNullReferenceException(string message, object parentObject)
            : this(message, parentObject, null)
        {
            ParentObject = parentObject;
        }

        public WeixinNullReferenceException(string message, object parentObject, Exception inner)
            : base(message, inner)
        {
            ParentObject = parentObject;
        }
    }
}
