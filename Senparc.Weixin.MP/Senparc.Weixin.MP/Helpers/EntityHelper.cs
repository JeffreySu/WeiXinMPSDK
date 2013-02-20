using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.Helpers
{
    public static class EntityHelper
    {
        public static DateTime BaseTime = new DateTime(1970, 1, 1);//Unix起始时间

        public static void FillEntityWithXml<T>(T entity, XDocument doc) where T : RequestMessageBase, new()
        {
            entity = entity ?? new T();
            var root = doc.Root;

            var props = entity.GetType().GetProperties();
            foreach (var prop in props)
            {
                var propName = prop.Name;
                if (root.Element(propName) != null)
                {
                    switch (prop.PropertyType.Name)
                    {
                        //case "String":
                        //    goto default;
                        case "DateTime":
                            prop.SetValue(entity, EntityHelper.BaseTime.AddTicks(long.Parse(root.Element(propName).Value + 8 * 60 * 60) * 10000000), null);
                            break;
                        case "Boolean":
                            if (propName == "FuncFlag")
                            {
                                prop.SetValue(entity, root.Element(propName).Value == "1", null);
                            }
                            else
                            {
                                goto default;
                            }
                            break;
                        case "Int32":
                            prop.SetValue(entity, int.Parse(root.Element(propName).Value), null);
                            break;
                        case "Int64":
                            prop.SetValue(entity, long.Parse(root.Element(propName).Value), null);
                            break;
                        case "Double":
                            prop.SetValue(entity, double.Parse(root.Element(propName).Value), null);
                            break;
                        case "RequestMsgType":
                            prop.SetValue(entity, MsgTypeHelper.GetMsgType(root.Element(propName).Value), null);
                            break;
                        default:
                            prop.SetValue(entity, root.Element(propName).Value, null);
                            break;
                    }
                }
            }
        }

        public static XDocument ConvertEntityToXml<T>(T entity) where T : class , new()
        {
            entity = entity ?? new T();
            var doc = new XDocument();
            doc.Add(new XElement("xml"));
            var root = doc.Root;

            //经过测试，微信对字段排序有严格要求，这里对排序进行强制约束
            var propNameOrder = new[] { "ToUserName", "FromUserName", "CreateTime", "MsgType", "Content", "ArticleCount", "Articles", "FuncFlag",/*以下是Atricle属性*/"Title ", "Description ", "PicUrl", "Url" }.ToList();
            Func<string, int> orderByPropName = propNameOrder.IndexOf;

            var props = entity.GetType().GetProperties().OrderBy(p => orderByPropName(p.Name)).ToList();
            foreach (var prop in props)
            {
                var propName = prop.Name;
                if (propName == "Articles")
                {
                    var atriclesElement = new XElement("Articles");
                    var articales = prop.GetValue(entity, null) as List<Article>;
                    foreach (var articale in articales)
                    {
                        var subNodes = ConvertEntityToXml(articale).Root.Elements();
                        atriclesElement.Add(new XElement("item", subNodes));
                    }
                    root.Add(atriclesElement);
                }
                else
                {
                    switch (prop.PropertyType.Name)
                    {
                        case "String":
                            root.Add(new XElement(propName,
                                                  new XCData(prop.GetValue(entity, null) as string ?? "")));
                            break;
                        case "DateTime":
                            root.Add(new XElement(propName, ((DateTime)prop.GetValue(entity, null)).Ticks / 10000000 - 8 * 60 * 60));
                            break;
                        case "Boolean":
                            if (propName == "FuncFlag")
                            {
                                root.Add(new XElement(propName, (bool)prop.GetValue(entity, null) ? "1" : "0"));
                            }
                            else
                            {
                                goto default;
                            }
                            break;
                        case "ResponseMsgType":
                            root.Add(new XElement(propName, prop.GetValue(entity, null).ToString().ToLower()));
                            break;
                        case "Article":
                            root.Add(new XElement(propName, prop.GetValue(entity, null).ToString().ToLower()));
                            break;
                        default:
                            root.Add(new XElement(propName, prop.GetValue(entity, null)));
                            break;
                    }
                }
            }
            return doc;
        }
    }
}
