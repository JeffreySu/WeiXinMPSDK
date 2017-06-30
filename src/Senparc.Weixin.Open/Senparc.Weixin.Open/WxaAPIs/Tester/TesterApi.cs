
/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc

    文件名：ModifyDomainApi.cs
    文件功能描述：成员管理接口


    创建标识：Senparc - 20170629
    
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
        #endregion

        #region 异步接口
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
        #endregion
    }
}
