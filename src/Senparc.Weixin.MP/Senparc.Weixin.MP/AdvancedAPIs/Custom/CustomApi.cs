/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：CustomAPI.cs
    文件功能描述：客服接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
 
    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间
 
    修改标识：Senparc - 20160718
    修改描述：增加其接口的异步方法
 
    修改标识：Senparc - 20160722
    修改描述：将其SendText方法增加了kfAccount的参数
   
    修改标识：Senparc - 20160802
    修改描述：将其Send方法增加了kfAccount的参数
 
    创建标识：Senparc - 20160808
    创建描述：增加SendCard
----------------------------------------------------------------*/

/* 
   API地址：http://mp.weixin.qq.com/wiki/1/70a29afed17f56d537c833f89be979c9.html
*/

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 客服接口
    /// </summary>
    public static class CustomApi
    {
        private const string URL_FORMAT = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}";

        #region 同步请求

        /// <summary>
        /// 发送文本信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="content"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        public static WxJsonResult SendText(string accessTokenOrAppId, string openId, string content,
            int timeOut = Config.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (string.IsNullOrWhiteSpace(kfAccount))
            {
                data = new
                 {
                     touser = openId,
                     msgtype = "text",
                     text = new
                     {
                         content = content
                     }
                 };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "text",
                    text = new
                    {
                        content = content
                    },
                    customservice = new
                    {
                        kf_account = kfAccount
                    }

                };
            }

            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                return CommonJsonSend.Send(accessToken, URL_FORMAT, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        public static WxJsonResult SendImage(string accessTokenOrAppId, string openId, string mediaId, int timeOut = Config.TIME_OUT, string kfAccount = "")
        {

            object data = null;
            if (string.IsNullOrWhiteSpace(kfAccount))
            {
                data = new
                {
                    touser = openId,
                    msgtype = "image",
                    image = new
                    {
                        media_id = mediaId
                    }

                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "image",
                    image = new
                    {
                        media_id = mediaId
                    },
                    CustomService = new
                    {
                        kf_account = kfAccount
                    }
                };
            }
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                return CommonJsonSend.Send(accessToken, URL_FORMAT, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount"></param>
        /// <returns></returns>
        public static WxJsonResult SendVoice(string accessTokenOrAppId, string openId, string mediaId, int timeOut = Config.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (string.IsNullOrWhiteSpace(kfAccount))
            {

                data = new
                {
                    touser = openId,
                    msgtype = "voice",
                    voice = new
                    {
                        media_id = mediaId
                    }

                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "voice",
                    voice = new
                    {
                        media_id = mediaId
                    },
                    CustomService = new
                    {
                        kf_account = kfAccount
                    }
                };
            }
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {

                return CommonJsonSend.Send(accessToken, URL_FORMAT, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发送视频消息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        public static WxJsonResult SendVideo(string accessTokenOrAppId, string openId, string mediaId, string title, string description, int timeOut = Config.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (string.IsNullOrWhiteSpace(kfAccount))
            {
                data = new
                {
                    touser = openId,
                    msgtype = "video",
                    video = new
                    {
                        media_id = mediaId,
                        title = title,
                        description = description
                    }
                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "video",
                    video = new
                    {
                        media_id = mediaId,
                        title = title,
                        description = description
                    },
                    CustomService = new
                    {
                        kf_account = kfAccount
                    }
                };
            }
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {

                return CommonJsonSend.Send(accessToken, URL_FORMAT, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 发送音乐消息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="title">音乐标题（非必须）</param>
        /// <param name="description">音乐描述（非必须）</param>
        /// <param name="musicUrl">音乐链接</param>
        /// <param name="hqMusicUrl">高品质音乐链接，wifi环境优先使用该链接播放音乐</param>
        /// <param name="thumbMediaId">视频缩略图的媒体ID</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        public static WxJsonResult SendMusic(string accessTokenOrAppId, string openId, string title, string description,
                                    string musicUrl, string hqMusicUrl, string thumbMediaId, int timeOut = Config.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (string.IsNullOrWhiteSpace(kfAccount))
            {
                data = new
                {
                    touser = openId,
                    msgtype = "music",
                    music = new
                    {
                        title = title,
                        description = description,
                        musicurl = musicUrl,
                        hqmusicurl = hqMusicUrl,
                        thumb_media_id = thumbMediaId
                    }

                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "music",
                    music = new
                    {
                        title = title,
                        description = description,
                        musicurl = musicUrl,
                        hqmusicurl = hqMusicUrl,
                        thumb_media_id = thumbMediaId
                    },
                    CustomService = new
                    {
                        kf_account = kfAccount
                    }

                };
            }
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                return CommonJsonSend.Send(accessToken, URL_FORMAT, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="articles"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        public static WxJsonResult SendNews(string accessTokenOrAppId, string openId, List<Article> articles, int timeOut = Config.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (string.IsNullOrWhiteSpace(kfAccount))
            {
                data = new
                {
                    touser = openId,
                    msgtype = "news",
                    news = new
                    {
                        articles = articles.Select(z => new
                        {
                            title = z.Title,
                            description = z.Description,
                            url = z.Url,
                            picurl = z.PicUrl //图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80
                        }).ToList()
                    }
                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "news",
                    news = new
                    {
                        articles = articles.Select(z => new
                        {
                            title = z.Title,
                            description = z.Description,
                            url = z.Url,
                            picurl = z.PicUrl//图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80
                        }).ToList()
                    },
                    CustomService = new
                    {
                        kf_account = kfAccount
                    }

                };
            }
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                return CommonJsonSend.Send(accessToken, URL_FORMAT, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发送图文消息（点击跳转到图文消息页面）
        /// 图文消息条数限制在8条以内，注意，如果图文数超过8，则将会无响应。
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut"></param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        public static WxJsonResult SendMpNews(string accessTokenOrAppId, string openId, string mediaId, int timeOut = Config.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (string.IsNullOrWhiteSpace(kfAccount))
            {
                data = new
                {
                    touser = openId,
                    msgtype = "mpnews",
                    mpnews = new
                    {
                        media_id = mediaId
                    }
                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "mpnews",
                    mpnews = new
                    {
                        media_id = mediaId
                    },
                    CustomService = new
                    {
                        kf_account = kfAccount
                    }
                };
            }
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {

                return CommonJsonSend.Send(accessToken, URL_FORMAT, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        #endregion

        #region 异步请求

        /// <summary>
        /// 【异步方法】发送文本信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="content"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> SendTextAsync(string accessTokenOrAppId, string openId, string content, int timeOut = Config.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (string.IsNullOrEmpty(kfAccount))
            {
                data = new
                {
                    touser = openId,
                    msgtype = "text",
                    text = new
                    {
                        content = content
                    }

                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "text",
                    text = new
                    {
                        content = content
                    },
                    customservice = new
                    {
                        kf_account = kfAccount
                    }

                };
            }

            return await ApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync(accessToken, URL_FORMAT, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 【异步方法】发送图片消息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> SendImageAsync(string accessTokenOrAppId, string openId, string mediaId, int timeOut = Config.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (string.IsNullOrWhiteSpace(kfAccount))
            {
                data = new
                {
                    touser = openId,
                    msgtype = "image",
                    image = new
                    {
                        media_id = mediaId
                    }
                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "image",
                    image = new
                    {
                        media_id = mediaId
                    },
                    CustomService = new
                    {
                        kf_account = kfAccount
                    }
                };
            }
            return await ApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync(accessToken, URL_FORMAT, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】发送语音消息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> SendVoiceAsync(string accessTokenOrAppId, string openId, string mediaId, int timeOut = Config.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (string.IsNullOrWhiteSpace(kfAccount))
            {
                data = new
                {
                    touser = openId,
                    msgtype = "voice",
                    voice = new
                    {
                        media_id = mediaId
                    }
                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "voice",
                    voice = new
                    {
                        media_id = mediaId
                    },
                    CustomService = new
                    {
                        kf_account = kfAccount
                    }
                };
            }
            return await ApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync(accessToken, URL_FORMAT, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】发送视频消息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> SendVideoAsync(string accessTokenOrAppId, string openId, string mediaId, string title, string description, int timeOut = Config.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (string.IsNullOrWhiteSpace(kfAccount))
            {
                data = new
                {
                    touser = openId,
                    msgtype = "video",
                    video = new
                    {
                        media_id = mediaId,
                        title = title,
                        description = description
                    }
                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "video",
                    video = new
                    {
                        media_id = mediaId,
                        title = title,
                        description = description
                    },
                    CustomService = new
                    {
                        kf_account = kfAccount
                    }
                };
            }
            return await ApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync(accessToken, URL_FORMAT, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 【异步方法】发送音乐消息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="title">音乐标题（非必须）</param>
        /// <param name="description">音乐描述（非必须）</param>
        /// <param name="musicUrl">音乐链接</param>
        /// <param name="hqMusicUrl">高品质音乐链接，wifi环境优先使用该链接播放音乐</param>
        /// <param name="thumbMediaId">视频缩略图的媒体ID</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> SendMusicAsync(string accessTokenOrAppId, string openId, string title, string description,
                                    string musicUrl, string hqMusicUrl, string thumbMediaId, int timeOut = Config.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (string.IsNullOrWhiteSpace(kfAccount))
            {
                data = new
                {
                    touser = openId,
                    msgtype = "music",
                    music = new
                    {
                        title = title,
                        description = description,
                        musicurl = musicUrl,
                        hqmusicurl = hqMusicUrl,
                        thumb_media_id = thumbMediaId
                    }

                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "music",
                    music = new
                    {
                        title = title,
                        description = description,
                        musicurl = musicUrl,
                        hqmusicurl = hqMusicUrl,
                        thumb_media_id = thumbMediaId
                    },
                    CustomService = new
                    {
                        kf_account = kfAccount
                    }

                };
            }
            return await ApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync(accessToken, URL_FORMAT, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】发送图文消息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="articles"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> SendNewsAsync(string accessTokenOrAppId, string openId, List<Article> articles, int timeOut = Config.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (string.IsNullOrWhiteSpace(kfAccount))
            {
                data = new
                {
                    touser = openId,
                    msgtype = "news",
                    news = new
                    {
                        articles = articles.Select(z => new
                        {
                            title = z.Title,
                            description = z.Description,
                            url = z.Url,
                            picurl = z.PicUrl //图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80
                        }).ToList()
                    }

                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "news",
                    news = new
                    {
                        articles = articles.Select(z => new
                        {
                            title = z.Title,
                            description = z.Description,
                            url = z.Url,
                            picurl = z.PicUrl//图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80
                        }).ToList()
                    },
                    CustomService = new
                    {
                        kf_account = kfAccount
                    }

                };
            }
            return await ApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync(accessToken, URL_FORMAT, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 【异步方法】发送图文消息（点击跳转到图文消息页面）
        /// 图文消息条数限制在8条以内，注意，如果图文数超过8，则将会无响应。
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut"></param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> SendMpNewsAsync(string accessTokenOrAppId, string openId, string mediaId, int timeOut = Config.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (string.IsNullOrWhiteSpace(kfAccount))
            {
                data = new
                {
                    touser = openId,
                    msgtype = "mpnews",
                    mpnews = new
                    {
                        media_id = mediaId
                    }
                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "mpnews",
                    mpnews = new
                    {
                        media_id = mediaId
                    },
                    CustomService = new
                    {
                        kf_account = kfAccount
                    }
                };
            }
            return await ApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync(accessToken, URL_FORMAT, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        #endregion

        ///
        /// 发送卡券 查看card_ext字段详情及签名规则，特别注意客服消息接口投放卡券仅支持非自定义Code码的卡券。 
        ///
        public static WxJsonResult SendCard(string accessTokenOrAppId, string openId, string cardId, CardExt cardExt, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    touser = openId,
                    msgtype = "wxcard",
                    wxcard = new
                    {
                        card_id = cardId,
                        card_ext = cardExt
                    }
                };
                JsonSetting jsonSetting = new JsonSetting()
                {
                    TypesToIgnore = new List<System.Type>() { typeof(CardExt) }
                };

                return CommonJsonSend.Send(accessToken, URL_FORMAT, data, timeOut: timeOut, jsonSetting: jsonSetting);

            }, accessTokenOrAppId);
        }
    }
}