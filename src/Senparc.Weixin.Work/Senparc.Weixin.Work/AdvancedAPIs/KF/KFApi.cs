/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：KFApi.cs
    文件功能描述：发送客服消息
    
    
    创建标识：Senparc - 20160309
 
    修改标识：Senparc - 20160720
    修改描述：增加其接口的异步方法

    修改标识：Senparc - 20170712
    修改描述：v14.5.1 AccessToken HandlerWaper改造

    修改标识：Senparc - 20190129
    修改描述：统一 CommonJsonSend.Send<T>() 方法请求接口

 
----------------------------------------------------------------*/

/*
    官方文档：http://qydev.weixin.qq.com/wiki/index.php?title=%E4%BC%81%E4%B8%9A%E5%AE%A2%E6%9C%8D%E6%8E%A5%E5%8F%A3%E8%AF%B4%E6%98%8E
 */

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.KF;
using Senparc.Weixin.Work.CommonAPIs;
using Senparc.Weixin.Work.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 发送消息
    /// </summary>
    public static class KFApi
    {
        private static string _urlFormat = Config.ApiWorkHost + "/cgi-bin/kf/send?access_token={0}";

        #region 同步方法


        /// <summary>
        /// 发送文本信息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="senderType">发送人类型</param>
        /// <param name="senderId">发送人标志</param>
        /// <param name="receiverType">接收人类型</param>
        /// <param name="receiverId">接收人标志</param>
        /// <param name="content">消息内容</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "KFApi.SendText", true)]
        public static WorkJsonResult SendText(string accessTokenOrAppKey, KF_User_Type senderType, string senderId, KF_User_Type receiverType,
            string receiverId, string content, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    sender = new
                    {
                        type = senderType.ToString(),
                        id = senderId
                    },
                    receiver = new
                    {
                        type = receiverType.ToString(),
                        id = receiverId
                    },
                    msgtype = "text",
                    text = new
                    {
                        content = content
                    }
                };
                return CommonJsonSend.Send<WorkJsonResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 发送图片信息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="senderType">发送人类型</param>
        /// <param name="senderId">发送人标志</param>
        /// <param name="receiverType">接收人类型</param>
        /// <param name="receiverId">接收人标志</param>
        /// <param name="mediaId">图片的mediaId</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "KFApi.SendImage", true)]
        public static WorkJsonResult SendImage(string accessTokenOrAppKey, KF_User_Type senderType, string senderId, KF_User_Type receiverType,
            string receiverId, string mediaId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    sender = new
                    {
                        type = senderType.ToString(),
                        id = senderId
                    },
                    receiver = new
                    {
                        type = receiverType.ToString(),
                        id = receiverId
                    },
                    msgtype = "image",
                    image = new
                    {
                        media_id = mediaId
                    }
                };
                return CommonJsonSend.Send<WorkJsonResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 发送文件信息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="senderType">发送人类型</param>
        /// <param name="senderId">发送人标志</param>
        /// <param name="receiverType">接收人类型</param>
        /// <param name="receiverId">接收人标志</param>
        /// <param name="mediaId">文件的mediaId</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "KFApi.SendFile", true)]
        public static WorkJsonResult SendFile(string accessTokenOrAppKey, KF_User_Type senderType, string senderId, KF_User_Type receiverType,
            string receiverId, string mediaId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    sender = new
                    {
                        type = senderType.ToString(),
                        id = senderId
                    },
                    receiver = new
                    {
                        type = receiverType.ToString(),
                        id = receiverId
                    },
                    msgtype = "file",
                    file = new
                    {
                        media_id = mediaId
                    }
                };
                return CommonJsonSend.Send<WorkJsonResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 发送语音信息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="senderType">发送人类型</param>
        /// <param name="senderId">发送人标志</param>
        /// <param name="receiverType">接收人类型</param>
        /// <param name="receiverId">接收人标志</param>
        /// <param name="mediaId">语音的mediaId</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "KFApi.SendVoice", true)]
        public static WorkJsonResult SendVoice(string accessTokenOrAppKey, KF_User_Type senderType, string senderId, KF_User_Type receiverType,
            string receiverId, string mediaId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    sender = new
                    {
                        type = senderType.ToString(),
                        id = senderId
                    },
                    receiver = new
                    {
                        type = receiverType.ToString(),
                        id = receiverId
                    },
                    msgtype = "voice",
                    voice = new
                    {
                        media_id = mediaId
                    }
                };
                return CommonJsonSend.Send<WorkJsonResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 获取客服列表
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="type">不填时，同时返回内部、外部客服列表</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "KFApi.GetKFList", true)]
        public static GetKFListResult GetKFList(string accessTokenOrAppKey, KF_Type? type = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/kf/list?access_token={0}&type={1}", accessToken, type);

                return CommonJsonSend.Send<GetKFListResult>(null, url, null, CommonJsonSendType.GET, timeOut);
            }, accessTokenOrAppKey);


        }
        #endregion


        #region 异步方法
        /// <summary>
        /// 【异步方法】发送文本信息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="senderType">发送人类型</param>
        /// <param name="senderId">发送人标志</param>
        /// <param name="receiverType">接收人类型</param>
        /// <param name="receiverId">接收人标志</param>
        /// <param name="content">消息内容</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "KFApi.SendTextAsync", true)]
        public static async Task<WorkJsonResult> SendTextAsync(string accessTokenOrAppKey, KF_User_Type senderType, string senderId, KF_User_Type receiverType,
            string receiverId, string content, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new
                {
                    sender = new
                    {
                        type = senderType.ToString(),
                        id = senderId
                    },
                    receiver = new
                    {
                        type = receiverType.ToString(),
                        id = receiverId
                    },
                    msgtype = "text",
                    text = new
                    {
                        content = content
                    }
                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);


        }

        /// <summary>
        /// 【异步方法】发送图片信息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="senderType">发送人类型</param>
        /// <param name="senderId">发送人标志</param>
        /// <param name="receiverType">接收人类型</param>
        /// <param name="receiverId">接收人标志</param>
        /// <param name="mediaId">图片的mediaId</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "KFApi.SendImageAsync", true)]
        public static async Task<WorkJsonResult> SendImageAsync(string accessTokenOrAppKey, KF_User_Type senderType, string senderId, KF_User_Type receiverType,
            string receiverId, string mediaId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new
                {
                    sender = new
                    {
                        type = senderType.ToString(),
                        id = senderId
                    },
                    receiver = new
                    {
                        type = receiverType.ToString(),
                        id = receiverId
                    },
                    msgtype = "image",
                    image = new
                    {
                        media_id = mediaId
                    }
                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);


        }

        /// <summary>
        /// 【异步方法】发送文件信息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="senderType">发送人类型</param>
        /// <param name="senderId">发送人标志</param>
        /// <param name="receiverType">接收人类型</param>
        /// <param name="receiverId">接收人标志</param>
        /// <param name="mediaId">文件的mediaId</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "KFApi.SendFileAsync", true)]
        public static async Task<WorkJsonResult> SendFileAsync(string accessTokenOrAppKey, KF_User_Type senderType, string senderId, KF_User_Type receiverType,
            string receiverId, string mediaId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new
                {
                    sender = new
                    {
                        type = senderType.ToString(),
                        id = senderId
                    },
                    receiver = new
                    {
                        type = receiverType.ToString(),
                        id = receiverId
                    },
                    msgtype = "file",
                    file = new
                    {
                        media_id = mediaId
                    }
                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);


        }

        /// <summary>
        /// 【异步方法】发送语音信息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="senderType">发送人类型</param>
        /// <param name="senderId">发送人标志</param>
        /// <param name="receiverType">接收人类型</param>
        /// <param name="receiverId">接收人标志</param>
        /// <param name="mediaId">语音的mediaId</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "KFApi.SendVoiceAsync", true)]
        public static async Task<WorkJsonResult> SendVoiceAsync(string accessTokenOrAppKey, KF_User_Type senderType, string senderId, KF_User_Type receiverType,
            string receiverId, string mediaId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new
                {
                    sender = new
                    {
                        type = senderType.ToString(),
                        id = senderId
                    },
                    receiver = new
                    {
                        type = receiverType.ToString(),
                        id = receiverId
                    },
                    msgtype = "voice",
                    voice = new
                    {
                        media_id = mediaId
                    }
                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);


        }

        /// <summary>
        /// 获取客服列表
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="type">不填时，同时返回内部、外部客服列表</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "KFApi.GetKFListAsync", true)]
        public static async Task<GetKFListResult> GetKFListAsync(string accessTokenOrAppKey, KF_Type? type = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/kf/list?access_token={0}&type={1}", accessToken, type);

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetKFListResult>(null, url, null, CommonJsonSendType.GET, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);


        }
        #endregion
    }
}
