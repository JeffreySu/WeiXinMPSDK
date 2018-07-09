#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc
    
    文件名：CommentApi.cs
    文件功能描述：评论数据管理
    
    
    创建标识：Senparc - 20180131

    修改标识：Senparc - 20180318
    修改描述：v14.10.6 完善“查看指定文章的评论数据”接口（CommentApi.List()）的返回结果数据

----------------------------------------------------------------*/

/* 
   API地址：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1494572718_WzHIY
*/


using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.AdvancedAPIs.Comment.CommentJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 评论数据管理
    /// </summary>
    public static class CommentApi
    {

        #region 同步方法

        /// <summary>
        /// 打开已群发文章评论（新增接口）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="msg_data_id">群发返回的msg_data_id</param>
        /// <param name="index">（非必填）多图文时，用来指定第几篇图文，从0开始，不带默认返回该msg_data_id的第一篇图文</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult Open(string accessTokenOrAppId, uint msg_data_id, uint? index, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/comment/open?access_token={0}";
                var data = new
                {
                    msg_data_id = msg_data_id,
                    index = index
                };

                JsonSetting jsonSetting = new JsonSetting(ignoreNulls: true);
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 关闭已群发文章评论（新增接口）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="msg_data_id">群发返回的msg_data_id</param>
        /// <param name="index">（非必填）多图文时，用来指定第几篇图文，从0开始，不带默认返回该msg_data_id的第一篇图文</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult Close(string accessTokenOrAppId, uint msg_data_id, uint? index, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/comment/close?access_token={0}";
                var data = new
                {
                    msg_data_id = msg_data_id,
                    index = index
                };

                JsonSetting jsonSetting = new JsonSetting(ignoreNulls: true);
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 查看指定文章的评论数据（新增接口）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="msg_data_id">群发返回的msg_data_id</param>
        /// <param name="index">（非必填）多图文时，用来指定第几篇图文，从0开始，不带默认返回该msg_data_id的第一篇图文</param>
        /// <param name="begin">起始位置</param>
        /// <param name="count">获取数目（>=50会被拒绝）</param>
        /// <param name="type">type=0 普通评论&精选评论 type=1 普通评论 type=2 精选评论</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static ListResultJson List(string accessTokenOrAppId, uint msg_data_id, uint? index, uint begin, uint count, uint type, int timeOut = Config.TIME_OUT)
        {
            //返回JSON：
            /*
            {
            "errcode": 0,
            "errmsg": "ok",
            "comment": [
            {
            "user_comment_id": 9,
            "create_time": 1521255525,
            "content": "如果有什么大考验的话可能会发现自己啥都没改都白扯了吧",
            "comment_type": 0,
            "openid": "oufSm0Xw0nhuha_nWD6AfiZ3rgvA",
            "reply" :
                {
                    "content" : "CONTENT",
                    "create_time" : 1521265525
                }

            }
            ],
            "total": 1
            }
            */

            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/comment/list?access_token={0}";
                var data = new
                {
                    msg_data_id = msg_data_id,
                    index = index,
                    begin = begin,
                    count = count,
                    type = type
                };

                JsonSetting jsonSetting = new JsonSetting(ignoreNulls: true);
                return CommonJsonSend.Send<ListResultJson>(accessToken, urlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 将评论标记精选（新增接口）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="msg_data_id">群发返回的msg_data_id</param>
        /// <param name="index">（非必填）多图文时，用来指定第几篇图文，从0开始，不带默认返回该msg_data_id的第一篇图文</param>
        /// <param name="user_comment_id">用户评论id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult MarkElect(string accessTokenOrAppId, uint msg_data_id, uint? index, uint user_comment_id, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/comment/markelect?access_token={0}";
                var data = new
                {
                    msg_data_id = msg_data_id,
                    index = index,
                    user_comment_id = user_comment_id,
                };

                JsonSetting jsonSetting = new JsonSetting(ignoreNulls: true);
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 将评论取消精选（新增接口）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="msg_data_id">群发返回的msg_data_id</param>
        /// <param name="index">（非必填）多图文时，用来指定第几篇图文，从0开始，不带默认返回该msg_data_id的第一篇图文</param>
        /// <param name="user_comment_id">用户评论id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult UnmarkElect(string accessTokenOrAppId, uint msg_data_id, uint? index, uint user_comment_id, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/comment/unmarkelect?access_token={0}";
                var data = new
                {
                    msg_data_id = msg_data_id,
                    index = index,
                    user_comment_id = user_comment_id,
                };

                JsonSetting jsonSetting = new JsonSetting(ignoreNulls: true);
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 删除评论（新增接口）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="msg_data_id">群发返回的msg_data_id</param>
        /// <param name="index">（非必填）多图文时，用来指定第几篇图文，从0开始，不带默认返回该msg_data_id的第一篇图文</param>
        /// <param name="user_comment_id">用户评论id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult Delete(string accessTokenOrAppId, uint msg_data_id, uint? index, uint user_comment_id, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/comment/delete?access_token={0}";
                var data = new
                {
                    msg_data_id = msg_data_id,
                    index = index,
                    user_comment_id = user_comment_id,
                };

                JsonSetting jsonSetting = new JsonSetting(ignoreNulls: true);
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 回复评论（新增接口）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="msg_data_id">群发返回的msg_data_id</param>
        /// <param name="index">（非必填）多图文时，用来指定第几篇图文，从0开始，不带默认返回该msg_data_id的第一篇图文</param>
        /// <param name="user_comment_id">用户评论id</param>
        /// <param name="content">回复内容</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult ReplyAdd(string accessTokenOrAppId, uint msg_data_id, uint? index, uint user_comment_id, string content, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/comment/reply/add?access_token={0}";
                var data = new
                {
                    msg_data_id = msg_data_id,
                    index = index,
                    user_comment_id = user_comment_id,
                };

                JsonSetting jsonSetting = new JsonSetting(ignoreNulls: true);
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 删除回复（新增接口）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="msg_data_id">群发返回的msg_data_id</param>
        /// <param name="index">（非必填）多图文时，用来指定第几篇图文，从0开始，不带默认返回该msg_data_id的第一篇图文</param>
        /// <param name="user_comment_id">用户评论id</param>
        /// <param name="content">回复内容</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult ReplyDelete(string accessTokenOrAppId, uint msg_data_id, uint? index, uint user_comment_id, string content, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/comment/reply/delete?access_token={0}";
                var data = new
                {
                    msg_data_id = msg_data_id,
                    index = index,
                    user_comment_id = user_comment_id,
                };

                JsonSetting jsonSetting = new JsonSetting(ignoreNulls: true);
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppId);
        }


        #endregion

#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        ///【异步方法】 打开已群发文章评论（新增接口）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="msg_data_id">群发返回的msg_data_id</param>
        /// <param name="index">（非必填）多图文时，用来指定第几篇图文，从0开始，不带默认返回该msg_data_id的第一篇图文</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> OpenAsync(string accessTokenOrAppId, uint msg_data_id, uint? index, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/comment/open?access_token={0}";
                var data = new
                {
                    msg_data_id = msg_data_id,
                    index = index
                };

                JsonSetting jsonSetting = new JsonSetting(ignoreNulls: true);
                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】关闭已群发文章评论（新增接口）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="msg_data_id">群发返回的msg_data_id</param>
        /// <param name="index">（非必填）多图文时，用来指定第几篇图文，从0开始，不带默认返回该msg_data_id的第一篇图文</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static Task<WxJsonResult> CloseAsync(string accessTokenOrAppId, uint msg_data_id, uint? index, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/comment/close?access_token={0}";
                var data = new
                {
                    msg_data_id = msg_data_id,
                    index = index
                };

                JsonSetting jsonSetting = new JsonSetting(ignoreNulls: true);
                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 【异步方法】查看指定文章的评论数据（新增接口）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="msg_data_id">群发返回的msg_data_id</param>
        /// <param name="index">（非必填）多图文时，用来指定第几篇图文，从0开始，不带默认返回该msg_data_id的第一篇图文</param>
        /// <param name="begin">起始位置</param>
        /// <param name="count">获取数目（>=50会被拒绝）</param>
        /// <param name="type">type=0 普通评论&精选评论 type=1 普通评论 type=2 精选评论</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static Task<ListResultJson> ListAsync(string accessTokenOrAppId, uint msg_data_id, uint? index, uint begin, uint count, uint type, int timeOut = Config.TIME_OUT)
        {
            //返回JSON：
            /*
            {
            "errcode": 0,
            "errmsg": "ok",
            "comment": [
            {
            "user_comment_id": 9,
            "create_time": 1521255525,
            "content": "如果有什么大考验的话可能会发现自己啥都没改都白扯了吧",
            "comment_type": 0,
            "openid": "oufSm0Xw0nhuha_nWD6AfiZ3rgvA"
            }
            ],
            "total": 1
            }
            */

            return ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/comment/list?access_token={0}";
                var data = new
                {
                    msg_data_id = msg_data_id,
                    index = index,
                    begin = begin,
                    count = count,
                    type = type
                };

                JsonSetting jsonSetting = new JsonSetting(ignoreNulls: true);
                return CommonJsonSend.Send<ListResultJson>(accessToken, urlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】将评论标记精选（新增接口）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="msg_data_id">群发返回的msg_data_id</param>
        /// <param name="index">（非必填）多图文时，用来指定第几篇图文，从0开始，不带默认返回该msg_data_id的第一篇图文</param>
        /// <param name="user_comment_id">用户评论id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static Task<WxJsonResult> MarkElectAsync(string accessTokenOrAppId, uint msg_data_id, uint? index, uint user_comment_id, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/comment/markelect?access_token={0}";
                var data = new
                {
                    msg_data_id = msg_data_id,
                    index = index,
                    user_comment_id = user_comment_id,
                };

                JsonSetting jsonSetting = new JsonSetting(ignoreNulls: true);
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 【异步方法】将评论取消精选（新增接口）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="msg_data_id">群发返回的msg_data_id</param>
        /// <param name="index">（非必填）多图文时，用来指定第几篇图文，从0开始，不带默认返回该msg_data_id的第一篇图文</param>
        /// <param name="user_comment_id">用户评论id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static Task<WxJsonResult> UnmarkElectAsync(string accessTokenOrAppId, uint msg_data_id, uint? index, uint user_comment_id, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/comment/unmarkelect?access_token={0}";
                var data = new
                {
                    msg_data_id = msg_data_id,
                    index = index,
                    user_comment_id = user_comment_id,
                };

                JsonSetting jsonSetting = new JsonSetting(ignoreNulls: true);
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】删除评论（新增接口）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="msg_data_id">群发返回的msg_data_id</param>
        /// <param name="index">（非必填）多图文时，用来指定第几篇图文，从0开始，不带默认返回该msg_data_id的第一篇图文</param>
        /// <param name="user_comment_id">用户评论id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static Task<WxJsonResult> DeleteAsync(string accessTokenOrAppId, uint msg_data_id, uint? index, uint user_comment_id, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/comment/delete?access_token={0}";
                var data = new
                {
                    msg_data_id = msg_data_id,
                    index = index,
                    user_comment_id = user_comment_id,
                };

                JsonSetting jsonSetting = new JsonSetting(ignoreNulls: true);
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】回复评论（新增接口）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="msg_data_id">群发返回的msg_data_id</param>
        /// <param name="index">（非必填）多图文时，用来指定第几篇图文，从0开始，不带默认返回该msg_data_id的第一篇图文</param>
        /// <param name="user_comment_id">用户评论id</param>
        /// <param name="content">回复内容</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static Task<WxJsonResult> ReplyAddAsync(string accessTokenOrAppId, uint msg_data_id, uint? index, uint user_comment_id, string content, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/comment/reply/add?access_token={0}";
                var data = new
                {
                    msg_data_id = msg_data_id,
                    index = index,
                    user_comment_id = user_comment_id,
                };

                JsonSetting jsonSetting = new JsonSetting(ignoreNulls: true);
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】删除回复（新增接口）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="msg_data_id">群发返回的msg_data_id</param>
        /// <param name="index">（非必填）多图文时，用来指定第几篇图文，从0开始，不带默认返回该msg_data_id的第一篇图文</param>
        /// <param name="user_comment_id">用户评论id</param>
        /// <param name="content">回复内容</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static Task<WxJsonResult> ReplyDeleteAsync(string accessTokenOrAppId, uint msg_data_id, uint? index, uint user_comment_id, string content, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var urlFormat = Config.ApiMpHost + "/cgi-bin/comment/reply/delete?access_token={0}";
               var data = new
               {
                   msg_data_id = msg_data_id,
                   index = index,
                   user_comment_id = user_comment_id,
               };

               JsonSetting jsonSetting = new JsonSetting(ignoreNulls: true);
               return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting);
           }, accessTokenOrAppId);
        }

        #endregion
#endif
    }
}
