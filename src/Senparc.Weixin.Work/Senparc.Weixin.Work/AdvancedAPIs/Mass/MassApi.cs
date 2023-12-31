#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright(C) 2023 Senparc
    
    文件名：MailListApi.cs
    文件功能描述：发送消息接口
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
 
    修改标识：Senparc - 20150313
    修改描述：开放代理请求超时时间
 
    修改标识：Senparc - 20160720
    修改描述：增加其接口的异步方法
             
    修改标识：Senparc - 20170709
    修改描述：v0.3.2 修复Senparc.Weixin.QY.AdvancedAPIs.MassApi中，因为accessToken为null而导致消息发送失败的问题 

    修改标识：Senparc - 20170712
    修改描述：MP 14.5.1 AccessToken HandlerWaper改造

    修改标识：lishewen - 20190427
    修改描述：v3.7.502.1 “更新任务卡片”接口更新返回类型

    修改标识：Senparc - 20210120
    修改描述：v3.8.102 “发送应用信息”接口添加 enable_duplicate_check、duplicate_check_interval 参数

    修改标识：Senparc - 20210120
    修改描述：v3.15.8 支持“发送应用消息”的“模板卡片消息”类型（MassApi.SendTemplateCard() 方法）

    修改标识：Senparc - 20230226
    修改描述：v3.15.16 添加企业微信应用“消息撤回”（PR #2793）

    修改标识：Senparc - 20230612
    修改描述：v3.15.20 增加更新模版卡片消息（PR #2850）

----------------------------------------------------------------*/

