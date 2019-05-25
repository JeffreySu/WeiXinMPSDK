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
    
    文件名：CustomServiceAPI.cs
    文件功能描述：多客服接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20150306
    修改描述：增加多客服接口
 
    修改标识：Senparc - 20160520
    修改描述：增加邀请绑定客服帐号接口
 
    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间

    修改标识：Senparc - 20160718
    修改描述：增加其接口的异步方法

    修改标识：Senparc - 20170609
    修改描述：v14.4.12 修复 CustomServiceApi.GetMsgList() 方法时间戳问题

    修改标识：Senparc - 20170707
    修改描述：v14.5.1 完善异步方法async/await

    修改标识：Senparc - 20170522
    修改描述：v16.6.2 修改 DateTime 为 DateTimeOffset

----------------------------------------------------------------*/

/* 
    多客服接口聊天记录接口，官方API：http://mp.weixin.qq.com/wiki/index.php?title=%E8%8E%B7%E5%8F%96%E5%AE%A2%E6%9C%8D%E8%81%8A%E5%A4%A9%E8%AE%B0%E5%BD%95
*/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers;
using Senparc.NeuChar;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;
using Senparc.CO2NET.HttpUtility;
using Senparc.Weixin.MP.AdvancedAPIs.CustomService;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 多客服接口
    /// </summary>
    public static class CustomServiceApi
    {
        #region 同步方法


        /// <summary>
        /// 获取用户聊天记录
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="startTime">查询开始时间，会自动转为UNIX时间戳</param>
        /// <param name="endTime">查询结束时间，会自动转为UNIX时间戳，每次查询不能跨日查询</param>
        /// <param name="pageSize">每页大小，每页最多拉取1000条</param>
        /// <param name="pageIndex">查询第几页，从1开始</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.GetRecord", true)]
        public static GetRecordResult GetRecord(string accessTokenOrAppId, DateTime startTime, DateTime endTime, int pageSize = 10, int pageIndex = 1, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/customservice/msgrecord/getrecord?access_token={0}";

                //规范页码
                if (pageSize <= 0)
                {
                    pageSize = 1;
                }
                else if (pageSize > 50)
                {
                    pageSize = 50;
                }

                //组装发送消息
                var data = new
                {
                    starttime = DateTimeHelper.GetUnixDateTime(startTime),
                    endtime = DateTimeHelper.GetUnixDateTime(endTime),
                    pagesize = pageSize,
                    pageindex = pageIndex
                };

                return CommonJsonSend.Send<GetRecordResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取客服基本信息
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.GetCustomBasicInfo", true)]
        public static CustomInfoJson GetCustomBasicInfo(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/customservice/getkflist?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<CustomInfoJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
                //return GetCustomInfoResult<CustomInfoJson>(urlFormat);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取在线客服接待信息
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.GetCustomOnlineInfo", true)]
        public static CustomOnlineJson GetCustomOnlineInfo(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/customservice/getonlinekflist?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<CustomOnlineJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
                //return GetCustomInfoResult<CustomOnlineJson>(urlFormat);

            }, accessTokenOrAppId);
        }

        //private static T GetCustomInfoResult<T>(string urlFormat)
        //{
        //    var jsonString = HttpUtility.RequestUtility.HttpGet(urlFormat, Encoding.UTF8);
        //    JavaScriptSerializer js = new JavaScriptSerializer();
        //    return js.Deserialize<T>(jsonString);
        //}

        /// <summary>
        /// 添加客服账号
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号，账号前缀最多10个字符，必须是英文或者数字字符。如果没有公众号微信号，请前往微信公众平台设置。</param>
        /// <param name="nickName">客服昵称，最长6个汉字或12个英文字符</param>
        /// <param name="passWord">客服账号登录密码，格式为密码明文的32位加密MD5值</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.AddCustom", true)]
        public static WxJsonResult AddCustom(string accessTokenOrAppId, string kfAccount, string nickName, string passWord, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfaccount/add?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    kf_account = kfAccount,
                    nickname = nickName,
                    password = passWord
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 邀请绑定客服帐号
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="kfAccount">完整客服帐号，格式为：帐号前缀@公众号微信号</param>
        /// <param name="inviteWx">接收绑定邀请的客服微信号</param>

        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.InviteWorker", true)]
        public static WxJsonResult InviteWorker(string accessTokenOrAppId, string kfAccount, string inviteWx, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfaccount/inviteworker?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    kf_account = kfAccount,
                    invite_wx = inviteWx

                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 修改客服账号
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号，账号前缀最多10个字符，必须是英文或者数字字符。如果没有公众号微信号，请前往微信公众平台设置。</param>
        /// <param name="nickName">客服昵称，最长6个汉字或12个英文字符</param>
        /// <param name="passWord">客服账号登录密码，格式为密码明文的32位加密MD5值</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.UpdateCustom", true)]
        public static WxJsonResult UpdateCustom(string accessTokenOrAppId, string kfAccount, string nickName, string passWord, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfaccount/update?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    kf_account = kfAccount,
                    nickname = nickName,
                    password = passWord
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 上传客服头像
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="file">form-data中媒体文件标识，有filename、filelength、content-type等信息</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.UploadCustomHeadimg", true)]
        public static WxJsonResult UploadCustomHeadimg(string accessTokenOrAppId, string kfAccount, string file, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/customservice/kfaccount/uploadheadimg?access_token={0}&kf_account={1}", accessToken.AsUrlData(), kfAccount.AsUrlData());
                var fileDictionary = new Dictionary<string, string>();
                fileDictionary["media"] = file;
                return Post.PostFileGetJson<WxJsonResult>(url, null, fileDictionary, null, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 删除客服账号
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.DeleteCustom", true)]
        public static WxJsonResult DeleteCustom(string accessTokenOrAppId, string kfAccount, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfaccount/del?access_token={0}&kf_account={1}", accessToken.AsUrlData(), kfAccount.AsUrlData());
                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 创建会话
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">客户openid</param>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="text">附加信息，文本会展示在客服人员的多客服客户端(非必须)</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.CreateSession", true)]
        public static WxJsonResult CreateSession(string accessTokenOrAppId, string openId, string kfAccount, string text = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfsession/create?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    openid = openId,
                    kf_account = kfAccount,
                    text = text
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 关闭会话
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">客户openid</param>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="text">附加信息，文本会展示在客服人员的多客服客户端(非必须)</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.CloseSession", true)]
        public static WxJsonResult CloseSession(string accessTokenOrAppId, string openId, string kfAccount, string text = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfsession/close?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    openid = openId,
                    kf_account = kfAccount,
                    text = text
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取客户的会话状态
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">客户openid</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.GetSessionState", true)]
        public static GetSessionStateResultJson GetSessionState(string accessTokenOrAppId, string openId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfsession/getsession?access_token={0}&openid={1}", accessToken.AsUrlData(), openId.AsUrlData());

                return CommonJsonSend.Send<GetSessionStateResultJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取客服的会话列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号，账号前缀最多10个字符，必须是英文或者数字字符。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.GetSessionList", true)]
        public static GetSessionListResultJson GetSessionList(string accessTokenOrAppId, string kfAccount, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfsession/getsessionlist?access_token={0}&kf_account={1}", accessToken.AsUrlData(), kfAccount.AsUrlData());

                return CommonJsonSend.Send<GetSessionListResultJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取未接入会话列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.GetWaitCase", true)]
        public static GetWaitCaseResultJson GetWaitCase(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfsession/getwaitcase?access_token={0}", accessToken.AsUrlData());

                return CommonJsonSend.Send<GetWaitCaseResultJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取聊天记录
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="startTime">起始时间，unix时间戳</param>
        /// <param name="endTime">结束时间，unix时间戳，每次查询时段不能超过24小时</param>
        /// <param name="msgId">消息id顺序从小到大，从1开始</param>
        /// <param name="number">每次获取条数，最多10000条</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.GetMsgList", true)]
        public static GetMsgListResultJson GetMsgList(string accessTokenOrAppId, DateTime startTime, DateTime endTime, long msgId, int number, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/customservice/msgrecord/getmsglist?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    starttime = DateTimeHelper.GetUnixDateTime(startTime),
                    endtime = DateTimeHelper.GetUnixDateTime(endTime),
                    msgid = msgId,
                    number = number
                };
                return CommonJsonSend.Send<GetMsgListResultJson>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);

        }
        #endregion

        #region 异步方法


        /// <summary>
        /// 【异步方法】获取用户聊天记录
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="startTime">查询开始时间，会自动转为UNIX时间戳</param>
        /// <param name="endTime">查询结束时间，会自动转为UNIX时间戳，每次查询不能跨日查询</param>
        /// <param name="pageSize">每页大小，每页最多拉取1000条</param>
        /// <param name="pageIndex">查询第几页，从1开始</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.GetRecordAsync", true)]
        public static async Task<GetRecordResult> GetRecordAsync(string accessTokenOrAppId, DateTime startTime, DateTime endTime, int pageSize = 10, int pageIndex = 1, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var urlFormat = Config.ApiMpHost + "/customservice/msgrecord/getrecord?access_token={0}";

               //规范页码
               if (pageSize <= 0)
               {
                   pageSize = 1;
               }
               else if (pageSize > 50)
               {
                   pageSize = 50;
               }

               //组装发送消息
               var data = new
               {
                   starttime = DateTimeHelper.GetUnixDateTime(startTime),
                   endtime = DateTimeHelper.GetUnixDateTime(endTime),
                   pagesize = pageSize,
                   pageindex = pageIndex
               };

               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetRecordResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId);
        }

        /// <summary>
        ///【异步方法】 获取客服基本信息
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.GetCustomBasicInfoAsync", true)]
        public static async Task<CustomInfoJson> GetCustomBasicInfoAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/customservice/getkflist?access_token={0}", accessToken.AsUrlData());
               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<CustomInfoJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut).ConfigureAwait(false);
               //return GetCustomInfoResult<CustomInfoJson>(urlFormat);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取在线客服接待信息
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.GetCustomOnlineInfoAsync", true)]
        public static async Task<CustomOnlineJson> GetCustomOnlineInfoAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/customservice/getonlinekflist?access_token={0}", accessToken.AsUrlData());
               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<CustomOnlineJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut).ConfigureAwait(false);
               //return GetCustomInfoResult<CustomOnlineJson>(urlFormat);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }

        //private static T GetCustomInfoResult<T>(string urlFormat)
        //{
        //    var jsonString = HttpUtility.RequestUtility.HttpGet(urlFormat, Encoding.UTF8);
        //    JavaScriptSerializer js = new JavaScriptSerializer();
        //    return js.Deserialize<T>(jsonString);
        //}

        /// <summary>
        /// 【异步方法】添加客服账号
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号，账号前缀最多10个字符，必须是英文或者数字字符。如果没有公众号微信号，请前往微信公众平台设置。</param>
        /// <param name="nickName">客服昵称，最长6个汉字或12个英文字符</param>
        /// <param name="passWord">客服账号登录密码，格式为密码明文的32位加密MD5值</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.AddCustomAsync", true)]
        public static async Task<WxJsonResult> AddCustomAsync(string accessTokenOrAppId, string kfAccount, string nickName, string passWord, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfaccount/add?access_token={0}", accessToken.AsUrlData());

               var data = new
               {
                   kf_account = kfAccount,
                   nickname = nickName,
                   password = passWord
               };

               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】邀请绑定客服帐号
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="kfAccount">完整客服帐号，格式为：帐号前缀@公众号微信号</param>
        /// <param name="inviteWx">接收绑定邀请的客服微信号</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.InviteWorkerAsync", true)]
        public static async Task<WxJsonResult> InviteWorkerAsync(string accessTokenOrAppId, string kfAccount, string inviteWx, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfaccount/inviteworker?access_token={0}", accessToken.AsUrlData());

               var data = new
               {
                   kf_account = kfAccount,
                   invite_wx = inviteWx

               };

               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】设置客服信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号，账号前缀最多10个字符，必须是英文或者数字字符。如果没有公众号微信号，请前往微信公众平台设置。</param>
        /// <param name="nickName">客服昵称，最长6个汉字或12个英文字符</param>
        /// <param name="passWord">客服账号登录密码，格式为密码明文的32位加密MD5值</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.UpdateCustomAsync", true)]
        public static async Task<WxJsonResult> UpdateCustomAsync(string accessTokenOrAppId, string kfAccount, string nickName, string passWord, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfaccount/update?access_token={0}", accessToken.AsUrlData());

               var data = new
               {
                   kf_account = kfAccount,
                   nickname = nickName,
                   password = passWord
               };

               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】上传客服头像
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="file">form-data中媒体文件标识，有filename、filelength、content-type等信息</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.UploadCustomHeadimgAsync", true)]
        public static async Task<WxJsonResult> UploadCustomHeadimgAsync(string accessTokenOrAppId, string kfAccount, string file, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var url = string.Format(Config.ApiMpHost + "/customservice/kfaccount/uploadheadimg?access_token={0}&kf_account={1}", accessToken.AsUrlData(), kfAccount.AsUrlData());
               var fileDictionary = new Dictionary<string, string>();
               fileDictionary["media"] = file;
               return await Post.PostFileGetJsonAsync<WxJsonResult>(url, null, fileDictionary, null, timeOut: timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        ///【异步方法】 删除客服账号
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.DeleteCustomAsync", true)]
        public static async Task<WxJsonResult> DeleteCustomAsync(string accessTokenOrAppId, string kfAccount, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfaccount/del?access_token={0}&kf_account={1}", accessToken.AsUrlData(), kfAccount.AsUrlData());
               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        ///【异步方法】创建会话
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">客户openid</param>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="text">附加信息，文本会展示在客服人员的多客服客户端(非必须)</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.CreateSessionAsync", true)]
        public static async Task<WxJsonResult> CreateSessionAsync(string accessTokenOrAppId, string openId, string kfAccount, string text = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfsession/create?access_token={0}", accessToken.AsUrlData());

               var data = new
               {
                   openid = openId,
                   kf_account = kfAccount,
                   text = text
               };

               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】关闭会话
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">客户openid</param>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="text">附加信息，文本会展示在客服人员的多客服客户端(非必须)</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.CloseSessionAsync", true)]
        public static async Task<WxJsonResult> CloseSessionAsync(string accessTokenOrAppId, string openId, string kfAccount, string text = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfsession/close?access_token={0}", accessToken.AsUrlData());

               var data = new
               {
                   openid = openId,
                   kf_account = kfAccount,
                   text = text
               };

               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取客户的会话状态
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">客户openid</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.GetSessionStateAsync", true)]
        public static async Task<GetSessionStateResultJson> GetSessionStateAsync(string accessTokenOrAppId, string openId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfsession/getsession?access_token={0}&openid={1}", accessToken.AsUrlData(), openId.AsUrlData());

               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetSessionStateResultJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取客服的会话列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号，账号前缀最多10个字符，必须是英文或者数字字符。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.GetSessionListAsync", true)]
        public static async Task<GetSessionListResultJson> GetSessionListAsync(string accessTokenOrAppId, string kfAccount, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfsession/getsessionlist?access_token={0}&kf_account={1}", accessToken.AsUrlData(), kfAccount.AsUrlData());

               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetSessionListResultJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取未接入会话列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.GetWaitCaseAsync", true)]
        public static async Task<GetWaitCaseResultJson> GetWaitCaseAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var urlFormat = string.Format(Config.ApiMpHost + "/customservice/kfsession/getwaitcase?access_token={0}", accessToken.AsUrlData());

               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetWaitCaseResultJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut).ConfigureAwait(false);

           }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取聊天记录
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="startTime">起始时间，unix时间戳</param>
        /// <param name="endTime">结束时间，unix时间戳，每次查询时段不能超过24小时</param>
        /// <param name="msgId">消息id顺序从小到大，从1开始</param>
        /// <param name="number">每次获取条数，最多10000条</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CustomServiceApi.GetMsgListAsync", true)]
        public static async Task<GetMsgListResultJson> GetMsgListAsync(string accessTokenOrAppId, DateTime startTime, DateTime endTime, long msgId, int number, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var urlFormat = string.Format(Config.ApiMpHost + "/customservice/msgrecord/getmsglist?access_token={0}", accessToken.AsUrlData());
               var data = new
               {
                   starttime = DateTimeHelper.GetUnixDateTime(startTime),
                   endtime = DateTimeHelper.GetUnixDateTime(endTime),
                   msgid = msgId,
                   number = number
               };
               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetMsgListResultJson>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);
           }, accessTokenOrAppId).ConfigureAwait(false);

        }
        #endregion
    }
}