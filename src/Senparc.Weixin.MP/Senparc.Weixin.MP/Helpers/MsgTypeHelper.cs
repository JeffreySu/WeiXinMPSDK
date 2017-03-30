#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2017 Senparc
    
    文件名：MsgTypeHelper.cs
    文件功能描述：根据xml信息返回MsgType
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Xml.Linq;

namespace Senparc.Weixin.MP.Helpers
{
    /// <summary>
    /// 消息类型帮助类
    /// </summary>
    public static class MsgTypeHelper
    {
        #region RequestMsgType
        /// <summary>
        /// 根据xml信息，返回RequestMsgType
        /// </summary>
        /// <returns></returns>
        public static RequestMsgType GetRequestMsgType(XDocument doc)
        {
            return GetRequestMsgType(doc.Root.Element("MsgType").Value);
        }
        /// <summary>
        /// 根据xml信息，返回RequestMsgType
        /// </summary>
        /// <returns></returns>
        public static RequestMsgType GetRequestMsgType(string str)
        {
            return (RequestMsgType)Enum.Parse(typeof(RequestMsgType), str, true);
        }

        #endregion

        #region ResponseMsgType
        /// <summary>
        /// 根据xml信息，返回ResponseMsgType
        /// </summary>
        /// <returns></returns>
        public static ResponseMsgType GetResponseMsgType(XDocument doc)
        {
            return GetResponseMsgType(doc.Root.Element("MsgType").Value);
        }
        /// <summary>
        /// 根据xml信息，返回ResponseMsgType
        /// </summary>
        /// <returns></returns>
        public static ResponseMsgType GetResponseMsgType(string str)
        {
            return (ResponseMsgType)Enum.Parse(typeof(ResponseMsgType), str, true);
        }

        #endregion
    }
}
