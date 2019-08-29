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
    
    文件名：SemanticApi.cs
    文件功能描述：语意理解接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
 
    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间

    修改标识：Senparc - 20170707
    修改描述：v14.5.1 完善异步方法async/await
----------------------------------------------------------------*/

/*
    API：http://mp.weixin.qq.com/wiki/0/0ce78b3c9524811fee34aba3e33f3448.html
    文档下载：http://mp.weixin.qq.com/wiki/static/assets/f48efdb46b4bca35caed4f01ca92e7da.zip
 */

using System.Threading.Tasks;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs.Semantic;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 语意理解接口
    /// </summary>
    public static class SemanticApi
    {
        #region 同步方法

        /// <summary>
        /// 发送语义理解请求
        /// </summary>
        /// <typeparam name="T">语意理解返回的结果类型，在 AdvancedAPIs/Semantic/SemanticResult </typeparam>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="semanticPostData">语义理解请求需要post的数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "SemanticApi.SemanticSend", true)]
        public static T SemanticSend<T>(string accessTokenOrAppId, SemanticPostData semanticPostData, int timeOut = Config.TIME_OUT)
            where T : WxJsonResult, new()
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/semantic/semproxy/search?access_token={0}";

                //switch (semanticPostData.category)
                //{
                //    case "restaurant":
                //        BaseSemanticResultJson as Semantic_RestaurantResult;
                //}

                return CommonJsonSend.Send<T>(accessToken, urlFormat, semanticPostData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 【异步方法】发送语义理解请求
        /// </summary>
        /// <typeparam name="T">语意理解返回的结果类型，在 AdvancedAPIs/Semantic/SemanticResult </typeparam>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="semanticPostData">语义理解请求需要post的数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "SemanticApi.SemanticSendAsync", true)]
        public static async Task<T> SemanticSendAsync<T>(string accessTokenOrAppId, SemanticPostData semanticPostData, int timeOut = Config.TIME_OUT)
            where T : WxJsonResult,new()
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/semantic/semproxy/search?access_token={0}";

                //switch (semanticPostData.category)
                //{
                //    case "restaurant":
                //        BaseSemanticResultJson as Semantic_RestaurantResult;
                //}

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<T>(accessToken, urlFormat, semanticPostData, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        #endregion
    }
}