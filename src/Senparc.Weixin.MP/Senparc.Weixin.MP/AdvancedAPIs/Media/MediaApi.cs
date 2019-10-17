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

    文件名：MediaAPI.cs
    文件功能描述：素材管理接口（原多媒体文件接口）


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间

    修改标识：Senparc - 20150321
    修改描述：变更为素材管理接口

    修改标识：Senparc - 20150401
    修改描述：上传临时图文消息接口

    修改标识：Senparc - 20150407
    修改描述：上传永久视频接口修改
    
    修改标识：Senparc - 20160703
    修改描述：修改接口http为https
 
    修改标识：Senparc - 20160719
    修改描述：增加其接口的异步方法
   
    修改标识：Senparc - 20170305
    修改描述：v14.3.131 为MediaApi.Get()方法提供ApiHandlerWapper.TryCommonApi()方法支持，可以传入AppId

    修改标识：Senparc - 20170707
    修改描述：v14.5.1 完善异步方法async/await

    修改标识：Senparc - 20180424
    修改描述：v14.12.2 修正 MediaApi.GetForeverMedia() 方法永久视频的文件下载过程。

    修改标识：Senparc - 20190129
    修改描述：统一 CommonJsonSend.Send<T>() 方法请求接口

----------------------------------------------------------------*/