/*
    官方文档：
    http://qydev.weixin.qq.com/wiki/index.php?title=%E5%8F%91%E9%80%81%E6%B6%88%E6%81%AF
    https://open.work.weixin.qq.com/api/doc/90001/90143/90372
 */

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.NeuChar;
using Senparc.NeuChar.Entities;
using Senparc.NeuChar.Helpers;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.Work.AdvancedAPIs.Mass;
using Senparc.Weixin.Work.AdvancedAPIs.Mass.SendTemplateCard;
using Senparc.Weixin.Work.AdvancedAPIs.Mass.UpdateTemplateCard;
using Senparc.Weixin.Work.CommonAPIs;
using Senparc.Weixin.Work.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 发送消息
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Work, true)]
    public static class MassApi
    {
        private static string _urlFormat = Config.ApiWorkHost + "/cgi-bin/message/send?access_token={0}";
        private static string _updateTemplateCardFormat = Config.ApiWorkHost + "/cgi-bin/message/update_template_card?access_token={0}";
        private static string _taskUrlFormat = Config.ApiWorkHost + "/cgi-bin/message/update_taskcard?access_token={0}";
        private static string _recallUrlFormat = Config.ApiWorkHost + "/cgi-bin/message/recall?access_token={0}";

        #region 同步方法


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
        /// <param name="enableDuplicateCheck">表示是否开启重复消息检查，0表示否，1表示是，默认为0</param>
        /// <param name="duplicateCheckInterval">表示是否重复消息检查的时间间隔，默认1800s，最大不超过4小时</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MassResult SendText(string accessTokenOrAppKey, string agentId, string content,
            string toUser = null, string toParty = null, string toTag = null, int safe = 0,
            int enableDuplicateCheck = 0,
            int duplicateCheckInterval = 1800,
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
                    safe = safe,
                    enable_duplicate_check = enableDuplicateCheck,
                    duplicate_check_interval = duplicateCheckInterval
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<MassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
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
        /// <param name="enableDuplicateCheck">表示是否开启重复消息检查，0表示否，1表示是，默认为0</param>
        /// <param name="duplicateCheckInterval">表示是否重复消息检查的时间间隔，默认1800s，最大不超过4小时</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MassResult SendImage(string accessTokenOrAppKey, string agentId, string mediaId,
                        string toUser = null, string toParty = null, string toTag = null, int safe = 0,
                        int enableDuplicateCheck = 0,
                        int duplicateCheckInterval = 1800,
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
                    safe = safe,
                    enable_duplicate_check = enableDuplicateCheck,
                    duplicate_check_interval = duplicateCheckInterval
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<MassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
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
        /// <param name="enableDuplicateCheck">表示是否开启重复消息检查，0表示否，1表示是，默认为0</param>
        /// <param name="duplicateCheckInterval">表示是否重复消息检查的时间间隔，默认1800s，最大不超过4小时</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MassResult SendVoice(string accessTokenOrAppKey, string agentId, string mediaId,
                string toUser = null, string toParty = null, string toTag = null, int safe = 0,
                int enableDuplicateCheck = 0,
                int duplicateCheckInterval = 1800,
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
                    safe = safe,
                    enable_duplicate_check = enableDuplicateCheck,
                    duplicate_check_interval = duplicateCheckInterval
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<MassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
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
        /// <param name="enableDuplicateCheck">表示是否开启重复消息检查，0表示否，1表示是，默认为0</param>
        /// <param name="duplicateCheckInterval">表示是否重复消息检查的时间间隔，默认1800s，最大不超过4小时</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MassResult SendVideo(string accessTokenOrAppKey, string agentId, string mediaId,
            string toUser = null, string toParty = null, string toTag = null,
            string title = null, string description = null, int safe = 0,
            int enableDuplicateCheck = 0,
            int duplicateCheckInterval = 1800,
            int timeOut = Config.TIME_OUT)
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
                    safe = safe,
                    enable_duplicate_check = enableDuplicateCheck,
                    duplicate_check_interval = duplicateCheckInterval
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<MassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
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
        /// <param name="enableDuplicateCheck">表示是否开启重复消息检查，0表示否，1表示是，默认为0</param>
        /// <param name="duplicateCheckInterval">表示是否重复消息检查的时间间隔，默认1800s，最大不超过4小时</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MassResult SendFile(string accessTokenOrAppKey, string agentId, string mediaId,
                string toUser = null, string toParty = null, string toTag = null, int safe = 0,
                int enableDuplicateCheck = 0,
                int duplicateCheckInterval = 1800,
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
                    safe = safe,
                    enable_duplicate_check = enableDuplicateCheck,
                    duplicate_check_interval = duplicateCheckInterval
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<MassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
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
        /// <param name="enableDuplicateCheck">表示是否开启重复消息检查，0表示否，1表示是，默认为0</param>
        /// <param name="duplicateCheckInterval">表示是否重复消息检查的时间间隔，默认1800s，最大不超过4小时</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MassResult SendNews(string accessTokenOrAppKey, string agentId, List<Article> articles,
                string toUser = null, string toParty = null, string toTag = null, int safe = 0,
                int enableDuplicateCheck = 0,
                int duplicateCheckInterval = 1800,
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
                    },
                    enable_duplicate_check = enableDuplicateCheck,
                    duplicate_check_interval = duplicateCheckInterval
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<MassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
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
        /// <param name="enableDuplicateCheck">表示是否开启重复消息检查，0表示否，1表示是，默认为0</param>
        /// <param name="duplicateCheckInterval">表示是否重复消息检查的时间间隔，默认1800s，最大不超过4小时</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MassResult SendMpNews(string accessTokenOrAppKey, string agentId, List<MpNewsArticle> articles,
            string toUser = null, string toParty = null, string toTag = null, int safe = 0,
            int enableDuplicateCheck = 0,
            int duplicateCheckInterval = 1800,
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
                    safe = safe,
                    enable_duplicate_check = enableDuplicateCheck,
                    duplicate_check_interval = duplicateCheckInterval
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<MassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 发送textcard消息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="title">标题，不超过128个字节，超过会自动截断</param>
        /// <param name="description">描述，不超过512个字节，超过会自动截断</param>
        /// <param name="url">点击后跳转的链接</param>
        /// <param name="btntxt">按钮文字。 默认为“详情”， 不超过4个文字，超过自动截断。</param>
        /// <param name="enableDuplicateCheck">表示是否开启重复消息检查，0表示否，1表示是，默认为0</param>
        /// <param name="duplicateCheckInterval">表示是否重复消息检查的时间间隔，默认1800s，最大不超过4小时</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MassResult SendTextCard(string accessTokenOrAppKey, string agentId, string title, string description, string url, string btntxt = null, string toUser = null, string toParty = null, string toTag = null,
                int enableDuplicateCheck = 0,
                int duplicateCheckInterval = 1800,
                int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    touser = toUser,
                    toparty = toParty,
                    totag = toTag,
                    msgtype = "textcard",
                    agentid = agentId,
                    textcard = new
                    {
                        title = title,
                        description = description,
                        url = url,
                        btntxt = btntxt
                    },
                    enable_duplicate_check = enableDuplicateCheck,
                    duplicate_check_interval = duplicateCheckInterval
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<MassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 发送textcard消息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="content">markdown内容，最长不超过2048个字节，必须是utf8编码</param>
        /// <param name="enableDuplicateCheck">表示是否开启重复消息检查，0表示否，1表示是，默认为0</param>
        /// <param name="duplicateCheckInterval">表示是否重复消息检查的时间间隔，默认1800s，最大不超过4小时</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MassResult SendMarkdown(string accessTokenOrAppKey, string agentId, string content,
                string toUser = null, string toParty = null, string toTag = null,
                int enableDuplicateCheck = 0,
                int duplicateCheckInterval = 1800,
                int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    touser = toUser,
                    toparty = toParty,
                    totag = toTag,
                    msgtype = "markdown",
                    agentid = agentId,
                    markdown = new
                    {
                        content
                    },
                    enable_duplicate_check = enableDuplicateCheck,
                    duplicate_check_interval = duplicateCheckInterval
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<MassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 发送小程序通知消息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static MassResult SendMiniNoticeCard(string accessTokenOrAppKey, SendMiniProgramNoticeData data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<MassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 发送任务卡片消息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static MassResult SendTaskCard(string accessTokenOrAppKey, SendTaskCardNoticeData data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<MassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);
        }


        #region 模板卡片消息

        /// <summary>
        /// 发送模板卡片消息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static MassResult SendTemplateCard(string accessTokenOrAppKey, SendTemplateCardRequest data, int timeOut = Config.TIME_OUT)
        {
            //var ss = new Mass.SendTemplateCard.SendTemplateCardRequest<Template_Card_NewsNotice>(new());

            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<MassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 更新模版卡片消息
        /// https://developer.work.weixin.qq.com/document/path/94888
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static MassResult UpdateTemplateCard(string accessTokenOrAppKey, UpdateTemplateCardRequest data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                JsonSetting jsonSetting = new JsonSetting(true);
                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<MassResult>(accessToken, _updateTemplateCardFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);
        }

        #endregion

        /// <summary>
        /// 更新任务卡片消息状态
        /// <para>文档：https://developer.work.weixin.qq.com/document/path/90854</para>
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static UpdateTaskCardResultJson UpdateTaskCard(string accessTokenOrAppKey, UpdateTaskCardData data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<UpdateTaskCardResultJson>(accessToken, _taskUrlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 撤回应用消息
        /// <para>文档：https://developer.work.weixin.qq.com/document/path/94867</para>
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult Recall(string accessTokenOrAppKey, string msgId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var jsonSetting = new JsonSetting(true);
                var data = new
                {
                    msgid = msgId
                };

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<WorkJsonResult>(accessToken, _recallUrlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);
        }

        #endregion

        #region 异步方法
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
        /// <param name="enableDuplicateCheck">表示是否开启重复消息检查，0表示否，1表示是，默认为0</param>
        /// <param name="duplicateCheckInterval">表示是否重复消息检查的时间间隔，默认1800s，最大不超过4小时</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="limitedBytes">最大允许发送限制，如果超出限制，则分多条发送</param>
        /// <returns></returns>
        public static async Task<MassResult> SendTextAsync(string accessTokenOrAppKey, string agentId, string content, string toUser = null, string toParty = null, string toTag = null, int safe = 0,
                int enableDuplicateCheck = 0,
                int duplicateCheckInterval = 1800,
                int timeOut = Config.TIME_OUT, int limitedBytes = 2048)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                //尝试超长内容发送
                var trySendResult = await MessageHandlerHelper.TrySendLimistedText(accessTokenOrAppKey,
                    content, limitedBytes,
                    c => SendTextAsync(accessTokenOrAppKey, agentId, c, toUser, toParty, toTag, safe,
                    enableDuplicateCheck, duplicateCheckInterval, timeOut, limitedBytes));

                if (trySendResult != null)
                {
                    return trySendResult;
                }

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
                    safe = safe,
                    enable_duplicate_check = enableDuplicateCheck,
                    duplicate_check_interval = duplicateCheckInterval
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<MassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);


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
        /// <param name="enableDuplicateCheck">表示是否开启重复消息检查，0表示否，1表示是，默认为0</param>
        /// <param name="duplicateCheckInterval">表示是否重复消息检查的时间间隔，默认1800s，最大不超过4小时</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<MassResult> SendImageAsync(string accessTokenOrAppKey, string agentId, string mediaId, string toUser = null, string toParty = null, string toTag = null, int safe = 0,
                int enableDuplicateCheck = 0,
                int duplicateCheckInterval = 1800,
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
                    safe = safe,
                    enable_duplicate_check = enableDuplicateCheck,
                    duplicate_check_interval = duplicateCheckInterval
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<MassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);


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
        /// <param name="enableDuplicateCheck">表示是否开启重复消息检查，0表示否，1表示是，默认为0</param>
        /// <param name="duplicateCheckInterval">表示是否重复消息检查的时间间隔，默认1800s，最大不超过4小时</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<MassResult> SendVoiceAsync(string accessTokenOrAppKey, string agentId, string mediaId,
                        string toUser = null, string toParty = null, string toTag = null, int safe = 0,
                        int enableDuplicateCheck = 0,
                        int duplicateCheckInterval = 1800,
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
                    safe = safe,
                    enable_duplicate_check = enableDuplicateCheck,
                    duplicate_check_interval = duplicateCheckInterval
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<MassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);


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
        /// <param name="enableDuplicateCheck">表示是否开启重复消息检查，0表示否，1表示是，默认为0</param>
        /// <param name="duplicateCheckInterval">表示是否重复消息检查的时间间隔，默认1800s，最大不超过4小时</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<MassResult> SendVideoAsync(string accessTokenOrAppKey, string agentId, string mediaId,
                string toUser = null, string toParty = null, string toTag = null,
                string title = null, string description = null, int safe = 0,
                int enableDuplicateCheck = 0,
                int duplicateCheckInterval = 1800,
                int timeOut = Config.TIME_OUT)
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
                    safe = safe,
                    enable_duplicate_check = enableDuplicateCheck,
                    duplicate_check_interval = duplicateCheckInterval
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<MassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);


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
        /// <param name="enableDuplicateCheck">表示是否开启重复消息检查，0表示否，1表示是，默认为0</param>
        /// <param name="duplicateCheckInterval">表示是否重复消息检查的时间间隔，默认1800s，最大不超过4小时</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<MassResult> SendFileAsync(string accessTokenOrAppKey, string agentId, string mediaId,
                string toUser = null, string toParty = null, string toTag = null, int safe = 0,
                int enableDuplicateCheck = 0,
                int duplicateCheckInterval = 1800,
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
                    safe = safe,
                    enable_duplicate_check = enableDuplicateCheck,
                    duplicate_check_interval = duplicateCheckInterval
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<MassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);


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
        /// <param name="enableDuplicateCheck">表示是否开启重复消息检查，0表示否，1表示是，默认为0</param>
        /// <param name="duplicateCheckInterval">表示是否重复消息检查的时间间隔，默认1800s，最大不超过4小时</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<MassResult> SendNewsAsync(string accessTokenOrAppKey, string agentId, List<Article> articles,
                string toUser = null, string toParty = null, string toTag = null, int safe = 0,
                int enableDuplicateCheck = 0,
                int duplicateCheckInterval = 1800,
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
                    },
                    enable_duplicate_check = enableDuplicateCheck,
                    duplicate_check_interval = duplicateCheckInterval
                };

                var jsonSetting = new JsonSetting(true);
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<MassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);


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
        /// <param name="enableDuplicateCheck">表示是否开启重复消息检查，0表示否，1表示是，默认为0</param>
        /// <param name="duplicateCheckInterval">表示是否重复消息检查的时间间隔，默认1800s，最大不超过4小时</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<MassResult> SendMpNewsAsync(string accessTokenOrAppKey, string toUser, string toParty, string toTag, string agentId, List<MpNewsArticle> articles, int safe = 0,
                int enableDuplicateCheck = 0,
                int duplicateCheckInterval = 1800,
                int timeOut = Config.TIME_OUT)
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
                    safe = safe,
                    enable_duplicate_check = enableDuplicateCheck,
                    duplicate_check_interval = duplicateCheckInterval
                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<MassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);


        }

        /// <summary>
        /// 【异步方法】发送textcard消息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="title">标题，不超过128个字节，超过会自动截断</param>
        /// <param name="description">描述，不超过512个字节，超过会自动截断</param>
        /// <param name="url">点击后跳转的链接</param>
        /// <param name="btntxt">按钮文字。 默认为“详情”， 不超过4个文字，超过自动截断。</param>
        /// <param name="enableDuplicateCheck">表示是否开启重复消息检查，0表示否，1表示是，默认为0</param>
        /// <param name="duplicateCheckInterval">表示是否重复消息检查的时间间隔，默认1800s，最大不超过4小时</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<MassResult> SendTextCardAsync(string accessTokenOrAppKey, string agentId, string title, string description, string url, string btntxt = null,
            string toUser = null, string toParty = null, string toTag = null,
                int enableDuplicateCheck = 0,
                int duplicateCheckInterval = 1800, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new
                {
                    touser = toUser,
                    toparty = toParty,
                    totag = toTag,
                    msgtype = "textcard",
                    agentid = agentId,
                    textcard = new
                    {
                        title = title,
                        description = description,
                        url = url,
                        btntxt = btntxt
                    },
                    enable_duplicate_check = enableDuplicateCheck,
                    duplicate_check_interval = duplicateCheckInterval
                };

                var jsonSetting = new JsonSetting(true);

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<MassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】发送Markdown消息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="content">markdown内容，最长不超过2048个字节，必须是utf8编码</param>
        /// <param name="enableDuplicateCheck">表示是否开启重复消息检查，0表示否，1表示是，默认为0</param>
        /// <param name="duplicateCheckInterval">表示是否重复消息检查的时间间隔，默认1800s，最大不超过4小时</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<MassResult> SendMarkdownAsync(string accessTokenOrAppKey, string agentId, string content,
            string toUser = null, string toParty = null, string toTag = null,
                int enableDuplicateCheck = 0,
                int duplicateCheckInterval = 1800, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new
                {
                    touser = toUser,
                    toparty = toParty,
                    totag = toTag,
                    msgtype = "markdown",
                    agentid = agentId,
                    markdown = new
                    {
                        content
                    },
                    enable_duplicate_check = enableDuplicateCheck,
                    duplicate_check_interval = duplicateCheckInterval
                };

                var jsonSetting = new JsonSetting(true);

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<MassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】发送小程序通知消息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<MassResult> SendMiniNoticeCardAsync(string accessTokenOrAppKey, SendMiniProgramNoticeData data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var jsonSetting = new JsonSetting(true);
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<MassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】发送任务卡片消息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<MassResult> SendTaskCardAsync(string accessTokenOrAppKey, SendTaskCardNoticeData data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var jsonSetting = new JsonSetting(true);
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<MassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        #region 模板卡片消息

        /// <summary>
        /// 【异步方法】发送模板卡片消息
        /// <para>文档：https://developer.work.weixin.qq.com/document/path/90854</para>
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<MassResult> SendTemplateCardAsync(string accessTokenOrAppKey, SendTemplateCardRequest data, int timeOut = Config.TIME_OUT)
        {
            //var ss = new Mass.SendTemplateCard.SendTemplateCardRequest<Template_Card_NewsNotice>(new());

            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                JsonSetting jsonSetting = new JsonSetting(true);

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<MassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 【异步方法】更新模版卡片消息
        /// https://developer.work.weixin.qq.com/document/path/94888
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<MassResult> UpdateTemplateCardAsync(string accessTokenOrAppKey, UpdateTemplateCardRequest data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                JsonSetting jsonSetting = new JsonSetting(true);
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<MassResult>(accessToken, _updateTemplateCardFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);
        }

        #endregion


        /// <summary>
        /// 【异步方法】更新任务卡片消息状态
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<UpdateTaskCardResultJson> UpdateTaskCardAsync(string accessTokenOrAppKey, UpdateTaskCardData data, int timeOut = Config.TIME_OUT)
        {
            //https://work.weixin.qq.com/api/doc/90000/90135/91579
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var jsonSetting = new JsonSetting(true);
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<UpdateTaskCardResultJson>(accessToken, _taskUrlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 撤回应用消息
        /// <para>文档：https://developer.work.weixin.qq.com/document/path/94867</para>
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> RecallAsync(string accessTokenOrAppKey, string msgId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var jsonSetting = new JsonSetting(true);
                var data = new
                {
                    msgid = msgId
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, _recallUrlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);
        }
        #endregion
    }
}
