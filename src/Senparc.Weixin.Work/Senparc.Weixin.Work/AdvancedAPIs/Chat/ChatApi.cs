﻿/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：ChatApi.cs
    文件功能描述：企业号消息接口
    
    
    创建标识：Senparc - 20150728

    修改标识：Senparc - 20160720
    修改描述：增加其接口的异步方法

    修改标识：Senparc - 20170712
    修改描述：v14.5.1 AccessToken HandlerWaper改造
 
----------------------------------------------------------------*/

/*
    官方文档：http://qydev.weixin.qq.com/wiki/index.php?title=%E4%BC%81%E4%B8%9A%E5%8F%B7%E6%B6%88%E6%81%AF%E6%8E%A5%E5%8F%A3%E8%AF%B4%E6%98%8E
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.Work.AdvancedAPIs.Chat;
using Senparc.Weixin.Work.CommonAPIs;

namespace Senparc.Weixin.Work.AdvancedAPIs
{

    public static class ChatApi
    {
        #region 同步方法


        /// <summary>
        /// 创建会话
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId">会话id。字符串类型，最长32个字符。只允许字符0-9及字母a-zA-Z, 如果值内容为64bit无符号整型：要求值范围在[1, 2^63)之间，[2^63, 2^64)为系统分配会话id区间</param>
        /// <param name="name">会话标题</param>
        /// <param name="owner">管理员userid，必须是该会话userlist的成员之一</param>
        /// <param name="userlist">会话成员列表，成员用userid来标识。会话成员必须在3人或以上，1000人以下</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [Obsolete("此接口已被官方废除")]
        public static WorkJsonResult CreateChat(string accessTokenOrAppKey, string chatId, string name, string owner, string[] userlist, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/chat/create?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    chatid = chatId,
                    name = name,
                    owner = owner,
                    userlist = userlist
                };

                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 获取会话
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public static GetChatResult GetChat(string accessTokenOrAppKey, string chatId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/chat/get?access_token={0}&chatid={1}", accessToken.AsUrlData(), chatId.AsUrlData());

                return Get.GetJson<GetChatResult>(url);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 修改会话信息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId">会话id</param>
        /// <param name="opUser">操作人userid</param>
        /// <param name="name">会话标题</param>
        /// <param name="owner">管理员userid，必须是该会话userlist的成员之一</param>
        /// <param name="addUserList">会话新增成员列表，成员用userid来标识</param>
        /// <param name="delUserList">会话退出成员列表，成员用userid来标识</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult UpdateChat(string accessTokenOrAppKey, string chatId, string opUser, string name = null, string owner = null, string[] addUserList = null, string[] delUserList = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/chat/update?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    chatid = chatId,
                    op_user = opUser,
                    name = name,
                    owner = owner,
                    add_user_list = addUserList,
                    del_user_list = delUserList
                };

                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 退出会话
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId"></param>
        /// <param name="opUser"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult QuitChat(string accessTokenOrAppKey, string chatId, string opUser, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/chat/quit?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    chatid = chatId,
                    op_user = opUser,
                };

                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 清除消息未读状态
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="opUser">会话所有者的userid</param>
        /// <param name="type">会话类型：single|group，分别表示：群聊|单聊</param>
        /// <param name="chatIdOrUserId">会话值，为userid|chatid，分别表示：成员id|会话id，单聊是userid，群聊是chatid</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult ClearNotify(string accessTokenOrAppKey, string opUser, Chat_Type type, string chatIdOrUserId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/chat/clearnotify?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    op_user = opUser,
                    chat = new
                    {
                        type = type,
                        id = chatIdOrUserId
                    }
                };

                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 发消息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="sender">发送人的userId</param>
        /// <param name="type">接收人类型：single|group，分别表示：群聊|单聊</param>
        /// <param name="msgType">消息类型,text|image|file</param>
        /// <param name="chatIdOrUserId">会话值，为userid|chatid，分别表示：成员id|会话id，单聊是userid，群聊是chatid</param>
        /// <param name="contentOrMediaId">文本消息是content，图片或文件是mediaId</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult SendChatMessage(string accessTokenOrAppKey, string sender, Chat_Type type, ChatMsgType msgType, string chatIdOrUserId, string contentOrMediaId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/chat/send?access_token={0}", accessToken.AsUrlData());

                BaseSendChatMessageData data;

                switch (msgType)
                {
                    case ChatMsgType.text:
                        data = new SendTextMessageData()
                        {
                            receiver = new Receiver()
                            {
                                type = type.ToString(),
                                id = chatIdOrUserId
                            },
                            sender = sender,
                            msgtype = msgType.ToString(),
                            text = new Chat_Content()
                            {
                                content = contentOrMediaId
                            }
                        };
                        break;
                    case ChatMsgType.image:
                        data = new SendImageMessageData()
                        {
                            receiver = new Receiver()
                            {
                                type = type.ToString(),
                                id = chatIdOrUserId
                            },
                            sender = sender,
                            msgtype = msgType.ToString(),
                            image = new Chat_Image()
                            {
                                media_id = contentOrMediaId
                            }
                        };
                        break;
                    case ChatMsgType.file:
                        data = new SendFileMessageData()
                        {
                            receiver = new Receiver()
                            {
                                type = type.ToString(),
                                id = chatIdOrUserId
                            },
                            sender = sender,
                            msgtype = msgType.ToString(),
                            file = new Chat_File()
                            {
                                media_id = contentOrMediaId
                            }
                        };
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("msgType");
                }

                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        //{
        //    "text":
        //        {
        //            "content":"111"
        //        },
        //    "receiver":
        //        {
        //            "type":"group",
        //            "id":"1"
        //        },
        //    "sender":"005",
        //    "msgtype":"text"
        //}

        /// <summary>
        /// 设置成员新消息免打扰
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="userMuteList">成员新消息免打扰参数，数组，最大支持10000个成员</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static SetMuteResult SetMute(string accessTokenOrAppKey, List<UserMute> userMuteList, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/chat/setmute?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    user_mute_list = userMuteList
                };

                return CommonJsonSend.Send<SetMuteResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }
        #endregion

#if !NET35 && !NET40
        #region 异步方法
        /// <summary>
        /// 【异步方法】创建会话
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId">会话id。字符串类型，最长32个字符。只允许字符0-9及字母a-zA-Z, 如果值内容为64bit无符号整型：要求值范围在[1, 2^63)之间，[2^63, 2^64)为系统分配会话id区间</param>
        /// <param name="name">会话标题</param>
        /// <param name="owner">管理员userid，必须是该会话userlist的成员之一</param>
        /// <param name="userlist">会话成员列表，成员用userid来标识。会话成员必须在3人或以上，1000人以下</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [Obsolete("此接口已被官方废除")]
        public static async Task<WorkJsonResult> CreateChatAsync(string accessTokenOrAppKey, string chatId, string name, string owner, string[] userlist, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/chat/create?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    chatid = chatId,
                    name = name,
                    owner = owner,
                    userlist = userlist
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】获取会话
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public static async Task<GetChatResult> GetChatAsync(string accessTokenOrAppKey, string chatId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/chat/get?access_token={0}&chatid={1}", accessToken.AsUrlData(), chatId.AsUrlData());

                return await Get.GetJsonAsync<GetChatResult>(url);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】修改会话信息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId">会话id</param>
        /// <param name="opUser">操作人userid</param>
        /// <param name="name">会话标题</param>
        /// <param name="owner">管理员userid，必须是该会话userlist的成员之一</param>
        /// <param name="addUserList">会话新增成员列表，成员用userid来标识</param>
        /// <param name="delUserList">会话退出成员列表，成员用userid来标识</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> UpdateChatAsync(string accessTokenOrAppKey, string chatId, string opUser, string name = null, string owner = null, string[] addUserList = null, string[] delUserList = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/chat/update?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    chatid = chatId,
                    op_user = opUser,
                    name = name,
                    owner = owner,
                    add_user_list = addUserList,
                    del_user_list = delUserList
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】退出会话
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId"></param>
        /// <param name="opUser"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> QuitChatAsync(string accessTokenOrAppKey, string chatId, string opUser, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/chat/quit?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    chatid = chatId,
                    op_user = opUser,
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】清除消息未读状态
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="opUser">会话所有者的userid</param>
        /// <param name="type">会话类型：single|group，分别表示：群聊|单聊</param>
        /// <param name="chatIdOrUserId">会话值，为userid|chatid，分别表示：成员id|会话id，单聊是userid，群聊是chatid</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> ClearNotifyAsync(string accessTokenOrAppKey, string opUser, Chat_Type type, string chatIdOrUserId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/chat/clearnotify?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    op_user = opUser,
                    chat = new
                    {
                        type = type,
                        id = chatIdOrUserId
                    }
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】发消息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="sender">发送人的userId</param>
        /// <param name="type">接收人类型：single|group，分别表示：群聊|单聊</param>
        /// <param name="msgType">消息类型,text|image|file</param>
        /// <param name="chatIdOrUserId">会话值，为userid|chatid，分别表示：成员id|会话id，单聊是userid，群聊是chatid</param>
        /// <param name="contentOrMediaId">文本消息是content，图片或文件是mediaId</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> SendChatMessageAsync(string accessTokenOrAppKey, string sender, Chat_Type type, ChatMsgType msgType, string chatIdOrUserId, string contentOrMediaId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/chat/send?access_token={0}", accessToken.AsUrlData());

                BaseSendChatMessageData data;

                switch (msgType)
                {
                    case ChatMsgType.text:
                        data = new SendTextMessageData()
                        {
                            receiver = new Receiver()
                            {
                                type = type.ToString(),
                                id = chatIdOrUserId
                            },
                            sender = sender,
                            msgtype = msgType.ToString(),
                            text = new Chat_Content()
                            {
                                content = contentOrMediaId
                            }
                        };
                        break;
                    case ChatMsgType.image:
                        data = new SendImageMessageData()
                        {
                            receiver = new Receiver()
                            {
                                type = type.ToString(),
                                id = chatIdOrUserId
                            },
                            sender = sender,
                            msgtype = msgType.ToString(),
                            image = new Chat_Image()
                            {
                                media_id = contentOrMediaId
                            }
                        };
                        break;
                    case ChatMsgType.file:
                        data = new SendFileMessageData()
                        {
                            receiver = new Receiver()
                            {
                                type = type.ToString(),
                                id = chatIdOrUserId
                            },
                            sender = sender,
                            msgtype = msgType.ToString(),
                            file = new Chat_File()
                            {
                                media_id = contentOrMediaId
                            }
                        };
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("msgType");
                }

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        //{
        //    "text":
        //        {
        //            "content":"111"
        //        },
        //    "receiver":
        //        {
        //            "type":"group",
        //            "id":"1"
        //        },
        //    "sender":"005",
        //    "msgtype":"text"
        //}

        /// <summary>
        /// 【异步方法】设置成员新消息免打扰
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="userMuteList">成员新消息免打扰参数，数组，最大支持10000个成员</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<SetMuteResult> SetMuteAsync(string accessTokenOrAppKey, List<UserMute> userMuteList, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/chat/setmute?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    user_mute_list = userMuteList
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<SetMuteResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }
        #endregion
#endif
    }
}
