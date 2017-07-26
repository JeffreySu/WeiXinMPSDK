
/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc

    文件名：ModifyDomainApi.cs
    文件功能描述：成员管理接口


    创建标识：Senparc - 20170629

    修改标识：Senparc - 20170726

    注意：此项目是《微信开发深度解析：微信公众号、小程序高效开发秘籍》图书中第5章的WeixinMarketing项目源代码。
    本项目只包含了运行案例所必须的学习代码，以及精简的部分SenparcCore框架代码，不确保其他方面的稳定性、安全性，
    因此，请勿直接用于商业项目，例如安全性、缓存等需要根据具体情况进行调试。

    盛派网络保留所有权利。
    
----------------------------------------------------------------*/

using Senparc.Weixin.Open.WxaAPIs.Tester;
using Senparc.Weixin.HttpUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;

namespace Senparc.Weixin.Open.WxaAPIs
{
    public class TesterApi
    {
        #region 同步接口

        /// <summary>
        /// 创建开放平台帐号并绑定公众号/小程序。
        /// 该API用于创建一个开放平台帐号，并将一个尚未绑定开放平台帐号的公众号/小程序绑定至该开放平台帐号上。新创建的开放平台帐号的主体信息将设置为与之绑定的公众号或小程序的主体。
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="appId">授权公众号或小程序的appid</param>
        /// <returns></returns>
        [Obsolete("此方法已过期，请使用Senparc.Weixin.Open.MpAPIs.Open.Create()方法")]
        public static CreateJsonResult CreateTester(string accessToken, string appId)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/open/create?access_token={0}";
            var data = new { appid = appId };
            return CommonJsonSend.Send<CreateJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 【同步接口】绑定小程序的体验者 接口
        /// </summary>
        /// <param name="accessToken">authorizer_access_token</param>
        /// <param name="wechatid">微信号</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static TesterResultJson BindTester(string accessToken, string wechatid, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format("https://api.weixin.qq.com/wxa/bind_tester?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                wechatid = wechatid.ToString()
            };

            return CommonJsonSend.Send<TesterResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 【同步接口】解除绑定小程序的体验者 接口
        /// </summary>
        /// <param name="accessToken">authorizer_access_token</param>
        /// <param name="wechatid">微信号</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static TesterResultJson UnBindTester(string accessToken, string wechatid, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format("https://api.weixin.qq.com/wxa/unbind_tester?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                wechatid = wechatid.ToString()
            };

            return CommonJsonSend.Send<TesterResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取公众号/小程序所绑定的开放平台帐号
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="appId">授权公众号或小程序的appid</param>
        /// <returns></returns>
        [Obsolete("此方法已过期，请使用Senparc.Weixin.Open.MpAPIs.Open.Get()方法")]
        public static GetJsonResult GetTester(string accessToken, string appId)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/open/get?access_token={0}";
            var data = new { appid = appId };
            return CommonJsonSend.Send<GetJsonResult>(accessToken, urlFormat, data);
        }

        #endregion


        #region 异步接口
        /// <summary>
        /// 【异步方法】创建开放平台帐号并绑定公众号/小程序。
        /// 该API用于创建一个开放平台帐号，并将一个尚未绑定开放平台帐号的公众号/小程序绑定至该开放平台帐号上。新创建的开放平台帐号的主体信息将设置为与之绑定的公众号或小程序的主体。
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="appId">授权公众号或小程序的appid</param>
        /// <returns></returns>
        [Obsolete("此方法已过期，请使用Senparc.Weixin.Open.MpAPIs.Open.CreateAsync()方法")]
        public static async Task<CreateJsonResult> CreateTesterAsync(string accessToken, string appId)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/open/create?access_token={0}";
            var data = new { appid = appId };
            return await CommonJsonSend.SendAsync<CreateJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 【异步接口】绑定小程序的体验者 接口
        /// </summary>
        /// <param name="accessToken">authorizer_access_token</param>
        /// <param name="wechatid">微信号</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<TesterResultJson> BindTesterSync(string accessToken, string wechatid, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format("https://api.weixin.qq.com/wxa/bind_tester?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                wechatid = wechatid.ToString()
            };

            return await CommonJsonSend.SendAsync<TesterResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
        /// <summary>
        /// 【异步接口】解除绑定小程序的体验者 接口
        /// </summary>
        /// <param name="accessToken">authorizer_access_token</param>
        /// <param name="wechatid">微信号</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<TesterResultJson> UnBindTesterSync(string accessToken, string wechatid, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format("https://api.weixin.qq.com/wxa/unbind_tester?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                wechatid = wechatid.ToString()
            };

            return await CommonJsonSend.SendAsync<TesterResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取公众号/小程序所绑定的开放平台帐号
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="appId">授权公众号或小程序的appid</param>
        /// <returns></returns>
        [Obsolete("此方法已过期，请使用Senparc.Weixin.Open.MpAPIs.Open.GetAsync()方法")]
        public static async Task<GetJsonResult> GetTesterAsync(string accessToken, string appId)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/open/get?access_token={0}";
            var data = new { appid = appId };
            return await CommonJsonSend.SendAsync<GetJsonResult>(accessToken, urlFormat, data);
        }

        #endregion
    }
}
