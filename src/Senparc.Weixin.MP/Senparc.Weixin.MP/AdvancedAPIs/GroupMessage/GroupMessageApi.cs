#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc
    
    文件名：GroupMessageAPI.cs
    文件功能描述：高级群发接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
 
    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间

    修改标识：Senparc - 20160718
    修改描述：增加其接口的异步方法
    
    修改标识：Senparc - 20170402
    修改描述：v14.3.140 1、添加BaseGroupMessageDataByGroupId.send_ignore_reprint属性
                        2、优化代码

    修改标识：Senparc - 20170707
    修改描述：v14.5.1 完善异步方法async/await

    修改标识：Senparc - 2017224
    修改描述：v14.8.12 完成群发接口添加clientmsgid属性
   
    修改标识：Senparc - 20180319
    修改描述：v14.10.8 GroupMessageApi.SendGroupMessageByFilter() 方法修复判断错误

    修改标识：Senparc - 20180507
    修改描述：v14.12.4 删除群发接口 GroupMessageApi.DeleteSendMessage() 添加article_idx参数

    修改标识：Senparc - 20180928
    修改描述：增加GetSendSpeed,SetSendSpeed
----------------------------------------------------------------*/

/* 
   API地址：http://mp.weixin.qq.com/wiki/15/5380a4e6f02f2ffdc7981a8ed7a40753.html
*/

