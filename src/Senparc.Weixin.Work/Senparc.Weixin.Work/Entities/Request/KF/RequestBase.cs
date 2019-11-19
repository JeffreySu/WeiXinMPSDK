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

    文件名：RequestBase.cs
    文件功能描述：所有客服请求消息的基类


    创建标识：Senparc - 20180616

    修改标识：Senparc - 20181226
    修改描述：v3.3.2 修改 DateTime 为 DateTimeOffset

----------------------------------------------------------------*/

using Senparc.NeuChar;
using System;

namespace Senparc.Weixin.Work.Entities.Request.KF
{
    public class RequestBase
    {
        public RequestMsgType MsgType { get; protected set; }
        public string FromUserName { get; set; }
        public DateTimeOffset CreateTime { get; set; }
    }
}
