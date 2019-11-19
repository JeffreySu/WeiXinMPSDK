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
  
    修改标识：Senparc - 20160621
    修改描述：修改命名空间
              其改为Senparc.Weixin.MP.AdvancedAPIs      
    修改标识：Senparc - 20160719
    修改描述：增加其接口的异步方法

    修改标识：Senparc - 20170707
    修改描述：v14.5.1 完善异步方法async/await
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.Url;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 长短链接接口
    /// </summary>
    public class UrlApi
    {
        /*
        接口地址：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1443433600&token=&lang=zh_CN
                    将一条长链接转成短链接。
        主要使用场景： 开发者用于生成二维码的原链接（商品、支付二维码等）太长导致扫码速度和成功率下降，将原长链接通过此接口转成短链接再生成二维码将大大提升扫码速度和成功率。
        */
        #region 同步方法
        
        
        ///  <summary>
        /// 将一条长链接转成短链接。
        ///  </summary>
        ///  <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        ///  <param name="action">此处填long2short，代表长链接转短链接</param>
        ///  <param name="longUrl">需要转换的长链接，支持http://、https://、weixin://wxpay 格式的url</param>
        /// <param name="timeOut">请求超时时间</param>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "UrlApi.ShortUrl", true)]
        public static ShortUrlResult ShortUrl(string accessTokenOrAppId, string action, string longUrl, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/shorturl?access_token={0}";
                var data = new
                {
                    action = action,
                    long_url = longUrl
                };
                return CommonJsonSend.Send<ShortUrlResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        #endregion

        #region 异步方法
         ///  <summary>
        /// 将一条长链接转成短链接。
        ///  </summary>
        ///  <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        ///  <param name="action">此处填long2short，代表长链接转短链接</param>
        ///  <param name="longUrl">需要转换的长链接，支持http://、https://、weixin://wxpay 格式的url</param>
        /// <param name="timeOut">请求超时时间</param>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "UrlApi.ShortUrlAsync", true)]
        public static async Task<ShortUrlResult> ShortUrlAsync(string accessTokenOrAppId, string action, string longUrl, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/shorturl?access_token={0}";
                var data = new
                {
                    action = action,
                    long_url = longUrl
                };
                return await Senparc.Weixin .CommonAPIs .CommonJsonSend.SendAsync<ShortUrlResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        #endregion
    }
}
