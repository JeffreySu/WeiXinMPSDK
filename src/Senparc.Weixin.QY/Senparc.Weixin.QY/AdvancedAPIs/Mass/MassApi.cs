/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：MailListApi.cs
    文件功能描述：发送消息接口
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
 
    修改标识：Senparc - 20150313
    修改描述：开放代理请求超时时间
----------------------------------------------------------------*/

/*
    官方文档：http://qydev.weixin.qq.com/wiki/index.php?title=%E5%8F%91%E9%80%81%E6%B6%88%E6%81%AF
 */

using System.Collections.Generic;
using System.Linq;
using Senparc.Weixin.QY.AdvancedAPIs.Mass;
using Senparc.Weixin.QY.CommonAPIs;
using Senparc.Weixin.QY.Entities;

namespace Senparc.Weixin.QY.AdvancedAPIs
{
    /// <summary>
    /// 发送消息
    /// </summary>
    public static class MassApi
    {
        private const string URL_FORMAT = "https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}";

        /// <summary>
        /// 发送文本信息
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="content">消息内容</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MassResult SendText(string accessToken, string toUser, string toParty, string toTag, string agentId, string content, int safe = 0, int timeOut = Config.TIME_OUT)
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
            return CommonJsonSend.Send<MassResult>(accessToken, URL_FORMAT, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag"></param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="mediaId">媒体资源文件ID</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MassResult SendImage(string accessToken, string toUser, string toParty, string toTag, string agentId, string mediaId, int safe = 0, int timeOut = Config.TIME_OUT)
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
            return CommonJsonSend.Send<MassResult>(accessToken, URL_FORMAT, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="mediaId">媒体资源文件ID</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MassResult SendVoice(string accessToken, string toUser, string toParty, string toTag, string agentId, string mediaId, int safe = 0, int timeOut = Config.TIME_OUT)
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
            return CommonJsonSend.Send<MassResult>(accessToken, URL_FORMAT, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 发送视频消息
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
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
        public static MassResult SendVideo(string accessToken, string toUser, string toParty, string toTag, string agentId, string mediaId, string title, string description, int safe = 0, int timeOut = Config.TIME_OUT)
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
            return CommonJsonSend.Send<MassResult>(accessToken, URL_FORMAT, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 发送文件消息
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="mediaId">媒体资源文件ID</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MassResult SendFile(string accessToken, string toUser, string toParty, string toTag, string agentId, string mediaId, int safe = 0, int timeOut = Config.TIME_OUT)
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
            return CommonJsonSend.Send<MassResult>(accessToken, URL_FORMAT, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="articles">图文信息内容，包括title（标题）、description（描述）、url（点击后跳转的链接。企业可根据url里面带的code参数校验员工的真实身份）和picurl（图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80。如不填，在客户端不显示图片）</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MassResult SendNews(string accessToken, string toUser, string toParty, string toTag, string agentId, List<Article> articles, int safe = 0, int timeOut = Config.TIME_OUT)
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
            return CommonJsonSend.Send<MassResult>(accessToken, URL_FORMAT, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 发送mpnews消息
        /// 注：mpnews消息与news消息类似，不同的是图文消息内容存储在微信后台，并且支持保密选项。
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="articles"></param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MassResult SendMpNews(string accessToken, string toUser, string toParty, string toTag, string agentId, List<MpNewsArticle> articles, int safe = 0, int timeOut = Config.TIME_OUT)
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
            return CommonJsonSend.Send<MassResult>(accessToken, URL_FORMAT, data, CommonJsonSendType.POST, timeOut);
        }
    }
}
