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

    创建标识：Senparc - 20161224
    创建描述：SendVideo方法添加thumb_media_id参数 感谢 @hello2008zj 

    修改标识：Senparc - 20170707
    修改描述：v14.5.1 完善异步方法async/await

    修改标识：Senparc - 20180928
    修改描述：增加GetTypingStatus

    修改标识：Senparc - 20190129
    修改描述：统一 CommonJsonSend.Send<T>() 方法请求接口

----------------------------------------------------------------*/

/* 
   API地址：http://mp.weixin.qq.com/wiki/1/70a29afed17f56d537c833f89be979c9.html
   新地址（2019年3月）：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140547
*/



using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.NeuChar;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.CommonAPIs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 客服接口
    /// </summary>
    public static class CustomApi
    {
        /// <summary>
        /// 客服消息统一请求地址格式
        /// </summary>
        public static readonly string UrlFormat = Config.ApiMpHost + "/cgi-bin/message/custom/send?access_token={0}";

        #region 同步方法

        /// <summary>
        /// 发送文本信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="content"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomApi.SendText", true)]
        public static WxJsonResult SendText(string accessTokenOrAppId, string openId, string content,
            int timeOut = Config.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
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
                return CommonJsonSend.Send(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomApi.SendImage", true)]
        public static WxJsonResult SendImage(string accessTokenOrAppId, string openId, string mediaId, int timeOut = Config.TIME_OUT, string kfAccount = "")
        {

            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
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
                return CommonJsonSend.Send(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomApi.SendVoice", true)]
        public static WxJsonResult SendVoice(string accessTokenOrAppId, string openId, string mediaId, int timeOut = Config.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
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

                return CommonJsonSend.Send(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发送视频消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <param name="thumb_media_id"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomApi.SendVideo", true)]
        public static WxJsonResult SendVideo(string accessTokenOrAppId, string openId, string mediaId, string title, string description, int timeOut = Config.TIME_OUT, string kfAccount = "", string thumb_media_id = "")
        {
            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
            {
                data = new
                {
                    touser = openId,
                    msgtype = "video",
                    video = new
                    {
                        media_id = mediaId,
                        thumb_media_id = thumb_media_id,
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
                        thumb_media_id = thumb_media_id,
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

                return CommonJsonSend.Send(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 发送音乐消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="title">音乐标题（非必须）</param>
        /// <param name="description">音乐描述（非必须）</param>
        /// <param name="musicUrl">音乐链接</param>
        /// <param name="hqMusicUrl">高品质音乐链接，wifi环境优先使用该链接播放音乐</param>
        /// <param name="thumbMediaId">视频缩略图的媒体ID</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomApi.SendMusic", true)]
        public static WxJsonResult SendMusic(string accessTokenOrAppId, string openId, string title, string description,
                                    string musicUrl, string hqMusicUrl, string thumbMediaId, int timeOut = Config.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
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
                return CommonJsonSend.Send(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发送图文消息（点击跳转到外链）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="articles"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomApi.SendNews", true)]
        public static WxJsonResult SendNews(string accessTokenOrAppId, string openId, List<Article> articles, int timeOut = Config.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
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
                return CommonJsonSend.Send(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发送图文消息（点击跳转到图文消息页面）
        /// 图文消息条数限制在8条以内，注意，如果图文数超过8，则将会无响应。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut"></param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomApi.SendMpNews", true)]
        public static WxJsonResult SendMpNews(string accessTokenOrAppId, string openId, string mediaId, int timeOut = Config.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
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

                return CommonJsonSend.Send(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发送卡券
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="cardId"></param>
        /// <param name="cardExt"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomApi.SendCard", true)]
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
                    TypesToIgnoreNull = new List<System.Type>() { typeof(CardExt) }
                };

                return CommonJsonSend.Send(accessToken, UrlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发送小程序卡片（要求小程序与公众号已关联）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="title">小程序卡片的标题</param>
        /// <param name="appid">小程序的appid，要求小程序的appid需要与公众号有关联关系</param>
        /// <param name="pagepath">小程序的页面路径，跟app.json对齐，支持参数，比如pages/index/index?foo=bar</param>
        /// <param name="thumb_media_id">小程序卡片图片的媒体ID，小程序卡片图片建议大小为520*416</param>
        /// <param name="kfAccount"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomApi.SendMiniProgramPage", true)]
        public static WxJsonResult SendMiniProgramPage(string accessTokenOrAppId, string openId, string title, string appid, string pagepath, string thumb_media_id, string kfAccount="", int timeOut = Config.TIME_OUT)
        {
            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
            {
                data = new
                {
                    touser = openId,
                    msgtype = "miniprogrampage",
                    miniprogrampage = new
                    {
                        title = title,
                        appid=appid,
                        pagepath= pagepath,
                        thumb_media_id = thumb_media_id
                    }
                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "miniprogrampage",
                    miniprogrampage = new
                    {
                        title = title,
                        appid = appid,
                        pagepath = pagepath,
                        thumb_media_id = thumb_media_id
                    },
                    customservice = new
                    {
                        kf_account = kfAccount
                    }

                };
            }

            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                return CommonJsonSend.Send(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 客服输入状态
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="touser">普通用户（openid）</param>
        /// <param name="typingStatus">"Typing"：对用户下发“正在输入"状态 "CancelTyping"：取消对用户的”正在输入"状态</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomApi.GetTypingStatus", true)]
        public static WxJsonResult GetTypingStatus(string accessTokenOrAppId, string touser, string typingStatus, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/message/custom/typing?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    touser = touser,
                    command = typingStatus
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 发送客户菜单消息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId">接受人员OPenid</param>
        /// <param name="head">标题</param>
        /// <param name="menuList">内容</param>
        /// <param name="tail">结尾内容</param>
        /// <param name="timeOut">超时时间</param>     
        /// <returns></returns>
        public static WxJsonResult SendMenu(string accessTokenOrAppId, string openId,
        string head, List<SendMenuContent> menuList, string tail,
         int timeOut = Config.TIME_OUT)
        {
            var data = new
            {
                touser = openId,
                msgtype = "msgmenu",
                msgmenu = new
                {
                    head_content = head,
                    list = menuList,
                    tail_content = tail
                }
            };
            return ApiHandlerWapper.TryCommonApi(accessToken =>
             {
                 return CommonJsonSend.Send(accessToken, UrlFormat, data, timeOut: timeOut);
             }, accessTokenOrAppId);
        }
        #endregion

        #region 异步方法

        /// <summary>
        /// 【异步方法】发送文本信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="content"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomApi.SendTextAsync", true)]
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

            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync(accessToken, UrlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }


        /// <summary>
        /// 【异步方法】发送图片消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomApi.SendImageAsync", true)]
        public static async Task<WxJsonResult> SendImageAsync(string accessTokenOrAppId, string openId, string mediaId, int timeOut = Config.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
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
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync(accessToken, UrlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】发送语音消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomApi.SendVoiceAsync", true)]
        public static async Task<WxJsonResult> SendVoiceAsync(string accessTokenOrAppId, string openId, string mediaId, int timeOut = Config.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
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
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync(accessToken, UrlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】发送视频消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <param name="thumb_media_id"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomApi.SendVideoAsync", true)]
        public static async Task<WxJsonResult> SendVideoAsync(string accessTokenOrAppId, string openId, string mediaId, string title, string description, int timeOut = Config.TIME_OUT, string kfAccount = "", string thumb_media_id = "")
        {
            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
            {
                data = new
                {
                    touser = openId,
                    msgtype = "video",
                    video = new
                    {
                        media_id = mediaId,
                        thumb_media_id = thumb_media_id,
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
                        thumb_media_id = thumb_media_id,
                        title = title,
                        description = description
                    },
                    CustomService = new
                    {
                        kf_account = kfAccount
                    }
                };
            }
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync(accessToken, UrlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】发送音乐消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="title">音乐标题（非必须）</param>
        /// <param name="description">音乐描述（非必须）</param>
        /// <param name="musicUrl">音乐链接</param>
        /// <param name="hqMusicUrl">高品质音乐链接，wifi环境优先使用该链接播放音乐</param>
        /// <param name="thumbMediaId">视频缩略图的媒体ID</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomApi.SendMusicAsync", true)]
        public static async Task<WxJsonResult> SendMusicAsync(string accessTokenOrAppId, string openId, string title, string description,
                                    string musicUrl, string hqMusicUrl, string thumbMediaId, int timeOut = Config.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
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
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync(accessToken, UrlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】发送图文消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="articles"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomApi.SendNewsAsync", true)]
        public static async Task<WxJsonResult> SendNewsAsync(string accessTokenOrAppId, string openId, List<Article> articles, int timeOut = Config.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
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
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync(accessToken, UrlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }


        /// <summary>
        /// 【异步方法】发送图文消息（点击跳转到图文消息页面）
        /// 图文消息条数限制在8条以内，注意，如果图文数超过8，则将会无响应。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut"></param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomApi.SendMpNewsAsync", true)]
        public static async Task<WxJsonResult> SendMpNewsAsync(string accessTokenOrAppId, string openId, string mediaId, int timeOut = Config.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
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
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync(accessToken, UrlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】发送卡券
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="cardId"></param>
        /// <param name="cardExt"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomApi.SendCardAsync", true)]
        public static async Task<WxJsonResult> SendCardAsync(string accessTokenOrAppId, string openId, string cardId, CardExt cardExt, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
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
                    TypesToIgnoreNull = new List<System.Type>() { typeof(CardExt) }
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync(accessToken, UrlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】发送小程序卡片（要求小程序与公众号已关联）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="title">小程序卡片的标题</param>
        /// <param name="appid">小程序的appid，要求小程序的appid需要与公众号有关联关系</param>
        /// <param name="pagepath">小程序的页面路径，跟app.json对齐，支持参数，比如pages/index/index?foo=bar</param>
        /// <param name="thumb_media_id">小程序卡片图片的媒体ID，小程序卡片图片建议大小为520*416</param>
        /// <param name="kfAccount"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomApi.SendMiniProgramPageAsync", true)]
        public static async Task<WxJsonResult> SendMiniProgramPageAsync(string accessTokenOrAppId, string openId, string title, string appid, string pagepath, string thumb_media_id, string kfAccount = "", int timeOut = Config.TIME_OUT)
        {

            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
            {
                data = new
                {
                    touser = openId,
                    msgtype = "miniprogrampage",
                    miniprogrampage = new
                    {
                        title = title,
                        appid = appid,
                        pagepath = pagepath,
                        thumb_media_id = thumb_media_id
                    }
                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "miniprogrampage",
                    miniprogrampage = new
                    {
                        title = title,
                        appid = appid,
                        pagepath = pagepath,
                        thumb_media_id = thumb_media_id
                    },
                    customservice = new
                    {
                        kf_account = kfAccount
                    }

                };
            }

            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {

                return await CommonJsonSend.SendAsync(accessToken, UrlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }


        /// <summary>
        /// 【异步方法】客服输入状态
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="touser">普通用户（openid）</param>
        /// <param name="typingStatus">"Typing"：对用户下发“正在输入"状态 "CancelTyping"：取消对用户的”正在输入"状态</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomApi.TypingAsync", true)]
        public static async Task<WxJsonResult> GetTypingStatusAsync(string accessTokenOrAppId, string touser, string typingStatus, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/message/custom/typing?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    touser = touser,
                    command = typingStatus
                };

                return await Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】发送客户菜单消息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId">接受人员OPenid</param>
        /// <param name="head">标题</param>
        /// <param name="menuList">内容</param>
        /// <param name="tail">结尾内容</param>
        /// <param name="timeOut">超时时间</param>     
        /// <returns></returns>
        public static async Task<WxJsonResult> SendMenuAsync(string accessTokenOrAppId, string openId,
       string head, List<SendMenuContent> menuList, string tail,
        int timeOut = Config.TIME_OUT)
        {
            var data = new
            {
                touser = openId,
                msgtype = "msgmenu",
                msgmenu = new
                {
                    head_content = head,
                    list = menuList,
                    tail_content = tail
                }
            };
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
              {
                  return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync(accessToken, UrlFormat, data, timeOut: timeOut).ConfigureAwait(false);

              }, accessTokenOrAppId).ConfigureAwait(false);

        }

        #endregion

        /////
        ///// 发送卡券 查看card_ext字段详情及签名规则，特别注意客服消息接口投放卡券仅支持非自定义Code码的卡券。 
        /////

    }


}