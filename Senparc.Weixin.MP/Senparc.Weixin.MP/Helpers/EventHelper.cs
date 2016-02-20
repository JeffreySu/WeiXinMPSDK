/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：EventHelper.cs
    文件功能描述：xml中的Event字段转换为Event枚举
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Xml.Linq;

namespace Senparc.Weixin.MP.Helpers
{
    public class EventHelper
    {
        public static Event GetEventType(XDocument doc)
        {
            return GetEventType(doc.Root.Element("Event").Value);
        }

        public static Event GetEventType(string str)
        {
            return (Event)Enum.Parse(typeof(Event), str, true);
        }
    }
}
