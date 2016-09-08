using System;
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
        public static RequestInfoType GetRequestInfoType(XDocument doc)
        {
            return GetRequestInfoType(doc.Root.Element("InfoType").Value);
        }

        /// <summary>
        /// 根据xml信息，返回InfoType
        /// </summary>
        /// <returns></returns>
        public static RequestInfoType GetRequestInfoType(string str)
        {
            return (RequestInfoType)Enum.Parse(typeof(RequestInfoType), str, true);
        }
    }
}
