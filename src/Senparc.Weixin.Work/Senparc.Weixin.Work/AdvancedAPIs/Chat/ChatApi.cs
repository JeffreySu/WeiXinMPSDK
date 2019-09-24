/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ChatApi.cs
    文件功能描述：企业号消息接口
    
    
    创建标识：Senparc - 20150728

    修改标识：Senparc - 20160720
    修改描述：增加其接口的异步方法

    修改标识：Senparc - 20170712
    修改描述：v14.5.1 AccessToken HandlerWaper改造
 
    修改标识：pekrr1e - 20180503
    修改描述：v1.4.0 新增企业微信群聊会话功能支持
 
    修改标识：lishewen - 20180531
    修改描述：v1.6.1 创建会话返回结果
 
    修改标识：Senparc - 20190129
    修改描述：统一 CommonJsonSend.Send<T>() 方法请求接口

----------------------------------------------------------------*/

/*
    官方文档：https://work.weixin.qq.com/api/doc#13308
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.CO2NET.HttpUtility;
using Senparc.Weixin.Work.AdvancedAPIs.Chat;

namespace Senparc.Weixin.Work.AdvancedAPIs
{

    public static class ChatApi
    {
        private static string _urlFormatCreate = Config.ApiWorkHost + "/cgi-bin/appchat/create?access_token={0}";
        private static string _urlFormatUpdate = Config.ApiWorkHost + "/cgi-bin/appchat/update?access_token={0}";
        private static string _urlFormatGet = Config.ApiWorkHost + "/cgi-bin/appchat/get?access_token={0}&chatid={1}";
        private static string _urlFormatSend = Config.ApiWorkHost + "/cgi-bin/appchat/send?access_token={0}";

        #region 同步方法


        /// <summary>
        /// 创建会话
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId">群聊的唯一标志，不能与已有的群重复；字符串类型，最长32个字符。只允许字符0-9及字母a-zA-Z。如果不填，系统会随机生成群id</param>
        /// <param name="name">群聊名</param>
        /// <param name="owner">指定群主的id。如果不指定，系统会随机从userlist中选一人作为群主</param>
        /// <param name="userlist">群成员id列表。至少2人，至多500人</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ChatApi.CreateChat", true)]
        public static CreateChatResult CreateChat(string accessTokenOrAppKey, string chatId, string name, string owner, string[] userlist, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {

                var data = new
                {
                    chatid = chatId,
                    name = name,
                    owner = owner,
                    userlist = userlist
                };

                return CommonJsonSend.Send<CreateChatResult>(accessTokenOrAppKey, _urlFormatCreate, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 获取会话
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ChatApi.GetChat", true)]
        public static GetChatResult GetChat(string accessTokenOrAppKey, string chatId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(_urlFormatGet, accessToken.AsUrlData(), chatId.AsUrlData());

                return CommonJsonSend.Send<GetChatResult>(null, url, null, CommonJsonSendType.GET);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 修改会话信息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId">会话id</param>
        /// <param name="name">会话标题</param>
        /// <param name="owner">管理员userid，必须是该会话userlist的成员之一</param>
        /// <param name="addUserList">会话新增成员列表，成员用userid来标识</param>
        /// <param name="delUserList">会话退出成员列表，成员用userid来标识</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ChatApi.UpdateChat", true)]
        public static WorkJsonResult UpdateChat(string accessTokenOrAppKey, string chatId, string name = null, string owner = null, string[] addUserList = null, string[] delUserList = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    chatid = chatId,
                    name = name,
                    owner = owner,
                    add_user_list = addUserList,
                    del_user_list = delUserList
                };

                return CommonJsonSend.Send<WorkJsonResult>(accessToken, _urlFormatUpdate, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 发送简单消息（文本、图片、文件或语音）
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId">会话id</param>
        /// <param name="msgType">消息类型,text|image|file|voice</param>
        /// <param name="contentOrMediaId">文本消息是content，图片或文件是mediaId</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ChatApi.SendChatSimpleMessage", true)]
        public static WorkJsonResult SendChatSimpleMessage(string accessTokenOrAppKey, string chatId, ChatMsgType msgType, string contentOrMediaId, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                BaseSendChatMessageData data;

                switch (msgType)
                {
                    case ChatMsgType.text:
                        data = new SendTextMessageData(chatId, contentOrMediaId, safe);
                        break;
                    case ChatMsgType.image:
                        data = new SendImageMessageData(chatId, contentOrMediaId, safe);
                        break;
                    case ChatMsgType.voice:
                        data = new SendVoiceMessageData(chatId, contentOrMediaId, safe);
                        break;
                    case ChatMsgType.file:
                        data = new SendFileMessageData(chatId, contentOrMediaId, safe);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("msgType");
                }
                return CommonJsonSend.Send<WorkJsonResult>(accessToken, _urlFormatSend, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 发送视频消息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId">会话id</param>
        /// <param name="media_id">视频媒体文件id</param>
        /// <param name="title">视频消息的标题，不超过128个字节</param>
        /// <param name="description">视频消息的描述，不超过512个字节</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ChatApi.SendChatVideoMessage", true)]
        public static WorkJsonResult SendChatVideoMessage(string accessTokenOrAppKey, string chatId, string media_id, string title = null, string description = null, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new SendVideoMessageData(chatId, media_id, title, description, safe);
                return CommonJsonSend.Send<WorkJsonResult>(accessToken, _urlFormatSend, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 发送文本卡片消息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId">会话id</param>
        /// <param name="media_id">视频媒体文件id</param>
        /// <param name="title">标题，不超过128个字节</param>
        /// <param name="description">描述，不超过512个字节</param>
        /// <param name="url">点击后跳转的链接</param>
        /// <param name="btntxt">按钮文字， 默认为“详情”， 不超过4个文字</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ChatApi.SendChatTextCardMessage", true)]
        public static WorkJsonResult SendChatTextCardMessage(string accessTokenOrAppKey, string chatId, string title, string description, string url, string btntxt = null, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new SendTextCardMessageData(chatId, title, description, url, btntxt, safe);
                return CommonJsonSend.Send<WorkJsonResult>(accessToken, _urlFormatSend, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId">会话id</param>
        /// <param name="news">图文消息</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ChatApi.SendChatNewsMessage", true)]
        public static WorkJsonResult SendChatNewsMessage(string accessTokenOrAppKey, string chatId, Chat_News news, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new SendNewsMessageData(chatId, news, safe);
                return CommonJsonSend.Send<WorkJsonResult>(accessToken, _urlFormatSend, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 发送图文消息（mpnews）
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId">会话id</param>
        /// <param name="news">图文消息</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ChatApi.SendChatMpNewsMessage", true)]
        public static WorkJsonResult SendChatMpNewsMessage(string accessTokenOrAppKey, string chatId, Chat_MpNews mpnews, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new SendMpNewsMessageData(chatId, mpnews, safe);
                return CommonJsonSend.Send<WorkJsonResult>(accessToken, _urlFormatSend, data, CommonJsonSendType.POST, timeOut);
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
        [Obsolete("此接口已被官方废除")]
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ChatApi.QuitChat", true)]
        public static WorkJsonResult QuitChat(string accessTokenOrAppKey, string chatId, string opUser, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/chat/quit?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    chatid = chatId,
                    op_user = opUser,
                };

                return CommonJsonSend.Send<WorkJsonResult>(accessTokenOrAppKey, _urlFormatSend, data, CommonJsonSendType.POST, timeOut);
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
        [Obsolete("此接口已被官方废除")]
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ChatApi.ClearNotify", true)]
        public static WorkJsonResult ClearNotify(string accessTokenOrAppKey, string opUser, Chat_Type type, string chatIdOrUserId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/chat/clearnotify?access_token={0}", accessToken.AsUrlData());

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
        /// 设置成员新消息免打扰
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="userMuteList">成员新消息免打扰参数，数组，最大支持10000个成员</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [Obsolete("此接口已被官方废除")]
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ChatApi.SetMute", true)]
        public static SetMuteResult SetMute(string accessTokenOrAppKey, List<UserMute> userMuteList, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/chat/setmute?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    user_mute_list = userMuteList
                };

                return CommonJsonSend.Send<SetMuteResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }
        #endregion


        #region 异步方法
        /// <summary>
        /// 【异步方法】创建会话
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId">群聊的唯一标志，不能与已有的群重复；字符串类型，最长32个字符。只允许字符0-9及字母a-zA-Z。如果不填，系统会随机生成群id</param>
        /// <param name="name">群聊名</param>
        /// <param name="owner">指定群主的id。如果不指定，系统会随机从userlist中选一人作为群主</param>
        /// <param name="userlist">群成员id列表。至少2人，至多500人</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ChatApi.CreateChatAsync", true)]
        public static async Task<CreateChatResult> CreateChatAsync(string accessTokenOrAppKey, string chatId, string name, string owner, string[] userlist, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {

                var data = new
                {
                    chatid = chatId,
                    name = name,
                    owner = owner,
                    userlist = userlist
                };

                return await CommonJsonSend.SendAsync<CreateChatResult>(accessTokenOrAppKey, _urlFormatCreate, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取会话
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ChatApi.GetChatAsync", true)]
        public static async Task<GetChatResult> GetChatAsync(string accessTokenOrAppKey, string chatId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(_urlFormatGet, accessToken.AsUrlData(), chatId.AsUrlData());
                return await CommonJsonSend.SendAsync<GetChatResult>(null, url, null, CommonJsonSendType.GET).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】修改会话信息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId">会话id</param>
        /// <param name="name">会话标题</param>
        /// <param name="owner">管理员userid，必须是该会话userlist的成员之一</param>
        /// <param name="addUserList">会话新增成员列表，成员用userid来标识</param>
        /// <param name="delUserList">会话退出成员列表，成员用userid来标识</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ChatApi.UpdateChatAsync", true)]
        public static async Task<WorkJsonResult> UpdateChatAsync(string accessTokenOrAppKey, string chatId, string name = null, string owner = null, string[] addUserList = null, string[] delUserList = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new
                {
                    chatid = chatId,
                    name = name,
                    owner = owner,
                    add_user_list = addUserList,
                    del_user_list = delUserList
                };

                return await CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, _urlFormatUpdate, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】发送简单消息（文本、图片、文件或语音）
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId">会话id</param>
        /// <param name="msgType">消息类型,text|image|file|voice</param>
        /// <param name="contentOrMediaId">文本消息是content，图片或文件是mediaId</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ChatApi.SendChatSimpleMessageAsync", true)]
        public static async Task<WorkJsonResult> SendChatSimpleMessageAsync(string accessTokenOrAppKey, string chatId, ChatMsgType msgType, string contentOrMediaId, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                BaseSendChatMessageData data;

                switch (msgType)
                {
                    case ChatMsgType.text:
                        data = new SendTextMessageData(chatId, contentOrMediaId, safe);
                        break;
                    case ChatMsgType.image:
                        data = new SendImageMessageData(chatId, contentOrMediaId, safe);
                        break;
                    case ChatMsgType.voice:
                        data = new SendVoiceMessageData(chatId, contentOrMediaId, safe);
                        break;
                    case ChatMsgType.file:
                        data = new SendFileMessageData(chatId, contentOrMediaId, safe);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("msgType");
                }
                return await CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, _urlFormatSend, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】发送视频消息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId">会话id</param>
        /// <param name="media_id">视频媒体文件id</param>
        /// <param name="title">视频消息的标题，不超过128个字节</param>
        /// <param name="description">视频消息的描述，不超过512个字节</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ChatApi.SendChatVideoMessageAsync", true)]
        public static async Task<WorkJsonResult> SendChatVideoMessageAsync(string accessTokenOrAppKey, string chatId, string media_id, string title = null, string description = null, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new SendVideoMessageData(chatId, media_id, title, description, safe);
                return await CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, _urlFormatSend, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】发送文本卡片消息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId">会话id</param>
        /// <param name="media_id">视频媒体文件id</param>
        /// <param name="title">标题，不超过128个字节</param>
        /// <param name="description">描述，不超过512个字节</param>
        /// <param name="url">点击后跳转的链接</param>
        /// <param name="btntxt">按钮文字， 默认为“详情”， 不超过4个文字</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ChatApi.SendChatTextCardMessageAsync", true)]
        public static async Task<WorkJsonResult> SendChatTextCardMessageAsync(string accessTokenOrAppKey, string chatId, string title, string description, string url, string btntxt = null, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new SendTextCardMessageData(chatId, title, description, url, btntxt, safe);
                return await CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, _urlFormatSend, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】发送图文消息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId">会话id</param>
        /// <param name="news">图文消息</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ChatApi.SendChatNewsMessageAsync", true)]
        public static async Task<WorkJsonResult> SendChatNewsMessageAsync(string accessTokenOrAppKey, string chatId, Chat_News news, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new SendNewsMessageData(chatId, news, safe);
                return await CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, _urlFormatSend, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】发送图文消息（mpnews）
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId">会话id</param>
        /// <param name="news">图文消息</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ChatApi.SendChatMpNewsMessageAsync", true)]
        public static async Task<WorkJsonResult> SendChatMpNewsMessageAsync(string accessTokenOrAppKey, string chatId, Chat_MpNews mpnews, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new SendMpNewsMessageData(chatId, mpnews, safe);
                return await CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, _urlFormatSend, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】退出会话
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId"></param>
        /// <param name="opUser"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [Obsolete("此接口已被官方废除")]
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ChatApi.QuitChatAsync", true)]
        public static async Task<WorkJsonResult> QuitChatAsync(string accessTokenOrAppKey, string chatId, string opUser, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/chat/quit?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    chatid = chatId,
                    op_user = opUser,
                };

                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
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
        [Obsolete("此接口已被官方废除")]
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ChatApi.ClearNotifyAsync", true)]
        public static async Task<WorkJsonResult> ClearNotifyAsync(string accessTokenOrAppKey, string opUser, Chat_Type type, string chatIdOrUserId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/chat/clearnotify?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    op_user = opUser,
                    chat = new
                    {
                        type = type,
                        id = chatIdOrUserId
                    }
                };

                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
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
        [Obsolete("此接口已被官方废除")]
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ChatApi.SetMuteAsync", true)]
        public static async Task<SetMuteResult> SetMuteAsync(string accessTokenOrAppKey, List<UserMute> userMuteList, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/chat/setmute?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    user_mute_list = userMuteList
                };

                return await CommonJsonSend.SendAsync<SetMuteResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }
        #endregion
    }
}
