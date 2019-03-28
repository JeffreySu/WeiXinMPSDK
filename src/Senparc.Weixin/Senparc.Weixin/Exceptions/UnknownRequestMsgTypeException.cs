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
    
    文件名：UnknownRequestMsgTypeException.cs
    文件功能描述：未知请求类型
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20170101
    修改描述：1、统一构造函数调用（将第一个构造函数的base改为this）
              2、修改基类为MessageHandlerException
----------------------------------------------------------------*/


using System;

namespace Senparc.Weixin.Exceptions
{
    /// <summary>
    /// 未知请求类型异常
    /// </summary>
    public class UnknownRequestMsgTypeException : MessageHandlerException //ArgumentOutOfRangeException
    {
        public UnknownRequestMsgTypeException(string message)
            : this(message, null)
        {
        }

        public UnknownRequestMsgTypeException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