/*
    接口详见：http://mp.weixin.qq.com/wiki/index.php?title=%E4%B8%8A%E4%BC%A0%E4%B8%8B%E8%BD%BD%E5%A4%9A%E5%AA%92%E4%BD%93%E6%96%87%E4%BB%B6
 */

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.AdvancedAPIs.GroupMessage;
using Senparc.Weixin.MP.AdvancedAPIs.Media;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 素材管理接口（原多媒体文件接口）
    /// </summary>
    public static class MediaApi
    {
        #region 同步方法

        #region 临时素材
        /// <summary>
        /// 新增临时素材（原上传媒体文件）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="type">媒体文件类型</param>
        /// <param name="file">上传文件的绝对路径</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.UploadTemporaryMedia", true)]
        public static UploadTemporaryMediaResult UploadTemporaryMedia(string accessTokenOrAppId, UploadMediaFileType type, string file, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/media/upload?access_token={0}&type={1}", accessToken.AsUrlData(), type.ToString().AsUrlData());
                var fileDictionary = new Dictionary<string, string>();
                fileDictionary["media"] = file;
                return CO2NET.HttpUtility.Post.PostFileGetJson<UploadTemporaryMediaResult>(url, null, fileDictionary, null, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 上传临时图文消息素材（原上传图文消息素材）
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="news">图文消息组</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.UploadTemporaryNews", true)]
        public static UploadTemporaryMediaResult UploadTemporaryNews(string accessTokenOrAppId, int timeOut = Config.TIME_OUT, params NewsModel[] news)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/media/uploadnews?access_token={0}";

                var data = new
                {
                    articles = news
                };
                return CommonJsonSend.Send<UploadTemporaryMediaResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取临时素材（原下载媒体文件）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="mediaId"></param>
        /// <param name="stream"></param>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.Get", true)]
        public static void Get(string accessTokenOrAppId, string mediaId, Stream stream)
        {
            ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpFileHost + "/cgi-bin/media/get?access_token={0}&media_id={1}", accessToken.AsUrlData(), mediaId.AsUrlData());
                CO2NET.HttpUtility.Get.Download(url, stream);
                return new WxJsonResult() { errcode = ReturnCode.请求成功, errmsg = "ok" };//无实际意义
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取临时素材（原下载媒体文件），保存到指定文件夹
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="mediaId"></param>
        /// <param name="dir">储存目录</param>
        /// <returns>储存文件的完整路径</returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.Get", true)]
        public static string Get(string accessTokenOrAppId, string mediaId, string dir)
        {
            var result = ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpFileHost + "/cgi-bin/media/get?access_token={0}&media_id={1}", accessToken.AsUrlData(), mediaId.AsUrlData());
                var str = CO2NET.HttpUtility.Get.Download(url, dir);
                return new WxJsonResult() { errcode = ReturnCode.请求成功, errmsg = str };
            }, accessTokenOrAppId);
            return result.errmsg;
        }

        /// <summary>
        /// 附录：高清语音素材获取接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="mediaId"></param>
        /// <param name="stream"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.GetJssdk", true)]
        public static WxJsonResult GetJssdk(string accessTokenOrAppId, string mediaId, Stream stream, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/media/get/jssdk?access_token={0}&media_id={1}", accessToken.AsUrlData(), mediaId.AsUrlData());

                if (stream != null)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    CO2NET.HttpUtility.Get.Download(urlFormat, stream);
                }

                return new WxJsonResult() { errcode = ReturnCode.不合法的媒体文件id, errmsg = "invalid media_id" };//错误情况下的返回

            }, accessTokenOrAppId);
        }

        #endregion

        #region 永久素材
        /*
         1、新增的永久素材也可以在公众平台官网素材管理模块中看到
         2、永久素材的数量是有上限的，请谨慎新增。图文消息素材和图片素材的上限为5000，其他类型为1000
         3、调用该接口需https协议
         */

        /// <summary>
        /// 新增永久图文素材
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="news">图文消息组</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.UploadNews", true)]
        public static UploadForeverMediaResult UploadNews(string accessTokenOrAppId, int timeOut = Config.TIME_OUT, params NewsModel[] news)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/material/add_news?access_token={0}";

                var data = new
                {
                    articles = news
                };
                return CommonJsonSend.Send<UploadForeverMediaResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 新增其他类型永久素材(图片（image）、语音（voice）和缩略图（thumb）)
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="file">上传文件的绝对路径</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.UploadForeverMedia", true)]
        public static UploadForeverMediaResult UploadForeverMedia(string accessTokenOrAppId, string file, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/material/add_material?access_token={0}", accessToken.AsUrlData());

                //因为有文件上传，所以忽略dataDictionary，全部改用文件上传格式
                //var dataDictionary = new Dictionary<string, string>();
                //dataDictionary["type"] = UploadMediaFileType.image.ToString();

                var fileDictionary = new Dictionary<string, string>();
                //fileDictionary["type"] = UploadMediaFileType.image.ToString();//不提供此参数也可以上传成功
                fileDictionary["media"] = file;
                return CO2NET.HttpUtility.Post.PostFileGetJson<UploadForeverMediaResult>(url, null, fileDictionary, null, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 新增永久视频素材
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="file">上传文件的绝对路径</param>
        /// <param name="title"></param>
        /// <param name="introduction"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.UploadForeverVideo", true)]
        public static UploadForeverMediaResult UploadForeverVideo(string accessTokenOrAppId, string file, string title, string introduction, int timeOut = 40000)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/material/add_material?access_token={0}", accessToken.AsUrlData());
                var fileDictionary = new Dictionary<string, string>();
                fileDictionary["media"] = file;
                fileDictionary["description"] = string.Format("{{\"title\":\"{0}\", \"introduction\":\"{1}\"}}", title, introduction);

                return CO2NET.HttpUtility.Post.PostFileGetJson<UploadForeverMediaResult>(url, null, fileDictionary, null, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取永久图文素材
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.GetForeverNews", true)]
        public static GetNewsResultJson GetForeverNews(string accessTokenOrAppId, string mediaId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/material/get_material?access_token={0}";
                var data = new
                {
                    media_id = mediaId
                };
                return CommonJsonSend.Send<GetNewsResultJson>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取永久素材(除了图文、视频)
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="mediaId">要获取的素材的media_id</param>
        /// <param name="stream">写入文件流</param>
        /// <param name="timeOut"></param>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.GetForeverMedia", true)]
        public static WxJsonResult GetForeverMedia(string accessTokenOrAppId, string mediaId, Stream stream, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/material/get_material?access_token={0}", (object)accessToken.AsUrlData());

                var data = new
                {
                    media_id = mediaId
                };

                if (stream != null)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    CO2NET.HttpUtility.Post.Download(urlFormat, data.ToJson(), stream);
                }
                return new WxJsonResult() { errcode = ReturnCode.请求成功, errmsg = "ok" };//无实际意义
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取永久视频素材
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="mediaId">要获取的素材的media_id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.GetForeverVideo", true)]
        public static GetForeverMediaVideoResultJson GetForeverVideo(string accessTokenOrAppId, string mediaId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/material/get_material?access_token={0}";
                var data = new
                {
                    media_id = mediaId
                };
                return CommonJsonSend.Send<GetForeverMediaVideoResultJson>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 删除永久素材
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.DeleteForeverMedia", true)]
        public static WxJsonResult DeleteForeverMedia(string accessTokenOrAppId, string mediaId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/material/del_material?access_token={0}";
                var data = new
                {
                    media_id = mediaId
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 修改永久图文素材
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="mediaId">要修改的图文消息的id</param>
        /// <param name="index">要更新的文章在图文消息中的位置（多图文消息时，此字段才有意义），第一篇为0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="news">图文素材</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.UpdateForeverNews", true)]
        public static WxJsonResult UpdateForeverNews(string accessTokenOrAppId, string mediaId, int? index, NewsModel news, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/material/update_news?access_token={0}";

                var data = new
                {
                    media_id = mediaId,
                    index = index,
                    articles = news
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取素材总数
        /// 永久素材的总数，也会计算公众平台官网素材管理中的素材
        /// 图片和图文消息素材（包括单图文和多图文）的总数上限为5000，其他素材的总数上限为1000
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.GetMediaCount", true)]
        public static GetMediaCountResultJson GetMediaCount(string accessTokenOrAppId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/cgi-bin/material/get_materialcount?access_token={0}", accessToken.AsUrlData());

                return CommonJsonSend.Send<GetMediaCountResultJson>(null, url, null, CommonJsonSendType.GET);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取图文素材列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="offset">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回</param>
        /// <param name="count">返回素材的数量，取值在1到20之间</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.GetNewsMediaList", true)]
        public static MediaList_NewsResult GetNewsMediaList(string accessTokenOrAppId, int offset, int count, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/cgi-bin/material/batchget_material?access_token={0}", accessToken.AsUrlData());

                var date = new
                {
                    type = "news",
                    offset = offset,
                    count = count
                };

                return CommonJsonSend.Send<MediaList_NewsResult>(null, url, date, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);

        }

        /// <summary>
        /// 获取图片、视频、语音素材列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="type">素材的类型，图片（image）、视频（video）、语音 （voice）</param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.GetOthersMediaList", true)]
        public static MediaList_OthersResult GetOthersMediaList(string accessTokenOrAppId, UploadMediaFileType type, int offset,
                                                           int count, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/cgi-bin/material/batchget_material?access_token={0}", accessToken.AsUrlData());

                var date = new
                {
                    type = type.ToString(),
                    offset = offset,
                    count = count
                };

                return CommonJsonSend.Send<MediaList_OthersResult>(null, url, date, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 上传图文消息内的图片获取URL
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="file">上传文件的绝对路径</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.UploadImg", true)]
        public static UploadImgResult UploadImg(string accessTokenOrAppId, string file, int timeOut = Config.TIME_OUT)
        {
            //接口文档参考：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1444738729
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/media/uploadimg?access_token={0}", accessToken.AsUrlData());

                var fileDictionary = new Dictionary<string, string>();
                fileDictionary["media"] = file;
                return CO2NET.HttpUtility.Post.PostFileGetJson<UploadImgResult>(url, null, fileDictionary, null, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

        #region AI开放接口

        /// <summary>
        /// 提交语音
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="format"></param>
        /// <param name="voiceId"></param>
        /// <param name="lang"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.AddVoice", true)]
        public static WxJsonResult AddVoice(string accessTokenOrAppId, string format, string voiceId, string lang, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/media/voice/addvoicetorecofortext?access_token={0}";

                var data = new
                {
                    format = format,
                    voice_id = voiceId,
                    lang = lang
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取语音识别结果
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="voiceId"></param>
        /// <param name="lang"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.QueryRecoResult", true)]
        public static QueryRecoResultResultJson QueryRecoResult(string accessTokenOrAppId, string voiceId, string lang, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/media/voice/queryrecoresultfortext?access_token={0}";

                var data = new
                {
                    voice_id = voiceId,
                    lang = lang
                };
                return CommonJsonSend.Send<QueryRecoResultResultJson>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 微信翻译
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="Ifrom"></param>
        /// <param name="Ito"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.TranslateContent", true)]
        public static TranslateContentResultJson TranslateContent(string accessTokenOrAppId, string Ifrom, string Ito, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/media/voice/translatecontent?access_token={0}";

                var data = new
                {
                    Ifrom,
                    Ito
                };
                return CommonJsonSend.Send<TranslateContentResultJson>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

        #endregion

        #region 异步方法

        #region 临时素材
        /// <summary>
        /// 【异步方法】新增临时素材（原上传媒体文件）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="type"></param>
        /// <param name="file">上传文件的绝对路径</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.UploadTemporaryMediaAsync", true)]
        public static async Task<UploadTemporaryMediaResult> UploadTemporaryMediaAsync(string accessTokenOrAppId, UploadMediaFileType type, string file, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var url = string.Format(Config.ApiMpHost + "/cgi-bin/media/upload?access_token={0}&type={1}", accessToken.AsUrlData(), type.ToString().AsUrlData());
               var fileDictionary = new Dictionary<string, string>();
               fileDictionary["media"] = file;
               return await CO2NET.HttpUtility.Post.PostFileGetJsonAsync<UploadTemporaryMediaResult>(url, null, fileDictionary, null, null, null, false, timeOut: timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】上传临时图文消息素材（原上传图文消息素材）
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="news">图文消息组</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.UploadTemporaryNewsAsync", true)]
        public static async Task<UploadTemporaryMediaResult> UploadTemporaryNewsAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT, params NewsModel[] news)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               string urlFormat = Config.ApiMpHost + "/cgi-bin/media/uploadnews?access_token={0}";

               var data = new
               {
                   articles = news
               };
               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<UploadTemporaryMediaResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取临时素材（原下载媒体文件）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="mediaId"></param>
        /// <param name="stream"></param>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.GetAsync", true)]
        public static async Task GetAsync(string accessTokenOrAppId, string mediaId, Stream stream)
        {
            await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/media/get?access_token={0}&media_id={1}", accessToken.AsUrlData(), mediaId.AsUrlData());
                await CO2NET.HttpUtility.Get.DownloadAsync(url, stream).ConfigureAwait(false);
                return new WxJsonResult() { errcode = ReturnCode.请求成功, errmsg = "ok" };//无实际意义
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取临时素材（原下载媒体文件），保存到指定文件夹
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="mediaId"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.GetAsync", true)]
        public static async Task<string> GetAsync(string accessTokenOrAppId, string mediaId, string dir)
        {
            var result = await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/media/get?access_token={0}&media_id={1}", accessToken.AsUrlData(), mediaId.AsUrlData());
                var str = await CO2NET.HttpUtility.Get.DownloadAsync(url, dir).ConfigureAwait(false);
                return new WxJsonResult() { errcode = ReturnCode.请求成功, errmsg = str };
            }, accessTokenOrAppId).ConfigureAwait(false);
            return result.errmsg;
        }

        /// <summary>
        /// 【异步方法】附录：高清语音素材获取接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="mediaId"></param>
        /// <param name="stream"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.GetJssdkAsync", true)]
        public static async Task<WxJsonResult> GetJssdkAsync(string accessTokenOrAppId, string mediaId, Stream stream, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/media/get/jssdk?access_token={0}&media_id={1}", accessToken.AsUrlData(), mediaId.AsUrlData());

                if (stream != null)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    await CO2NET.HttpUtility.Get.DownloadAsync(urlFormat, stream).ConfigureAwait(false);
                }

                return new WxJsonResult() { errcode = ReturnCode.不合法的媒体文件id, errmsg = "invalid media_id" };//错误情况下的返回

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        #endregion

        #region 永久素材
        /*
         1、新增的永久素材也可以在公众平台官网素材管理模块中看到
         2、永久素材的数量是有上限的，请谨慎新增。图文消息素材和图片素材的上限为5000，其他类型为1000
         3、调用该接口需https协议
         */

        /// <summary>
        /// 【异步方法】新增永久图文素材
        /// </summary>
        /// <param name="accessTokenOrAppId">Token</param>
        /// <param name="news">图文消息组</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.UploadNewsAsync", true)]
        public static async Task<UploadForeverMediaResult> UploadNewsAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT, params NewsModel[] news)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               string urlFormat = Config.ApiMpHost + "/cgi-bin/material/add_news?access_token={0}";

               var data = new
               {
                   articles = news
               };
               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<UploadForeverMediaResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】新增其他类型永久素材(图片（image）、语音（voice）和缩略图（thumb）)
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="file">文件路径</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.UploadForeverMediaAsync", true)]
        public static async Task<UploadForeverMediaResult> UploadForeverMediaAsync(string accessTokenOrAppId, string file, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var url = string.Format(Config.ApiMpHost + "/cgi-bin/material/add_material?access_token={0}", accessToken.AsUrlData());

               //因为有文件上传，所以忽略dataDictionary，全部改用文件上传格式
               //var dataDictionary = new Dictionary<string, string>();
               //dataDictionary["type"] = UploadMediaFileType.image.ToString();

               var fileDictionary = new Dictionary<string, string>();
               //fileDictionary["type"] = UploadMediaFileType.image.ToString();//不提供此参数也可以上传成功
               fileDictionary["media"] = file;
               return await CO2NET.HttpUtility.Post.PostFileGetJsonAsync<UploadForeverMediaResult>(url, null, fileDictionary, null, timeOut: timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】新增永久视频素材
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="file">文件路径</param>
        /// <param name="title"></param>
        /// <param name="introduction"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.UploadForeverVideoAsync", true)]
        public static async Task<UploadForeverMediaResult> UploadForeverVideoAsync(string accessTokenOrAppId, string file, string title, string introduction, int timeOut = 40000)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var url = string.Format(Config.ApiMpHost + "/cgi-bin/material/add_material?access_token={0}", accessToken.AsUrlData());
               var fileDictionary = new Dictionary<string, string>();
               fileDictionary["media"] = file;
               fileDictionary["description"] = string.Format("{{\"title\":\"{0}\", \"introduction\":\"{1}\"}}", title, introduction);

               return await CO2NET.HttpUtility.Post.PostFileGetJsonAsync<UploadForeverMediaResult>(url, null, fileDictionary, null, timeOut: timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取永久图文素材
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.GetForeverNewsAsync", true)]
        public static async Task<GetNewsResultJson> GetForeverNewsAsync(string accessTokenOrAppId, string mediaId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               string url = Config.ApiMpHost + "/cgi-bin/material/get_material?access_token={0}";
               var data = new
               {
                   media_id = mediaId
               };
               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetNewsResultJson>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取永久素材(除了图文、视频)
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="mediaId">要获取的素材的media_id</param>
        /// <param name="stream">写入文件流</param>
        /// <param name="timeOut"></param>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.GetForeverMediaAsync", true)]
        public static async Task<WxJsonResult> GetForeverMediaAsync(string accessTokenOrAppId, string mediaId, Stream stream, int timeOut = Config.TIME_OUT)
        {

            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/material/get_material?access_token={0}", (object)accessToken.AsUrlData());

                var data = new
                {
                    media_id = mediaId
                };

                if (stream != null)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    await CO2NET.HttpUtility.Post.DownloadAsync(urlFormat, data.ToJson(), stream).ConfigureAwait(false);
                }
                return new WxJsonResult() { errcode = ReturnCode.请求成功, errmsg = "ok" };//无实际意义
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取永久视频素材
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="mediaId">要获取的素材的media_id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.GetForeverVideoAsync", true)]
        public static async Task<GetForeverMediaVideoResultJson> GetForeverVideoAsync(string accessTokenOrAppId, string mediaId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string url = Config.ApiMpHost + "/cgi-bin/material/get_material?access_token={0}";
                var data = new
                {
                    media_id = mediaId
                };
                var result = await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetForeverMediaVideoResultJson>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);
                return result;
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】删除永久素材
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.DeleteForeverMediaAsync", true)]
        public static async Task<WxJsonResult> DeleteForeverMediaAsync(string accessTokenOrAppId, string mediaId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               string url = Config.ApiMpHost + "/cgi-bin/material/del_material?access_token={0}";
               var data = new
               {
                   media_id = mediaId
               };
               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】修改永久图文素材
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="mediaId">要修改的图文消息的id</param>
        /// <param name="index">要更新的文章在图文消息中的位置（多图文消息时，此字段才有意义），第一篇为0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="news">图文素材</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.UpdateForeverNewsAsync", true)]
        public static async Task<WxJsonResult> UpdateForeverNewsAsync(string accessTokenOrAppId, string mediaId, int? index, NewsModel news, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               string url = Config.ApiMpHost + "/cgi-bin/material/update_news?access_token={0}";

               var data = new
               {
                   media_id = mediaId,
                   index = index,
                   articles = news
               };
               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取素材总数
        /// 永久素材的总数，也会计算公众平台官网素材管理中的素材
        /// 图片和图文消息素材（包括单图文和多图文）的总数上限为5000，其他素材的总数上限为1000
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.GetMediaCountAsync", true)]
        public static async Task<GetMediaCountResultJson> GetMediaCountAsync(string accessTokenOrAppId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               string url = string.Format(Config.ApiMpHost + "/cgi-bin/material/get_materialcount?access_token={0}", accessToken.AsUrlData());

               return await CommonJsonSend.SendAsync<GetMediaCountResultJson>(null, url, null, CommonJsonSendType.GET).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取图文素材列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="offset">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回</param>
        /// <param name="count">返回素材的数量，取值在1到20之间</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.GetNewsMediaListAsync", true)]
        public static async Task<MediaList_NewsResult> GetNewsMediaListAsync(string accessTokenOrAppId, int offset, int count, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               string url = string.Format(Config.ApiMpHost + "/cgi-bin/material/batchget_material?access_token={0}", accessToken.AsUrlData());

               var date = new
               {
                   type = "news",
                   offset = offset,
                   count = count
               };

               return await CommonJsonSend.SendAsync<MediaList_NewsResult>(null, url, date, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);

        }

        /// <summary>
        /// 【异步方法】获取图片、视频、语音素材列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="type">素材的类型，图片（image）、视频（video）、语音 （voice）</param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.GetOthersMediaListAsync", true)]
        public static async Task<MediaList_OthersResult> GetOthersMediaListAsync(string accessTokenOrAppId, UploadMediaFileType type, int offset,
                                                           int count, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               string url = string.Format(Config.ApiMpHost + "/cgi-bin/material/batchget_material?access_token={0}", accessToken.AsUrlData());

               var date = new
               {
                   type = type.ToString(),
                   offset = offset,
                   count = count
               };

               return await CommonJsonSend.SendAsync<MediaList_OthersResult>(null, url, date, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】上传图文消息内的图片获取URL
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="file"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.UploadImgAsync", true)]
        public static async Task<UploadImgResult> UploadImgAsync(string accessTokenOrAppId, string file, int timeOut = Config.TIME_OUT)
        {
            //接口文档参考：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1444738729
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/media/uploadimg?access_token={0}", accessToken.AsUrlData());

                var fileDictionary = new Dictionary<string, string>();
                fileDictionary["media"] = file;
                return await CO2NET.HttpUtility.Post.PostFileGetJsonAsync<UploadImgResult>(url, null, fileDictionary, null, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        #endregion

        #region AI开放接口

        /// <summary>
        /// 【异步方法】提交语音
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="format"></param>
        /// <param name="voiceId"></param>
        /// <param name="lang"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.AddVoiceAsync", true)]
        public static async Task<WxJsonResult> AddVoiceAsync(string accessTokenOrAppId, string format, string voiceId, string lang, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/media/voice/addvoicetorecofortext?access_token={0}";

                var data = new
                {
                    format = format,
                    voice_id = voiceId,
                    lang = lang
                };
                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取语音识别结果
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="format"></param>
        /// <param name="voiceId"></param>
        /// <param name="lang"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.QueryRecoResultAsync", true)]
        public static async Task<QueryRecoResultResultJson> QueryRecoResultAsync(string accessTokenOrAppId, string voiceId, string lang, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/media/voice/queryrecoresultfortext?access_token={0}";

                var data = new
                {
                    voice_id = voiceId,
                    lang = lang
                };
                return await CommonJsonSend.SendAsync<QueryRecoResultResultJson>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】微信翻译
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="Ifrom"></param>
        /// <param name="Ito"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "MediaApi.TranslateContentAsync", true)]
        public static async Task<TranslateContentResultJson> TranslateContentAsync(string accessTokenOrAppId, string Ifrom, string Ito, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/media/voice/translatecontent?access_token={0}";

                var data = new
                {
                    Ifrom,
                    Ito
                };
                return await CommonJsonSend.SendAsync<TranslateContentResultJson>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        #endregion

        #endregion
    }
}
