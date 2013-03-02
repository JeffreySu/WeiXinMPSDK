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
        /// <summary>
        /// 根据XML信息填充实实体
        /// </summary>
        /// <typeparam name="T">MessageBase为基类的类型，Response和Request都可以</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="doc">XML</param>
        public static void FillEntityWithXml<T>(T entity, XDocument doc) where T : /*MessageBase*/ class, new()
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
                            prop.SetValue(entity, DateTimeHelper.GetDateTimeFromXml(root.Element(propName).Value), null);
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
                        //以下为枚举类型
                        case "RequestMsgType":
                            prop.SetValue(entity, MsgTypeHelper.GetRequestMsgType(root.Element(propName).Value), null);
                            break;
                        case "ResponseMsgType"://Response适用
                            prop.SetValue(entity, MsgTypeHelper.GetResponseMsgType(root.Element(propName).Value), null);
                            break;
                        case "Event":
                            prop.SetValue(entity, EventHelper.GetEventType(root.Element(propName).Value), null);
                            break;
                        //以下为实体类型
                        case "List`1"://List<T>类型，ResponseMessageNews适用
                            var genericArguments = prop.PropertyType.GetGenericArguments();
                            if (genericArguments[0].Name == "Article")//Response适用
                            {
                                //文章下属节点item
                                List<Article> articles=new List<Article>();
                                foreach (var item in root.Element(propName).Elements("item"))
                                {
                                    var article = new Article();
                                    FillEntityWithXml(article,new XDocument(item));
                                    articles.Add(article);
                                }
                                prop.SetValue(entity, articles,null);
                            }
                            break;
                        case "Music"://ResponseMessageMusic适用
                            Music music=new Music();
                            FillEntityWithXml(music, new XDocument(root.Element(propName)));
                            prop.SetValue(entity, music, null);
                            break;
                        default:
                            prop.SetValue(entity, root.Element(propName).Value, null);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 将实体转为XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static XDocument ConvertEntityToXml<T>(T entity) where T : class , new()
        {
            entity = entity ?? new T();
            var doc = new XDocument();
            doc.Add(new XElement("xml"));
            var root = doc.Root;

            /* 注意！
             * 经过测试，微信对字段排序有严格要求，这里对排序进行强制约束
            */
            var propNameOrder = new List<string>();
            //不同返回类型需要对应不同特殊格式的排序
            if (typeof(T) == typeof(ResponseMessageNews))
            {
                propNameOrder.AddRange(new[] { "ToUserName", "FromUserName", "CreateTime", "MsgType", "Content", "ArticleCount", "Articles", "FuncFlag",/*以下是Atricle属性*/ "Title ", "Description ", "PicUrl", "Url" });
            }
            else if (typeof(T) == typeof(ResponseMessageMusic))
            {
                propNameOrder.AddRange(new[] { "ToUserName", "FromUserName", "CreateTime", "MsgType", "Music", "FuncFlag",/*以下是Music属性*/ "Title ", "Description ", "MusicUrl", "HQMusicUrl" });
            }
            else
            {
                //如Text类型
                propNameOrder.AddRange(new[] { "ToUserName", "FromUserName", "CreateTime", "MsgType", "Content", "FuncFlag" });
            }

            Func<string, int> orderByPropName = propNameOrder.IndexOf;

            var props = entity.GetType().GetProperties().OrderBy(p => orderByPropName(p.Name)).ToList();
            foreach (var prop in props)
            {
                var propName = prop.Name;
                if (propName == "Articles")
                {
                    //文章列表
                    var atriclesElement = new XElement("Articles");
                    var articales = prop.GetValue(entity, null) as List<Article>;
                    foreach (var articale in articales)
                    {
                        var subNodes = ConvertEntityToXml(articale).Root.Elements();
                        atriclesElement.Add(new XElement("item", subNodes));
                    }
                    root.Add(atriclesElement);
                }
                else if (propName == "Music")
                {
                    //音乐格式
                    var musicElement = new XElement("Music");
                    var music = prop.GetValue(entity, null) as Music;
                    var subNodes = ConvertEntityToXml(music).Root.Elements();
                    musicElement.Add(subNodes);
                    root.Add(musicElement);
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
                            root.Add(new XElement(propName, DateTimeHelper.GetWeixinDateTime((DateTime)prop.GetValue(entity, null))));
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
