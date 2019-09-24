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

using Senparc.Weixin.MP.AdvancedAPIs.UserTag;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 用户标签接口
    /// </summary>
    public class UserTagApi
    {
        #region 同步方法

        /// <summary>
        /// 创建标签
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="name"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "UserTagApi.Create", true)]
        public static CreateTagResult Create(string accessTokenOrAppId, string name, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/tags/create?access_token={0}";
                var data = new
                {
                    tag = new
                    {
                        name = name
                    }
                };
                return CommonJsonSend.Send<CreateTagResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 获取公众号已创建的标签
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "UserTagApi.Get", true)]
        public static TagJson Get(string accessTokenOrAppId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/tags/get?access_token={0}";
                var url = string.Format(urlFormat, accessToken);
                return CommonJsonSend.Send<TagJson>(null, url, null, CommonJsonSendType.GET);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 编辑标签
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "UserTagApi.Update", true)]
        public static WxJsonResult Update(string accessTokenOrAppId, int id, string name, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/tags/update?access_token={0}";
                var data = new
                {
                    tag = new
                    {
                        id = id,
                        name = name
                    }
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="id"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "UserTagApi.Delete", true)]
        public static WxJsonResult Delete(string accessTokenOrAppId, int id, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/tags/delete?access_token={0}";

                var data = new
                {
                    tag = new
                    {
                        id = id
                    }
                };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 获取标签下粉丝列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="tagid"></param>
        /// <param name="nextOpenid"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "UserTagApi.Get", true)]
        public static UserTagJsonResult Get(string accessTokenOrAppId, int tagid, string nextOpenid = "", int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/user/tag/get?access_token={0}";
                var data = new
                {
                    tagid = tagid,
                    next_openid = nextOpenid
                };
                return CommonJsonSend.Send<UserTagJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 批量为用户打标签
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="tagid"></param>
        /// <param name="openid_list"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "UserTagApi.BatchTagging", true)]
        public static WxJsonResult BatchTagging(string accessTokenOrAppId, int tagid, List<string> openid_list, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/tags/members/batchtagging?access_token={0}";
                var data = new
                {
                    openid_list = openid_list,
                    tagid = tagid
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 批量为用户取消标签
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="tagid"></param>
        /// <param name="openid_list"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "UserTagApi.BatchUntagging", true)]
        public static WxJsonResult BatchUntagging(string accessTokenOrAppId, int tagid, List<string> openid_list, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/tags/members/batchuntagging?access_token={0}";
                var data = new
                {
                    openid_list = openid_list,
                    tagid = tagid
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 获取用户身上的标签列表
        /// </summary>
        /// <param name="accessTokenOrAppid"></param>
        /// <param name="openid"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "UserTagApi.UserTagList", true)]
        public static UserTagListResult UserTagList(string accessTokenOrAppid, string openid, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/tags/getidlist?access_token={0}";
                var data = new
                {
                    openid = openid
                };
                return CommonJsonSend.Send<UserTagListResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppid);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 【异步方法】创建标签
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="name"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "UserTagApi.CreateAsync", true)]
        public static async Task<CreateTagResult> CreateAsync(string accessTokenOrAppId, string name, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/tags/create?access_token={0}";
                var data = new
                {
                    tag = new
                    {
                        name = name
                    }
                };
                return await CommonJsonSend.SendAsync<CreateTagResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】获取公众号已创建的标签
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "UserTagApi.GetAsync", true)]
        public static async Task<TagJson> GetAsync(string accessTokenOrAppId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/tags/get?access_token={0}";
                var url = string.Format(urlFormat, accessToken);
                return await CommonJsonSend.SendAsync<TagJson>(null, url, null, CommonJsonSendType.GET).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】编辑标签
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "UserTagApi.UpdateAsync", true)]
        public static async Task<WxJsonResult> UpdateAsync(string accessTokenOrAppId, int id, string name, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/tags/update?access_token={0}";
                var data = new
                {
                    tag = new
                    {
                        id = id,
                        name = name
                    }
                };
                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】删除标签
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="id"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "UserTagApi.DeleteAsync", true)]
        public static async Task<WxJsonResult> DeleteAsync(string accessTokenOrAppId, int id, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/tags/delete?access_token={0}";

                var data = new
                {
                    tag = new
                    {
                        id = id
                    }
                };

                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】获取标签下粉丝列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="tagid"></param>
        /// <param name="nextOpenid"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "UserTagApi.GetAsync", true)]
        public static async Task<UserTagJsonResult> GetAsync(string accessTokenOrAppId, int tagid, string nextOpenid = "", int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/user/tag/get?access_token={0}";
                var data = new
                {
                    tagid = tagid,
                    next_openid = nextOpenid
                };
                return await CommonJsonSend.SendAsync<UserTagJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】批量为用户打标签
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="tagid"></param>
        /// <param name="openid_list"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "UserTagApi.BatchTaggingAsync", true)]
        public static async Task<WxJsonResult> BatchTaggingAsync(string accessTokenOrAppId, int tagid, List<string> openid_list, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/tags/members/batchtagging?access_token={0}";
                var data = new
                {
                    openid_list = openid_list,
                    tagid = tagid
                };
                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】批量为用户取消标签
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="tagid"></param>
        /// <param name="openid_list"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "UserTagApi.BatchUntaggingAsync", true)]
        public static async Task<WxJsonResult> BatchUntaggingAsync(string accessTokenOrAppId, int tagid, List<string> openid_list, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/tags/members/batchuntagging?access_token={0}";
                var data = new
                {
                    openid_list = openid_list,
                    tagid = tagid
                };
                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】获取用户身上的标签列表
        /// </summary>
        /// <param name="accessTokenOrAppid"></param>
        /// <param name="openid"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "UserTagApi.UserTagListAsync", true)]
        public static async Task<UserTagListResult> UserTagListAsync(string accessTokenOrAppid, string openid, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/tags/getidlist?access_token={0}";
                var data = new
                {
                    openid = openid
                };
                return await CommonJsonSend.SendAsync<UserTagListResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppid).ConfigureAwait(false);
        }
        #endregion
    }
}
