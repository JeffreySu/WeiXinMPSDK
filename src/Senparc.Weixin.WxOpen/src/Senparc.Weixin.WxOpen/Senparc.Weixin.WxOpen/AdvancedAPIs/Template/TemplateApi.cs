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
    
    文件名：TemplateAPI.cs
    文件功能描述：小程序的模板消息接口

    修改标识：Senparc - 20170225
    修改描述：v1.2.1 修改模板消息URL

    修改标识：Senparc - 20170707
    修改描述：完善异步方法async/await

    修改标识：Senparc - 20170707
    修改描述：v1.7.4 完善模板消息发送参数

    修改标识：Senparc - 20180712
    修改描述：v2.0.11.2 修正 TemplateApi.Add() 方法返回类型

    修改标识：Senparc - 20180712
    修改描述：v2.4.1 TemplateApi.LibraryGet() 方法修正 API 地址

    修改标识：Senparc - 20181009
    修改描述：添加下发小程序和公众号统一的服务消息接口

----------------------------------------------------------------*/

/*
    API：https://mp.weixin.qq.com/debug/wxadoc/dev/api/notice.html#接口说明
 */

using System.Threading.Tasks;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Template
{
    /// <summary>
    /// 模板消息接口
    /// </summary>
    public static class TemplateApi
    {
        #region 同步方法

        /// <summary>
        /// 小程序模板消息接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="templateId"></param>
        /// <param name="data"></param>
        /// <param name="emphasisKeyword">模板需要放大的关键词，不填则默认无放大</param>
        /// <param name="color">模板内容字体的颜色，不填默认黑色（非必填）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="formId">表单提交场景下，为 submit 事件带上的 formId；支付场景下，为本次支付的 prepay_id</param>
        /// <param name="page">点击模板查看详情跳转页面，不填则模板无跳转（非必填）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TemplateApi.SendTemplateMessage", true)]
        public static WxJsonResult SendTemplateMessage(string accessTokenOrAppId, string openId, string templateId,
            object data, string formId, string page = null, string emphasisKeyword = null, string color = null, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/wxopen/template/send?access_token={0}";
                var msgData = new TempleteModel()
                {
                    touser = openId,
                    template_id = templateId,
                    color = color,
                    page = page,
                    form_id = formId,
                    data = data,
                    emphasis_keyword = emphasisKeyword,
                };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, msgData, timeOut: timeOut);

            }, accessTokenOrAppId);

            /*
            示例：
            {
              "touser": "OPENID",  
              "template_id": "TEMPLATE_ID", 
              "page": "index",          
              "form_id": "FORMID",         
              "data": {
                  "keyword1": {
                      "value": "339208499", 
                      "color": "#173177"
                  }, 
                  "keyword2": {
                      "value": "2015年01月05日 12:30", 
                      "color": "#173177"
                  }, 
                  "keyword3": {
                      "value": "粤海喜来登酒店", 
                      "color": "#173177"
                  } , 
                  "keyword4": {
                      "value": "广州市天河区天河路208号", 
                      "color": "#173177"
                  } 
              },
              "emphasis_keyword": "keyword1.DATA" 
            }
          */
        }

        /// <summary>
        /// 下发小程序和公众号统一的服务消息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="msgData"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TemplateApi.UniformSend", true)]
        public static WxJsonResult UniformSend(string accessTokenOrAppId, UniformSendData msgData, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/wxopen/template/uniform_send?access_token={0}";

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, msgData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #region 模板快速设置

        /// <summary>
        /// 获取小程序模板库标题列表
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="offset">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。</param>
        /// <param name="count">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TemplateApi.LibraryList", true)]
        public static LibraryListJsonResult LibraryList(string accessToken, int offset, int count, int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/cgi-bin/wxopen/template/library/list?access_token={0}";
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
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TemplateApi.LibraryGet", true)]
        public static LibraryGetJsonResult LibraryGet(string accessToken, string id, int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/cgi-bin/wxopen/template/library/get?access_token={0}";
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
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TemplateApi.Add", true)]
        public static AddJsonResult Add(string accessToken, string id, int[] keywordIdList, int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/cgi-bin/wxopen/template/add?access_token={0}";
            var data = new
            {
                id = id,
                keyword_id_list = keywordIdList
            };
            return CommonJsonSend.Send<AddJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
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
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TemplateApi.List", true)]
        public static ListJsonResult List(string accessToken, int offset, int count, int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/cgi-bin/wxopen/template/list?access_token={0}";
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
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TemplateApi.Del", true)]
        public static WxJsonResult Del(string accessToken, string templateId, int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/cgi-bin/wxopen/template/del?access_token={0}";
            var data = new
            {
                template_id = templateId
            };
            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }


        #endregion


        #endregion


        #region 异步方法

        /// <summary>
        /// 【异步方法】小程序模板消息接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="templateId"></param>
        /// <param name="data"></param>
        /// <param name="emphasisKeyword">模板需要放大的关键词，不填则默认无放大</param>
        /// <param name="color">模板内容字体的颜色，不填默认黑色（非必填）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="formId">表单提交场景下，为 submit 事件带上的 formId；支付场景下，为本次支付的 prepay_id</param>
        /// <param name="page">点击模板查看详情跳转页面，不填则模板无跳转（非必填）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TemplateApi.SendTemplateMessageAsync", true)]
        public static async Task<WxJsonResult> SendTemplateMessageAsync(string accessTokenOrAppId, string openId, string templateId, object data, string formId, string page = null, string emphasisKeyword = null, string color = null, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/wxopen/template/send?access_token={0}";
                var msgData = new TempleteModel()
                {
                    touser = openId,
                    template_id = templateId,
                    color = color,
                    page = page,
                    form_id = formId,
                    data = data,
                    emphasis_keyword = emphasisKeyword,
                };

                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, msgData, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】下发小程序和公众号统一的服务消息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="msgData"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TemplateApi.UniformSendAsync", true)]
        public static async Task<WxJsonResult> UniformSendAsync(string accessTokenOrAppId, UniformSendData msgData, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/wxopen/template/uniform_send?access_token={0}";

                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, msgData, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        #region 模板快速设置

        /// <summary>
        /// 【异步方法】获取小程序模板库标题列表
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="offset">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。</param>
        /// <param name="count">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TemplateApi.LibraryListAsync", true)]
        public static async Task<LibraryListJsonResult> LibraryListAsync(string accessToken, int offset, int count, int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/cgi-bin/wxopen/template/library/list?access_token={0}";
            var data = new
            {
                offset = offset,
                count = count
            };
            return await CommonJsonSend.SendAsync<LibraryListJsonResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取模板库某个模板标题下关键词库
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="id">模板标题id，可通过接口获取，也可登录小程序后台查看获取</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TemplateApi.LibraryGetAsync", true)]
        public static async Task<LibraryGetJsonResult> LibraryGetAsync(string accessToken, string id, int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/cgi-bin/wxopen/template/library/get?access_token={0}";
            var data = new
            {
                id = id
            };
            return await CommonJsonSend.SendAsync<LibraryGetJsonResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);
        }


        /// <summary>
        /// 【异步方法】组合模板并添加至帐号下的个人模板库
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="id">模板标题id，可通过接口获取，也可登录小程序后台查看获取</param>
        /// <param name="keywordIdList">开发者自行组合好的模板关键词列表，关键词顺序可以自由搭配（例如[3,5,4]或[4,5,3]），最多支持10个关键词组合</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TemplateApi.AddAsync", true)]
        public static async Task<AddJsonResult> AddAsync(string accessToken, string id, int[] keywordIdList, int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/cgi-bin/wxopen/template/add?access_token={0}";
            var data = new
            {
                id = id,
                keyword_id_list = keywordIdList
            };
            return await CommonJsonSend.SendAsync<AddJsonResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);
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
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TemplateApi.ListAsync", true)]
        public static async Task<ListJsonResult> ListAsync(string accessToken, int offset, int count, int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/cgi-bin/wxopen/template/list?access_token={0}";
            var data = new
            {
                offset = offset,
                count = count
            };
            return await CommonJsonSend.SendAsync<ListJsonResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】删除帐号下的某个模板
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="templateId">要删除的模板id</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TemplateApi.DelAsync", true)]
        public static async Task<WxJsonResult> DelAsync(string accessToken, string templateId, int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/cgi-bin/wxopen/template/del?access_token={0}";
            var data = new
            {
                template_id = templateId
            };
            return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);
        }


        #endregion


        #endregion
    }
}
