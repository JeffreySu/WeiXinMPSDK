#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Suzhou Senparc Network Technology Co.,Ltd.

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
    
    文件名：WorkResponseMessageUnknownType.cs
    文件功能描述：企业号未知响应类型
    
    
    创建标识：Senparc - 20190916
    
----------------------------------------------------------------*/


using Senparc.NeuChar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Senparc.Weixin.Work.Entities.Response
{
    /// <summary>
    /// 企业号未知响应类型
    /// </summary>
    public class WorkResponseMessageUnknownType : WorkResponseMessageBase, IWorkResponseMessageBase
    {
        public override ResponseMsgType MsgType
        {
            get { return ResponseMsgType.Unknown; }
        }

        /// <summary>
        /// 响应消息的XML对象（明文）
        /// </summary>
        public XDocument ResponseDocument { get; set; }

    }
}
