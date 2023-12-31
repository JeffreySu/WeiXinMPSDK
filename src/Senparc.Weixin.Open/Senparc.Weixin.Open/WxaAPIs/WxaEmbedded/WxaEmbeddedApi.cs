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
    Copyright (C) 2024 Senparc
  
    文件名：WxaEmbeddedApi.cs
    文件功能描述：半屏小程序管理接口
    
    
    创建标识：mc7246 - 20220706

----------------------------------------------------------------*/


using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Open.WxaAPIs.WxaEmbedded;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    /// <summary>
    /// 半屏小程序管理
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Open, true)]
    public class WxaEmbeddedApi
    {
        #region 同步方法
        /// <summary>
        /// 添加半屏小程序
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="appid">添加的半屏小程序 appid</param>
        /// <param name="apply_reason">申请理由，不超过 30 个字</param>
        /// <returns></returns>
        public static WxJsonResult AddEmbedded(string accessToken, string appid, string apply_reason)
        {
            var url = $"{Config.ApiMpHost}/wxaapi/wxaembedded/add_embedded?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                appid,
                apply_reason
            };

            return CommonJsonSend.Send<WxJsonResult>(null, url, data);
        }


        /// <summary>
        /// 删除半屏小程序
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="appid">已添加的半屏小程序appid</param>
        /// <returns></returns>
        public static WxJsonResult DelEmbedded(string accessToken, string appid)
        {
            var url = $"{Config.ApiMpHost}/wxaapi/wxaembedded/del_embedded?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                appid
            };

            return CommonJsonSend.Send<WxJsonResult>(null, url, data);
        }

        /// <summary>
        /// 取消授权小程序
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="flag">半屏小程序授权方式。0表示需要管理员验证；1表示自动通过；2表示自动拒绝。</param>
        /// <returns></returns>
        public static WxJsonResult DelAuthorize(string accessToken, int flag)
        {
            var url = $"{Config.ApiMpHost}/wxaapi/wxaembedded/del_authorize?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                flag
            };

            return CommonJsonSend.Send<WxJsonResult>(null, url, data);
        }

        /// <summary>
        /// 获取半屏小程序调用列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="start">query参数，分页起始值 ，默认值为0</param>
        /// <param name="num">query参数，一次拉取最大值，最大 1000，默认值为10</param>
        /// <returns></returns>
        public static GetListJsonResult GetList(string accessToken, int start=0, int num=10)
        {
            var url = $"{Config.ApiMpHost}/wxaapi/wxaembedded/get_list?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                start,
                num
            };

            return CommonJsonSend.Send<GetListJsonResult>(null, url, data, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 获取半屏小程序授权列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="start"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static GetListJsonResult GetOwnList(string accessToken, int start = 0, int num = 10)
        {
            var url = $"{Config.ApiMpHost}/wxaapi/wxaembedded/get_own_list?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                start,
                num
            };

            return CommonJsonSend.Send<GetListJsonResult>(null, url, data, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 设置授权方式
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="flag">半屏小程序授权方式。0表示需要管理员验证；1表示自动通过；2表示自动拒绝。</param>
        /// <returns></returns>
        public static WxJsonResult SetAuthorize(string accessToken, int flag)
        {
            var url = $"{Config.ApiMpHost}/wxaapi/wxaembedded/get_own_list?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                flag
            };

            return CommonJsonSend.Send<WxJsonResult>(null, url, data);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 添加半屏小程序
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="appid">添加的半屏小程序 appid</param>
        /// <param name="apply_reason">申请理由，不超过 30 个字</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> AddEmbeddedAsync(string accessToken, string appid, string apply_reason)
        {
            var url = $"{Config.ApiMpHost}/wxaapi/wxaembedded/add_embedded?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                appid,
                apply_reason
            };

            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data).ConfigureAwait(false);
        }


        /// <summary>
        /// 删除半屏小程序
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="appid">已添加的半屏小程序appid</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> DelEmbeddedAsync(string accessToken, string appid)
        {
            var url = $"{Config.ApiMpHost}/wxaapi/wxaembedded/del_embedded?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                appid
            };

            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data).ConfigureAwait(false);
        }

        /// <summary>
        /// 取消授权小程序
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="flag">半屏小程序授权方式。0表示需要管理员验证；1表示自动通过；2表示自动拒绝。</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> DelAuthorizeAsync(string accessToken, int flag)
        {
            var url = $"{Config.ApiMpHost}/wxaapi/wxaembedded/del_authorize?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                flag
            };

            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取半屏小程序调用列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="start">query参数，分页起始值 ，默认值为0</param>
        /// <param name="num">query参数，一次拉取最大值，最大 1000，默认值为10</param>
        /// <returns></returns>
        public static async Task<GetListJsonResult> GetListAsync(string accessToken, int start=0, int num=10)
        {
            var url = $"{Config.ApiMpHost}/wxaapi/wxaembedded/get_list?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                start,
                num
            };

            return await CommonJsonSend.SendAsync<GetListJsonResult>(null, url, data, CommonJsonSendType.GET).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取半屏小程序授权列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="start"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static async Task<GetListJsonResult> GetOwnListAsync(string accessToken, int start = 0, int num = 10)
        {
            var url = $"{Config.ApiMpHost}/wxaapi/wxaembedded/get_own_list?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                start,
                num
            };

            return await CommonJsonSend.SendAsync<GetListJsonResult>(null, url, data, CommonJsonSendType.GET).ConfigureAwait(false);
        }

        /// <summary>
        /// 设置授权方式
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="flag">半屏小程序授权方式。0表示需要管理员验证；1表示自动通过；2表示自动拒绝。</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> SetAuthorizeAsync(string accessToken, int flag)
        {
            var url = $"{Config.ApiMpHost}/wxaapi/wxaembedded/get_own_list?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                flag
            };

            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data).ConfigureAwait(false);
        }
        #endregion

    }
}
