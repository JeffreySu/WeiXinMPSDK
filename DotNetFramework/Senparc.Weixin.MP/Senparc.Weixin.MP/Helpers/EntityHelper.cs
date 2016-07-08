/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：EntityHelper.cs
    文件功能描述：实体与xml相互转换
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Senparc.Weixin.Helpers;
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
        public static void FillEntityWithXml<T>(this T entity, XDocument doc) where T : /*MessageBase*/ class, new()
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
                            //已设为只读
                            //prop.SetValue(entity, MsgTypeHelper.GetRequestMsgType(root.Element(propName).Value), null);
                            break;
                        case "ResponseMsgType"://Response适用
                            //已设为只读
                            //prop.SetValue(entity, MsgTypeHelper.GetResponseMsgType(root.Element(propName).Value), null);
                            break;
                        case "Event":
                            //已设为只读
                            //prop.SetValue(entity, EventHelper.GetEventType(root.Element(propName).Value), null);
                            break;
                        //以下为实体类型
                        case "List`1"://List<T>类型，ResponseMessageNews适用
                            var genericArguments = prop.PropertyType.GetGenericArguments();
                            if (genericArguments[0].Name == "Article")//ResponseMessageNews适用
                            {
                                //文章下属节点item
                                List<Article> articles = new List<Article>();
                                foreach (var item in root.Element(propName).Elements("item"))
                                {
                                    var article = new Article();
                                    FillEntityWithXml(article, new XDocument(item));
                                    articles.Add(article);
                                }
                                prop.SetValue(entity, articles, null);
                            }
                            else if (genericArguments[0].Name == "Account")
                            {
                                List<CustomerServiceAccount> accounts = new List<CustomerServiceAccount>();
                                foreach (var item in root.Elements(propName))
                                {
                                    var account = new CustomerServiceAccount();
                                    FillEntityWithXml(account, new XDocument(item));
                                    accounts.Add(account);
                                }
                                prop.SetValue(entity, accounts, null);
                            }
                            else if (genericArguments[0].Name == "PicItem")
                            {
                                List<PicItem> picItems = new List<PicItem>();
                                foreach (var item in root.Elements(propName).Elements("item"))
                                {
                                    var picItem = new PicItem();
                                    var picMd5Sum = item.Element("PicMd5Sum").Value;
                                    Md5Sum md5Sum = new Md5Sum() { PicMd5Sum = picMd5Sum };
                                    picItem.item = md5Sum;
                                    picItems.Add(picItem);
                                }
                                prop.SetValue(entity, picItems, null);
                            }
                            else if (genericArguments[0].Name == "AroundBeacon")
                            {
                                List<AroundBeacon> aroundBeacons = new List<AroundBeacon>();
                                foreach (var item in root.Elements(propName).Elements("AroundBeacon"))
                                {
                                    var aroundBeaconItem = new AroundBeacon();
                                    FillEntityWithXml(aroundBeaconItem, new XDocument(item));
                                    aroundBeacons.Add(aroundBeaconItem);
                                }
                                prop.SetValue(entity, aroundBeacons, null);
                            }
                            break;
                            break;
                        case "Music"://ResponseMessageMusic适用
                            Music music = new Music();
                            FillEntityWithXml(music, new XDocument(root.Element(propName)));
                            prop.SetValue(entity, music, null);
                            break;
                        case "Image"://ResponseMessageImage适用
                            Image image = new Image();
                            FillEntityWithXml(image, new XDocument(root.Element(propName)));
                            prop.SetValue(entity, image, null);
                            break;
                        case "Voice"://ResponseMessageVoice适用
                            Voice voice = new Voice();
                            FillEntityWithXml(voice, new XDocument(root.Element(propName)));
                            prop.SetValue(entity, voice, null);
                            break;
                        case "Video"://ResponseMessageVideo适用
                            Video video = new Video();
                            FillEntityWithXml(video, new XDocument(root.Element(propName)));
                            prop.SetValue(entity, video, null);
                            break;
                        case "ScanCodeInfo"://扫码事件中的ScanCodeInfo适用
                            ScanCodeInfo scanCodeInfo = new ScanCodeInfo();
                            FillEntityWithXml(scanCodeInfo, new XDocument(root.Element(propName)));
                            prop.SetValue(entity, scanCodeInfo, null);
                            break;
                        case "SendLocationInfo"://弹出地理位置选择器的事件推送中的SendLocationInfo适用
                            SendLocationInfo sendLocationInfo = new SendLocationInfo();
                            FillEntityWithXml(sendLocationInfo, new XDocument(root.Element(propName)));
                            prop.SetValue(entity, sendLocationInfo, null);
                            break;
                        case "SendPicsInfo"://系统拍照发图中的SendPicsInfo适用
                            SendPicsInfo sendPicsInfo = new SendPicsInfo();
                            FillEntityWithXml(sendPicsInfo, new XDocument(root.Element(propName)));
                            prop.SetValue(entity, sendPicsInfo, null);
                            break;
                        case "ChosenBeacon"://摇一摇事件通知
                            ChosenBeacon chosenBeacon = new ChosenBeacon();
                            FillEntityWithXml(chosenBeacon, new XDocument(root.Element(propName)));
                            prop.SetValue(entity, chosenBeacon, null);
                            break;
                        case "AroundBeacon"://摇一摇事件通知
                            AroundBeacon aroundBeacon = new AroundBeacon();
                            FillEntityWithXml(aroundBeacon, new XDocument(root.Element(propName)));
                            prop.SetValue(entity, aroundBeacon, null);
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
        /// <typeparam name="T">RequestMessage或ResponseMessage</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public static XDocument ConvertEntityToXml<T>(this T entity) where T : class , new()
        {
            entity = entity ?? new T();
            var doc = new XDocument();
            doc.Add(new XElement("xml"));
            var root = doc.Root;

            /* 注意！
             * 经过测试，微信对字段排序有严格要求，这里对排序进行强制约束
            */
            var propNameOrder = new List<string>() { "ToUserName", "FromUserName", "CreateTime", "MsgType" };
            //不同返回类型需要对应不同特殊格式的排序
            if (entity is ResponseMessageNews)
            {
                propNameOrder.AddRange(new[] { "ArticleCount", "Articles", "FuncFlag",/*以下是Atricle属性*/ "Title ", "Description ", "PicUrl", "Url" });
            }
            else if (entity is ResponseMessageTransfer_Customer_Service)
            {
                propNameOrder.AddRange(new[] { "TransInfo", "KfAccount", "FuncFlag" });
            }
            else if (entity is ResponseMessageMusic)
            {
                propNameOrder.AddRange(new[] { "Music", "FuncFlag", "ThumbMediaId",/*以下是Music属性*/ "Title ", "Description ", "MusicUrl", "HQMusicUrl" });
            }
            else if (entity is ResponseMessageImage)
            {
                propNameOrder.AddRange(new[] { "Image",/*以下是Image属性*/ "MediaId " });
            }
            else if (entity is ResponseMessageVoice)
            {
                propNameOrder.AddRange(new[] { "Voice",/*以下是Voice属性*/ "MediaId " });
            }
            else if (entity is ResponseMessageVideo)
            {
                propNameOrder.AddRange(new[] { "Video",/*以下是Video属性*/ "MediaId ", "Title", "Description" });
            }
            else
            {
                //如Text类型
                propNameOrder.AddRange(new[] { "Content", "FuncFlag" });
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
                else if (propName == "TransInfo")
                {
                    var transInfoElement = new XElement("TransInfo");
                    var transInfo = prop.GetValue(entity, null) as List<CustomerServiceAccount>;
                    foreach (var account in transInfo)
                    {
                        var trans = ConvertEntityToXml(account).Root.Elements();
                        transInfoElement.Add(trans);
                    }

                    root.Add(transInfoElement);
                }
                else if (propName == "Music" || propName == "Image" || propName == "Video" || propName == "Voice")
                {
                    //音乐、图片、视频、语音格式
                    var musicElement = new XElement(propName);
                    var media = prop.GetValue(entity, null);// as Music;
                    var subNodes = ConvertEntityToXml(media).Root.Elements();
                    musicElement.Add(subNodes);
                    root.Add(musicElement);
                }
                else if (propName == "PicList")
                {
                    var picListElement = new XElement("PicList");
                    var picItems = prop.GetValue(entity, null) as List<PicItem>;
                    foreach (var picItem in picItems)
                    {
                        var item = ConvertEntityToXml(picItem).Root.Elements();
                        picListElement.Add(item);
                    }
                    root.Add(picListElement);
                }
                //else if (propName == "KfAccount")
                //{
                //    //TODO:可以交给string处理
                //    root.Add(new XElement(propName, prop.GetValue(entity, null).ToString().ToLower()));
                //}
                else
                {
                    //其他非特殊类型
                    switch (prop.PropertyType.Name)
                    {
                        case "String":
                            root.Add(new XElement(propName,
                                             new XCData(prop.GetValue(entity, null) as string ?? "")));
                            break;
                        case "DateTime":
                            root.Add(new XElement(propName,
                                                  DateTimeHelper.GetWeixinDateTime(
                                                      (DateTime)prop.GetValue(entity, null))));
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
                            root.Add(new XElement(propName, new XCData(prop.GetValue(entity, null).ToString().ToLower())));
                            break;
                        case "Article":
                            root.Add(new XElement(propName, prop.GetValue(entity, null).ToString().ToLower()));
                            break;
                        case "TransInfo":
                            root.Add(new XElement(propName, prop.GetValue(entity, null).ToString().ToLower()));
                            break;
                        default:
                            if (prop.PropertyType.IsClass && prop.PropertyType.IsPublic)
                            {
                                //自动处理其他实体属性
                                var subEntity = prop.GetValue(entity, null);
                                var subNodes = ConvertEntityToXml(subEntity).Root.Elements();
                                root.Add(new XElement(propName, subNodes));
                            }
                            else
                            {
                                root.Add(new XElement(propName, prop.GetValue(entity, null)));

                            }
                            break;
                    }
                }
            }
            return doc;
        }

        /// <summary>
        /// 将实体转为XML字符串
        /// </summary>
        /// <typeparam name="T">RequestMessage或ResponseMessage</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public static string ConvertEntityToXmlString<T>(this T entity) where T : class , new()
        {
            return entity.ConvertEntityToXml().ToString();
        }

        /// <summary>
        /// ResponseMessageBase.CreateFromRequestMessage&lt;T&gt;(requestMessage)的扩展方法
        /// </summary>
        /// <typeparam name="T">需要生成的ResponseMessage类型</typeparam>
        /// <param name="requestMessage">IRequestMessageBase接口下的接收信息类型</param>
        /// <returns></returns>
        public static T CreateResponseMessage<T>(this IRequestMessageBase requestMessage) where T : ResponseMessageBase
        {
            return ResponseMessageBase.CreateFromRequestMessage<T>(requestMessage);
        }

        /// <summary>
        /// ResponseMessageBase.CreateFromResponseXml(xml)的扩展方法
        /// </summary>
        /// <param name="xml">返回给服务器的Response Xml</param>
        /// <returns></returns>
        public static IResponseMessageBase CreateResponseMessage(this string xml)
        {
            return ResponseMessageBase.CreateFromResponseXml(xml);
        }

        /// <summary>
        /// 检查是否是通过场景二维码扫入
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public static bool IsFromScene(this RequestMessageEvent_Subscribe requestMessage)
        {
            return !string.IsNullOrEmpty(requestMessage.EventKey);
        }
    }
}
