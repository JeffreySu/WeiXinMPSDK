/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：GroupMessageAPI.cs
    文件功能描述：高级群发接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

/* 
   API地址：http://mp.weixin.qq.com/wiki/15/5380a4e6f02f2ffdc7981a8ed7a40753.html
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.GroupMessage
{
    /// <summary>
    /// 高级群发接口
    /// </summary>
    public static class GroupMessageApi
    {
        /// <summary>
        /// 根据分组进行群发【订阅号与服务号认证后均可用】
        /// 
        /// 请注意：
        /// 1、该接口暂时仅提供给已微信认证的服务号
        /// 2、虽然开发者使用高级群发接口的每日调用限制为100次，但是用户每月只能接收4条，请小心测试
        /// 3、无论在公众平台网站上，还是使用接口群发，用户每月只能接收4条群发消息，多于4条的群发将对该用户发送失败。
        /// 
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="groupId">群发到的分组的group_id，参加用户管理中用户分组接口，若is_to_all值为true，可不填写group_id</param>
        /// <param name="mediaId">用于群发的消息的media_id</param>
        /// <param name="type"></param>
        /// <param name="isToAll">用于设定是否向全部用户发送，值为true或false，选择true该消息群发给所有用户，选择false可根据group_id发送给指定群组的用户</param>
        /// <returns></returns>
        public static SendResult SendGroupMessageByGroupId(string accessToken, string groupId, string mediaId, GroupMessageType type, bool isToAll = false, int timeOut = Config.TIME_OUT)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}";
            
            BaseGroupMessageDataByGroupId baseData = null;
            switch (type)
            {
                case GroupMessageType.image:
                    baseData = new GroupMessageByGroupId_ImageData()
                        {
                            filter = new GroupMessageByGroupId_GroupId()
                                {
                                  group_id  = groupId,
                                  is_to_all = isToAll
                                },
                            image = new GroupMessageByGroupId_MediaId()
                                 {
                                     media_id = mediaId
                                 },
                             msgtype = "image"
                        };
                    break;
                case GroupMessageType.voice:
                    baseData = new GroupMessageByGroupId_VoiceData()
                    {
                        filter = new GroupMessageByGroupId_GroupId()
                        {
                            group_id = groupId,
                            is_to_all = isToAll
                        },
                        voice = new GroupMessageByGroupId_MediaId()
                        {
                            media_id = mediaId
                        },
                        msgtype = "voice"
                    };
                    break;
                case GroupMessageType.mpnews:
                    baseData = new GroupMessageByGroupId_MpNewsData()
                    {
                        filter = new GroupMessageByGroupId_GroupId()
                        {
                            group_id = groupId,
                            is_to_all = isToAll
                        },
                        mpnews = new GroupMessageByGroupId_MediaId()
                        {
                            media_id = mediaId
                        },
                        msgtype = "mpnews"
                    };
                    break;
                case GroupMessageType.video:
                    baseData = new GroupMessageByGroupId_MpVideoData()
                    {
                        filter = new GroupMessageByGroupId_GroupId()
                        {
                            group_id = groupId,
                            is_to_all = isToAll
                        },
                        mpvideo = new GroupMessageByGroupId_MediaId()
                        {
                            media_id = mediaId
                        },
                        msgtype = "mpvideo"
                    };
                    break;
                case GroupMessageType.text:
                    throw new Exception("发送文本信息请使用SendTextGroupMessageByGroupId方法。");
                    break;
                default:
                    throw new Exception("参数错误。");
                    break;
            }

            return CommonJsonSend.Send<SendResult>(accessToken, urlFormat, baseData, timeOut: timeOut);
        }

        /// <summary>
        /// 根据GroupId进行群发文本信息【订阅号与服务号认证后均可用】
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="groupId">群发到的分组的group_id，参加用户管理中用户分组接口，若is_to_all值为true，可不填写group_id</param>
        /// <param name="content">用于群发文本消息的content</param>
        /// <param name="isToAll">用于设定是否向全部用户发送，值为true或false，选择true该消息群发给所有用户，选择false可根据group_id发送给指定群组的用户</param>
        /// <returns></returns>
        public static SendResult SendTextGroupMessageByGroupId(string accessToken, string groupId, string content, bool isToAll = false, int timeOut = Config.TIME_OUT)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}";

            BaseGroupMessageDataByGroupId baseData = new GroupMessageByGroupId_TextData()
                    {
                        filter = new GroupMessageByGroupId_GroupId()
                        {
                            group_id = groupId,
                            is_to_all = isToAll
                        },
                        text = new GroupMessageByGroupId_Content()
                        {
                            content = content
                        },
                        msgtype = "text"
                    };

            return CommonJsonSend.Send<SendResult>(accessToken, urlFormat, baseData, timeOut: timeOut);
        }

        /// <summary>
        /// 根据OpenId进行群发
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="mediaId">用于群发的消息的media_id</param>
        /// <param name="type"></param>
        /// <param name="openIds">openId字符串数组</param>
        /// 注意mediaId和content不可同时为空
        /// <returns></returns>
        public static SendResult SendGroupMessageByOpenId(string accessToken, GroupMessageType type, string mediaId, int timeOut = Config.TIME_OUT, params string[] openIds)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}";

            BaseGroupMessageDataByOpenId baseData = null;
            switch (type)
            {
                case GroupMessageType.image:
                    baseData = new GroupMessageByOpenId_ImageData()
                    {
                        touser = openIds,
                        image = new GroupMessageByOpenId_MediaId()
                        {
                            media_id = mediaId
                        },
                        msgtype = "image"
                    };
                    break;
                case GroupMessageType.voice:
                    baseData = new GroupMessageByOpenId_VoiceData()
                    {
                        touser = openIds,
                        voice = new GroupMessageByOpenId_MediaId()
                        {
                            media_id = mediaId
                        },
                        msgtype = "voice"
                    };
                    break;
                case GroupMessageType.mpnews:
                    baseData = new GroupMessageByOpenId_MpNewsData()
                    {
                        touser = openIds,
                        mpnews = new GroupMessageByOpenId_MediaId()
                        {
                            media_id = mediaId
                        },
                        msgtype = "mpnews"
                    };
                    break;
                case GroupMessageType.video:
                    throw new Exception("发送视频信息请使用SendVideoGroupMessageByOpenId方法。");
                    break;
                case GroupMessageType.text:
                    throw new Exception("发送文本信息请使用SendTextGroupMessageByOpenId方法。");
                    break;
                default:
                    throw new Exception("参数错误。");
                    break;
            }
            return CommonJsonSend.Send<SendResult>(accessToken, urlFormat, baseData, timeOut: timeOut);
        }

        /// <summary>
        /// 根据OpenID列表群发文本消息【订阅号不可用，服务号认证后可用】
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="content"></param>
        /// <param name="openIds">openId字符串数组</param>
        /// 注意mediaId和content不可同时为空
        /// <returns></returns>
        public static SendResult SendTextGroupMessageByOpenId(string accessToken, string content, int timeOut = Config.TIME_OUT, params string[] openIds)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}";

            BaseGroupMessageDataByOpenId baseData = new GroupMessageByOpenId_TextData()
                    {
                        touser = openIds,
                        text = new GroupMessageByOpenId_Content()
                        {
                            content = content
                        },
                        msgtype = "text"
                    };

            return CommonJsonSend.Send<SendResult>(accessToken, urlFormat, baseData, timeOut: timeOut);
        }

        /// <summary>
        /// 根据OpenID列表群发视频消息【订阅号不可用，服务号认证后可用】
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="title"></param>
        /// <param name="mediaId"></param>
        /// <param name="openIds">openId字符串数组</param>
        /// <param name="description"></param>
        /// 注意mediaId和content不可同时为空
        /// <returns></returns>
        public static SendResult SendVideoGroupMessageByOpenId(string accessToken, string title, string description, string mediaId, int timeOut = Config.TIME_OUT, params string[] openIds)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}";

            BaseGroupMessageDataByOpenId baseData = new GroupMessageByOpenId_MpVideoData()
            {
                touser = openIds,
                video = new GroupMessageByOpenId_Video()
                {
                    title = title,
                    description = description,
                    media_id = mediaId
                },
                msgtype = "video"
            };

            return CommonJsonSend.Send<SendResult>(accessToken, urlFormat, baseData, timeOut: timeOut);
        }

        /// <summary>
        /// 删除群发消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="mediaId">发送出去的消息ID</param>
        /// <returns></returns>
        public static WxJsonResult DeleteSendMessage(string accessToken, string mediaId, int timeOut = Config.TIME_OUT)
        {
            //官方API地址为https://api.weixin.qq.com//cgi-bin/message/mass/delete?access_token={0}，应该是多了一个/
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/delete?access_token={0}";

            var data = new
            {
                msgid = mediaId
            };
            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }

        /// <summary>
        /// 预览接口【订阅号与服务号认证后均可用】
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="mediaId">用于群发的消息的media_id</param>
        /// <param name="type"></param>
        /// <param name="openId">接收消息用户对应该公众号的openid</param>
        /// 注意mediaId和content不可同时为空
        /// <returns></returns>
        public static SendResult SendGroupMessagePreview(string accessToken, GroupMessageType type, string mediaId, string openId, int timeOut = Config.TIME_OUT)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/preview?access_token={0}";

            BaseGroupMessageDataPreview baseData = null;
            switch (type)
            {
                case GroupMessageType.image:
                    baseData = new GroupMessagePreview_ImageData()
                    {
                        touser = openId,
                        image = new GroupMessagePreview_MediaId()
                        {
                            media_id = mediaId
                        },
                        msgtype = "image"
                    };
                    break;
                case GroupMessageType.voice:
                    baseData = new GroupMessagePreview_VoiceData()
                    {
                        touser = openId,
                        voice = new GroupMessagePreview_MediaId()
                        {
                            media_id = mediaId
                        },
                        msgtype = "voice"
                    };
                    break;
                case GroupMessageType.mpnews:
                    baseData = new GroupMessagePreview_MpNewsData()
                    {
                        touser = openId,
                        mpnews = new GroupMessagePreview_MediaId()
                        {
                            media_id = mediaId
                        },
                        msgtype = "mpnews"
                    };
                    break;
                case GroupMessageType.video:
                    baseData = new GroupMessagePreview_MpVideoData()
                    {
                        touser = openId,
                        mpvideo = new GroupMessagePreview_MediaId()
                        {
                            media_id = mediaId
                        },
                        msgtype = "mpvideo"
                    };
                    break;
                case GroupMessageType.text:
                    throw new Exception("发送文本信息请使用SendTextGroupMessagePreview方法。");
                    break;
                default:
                    throw new Exception("参数错误。");
                    break;
            }
            return CommonJsonSend.Send<SendResult>(accessToken, urlFormat, baseData, timeOut: timeOut);
        }

        /// <summary>
        /// 预览接口Test【订阅号与服务号认证后均可用】
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="content"></param>
        /// <param name="openId">接收消息用户对应该公众号的openid</param>
        /// 注意mediaId和content不可同时为空
        /// <returns></returns>
        public static SendResult SendTextGroupMessagePreview(string accessToken, string content, string openId, int timeOut = Config.TIME_OUT)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/preview?access_token={0}";

            BaseGroupMessageDataPreview baseData = new GroupMessagePreview_TextData()
            {
                touser = openId,
                text = new GroupMessagePreview_Content()
                {
                    content = content
                },
                msgtype = "text"
            };

            return CommonJsonSend.Send<SendResult>(accessToken, urlFormat, baseData, timeOut: timeOut);
        }

        /// <summary>
        /// 查询群发消息发送状态【订阅号与服务号认证后均可用】
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="msgId">群发消息后返回的消息id</param>
        /// <returns></returns>
        public static GetSendResult GetGroupMessageResult(string accessToken, string msgId, int timeOut = Config.TIME_OUT)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/get?access_token={0}";

            var data = new
                {
                    msg_id = msgId
                };

            return CommonJsonSend.Send<GetSendResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }
    }
}