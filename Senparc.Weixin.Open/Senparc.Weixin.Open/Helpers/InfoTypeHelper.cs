using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Senparc.Weixin.Open.Helpers
{
    public static class InfoTypeHelper
    {
        /// <summary>
        /// 根据xml信息，返回InfoType
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static InfoType GetRequestInfoType(XDocument doc)
        {
            return GetRequestInfoType(doc.Root.Element("InfoType").Value);
        }

        /// <summary>
        /// 根据xml信息，返回InfoType
        /// </summary>
        /// <returns></returns>
        public static InfoType GetRequestInfoType(string str)
        {
            return (InfoType)Enum.Parse(typeof(InfoType), str, true);
        }
    }
}
