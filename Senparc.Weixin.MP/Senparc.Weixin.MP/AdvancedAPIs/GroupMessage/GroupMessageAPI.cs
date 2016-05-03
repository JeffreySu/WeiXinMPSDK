/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：GroupMessageAPI.cs
    文件功能描述：高级群发接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
 
    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间
----------------------------------------------------------------*/

/* 
   API地址：http://mp.weixin.qq.com/wiki/15/5380a4e6f02f2ffdc7981a8ed7a40753.html
*/

using System;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs.GroupMessage;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.MP.AdvancedAPIs
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
        /// 4、群发视频时需要先调用GetVideoMediaIdResult接口获取专用的MediaId然后进行群发
        /// 
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="groupId">群发到的分组的group_id，参加用户管理中用户分组接口，若is_to_all值为true，可不填写group_id</param>
        /// <param name="value">群发媒体文件时传入mediaId,群发文本消息时传入content,群发卡券时传入cardId</param>
        /// <param name="type"></param>
        /// <param name="isToAll">用于设定是否向全部用户发送，值为true或false，选择true该消息群发给所有用户，选择false可根据group_id发送给指定群组的用户</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static SendResult SendGroupMessageByGroupId(string accessTokenOrAppId, string groupId, string value, GroupMessageType type, bool isToAll = false, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
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
                                group_id = groupId,
                                is_to_all = isToAll
                            },
                            image = new GroupMessageByGroupId_MediaId()
                            {
                                media_id = value
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
                                media_id = value
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
                                media_id = value
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
                                media_id = value
                            },
                            msgtype = "mpvideo"
                        };
                        break;
                    case GroupMessageType.wxcard:
                        baseData = new GroupMessageByGroupId_WxCardData()
                        {
                            filter = new GroupMessageByGroupId_GroupId()
                            {
                                group_id = groupId,
                                is_to_all = isToAll
                            },
                            wxcard = new GroupMessageByGroupId_WxCard()
                            {
                                card_id = value
                            },
                            msgtype = "wxcard"
                        };
                        break;
                    case GroupMessageType.text:
                        baseData = new GroupMessageByGroupId_TextData()
                        {
                            filter = new GroupMessageByGroupId_GroupId()
                            {
                                group_id = groupId,
                                is_to_all = isToAll
                            },
                            text = new GroupMessageByGroupId_Content()
                            {
                                content = value
                            },
                            msgtype = "text"
                        };
                        break;
                    default:
                        throw new Exception("参数错误。");
                        break;
                }

                return CommonJsonSend.Send<SendResult>(accessToken, urlFormat, baseData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 根据OpenId进行群发
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="value">群发媒体文件时传入mediaId,群发文本消息时传入content,群发卡券时传入cardId</param>
        /// <param name="type"></param>
        /// <param name="openIds">openId字符串数组</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static SendResult SendGroupMessageByOpenId(string accessTokenOrAppId, GroupMessageType type, string value, int timeOut = Config.TIME_OUT, params string[] openIds)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
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
                                media_id = value
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
                                media_id = value
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
                                media_id = value
                            },
                            msgtype = "mpnews"
                        };
                        break;
                    case GroupMessageType.wxcard:
                        baseData = new GroupMessageByOpenId_WxCardData()
                        {
                            touser = openIds,
                            wxcard = new GroupMessageByOpenId_WxCard()
                            {
                                card_id = value
                            },
                            msgtype = "wxcard"
                        };
                        break;
                    case GroupMessageType.video:
                        throw new Exception("发送视频信息请使用SendVideoGroupMessageByOpenId方法。");
                        break;
                    case GroupMessageType.text:
                        baseData = new GroupMessageByOpenId_TextData()
                        {
                            touser = openIds,
                            text = new GroupMessageByOpenId_Content()
                            {
                                content = value
                            },
                            msgtype = "text"
                        };
                        break;
                    default:
                        throw new Exception("参数错误。");
                        break;
                }
                return CommonJsonSend.Send<SendResult>(accessToken, urlFormat, baseData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 根据OpenID列表群发视频消息【订阅号不可用，服务号认证后可用】
        /// 注意：群发视频时需要先调用GetVideoMediaIdResult接口获取专用的MediaId然后进行群发
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="title"></param>
        /// <param name="mediaId"></param>
        /// <param name="openIds">openId字符串数组</param>
        /// <param name="description"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static SendResult SendVideoGroupMessageByOpenId(string accessTokenOrAppId, string title, string description, string mediaId, int timeOut = Config.TIME_OUT, params string[] openIds)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
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
                    msgtype = "mpvideo"
                };

                return CommonJsonSend.Send<SendResult>(accessToken, urlFormat, baseData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 删除群发消息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="msgId">发送出去的消息ID</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult DeleteSendMessage(string accessTokenOrAppId, string msgId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                //官方API地址为https://api.weixin.qq.com//cgi-bin/message/mass/delete?access_token={0}，应该是多了一个/
                const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/delete?access_token={0}";

                var data = new
                {
                    msg_id = msgId
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 预览接口【订阅号与服务号认证后均可用】
        /// 注意：openId与wxName两者任选其一，同时传入以wxName优先
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="value">群发媒体消息时为media_id，群发文本信息为content</param>
        /// <param name="type"></param>
        /// <param name="openId">接收消息用户对应该公众号的openid</param>
        /// <param name="wxName">接收消息用户的微信号</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static SendResult SendGroupMessagePreview(string accessTokenOrAppId, GroupMessageType type, string value, string openId, string wxName = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/preview?access_token={0}";

                BaseGroupMessageDataPreview baseData = null;
                switch (type)
                {
                    case GroupMessageType.image:
                        baseData = new GroupMessagePreview_ImageData()
                        {
                            touser = openId,
                            towxname = wxName,
                            image = new GroupMessagePreview_MediaId()
                            {
                                media_id = value
                            },
                            msgtype = "image"
                        };
                        break;
                    case GroupMessageType.voice:
                        baseData = new GroupMessagePreview_VoiceData()
                        {
                            touser = openId,
                            towxname = wxName,
                            voice = new GroupMessagePreview_MediaId()
                            {
                                media_id = value
                            },
                            msgtype = "voice"
                        };
                        break;
                    case GroupMessageType.mpnews:
                        baseData = new GroupMessagePreview_MpNewsData()
                        {
                            touser = openId,
                            towxname = wxName,
                            mpnews = new GroupMessagePreview_MediaId()
                            {
                                media_id = value
                            },
                            msgtype = "mpnews"
                        };
                        break;
                    case GroupMessageType.video:
                        baseData = new GroupMessagePreview_MpVideoData()
                        {
                            touser = openId,
                            towxname = wxName,
                            mpvideo = new GroupMessagePreview_MediaId()
                            {
                                media_id = value
                            },
                            msgtype = "mpvideo"
                        };
                        break;
                    case GroupMessageType.text:
                        baseData = new GroupMessagePreview_TextData()
                        {
                            touser = openId,
                            towxname = wxName,
                            text = new GroupMessagePreview_Content()
                            {
                                content = value
                            },
                            msgtype = "text"
                        };
                        break;
                    case GroupMessageType.wxcard:
                        throw new Exception("发送卡券息请使用WxCardGroupMessagePreview方法。");
                        break;
                    default:
                        throw new Exception("参数错误。");
                        break;
                }
                return CommonJsonSend.Send<SendResult>(accessToken, urlFormat, baseData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 预览卡券接口
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId"></param>
        /// <param name="code"></param>
        /// <param name="openId"></param>
        /// <param name="wxName"></param>
        /// <param name="timestamp"></param>
        /// <param name="signature"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static SendResult WxCardGroupMessagePreview(string accessTokenOrAppId, string cardId, string code,
            string openId, string wxName, string timestamp, string signature, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/preview?access_token={0}";

                BaseGroupMessageDataPreview baseData = new GroupMessagePreview_WxCardData()
                {
                    touser = openId,
                    towxname = wxName,
                    wxcard = new GroupMessagePreview_WxCard()
                    {
                        card_id = cardId,
                        card_ext = string.Format("\"code\":\"{0}\",\"openid\":\"{1}\",\"timestamp\":\"{2}\",\"signature\":\"{3}\"", code, openId, timestamp, signature)
                    },
                    msgtype = "wxcard"
                };

                return CommonJsonSend.Send<SendResult>(accessToken, urlFormat, baseData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询群发消息发送状态【订阅号与服务号认证后均可用】
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="msgId">群发消息后返回的消息id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetSendResult GetGroupMessageResult(string accessTokenOrAppId, string msgId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/get?access_token={0}";

                var data = new
                {
                    msg_id = msgId
                };

                return CommonJsonSend.Send<GetSendResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取视频群发用的MediaId
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="mediaId"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static VideoMediaIdResult GetVideoMediaIdResult(string accessTokenOrAppId, string mediaId, string title,
            string description, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format("https://file.api.weixin.qq.com/cgi-bin/media/uploadvideo?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    media_id = mediaId,
                    title = title,
                    description = description
                };

                return CommonJsonSend.Send<VideoMediaIdResult>(null, url, data, CommonJsonSendType.POST, timeOut, true);

            }, accessTokenOrAppId);
        }
    }
}