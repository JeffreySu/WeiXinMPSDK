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

    文件名：BrowserUtility.cs
    文件功能描述：浏览器公共类


    创建标识：Senparc - 20150419

    修改标识：Senparc - 20161219
    修改描述：v4.9.6 修改错别字：Browser->Browser

    修改标识：Senparc - 20161219
    修改描述：v4.11.2 修改SideInWeixinBrowser判断逻辑

    修改标识：Senparc - 20180513
    修改描述：v4.11.2 1、增加对小程序请求的判断方法 SideInWeixinMiniProgram()
                      2、添加 GetUserAgent() 方法

    ----  CO2NET   ----
    ----  split from Senparc.Weixin/Utilities/BrowserUtility.cs  ----

    修改标识：Senparc - 20180531
    修改描述：v0.1.0 移植 BrowserUtility

----------------------------------------------------------------*/

using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Senparc.Weixin.XmlUtility
{
    /// <summary>
    /// XML 工具类
    /// </summary>
    public static class XmlUtility
    {

        #region 反序列化

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="xml">XML字符串</param>
        /// <returns></returns>
        [Obsolete("请使用 Senparc.CO2NET.Utilities.XmlUtility.Deserialize<T>(xml) 方法")]
        public static object Deserialize<T>(string xml)
        {
            return CO2NET.XmlUtility.XmlUtility.Deserialize<T>(xml);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        [Obsolete("请使用 Senparc.CO2NET.Utilities.XmlUtility.Deserialize<T>(stream) 方法")]
        public static object Deserialize<T>(Stream stream)
        {
            return CO2NET.XmlUtility.XmlUtility.Deserialize<T>(stream);
        }

        #endregion

        #region 序列化

        /// <summary>
        /// 序列化
        /// 说明：此方法序列化复杂类，如果没有声明XmlInclude等特性，可能会引发“使用 XmlInclude 或 SoapInclude 特性静态指定非已知的类型。”的错误。
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        [Obsolete("请使用 Senparc.CO2NET.Utilities.XmlUtility.Serializer<T>(obj) 方法")]
        public static string Serializer<T>(T obj)
        {
            return CO2NET.XmlUtility.XmlUtility.Serializer<T>(obj);
        }

        #endregion

        /// <summary>
        /// 序列化将流转成XML字符串
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        [Obsolete("请使用 Senparc.CO2NET.Utilities.XmlUtility.Convert(stream) 方法")]
        public static XDocument Convert(Stream stream)
        {
            return CO2NET.XmlUtility.XmlUtility.Convert(stream);
        }

    }
}
