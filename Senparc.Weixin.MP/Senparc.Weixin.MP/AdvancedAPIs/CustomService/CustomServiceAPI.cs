﻿/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：CustomServiceAPI.cs
    文件功能描述：多客服接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20150306
    修改描述：增加多客服接口
 
    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间
----------------------------------------------------------------*/

/* 
    多客服接口聊天记录接口，官方API：http://mp.weixin.qq.com/wiki/index.php?title=%E8%8E%B7%E5%8F%96%E5%AE%A2%E6%9C%8D%E8%81%8A%E5%A4%A9%E8%AE%B0%E5%BD%95
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.CustomService
{
    /// <summary>
    /// 多客服接口
    /// </summary>
    public static class CustomServiceApi
    {
        /// <summary>
        /// 获取用户聊天记录
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="startTime">查询开始时间，会自动转为UNIX时间戳</param>
        /// <param name="endTime">查询结束时间，会自动转为UNIX时间戳，每次查询不能跨日查询</param>
        /// <param name="openId">（非必须）普通用户的标识，对当前公众号唯一</param>
        /// <param name="pageSize">每页大小，每页最多拉取1000条</param>
        /// <param name="pageIndex">查询第几页，从1开始</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetRecordResult GetRecord(string accessToken, DateTime startTime, DateTime endTime, string openId = null, int pageSize = 10, int pageIndex = 1, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/customservice/getrecord?access_token={0}";

            //规范页码
            if (pageSize <= 0)
            {
                pageSize = 1;
            }
            else if (pageSize > 1000)
            {
                pageSize = 1000;
            }

            //组装发送消息
            var data = new
            {
                starttime = DateTimeHelper.GetWeixinDateTime(startTime),
                endtime = DateTimeHelper.GetWeixinDateTime(endTime),
                openId = openId,
                pagesize = pageSize,
                pageIndex = pageIndex
            };

            return CommonJsonSend.Send<GetRecordResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }

        /// <summary>
        /// 获取客服基本信息
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CustomInfoJson GetCustomBasicInfo(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/cgi-bin/customservice/getkflist?access_token={0}", accessToken);
            return CommonJsonSend.Send<CustomInfoJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
            //return GetCustomInfoResult<CustomInfoJson>(urlFormat);
        }

        /// <summary>
        /// 获取在线客服接待信息
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CustomOnlineJson GetCustomOnlineInfo(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/cgi-bin/customservice/getonlinekflist?access_token={0}", accessToken);
            return CommonJsonSend.Send<CustomOnlineJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
            //return GetCustomInfoResult<CustomOnlineJson>(urlFormat);
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
        /// <param name="accessToken"></param>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号，账号前缀最多10个字符，必须是英文或者数字字符。如果没有公众号微信号，请前往微信公众平台设置。</param>
        /// <param name="nickName">客服昵称，最长6个汉字或12个英文字符</param>
        /// <param name="passWord">客服账号登录密码，格式为密码明文的32位加密MD5值</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult AddCustom(string accessToken, string kfAccount, string nickName, string passWord, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/customservice/kfaccount/add?access_token={0}", accessToken);

            var data = new
            {
                kf_account = kfAccount,
                nickname = nickName,
                password = passWord
            };

            return CommonJsonSend.Send<CustomOnlineJson>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 设置客服信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号，账号前缀最多10个字符，必须是英文或者数字字符。如果没有公众号微信号，请前往微信公众平台设置。</param>
        /// <param name="nickName">客服昵称，最长6个汉字或12个英文字符</param>
        /// <param name="passWord">客服账号登录密码，格式为密码明文的32位加密MD5值</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult UpdateCustom(string accessToken, string kfAccount, string nickName, string passWord, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/customservice/kfaccount/update?access_token={0}", accessToken);

            var data = new
            {
                kf_account = kfAccount,
                nickname = nickName,
                password = passWord
            };

            return CommonJsonSend.Send<CustomOnlineJson>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 上传客服头像
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="file">form-data中媒体文件标识，有filename、filelength、content-type等信息</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult UploadCustomHeadimg(string accessToken, string kfAccount, string file, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format("http://api.weixin.qq.com/customservice/kfaccount/uploadheadimg?access_token={0}&kf_account={1}", accessToken, kfAccount);
            var fileDictionary = new Dictionary<string, string>();
            fileDictionary["media"] = file;
            return HttpUtility.Post.PostFileGetJson<WxJsonResult>(url, null, fileDictionary, null, timeOut: timeOut);
        }

        /// <summary>
        /// 删除客服账号
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult DeleteCustom(string accessToken, string kfAccount, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/customservice/kfaccount/del?access_token={0}&kf_account={1}", accessToken, kfAccount);
            return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
        }

        /// <summary>
        /// 创建会话
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId">客户openid</param>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="text">附加信息，文本会展示在客服人员的多客服客户端(非必须)</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult CreateSession(string accessToken, string openId, string kfAccount, string text = null, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/customservice/kfsession/create?access_token={0}", accessToken);

            var data = new
            {
                openid = openId,
                kf_account = kfAccount,
                text = text
            };

            return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 关闭会话
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId">客户openid</param>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="text">附加信息，文本会展示在客服人员的多客服客户端(非必须)</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult CloseSession(string accessToken, string openId, string kfAccount, string text = null, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/customservice/kfsession/close?access_token={0}", accessToken);

            var data = new
            {
                openid = openId,
                kf_account = kfAccount,
                text = text
            };

            return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 获取客户的会话状态
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId">客户openid</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetSessionStateResultJson GetSessionState(string accessToken, string openId, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/customservice/kfsession/getsession?access_token={0}&openid={1}", accessToken, openId);

            return CommonJsonSend.Send<GetSessionStateResultJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
        }

        /// <summary>
        /// 获取客服的会话列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号，账号前缀最多10个字符，必须是英文或者数字字符。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetSessionListResultJson GetSessionList(string accessToken, string kfAccount, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/customservice/kfsession/getsessionlist?access_token={0}&kf_account={1}", accessToken, kfAccount);

            return CommonJsonSend.Send<GetSessionListResultJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
        }

        /// <summary>
        /// 获取未接入会话列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetWaitCaseResultJson GetWaitCase(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/customservice/kfsession/getwaitcase?access_token={0}", accessToken);

            return CommonJsonSend.Send<GetWaitCaseResultJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
        }
    }
}