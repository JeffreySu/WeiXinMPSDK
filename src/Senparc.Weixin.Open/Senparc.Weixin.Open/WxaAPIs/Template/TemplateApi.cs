#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2017 Senparc
    
    文件名：SnsApi.cs
    文件功能描述：小程序微信登录
    
    创建标识：Senparc - 20170827

----------------------------------------------------------------*/

using System;
using System.IO;
using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.Open.WxaAPIs.Template.TemplateJson;

namespace Senparc.Weixin.Open.WxaAPIs.Template
{
    /// <summary>
    /// 小程序模板消息接口
    /// </summary>
    public static class TemplateApi
    {
        #region 同步请求


        #region 模板快速设置

        /// <summary>
        /// 获取小程序模板库标题列表
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="offset">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。</param>
        /// <param name="count">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static LibraryListJsonResult LibraryList(string accessToken, int offset, int count, int timeOut = Config.TIME_OUT)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/wxopen/template/library/list?access_token={0}";
            var data = new
            {
                offset = offset,
                count = count
            };
            return CommonJsonSend.Send<LibraryListJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }

        /// <summary>
        /// 获取模板库某个模板标题下关键词库
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="id">模板标题id，可通过接口获取，也可登录小程序后台查看获取</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static LibraryGetJsonResult LibraryGet(string accessToken, string id, int timeOut = Config.TIME_OUT)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/wxopen/template/library/list?access_token={0}";
            var data = new
            {
                id = id
            };
            return CommonJsonSend.Send<LibraryGetJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }


        /// <summary>
        /// 组合模板并添加至帐号下的个人模板库
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="id">模板标题id，可通过接口获取，也可登录小程序后台查看获取</param>
        /// <param name="keywordIdList">开发者自行组合好的模板关键词列表，关键词顺序可以自由搭配（例如[3,5,4]或[4,5,3]），最多支持10个关键词组合</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static LibraryGetJsonResult Add(string accessToken, string id, int[] keywordIdList, int timeOut = Config.TIME_OUT)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/wxopen/template/add?access_token={0}";
            var data = new
            {
                id = id,
                keyword_id_list = keywordIdList
            };
            return CommonJsonSend.Send<LibraryGetJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }

        #endregion


        #region 对已存在模板进行操作

        /// <summary>
        /// 获取帐号下已存在的模板列表
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="offset">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。最后一页的list长度可能小于请求的count</param>
        /// <param name="count">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。最后一页的list长度可能小于请求的count</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static ListJsonResult List(string accessToken, int offset, int count, int timeOut = Config.TIME_OUT)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/wxopen/template/list?access_token={0}";
            var data = new
            {
                offset = offset,
                count = count
            };
            return CommonJsonSend.Send<ListJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }

        /// <summary>
        /// 删除帐号下的某个模板
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="templateId">要删除的模板id</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static WxJsonResult Del(string accessToken, string templateId, int timeOut = Config.TIME_OUT)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/wxopen/template/del?access_token={0}";
            var data = new
            {
                template_id = templateId
            };
            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }


        #endregion

        #endregion

        #region 异步请求


        #region 模板快速设置

        /// <summary>
        /// 【异步方法】获取小程序模板库标题列表
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="offset">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。</param>
        /// <param name="count">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static async Task<LibraryListJsonResult> LibraryListAsync(string accessToken, int offset, int count, int timeOut = Config.TIME_OUT)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/wxopen/template/library/list?access_token={0}";
            var data = new
            {
                offset = offset,
                count = count
            };
            return await CommonJsonSend.SendAsync<LibraryListJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }

        /// <summary>
        /// 【异步方法】获取模板库某个模板标题下关键词库
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="id">模板标题id，可通过接口获取，也可登录小程序后台查看获取</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static async Task<LibraryGetJsonResult> LibraryGetAsync(string accessToken, string id, int timeOut = Config.TIME_OUT)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/wxopen/template/library/list?access_token={0}";
            var data = new
            {
                id = id
            };
            return await CommonJsonSend.SendAsync<LibraryGetJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }


        /// <summary>
        /// 【异步方法】组合模板并添加至帐号下的个人模板库
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="id">模板标题id，可通过接口获取，也可登录小程序后台查看获取</param>
        /// <param name="keywordIdList">开发者自行组合好的模板关键词列表，关键词顺序可以自由搭配（例如[3,5,4]或[4,5,3]），最多支持10个关键词组合</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static async Task<LibraryGetJsonResult> AddAsync(string accessToken, string id, int[] keywordIdList, int timeOut = Config.TIME_OUT)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/wxopen/template/add?access_token={0}";
            var data = new
            {
                id = id,
                keyword_id_list = keywordIdList
            };
            return await CommonJsonSend.SendAsync<LibraryGetJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }

        #endregion


        #region 对已存在模板进行操作

        /// <summary>
        /// 【异步方法】获取帐号下已存在的模板列表
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="offset">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。最后一页的list长度可能小于请求的count</param>
        /// <param name="count">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。最后一页的list长度可能小于请求的count</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static async Task<ListJsonResult> ListAsync(string accessToken, int offset, int count, int timeOut = Config.TIME_OUT)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/wxopen/template/list?access_token={0}";
            var data = new
            {
                offset = offset,
                count = count
            };
            return await CommonJsonSend.SendAsync<ListJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }

        /// <summary>
        /// 【异步方法】删除帐号下的某个模板
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="templateId">要删除的模板id</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> DelAsync(string accessToken, string templateId, int timeOut = Config.TIME_OUT)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/wxopen/template/del?access_token={0}";
            var data = new
            {
                template_id = templateId
            };
            return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }


        #endregion


        #endregion
    }
}