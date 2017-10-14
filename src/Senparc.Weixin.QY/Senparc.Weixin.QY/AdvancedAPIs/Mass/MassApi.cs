/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：MailListApi.cs
    文件功能描述：发送消息接口
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
 
    修改标识：Senparc - 20150313
    修改描述：开放代理请求超时时间
 
    修改标识：Senparc - 20160720
    修改描述：增加其接口的异步方法

    
    -----------------------------------
    
    修改标识：Senparc - 20170616
    修改描述：从QY移植，同步Work接口
         
    修改标识：Senparc - 20170709
    修改描述：v4.3.6 修复Senparc.Weixin.QY.AdvancedAPIs.MassApi中，因为accessToken为null而导致消息发送失败的问题 

    修改标识：Senparc - 20170712
    修改描述：v14.5.1 AccessToken HandlerWaper改造
----------------------------------------------------------------*/

/*
    官方文档：http://work.weixin.qq.com/api/doc#10167
 */

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senparc.Weixin.QY.AdvancedAPIs.Mass;
using Senparc.Weixin.QY.CommonAPIs;
using Senparc.Weixin.QY.Entities;
using Senparc.Weixin.Helpers;

namespace Senparc.Weixin.QY.AdvancedAPIs
{
    /// <summary>
    /// 发送消息
    /// </summary>
    public static class MassApi
    {
        private const string URL_FORMAT = "https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}";

        #region 同步请求


        /// <summary>
        /// 发送文本信息【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="content">消息内容</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MassResult SendText(string accessTokenOrAppKey, string agentId, string content,
            string toUser = null, string toParty = null, string toTag = null, int safe = 0,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    touser = toUser,
                    toparty = toParty,
                    totag = toTag,
                    msgtype = "text",
                    agentid = agentId,
                    text = new
                    {
                        content = content
                    },
                    safe = safe
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<MassResult>(accessToken, URL_FORMAT, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 发送图片消息【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag"></param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="mediaId">媒体资源文件ID</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MassResult SendImage(string accessTokenOrAppKey, string agentId, string mediaId,
                        string toUser = null, string toParty = null, string toTag = null, int safe = 0,
                        int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    touser = toUser,
                    toparty = toParty,
                    totag = toTag,
                    msgtype = "image",
                    agentid = agentId,
                    image = new
                    {
                        media_id = mediaId
                    },
                    safe = safe
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<MassResult>(accessToken, URL_FORMAT, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 发送语音消息【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="mediaId">媒体资源文件ID</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MassResult SendVoice(string accessTokenOrAppKey, string agentId, string mediaId,
             string toUser = null, string toParty = null, string toTag = null, int safe = 0,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    touser = toUser,
                    toparty = toParty,
                    totag = toTag,
                    msgtype = "voice",
                    agentid = agentId,
                    voice = new
                    {
                        media_id = mediaId
                    },
                    safe = safe
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<MassResult>(accessToken, URL_FORMAT, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 发送视频消息【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="mediaId">媒体资源文件ID</param>
        /// <param name="title">视频消息的标题</param>
        /// <param name="description">视频消息的描述</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MassResult SendVideo(string accessTokenOrAppKey, string agentId, string mediaId,
            string toUser = null, string toParty = null, string toTag = null,
            string title = null, string description = null, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    touser = toUser,
                    toparty = toParty,
                    totag = toTag,
                    msgtype = "video",
                    agentid = agentId,
                    video = new
                    {
                        media_id = mediaId,
                        title = title,
                        description = description,
                    },
                    safe = safe
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<MassResult>(accessToken, URL_FORMAT, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);

        }

        /// <summary>
        /// 发送文件消息【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="mediaId">媒体资源文件ID</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MassResult SendFile(string accessTokenOrAppKey, string agentId, string mediaId,
                string toUser = null, string toParty = null, string toTag = null, int safe = 0,
                int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    touser = toUser,
                    toparty = toParty,
                    totag = toTag,
                    msgtype = "file",
                    agentid = agentId,
                    file = new
                    {
                        media_id = mediaId
                    },
                    safe = safe
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<MassResult>(accessToken, URL_FORMAT, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);

        }

        /// <summary>
        /// 发送图文消息【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="articles">图文信息内容，包括title（标题）、description（描述）、url（点击后跳转的链接。企业可根据url里面带的code参数校验员工的真实身份）和picurl（图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80。如不填，在客户端不显示图片）</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MassResult SendNews(string accessTokenOrAppKey, string agentId, List<Article> articles,
                string toUser = null, string toParty = null, string toTag = null, int safe = 0,
                int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    touser = toUser,
                    toparty = toParty,
                    totag = toTag,
                    msgtype = "news",
                    agentid = agentId,
                    news = new
                    {
                        articles = articles.Select(z => new
                        {
                            title = z.Title,
                            description = z.Description,
                            url = z.Url,
                            picurl = z.PicUrl//图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80
                        }).ToList()
                    }
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<MassResult>(accessToken, URL_FORMAT, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);

        }

        /// <summary>
        /// 发送mpnews消息【QY移植修改】
        /// 注：mpnews消息与news消息类似，不同的是图文消息内容存储在微信后台，并且支持保密选项。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="articles"></param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MassResult SendMpNews(string accessTokenOrAppKey, string agentId, List<MpNewsArticle> articles,
            string toUser = null, string toParty = null, string toTag = null, int safe = 0,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    touser = toUser,
                    toparty = toParty,
                    totag = toTag,
                    msgtype = "mpnews",
                    agentid = agentId,
                    mpnews = new
                    {
                        articles = articles.Select(z => new
                        {
                            title = z.title,
                            thumb_media_id = z.thumb_media_id,
                            author = z.author,
                            content_source_url = z.content_source_url,
                            content = z.content,
                            digest = z.digest,
                            show_cover_pic = z.show_cover_pic
                        }).ToList(),
                    },
                    safe = safe
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<MassResult>(accessToken, URL_FORMAT, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);

        }
        #endregion

#if !NET35 && !NET40
        #region 异步请求
        /// <summary>
        /// 【异步方法】发送文本信息【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="content">消息内容</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<MassResult> SendTextAsync(string accessTokenOrAppKey, string agentId, string content, string toUser = null, string toParty = null, string toTag = null, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new
                {
                    touser = toUser,
                    toparty = toParty,
                    totag = toTag,
                    msgtype = "text",
                    agentid = agentId,
                    text = new
                    {
                        content = content
                    },
                    safe = safe
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<MassResult>(accessToken, URL_FORMAT, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);
            
        }