using System;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.MP.AdvancedAPIs.GroupMessage;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 高级群发接口
    /// </summary>
    public static class GroupMessageApi
    {

        //官方文档：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1481187827_i0l21

        #region 同步方法

        #region 根据分组或标签群发

        /// <summary>
        /// 根据分组或标签进行群发【订阅号与服务号认证后均可用】
        /// 
        /// 请注意：
        /// 1、该接口暂时仅提供给已微信认证的服务号
        /// 2、虽然开发者使用高级群发接口的每日调用限制为100次，但是用户每月只能接收4条，请小心测试
        /// 3、无论在公众平台网站上，还是使用接口群发，用户每月只能接收4条群发消息，多于4条的群发将对该用户发送失败。
        /// 4、群发视频时需要先调用GetVideoMediaIdResult接口获取专用的MediaId然后进行群发
        /// 
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="groupId">群发到的分组的group_id，参见用户管理中用户分组接口，若is_to_all值为true，可不填写group_id；如果groupId和tagId同时填写，优先使用groupId；groupId和tagId最多只能使用一个</param>
        /// <param name="tagId">群发到的标签的tag_id，若is_to_all值为true，可不填写tag_id；如果groupId和tagId同时填写，优先使用groupId；groupId和tagId最多只能使用一个</param>
        /// <param name="value">群发媒体文件时传入mediaId,群发文本消息时传入content,群发卡券时传入cardId</param>
        /// <param name="type"></param>
        /// <param name="isToAll">用于设定是否向全部用户发送，值为true或false，选择true该消息群发给所有用户，选择false可根据group_id发送给指定群组的用户</param>
        /// <param name="sendIgnoreReprint">待群发的文章被判定为转载时，是否继续群发</param>
        /// <param name="clientmsgid">开发者侧群发msgid，长度限制64字节，如不填，则后台默认以群发范围和群发内容的摘要值做为clientmsgid</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "GroupMessageApi.SendGroupMessageByFilter", true)]
        private static SendResult SendGroupMessageByFilter(string accessTokenOrAppId, string groupId, string tagId, string value, GroupMessageType type, bool isToAll = false, bool sendIgnoreReprint = false, string clientmsgid = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/mass/sendall?access_token={0}";

                BaseGroupMessageDataByFilter baseData = null;
                BaseGroupMessageByFilter filter = null;
                if (!groupId.IsNullOrEmpty())
                {
                    filter = new GroupMessageByGroupId()
                    {
                        group_id = groupId,
                        is_to_all = isToAll,
                    };
                }
                else
                {
                    filter = new GroupMessageByTagId()
                    {
                        tag_id = tagId,
                        is_to_all = isToAll,
                    };
                }

                switch (type)
                {
                    case GroupMessageType.image:
                        baseData = new GroupMessageByFilter_ImageData()
                        {
                            filter = filter,
                            image = new GroupMessageByGroupId_MediaId()
                            {
                                media_id = value
                            },
                            msgtype = "image"
                        };
                        break;
                    case GroupMessageType.voice:
                        baseData = new GroupMessageByFilter_VoiceData()
                        {
                            filter = filter,
                            voice = new GroupMessageByGroupId_MediaId()
                            {
                                media_id = value
                            },
                            msgtype = "voice"
                        };
                        break;
                    case GroupMessageType.mpnews:
                        baseData = new GroupMessageByFilter_MpNewsData()
                        {
                            filter = filter,
                            mpnews = new GroupMessageByGroupId_MediaId()
                            {
                                media_id = value
                            },
                            msgtype = "mpnews"
                        };
                        break;
                    case GroupMessageType.video:
                        baseData = new GroupMessageByFilter_MpVideoData()
                        {
                            filter = filter,
                            mpvideo = new GroupMessageByGroupId_MediaId()
                            {
                                media_id = value
                            },
                            msgtype = "mpvideo"
                        };
                        break;
                    case GroupMessageType.wxcard:
                        baseData = new GroupMessageByFilter_WxCardData()
                        {
                            filter = filter,
                            wxcard = new GroupMessageByGroupId_WxCard()
                            {
                                card_id = value
                            },
                            msgtype = "wxcard"
                        };
                        break;
                    case GroupMessageType.text:
                        baseData = new GroupMessageByFilter_TextData()
                        {
                            filter = filter,
                            text = new GroupMessageByGroupId_Content()
                            {
                                content = value
                            },
                            msgtype = "text"
                        };
                        break;
                    default:
                        throw new Exception("参数错误。");
                        //break;
                }

                baseData.send_ignore_reprint = sendIgnoreReprint ? 0 : 1;//待群发的文章被判定为转载时，是否继续群发
                baseData.clientmsgid = clientmsgid;

                return CommonJsonSend.Send<SendResult>(accessToken, urlFormat, baseData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 根据[分组]进行群发【订阅号与服务号认证后均可用】
        /// 
        /// 请注意：
        /// 1、该接口暂时仅提供给已微信认证的服务号
        /// 2、虽然开发者使用高级群发接口的每日调用限制为100次，但是用户每月只能接收4条，请小心测试
        /// 3、无论在公众平台网站上，还是使用接口群发，用户每月只能接收4条群发消息，多于4条的群发将对该用户发送失败。
        /// 4、群发视频时需要先调用GetVideoMediaIdResult接口获取专用的MediaId然后进行群发
        /// 
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="groupId">群发到的分组的group_id，参见用户管理中用户分组接口，若is_to_all值为true，可不填写group_id；如果groupId和tagId同时填写，优先使用groupId；groupId和tagId最多只能使用一个</param>
        /// <param name="value">群发媒体文件时传入mediaId,群发文本消息时传入content,群发卡券时传入cardId</param>
        /// <param name="type"></param>
        /// <param name="isToAll">用于设定是否向全部用户发送，值为true或false，选择true该消息群发给所有用户，选择false可根据group_id发送给指定群组的用户</param>
        /// <param name="sendIgnoreReprint">待群发的文章被判定为转载时，是否继续群发</param>
        /// <param name="clientmsgid">开发者侧群发msgid，长度限制64字节，如不填，则后台默认以群发范围和群发内容的摘要值做为clientmsgid</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "GroupMessageApi.SendGroupMessageByGroupId", true)]
        public static SendResult SendGroupMessageByGroupId(string accessTokenOrAppId, string groupId, string value, GroupMessageType type, bool isToAll = false, bool sendIgnoreReprint = false, string clientmsgid = null,
            int timeOut = Config.TIME_OUT)
        {
            return SendGroupMessageByFilter(accessTokenOrAppId, groupId, null, value, type, isToAll, sendIgnoreReprint, clientmsgid,
                timeOut);
        }

        /// <summary>
        /// 根据[标签]进行群发【订阅号与服务号认证后均可用】
        /// 
        /// 请注意：
        /// 1、该接口暂时仅提供给已微信认证的服务号
        /// 2、虽然开发者使用高级群发接口的每日调用限制为100次，但是用户每月只能接收4条，请小心测试
        /// 3、无论在公众平台网站上，还是使用接口群发，用户每月只能接收4条群发消息，多于4条的群发将对该用户发送失败。
        /// 4、群发视频时需要先调用GetVideoMediaIdResult接口获取专用的MediaId然后进行群发
        /// 
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="tagId">群发到的标签的tag_id，若is_to_all值为true，可不填写tag_id</param>
        /// <param name="value">群发媒体文件时传入mediaId,群发文本消息时传入content,群发卡券时传入cardId</param>
        /// <param name="type"></param>
        /// <param name="isToAll">用于设定是否向全部用户发送，值为true或false，选择true该消息群发给所有用户，选择false可根据group_id发送给指定群组的用户</param>
        /// <param name="sendIgnoreReprint">待群发的文章被判定为转载时，是否继续群发</param>
        /// <param name="clientmsgid">开发者侧群发msgid，长度限制64字节，如不填，则后台默认以群发范围和群发内容的摘要值做为clientmsgid</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "GroupMessageApi.SendGroupMessageByTagId", true)]
        public static SendResult SendGroupMessageByTagId(string accessTokenOrAppId, string tagId, string value, GroupMessageType type, bool isToAll = false, bool sendIgnoreReprint = false, string clientmsgid = null,
            int timeOut = Config.TIME_OUT)
        {
            return SendGroupMessageByFilter(accessTokenOrAppId, null, tagId, value, type, isToAll, sendIgnoreReprint, clientmsgid,
                timeOut);
        }


        #endregion

        /// <summary>
        /// 根据OpenId进行群发【订阅号不可用，服务号认证后可用】
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="value">群发媒体文件时传入mediaId,群发文本消息时传入content,群发卡券时传入cardId</param>
        /// <param name="type"></param>
        /// <param name="openIds">openId字符串数组</param>
        /// <param name="clientmsgid">开发者侧群发msgid，长度限制64字节，如不填，则后台默认以群发范围和群发内容的摘要值做为clientmsgid</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "GroupMessageApi.SendGroupMessageByOpenId", true)]
        public static SendResult SendGroupMessageByOpenId(string accessTokenOrAppId, GroupMessageType type, string value, string clientmsgid = null, int timeOut = Config.TIME_OUT, params string[] openIds)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/mass/send?access_token={0}";

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

                baseData.clientmsgid = clientmsgid;

                return CommonJsonSend.Send<SendResult>(accessToken, urlFormat, baseData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 根据OpenID列表群发视频消息【订阅号不可用，服务号认证后可用】
        /// 注意：群发视频时需要先调用GetVideoMediaIdResult接口获取专用的MediaId然后进行群发
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="title"></param>
        /// <param name="mediaId"></param>
        /// <param name="clientmsgid">开发者侧群发msgid，长度限制64字节，如不填，则后台默认以群发范围和群发内容的摘要值做为clientmsgid</param>
        /// <param name="openIds">openId字符串数组</param>
        /// <param name="description"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "GroupMessageApi.SendVideoGroupMessageByOpenId", true)]
        public static SendResult SendVideoGroupMessageByOpenId(string accessTokenOrAppId, string title, string description, string mediaId, string clientmsgid = null, int timeOut = Config.TIME_OUT, params string[] openIds)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/mass/send?access_token={0}";

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

                baseData.clientmsgid = clientmsgid;

                return CommonJsonSend.Send<SendResult>(accessToken, urlFormat, baseData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 删除群发消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="msgId">发送出去的消息ID</param>
        /// <param name="articleIdx">（非必填）要删除的文章在图文消息中的位置，第一篇编号为1，该字段不填或填0会删除全部文章</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "GroupMessageApi.DeleteSendMessage", true)]
        public static WxJsonResult DeleteSendMessage(string accessTokenOrAppId, string msgId, int? articleIdx, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                //官方API地址为https://api.weixin.qq.com//cgi-bin/message/mass/delete?access_token={0}，应该是多了一个/
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/mass/delete?access_token={0}";

                var data = new
                {
                    msg_id = msgId,
                    article_idx = articleIdx
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 预览接口【订阅号与服务号认证后均可用】
        /// 注意：openId与wxName两者任选其一，同时传入以wxName优先
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="value">群发媒体消息时为media_id，群发文本信息为content</param>
        /// <param name="type"></param>
        /// <param name="openId">接收消息用户对应该公众号的openid</param>
        /// <param name="wxName">接收消息用户的微信号</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "GroupMessageApi.SendGroupMessagePreview", true)]
        public static SendResult SendGroupMessagePreview(string accessTokenOrAppId, GroupMessageType type, string value, string openId, string wxName = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/mass/preview?access_token={0}";

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
                    default:
                        throw new Exception("参数错误。");
                }
                return CommonJsonSend.Send<SendResult>(accessToken, urlFormat, baseData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 预览卡券接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId"></param>
        /// <param name="code"></param>
        /// <param name="openId"></param>
        /// <param name="wxName"></param>
        /// <param name="timestamp"></param>
        /// <param name="signature"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "GroupMessageApi.WxCardGroupMessagePreview", true)]
        public static SendResult WxCardGroupMessagePreview(string accessTokenOrAppId, string cardId, string code,
            string openId, string wxName, string timestamp, string signature, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/mass/preview?access_token={0}";

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="msgId">群发消息后返回的消息id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "GroupMessageApi.GetGroupMessageResult", true)]
        public static GetSendResult GetGroupMessageResult(string accessTokenOrAppId, string msgId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/mass/get?access_token={0}";

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="mediaId"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "GroupMessageApi.GetVideoMediaIdResult", true)]
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

        /// <summary>
        /// 获取群发速度
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "GroupMessageApi.GetSendSpeed", true)]
        public static GetSpeedResult GetSendSpeed(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/mass/speed/get?access_token={0}";

                return CommonJsonSend.Send<GetSpeedResult>(null, urlFormat, null, CommonJsonSendType.POST, timeOut, true);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 设置群发速度
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="speed">群发速度的级别</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "GroupMessageApi.SetSendSpeed", true)]
        public static WxJsonResult SetSendSpeed(string accessTokenOrAppId, int speed, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/mass/speed/set?access_token={0}";

                var data = new
                {
                    speed = speed
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut, true);

            }, accessTokenOrAppId);
        }

        #endregion

        #region 异步方法

        #region 根据分组或标签群发

        /// <summary>
        /// 【异步方法】根据分组进行群发【订阅号与服务号认证后均可用】
        /// 
        /// 请注意：
        /// 1、该接口暂时仅提供给已微信认证的服务号
        /// 2、虽然开发者使用高级群发接口的每日调用限制为100次，但是用户每月只能接收4条，请小心测试
        /// 3、无论在公众平台网站上，还是使用接口群发，用户每月只能接收4条群发消息，多于4条的群发将对该用户发送失败。
        /// 4、群发视频时需要先调用GetVideoMediaIdResult接口获取专用的MediaId然后进行群发
        /// 
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="groupId">群发到的分组的group_id，参见用户管理中用户分组接口，若is_to_all值为true，可不填写group_id；如果groupId和tagId同时填写，优先使用groupId；groupId和tagId最多只能使用一个</param>
        /// <param name="tagId">群发到的标签的tag_id，若is_to_all值为true，可不填写tag_id；如果groupId和tagId同时填写，优先使用groupId；groupId和tagId最多只能使用一个</param>
        /// <param name="value">群发媒体文件时传入mediaId,群发文本消息时传入content,群发卡券时传入cardId</param>
        /// <param name="type"></param>
        /// <param name="isToAll">用于设定是否向全部用户发送，值为true或false，选择true该消息群发给所有用户，选择false可根据group_id发送给指定群组的用户</param>
        /// <param name="sendIgnoreReprint">待群发的文章被判定为转载时，是否继续群发</param>
        /// <param name="clientmsgid">开发者侧群发msgid，长度限制64字节，如不填，则后台默认以群发范围和群发内容的摘要值做为clientmsgid</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "GroupMessageApi.SendGroupMessageByFilterAsync", true)]
        private static async Task<SendResult> SendGroupMessageByFilterAsync(string accessTokenOrAppId, string groupId, string tagId, string value, GroupMessageType type, bool isToAll = false, bool sendIgnoreReprint = false, string clientmsgid = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/mass/sendall?access_token={0}";

                BaseGroupMessageDataByFilter baseData = null;
                BaseGroupMessageByFilter filter = null;
                if (!groupId.IsNullOrEmpty())
                {
                    filter = new GroupMessageByGroupId()
                    {
                        group_id = groupId,
                        is_to_all = isToAll,
                    };
                }
                else
                {
                    filter = new GroupMessageByTagId()
                    {
                        tag_id = tagId,
                        is_to_all = isToAll,
                    };
                }

                switch (type)
                {
                    case GroupMessageType.image:
                        baseData = new GroupMessageByFilter_ImageData()
                        {
                            filter = filter,
                            image = new GroupMessageByGroupId_MediaId()
                            {
                                media_id = value
                            },
                            msgtype = "image"
                        };
                        break;
                    case GroupMessageType.voice:
                        baseData = new GroupMessageByFilter_VoiceData()
                        {
                            filter = filter,
                            voice = new GroupMessageByGroupId_MediaId()
                            {
                                media_id = value
                            },
                            msgtype = "voice"
                        };
                        break;
                    case GroupMessageType.mpnews:
                        baseData = new GroupMessageByFilter_MpNewsData()
                        {
                            filter = filter,
                            mpnews = new GroupMessageByGroupId_MediaId()
                            {
                                media_id = value
                            },
                            msgtype = "mpnews"
                        };
                        break;
                    case GroupMessageType.video:
                        baseData = new GroupMessageByFilter_MpVideoData()
                        {
                            filter = filter,
                            mpvideo = new GroupMessageByGroupId_MediaId()
                            {
                                media_id = value
                            },
                            msgtype = "mpvideo"
                        };
                        break;
                    case GroupMessageType.wxcard:
                        baseData = new GroupMessageByFilter_WxCardData()
                        {
                            filter = filter,
                            wxcard = new GroupMessageByGroupId_WxCard()
                            {
                                card_id = value
                            },
                            msgtype = "wxcard"
                        };
                        break;
                    case GroupMessageType.text:
                        baseData = new GroupMessageByFilter_TextData()
                        {
                            filter = filter,
                            text = new GroupMessageByGroupId_Content()
                            {
                                content = value
                            },
                            msgtype = "text"
                        };
                        break;
                    default:
                        throw new Exception("参数错误。");
                        //break;
                }

                baseData.send_ignore_reprint = sendIgnoreReprint ? 0 : 1;//待群发的文章被判定为转载时，是否继续群发
                baseData.clientmsgid = clientmsgid;

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<SendResult>(accessToken, urlFormat, baseData, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }


        /// <summary>
        /// 【异步方法】根据[分组]进行群发【订阅号与服务号认证后均可用】
        /// 
        /// 请注意：
        /// 1、该接口暂时仅提供给已微信认证的服务号
        /// 2、虽然开发者使用高级群发接口的每日调用限制为100次，但是用户每月只能接收4条，请小心测试
        /// 3、无论在公众平台网站上，还是使用接口群发，用户每月只能接收4条群发消息，多于4条的群发将对该用户发送失败。
        /// 4、群发视频时需要先调用GetVideoMediaIdResult接口获取专用的MediaId然后进行群发
        /// 
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="groupId">群发到的分组的group_id，参见用户管理中用户分组接口，若is_to_all值为true，可不填写group_id；如果groupId和tagId同时填写，优先使用groupId；groupId和tagId最多只能使用一个</param>
        /// <param name="value">群发媒体文件时传入mediaId,群发文本消息时传入content,群发卡券时传入cardId</param>
        /// <param name="type"></param>
        /// <param name="isToAll">用于设定是否向全部用户发送，值为true或false，选择true该消息群发给所有用户，选择false可根据group_id发送给指定群组的用户</param>
        /// <param name="sendIgnoreReprint">待群发的文章被判定为转载时，是否继续群发</param>
        /// <param name="clientmsgid">开发者侧群发msgid，长度限制64字节，如不填，则后台默认以群发范围和群发内容的摘要值做为clientmsgid</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "GroupMessageApi.SendGroupMessageByGroupIdAsync", true)]
        public static async Task<SendResult> SendGroupMessageByGroupIdAsync(string accessTokenOrAppId, string groupId, string value, GroupMessageType type, bool isToAll = false, bool sendIgnoreReprint = false, string clientmsgid = null,
            int timeOut = Config.TIME_OUT)
        {
            return await SendGroupMessageByFilterAsync(accessTokenOrAppId, groupId, null, value, type, isToAll, sendIgnoreReprint, clientmsgid,
                timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】根据[标签]进行群发【订阅号与服务号认证后均可用】
        /// 
        /// 请注意：
        /// 1、该接口暂时仅提供给已微信认证的服务号
        /// 2、虽然开发者使用高级群发接口的每日调用限制为100次，但是用户每月只能接收4条，请小心测试
        /// 3、无论在公众平台网站上，还是使用接口群发，用户每月只能接收4条群发消息，多于4条的群发将对该用户发送失败。
        /// 4、群发视频时需要先调用GetVideoMediaIdResult接口获取专用的MediaId然后进行群发
        /// 
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="tagId">群发到的标签的tag_id，若is_to_all值为true，可不填写tag_id</param>
        /// <param name="value">群发媒体文件时传入mediaId,群发文本消息时传入content,群发卡券时传入cardId</param>
        /// <param name="type"></param>
        /// <param name="isToAll">用于设定是否向全部用户发送，值为true或false，选择true该消息群发给所有用户，选择false可根据group_id发送给指定群组的用户</param>
        /// <param name="sendIgnoreReprint">待群发的文章被判定为转载时，是否继续群发</param>
        /// <param name="clientmsgid">开发者侧群发msgid，长度限制64字节，如不填，则后台默认以群发范围和群发内容的摘要值做为clientmsgid</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "GroupMessageApi.SendGroupMessageByTagIdAsync", true)]
        public static async Task<SendResult> SendGroupMessageByTagIdAsync(string accessTokenOrAppId, string tagId, string value, GroupMessageType type, bool isToAll = false, bool sendIgnoreReprint = false, string clientmsgid = null,
            int timeOut = Config.TIME_OUT)
        {
            return await SendGroupMessageByFilterAsync(accessTokenOrAppId, null, tagId, value, type, isToAll, sendIgnoreReprint, clientmsgid,
                timeOut).ConfigureAwait(false);
        }


        #endregion


        /// <summary>
        /// 【异步方法】根据OpenId进行群发【订阅号不可用，服务号认证后可用】
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="value">群发媒体文件时传入mediaId,群发文本消息时传入content,群发卡券时传入cardId</param>
        /// <param name="type"></param>
        /// <param name="openIds">openId字符串数组</param>
        /// <param name="clientmsgid">开发者侧群发msgid，长度限制64字节，如不填，则后台默认以群发范围和群发内容的摘要值做为clientmsgid</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "GroupMessageApi.SendGroupMessageByOpenIdAsync", true)]
        public static async Task<SendResult> SendGroupMessageByOpenIdAsync(string accessTokenOrAppId, GroupMessageType type, string value, string clientmsgid = null, int timeOut = Config.TIME_OUT, params string[] openIds)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/mass/send?access_token={0}";

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
                    //break;
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
                        //break;
                }

                baseData.clientmsgid = clientmsgid;

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<SendResult>(accessToken, urlFormat, baseData, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】根据OpenID列表群发视频消息【订阅号不可用，服务号认证后可用】
        /// 注意：群发视频时需要先调用GetVideoMediaIdResult接口获取专用的MediaId然后进行群发
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="title"></param>
        /// <param name="mediaId"></param>
        /// <param name="clientmsgid">开发者侧群发msgid，长度限制64字节，如不填，则后台默认以群发范围和群发内容的摘要值做为clientmsgid</param>
        /// <param name="openIds">openId字符串数组</param>
        /// <param name="description"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "GroupMessageApi.SendVideoGroupMessageByOpenIdAsync", true)]
        public static async Task<SendResult> SendVideoGroupMessageByOpenIdAsync(string accessTokenOrAppId, string title, string description, string mediaId, string clientmsgid = null, int timeOut = Config.TIME_OUT, params string[] openIds)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/mass/send?access_token={0}";

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

                baseData.clientmsgid = clientmsgid;

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<SendResult>(accessToken, urlFormat, baseData, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】删除群发消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="msgId">发送出去的消息ID</param>
        /// <param name="articleIdx">（非必填）要删除的文章在图文消息中的位置，第一篇编号为1，该字段不填或填0会删除全部文章</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "GroupMessageApi.DeleteSendMessageAsync", true)]
        public static async Task<WxJsonResult> DeleteSendMessageAsync(string accessTokenOrAppId, string msgId, int? articleIdx, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                //官方API地址为https://api.weixin.qq.com//cgi-bin/message/mass/delete?access_token={0}，应该是多了一个/
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/mass/delete?access_token={0}";

                var data = new
                {
                    msg_id = msgId,
                    article_idx = articleIdx
                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】预览接口【订阅号与服务号认证后均可用】
        /// 注意：openId与wxName两者任选其一，同时传入以wxName优先
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="value">群发媒体消息时为media_id，群发文本信息为content</param>
        /// <param name="type"></param>
        /// <param name="openId">接收消息用户对应该公众号的openid</param>
        /// <param name="wxName">接收消息用户的微信号</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "GroupMessageApi.SendGroupMessagePreviewAsync", true)]
        public static async Task<SendResult> SendGroupMessagePreviewAsync(string accessTokenOrAppId, GroupMessageType type, string value, string openId, string wxName = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/mass/preview?access_token={0}";

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
                    default:
                        throw new Exception("参数错误。");
                }
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<SendResult>(accessToken, urlFormat, baseData, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】预览卡券接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId"></param>
        /// <param name="code"></param>
        /// <param name="openId"></param>
        /// <param name="wxName"></param>
        /// <param name="timestamp"></param>
        /// <param name="signature"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "GroupMessageApi.WxCardGroupMessagePreviewAsync", true)]
        public static async Task<SendResult> WxCardGroupMessagePreviewAsync(string accessTokenOrAppId, string cardId, string code,
            string openId, string wxName, string timestamp, string signature, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/mass/preview?access_token={0}";

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

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<SendResult>(accessToken, urlFormat, baseData, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】查询群发消息发送状态【订阅号与服务号认证后均可用】
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="msgId">群发消息后返回的消息id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "GroupMessageApi.GetGroupMessageResultAsync", true)]
        public static async Task<GetSendResult> GetGroupMessageResultAsync(string accessTokenOrAppId, string msgId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/mass/get?access_token={0}";

                var data = new
                {
                    msg_id = msgId
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetSendResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取视频群发用的MediaId
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="mediaId"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "GroupMessageApi.GetVideoMediaIdResultAsync", true)]
        public static async Task<VideoMediaIdResult> GetVideoMediaIdResultAsync(string accessTokenOrAppId, string mediaId, string title,
            string description, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string url = string.Format("https://file.api.weixin.qq.com/cgi-bin/media/uploadvideo?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    media_id = mediaId,
                    title = title,
                    description = description
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<VideoMediaIdResult>(null, url, data, CommonJsonSendType.POST, timeOut, true).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取群发速度
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "GroupMessageApi.GetSendSpeedAsync", true)]
        public static async Task<GetSpeedResult> GetSendSpeedAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/mass/speed/get?access_token={0}";
                
                return await Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetSpeedResult>(null, urlFormat, null, CommonJsonSendType.POST, timeOut, true).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】设置群发速度
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="speed">群发速度的级别</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "GroupMessageApi.SetSendSpeedAsync", true)]
        public static async Task<WxJsonResult> SetSendSpeedAsync(string accessTokenOrAppId, int speed, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/mass/speed/set?access_token={0}";

                var data = new
                {
                    speed = speed
                };

                return await Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut, true).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        #endregion
    }
}
