﻿/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
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