        /// <summary>
        /// 【异步方法】发送图片消息【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag"></param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="mediaId">媒体资源文件ID</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<MassResult> SendImageAsync(string accessTokenOrAppKey, string agentId, string mediaId, string toUser = null, string toParty = null, string toTag = null, int safe = 0,
            int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new
                {
                    touser = toUser,
                    toparty = toParty,
                    totag = toTag,
                    msgtype = "image",
                    agentid = agentId,
                    image = new
                    {
                        media_id = mediaId
                    },
                    safe = safe
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<MassResult>(accessToken, URL_FORMAT, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);
            
        }

        /// <summary>
        /// 【异步方法】发送语音消息【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="mediaId">媒体资源文件ID</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<MassResult> SendVoiceAsync(string accessTokenOrAppKey, string agentId, string mediaId,
                        string toUser = null, string toParty = null, string toTag = null, int safe = 0,
                        int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new
                {
                    touser = toUser,
                    toparty = toParty,
                    totag = toTag,
                    msgtype = "voice",
                    agentid = agentId,
                    voice = new
                    {
                        media_id = mediaId
                    },
                    safe = safe
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<MassResult>(accessToken, URL_FORMAT, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);
            
        }

        /// <summary>
        /// 【异步方法】发送视频消息【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="mediaId">媒体资源文件ID</param>
        /// <param name="title">视频消息的标题</param>
        /// <param name="description">视频消息的描述</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<MassResult> SendVideoAsync(string accessTokenOrAppKey, string agentId, string mediaId,
                string toUser = null, string toParty = null, string toTag = null,
                string title = null, string description = null, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new
                {
                    touser = toUser,
                    toparty = toParty,
                    totag = toTag,
                    msgtype = "video",
                    agentid = agentId,
                    video = new
                    {
                        media_id = mediaId,
                        title = title,
                        description = description,
                    },
                    safe = safe
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<MassResult>(accessToken, URL_FORMAT, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);
            
        }

        /// <summary>
        /// 【异步方法】发送文件消息【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="mediaId">媒体资源文件ID</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<MassResult> SendFileAsync(string accessTokenOrAppKey, string agentId, string mediaId,
                string toUser = null, string toParty = null, string toTag = null, int safe = 0,
                int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new
                {
                    touser = toUser,
                    toparty = toParty,
                    totag = toTag,
                    msgtype = "file",
                    agentid = agentId,
                    file = new
                    {
                        media_id = mediaId
                    },
                    safe = safe
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<MassResult>(accessToken, URL_FORMAT, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);
            
        }

        /// <summary>
        /// 【异步方法】发送图文消息【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="articles">图文信息内容，包括title（标题）、description（描述）、url（点击后跳转的链接。企业可根据url里面带的code参数校验员工的真实身份）和picurl（图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80。如不填，在客户端不显示图片）</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<MassResult> SendNewsAsync(string accessTokenOrAppKey, string agentId, List<Article> articles,
                string toUser = null, string toParty = null, string toTag = null, int safe = 0,
                int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new
                {
                    touser = toUser,
                    toparty = toParty,
                    totag = toTag,
                    msgtype = "news",
                    agentid = agentId,
                    news = new
                    {
                        articles = articles.Select(z => new
                        {
                            title = z.Title,
                            description = z.Description,
                            url = z.Url,
                            picurl = z.PicUrl//图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80
                        }).ToList()
                    }
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<MassResult>(accessToken, URL_FORMAT, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);
            
        }

        /// <summary>
        /// 【异步方法】发送mpnews消息【QY移植修改】
        /// 注：mpnews消息与news消息类似，不同的是图文消息内容存储在微信后台，并且支持保密选项。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="articles"></param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<MassResult> SendMpNewsAsync(string accessTokenOrAppKey, string toUser, string toParty, string toTag, string agentId, List<MpNewsArticle> articles, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new
                {
                    touser = toUser,
                    toparty = toParty,
                    totag = toTag,
                    msgtype = "mpnews",
                    agentid = agentId,
                    mpnews = new
                    {
                        articles = articles.Select(z => new
                        {
                            title = z.title,
                            thumb_media_id = z.thumb_media_id,
                            author = z.author,
                            content_source_url = z.content_source_url,
                            content = z.content,
                            digest = z.digest,
                            show_cover_pic = z.show_cover_pic
                        }).ToList(),
                    },
                    safe = safe
                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<MassResult>(accessToken, URL_FORMAT, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
            
        }
        #endregion
#endif
    }
}
