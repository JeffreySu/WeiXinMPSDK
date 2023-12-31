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
    
    文件名：TemplateApi.cs
    文件功能描述：公众号订阅通知
    
    创建标识：Senparc - 20210302

    修改标识：ccccccmd - 20210329
    修改描述：v16.11.201 服务号订阅通知相关接口&补充小程序[获取小程序账号的类目]接口

    修改标识：Senparc - 20210504
    修改描述：v16.12.101 修改“addTemplate选用模板”接口

----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Threading.Tasks;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Entities.TemplateMessage;
using Senparc.Weixin.MP.AdvancedAPIs.NewTmpl.NewTmplJson;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;

namespace Senparc.Weixin.MP.AdvancedAPIs.NewTmpl
{
    //文档：https://developers.weixin.qq.com/doc/offiaccount/Subscription_Messages/intro.html

    /// <summary>
    /// 公众号订阅消息模板的管理
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_OfficialAccount,true)]
    public static class NewTmplApi
    {
        #region 同步方法
        #region 模板快速设置
        /// <summary>
        /// 获取类目下的公共模板，可从中选用模板使用
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="ids">类目 id，多个用逗号隔开</param>
        /// <param name="start">用于分页，表示从 start 开始。从 0 开始计数。</param>
        /// <param name="limit">用于分页，表示拉取 limit 条记录。最大为 30。</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static GetPubTemplateTitlesJsonResult GetPubTemplateTitles(string accessToken, string ids, int start,
            int limit, int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/wxaapi/newtmpl/getpubtemplatetitles?access_token={0}";
            urlFormat = $"{urlFormat}&ids={ids}&start={start}&limit={limit}";
            return CommonJsonSend.Send<GetPubTemplateTitlesJsonResult>(accessToken, urlFormat, null,
                CommonJsonSendType.GET, timeOut: timeOut);
        }

        /// <summary>
        /// 获取公共模板下的关键词列表
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="tid">模板标题 id，可通过接口获取</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static GetPubTemplateKeyWordsByIdJsonResult GetPubTemplateKeyWordsById(string accessToken, string tid,
            int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/wxaapi/newtmpl/getpubtemplatekeywords?access_token={0}";
            urlFormat = $"{urlFormat}&tid={tid}";
            return CommonJsonSend.Send<GetPubTemplateKeyWordsByIdJsonResult>(accessToken, urlFormat, null,
                CommonJsonSendType.GET, timeOut: timeOut);
        }





        /// <summary>
        /// 从公共模板库中选用模板，到私有模板库中
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="tid">模板标题 id，可通过getPubTemplateTitleList接口获取，也可登录公众号后台查看获取</param>
        /// <param name="kidList">开发者自行组合好的模板关键词列表，关键词顺序可以自由搭配（例如 [3,5,4] 或 [4,5,3]），最多支持5个，最少2个关键词组合</param>
        /// <param name="sceneDesc">服务场景描述，15个字以内</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static AddTemplateJsonResult AddTemplate(string accessToken, string tid, int[] kidList,
            string sceneDesc, int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/wxaapi/newtmpl/addtemplate?access_token={0}";

            var data = new {
                tid = tid,
                kidList = kidList,
                sceneDesc = sceneDesc
            };
           
            return CommonJsonSend.Send<AddTemplateJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }
        #endregion


        #region 对已存在模板进行操作
        /// <summary>
        /// 获取私有的模板列表
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static GetTemplateListJsonResult GetTemplateList(string accessToken, int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/wxaapi/newtmpl/gettemplate?access_token={0}";
            return CommonJsonSend.Send<GetTemplateListJsonResult>(accessToken, urlFormat, null, CommonJsonSendType.GET,
                timeOut: timeOut);
        }

        /// <summary>
        /// 获取公众号所属类目，可用于查询类目下的公共模板
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static object GetCategory(string accessToken, int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/wxaapi/newtmpl/getcategory?access_token={0}";
            return CommonJsonSend.Send<GetCategoryJsonResult>(accessToken, urlFormat, null, CommonJsonSendType.GET,
                timeOut: timeOut);
        }


        /// <summary>
        /// 删除私有模板库中的模板
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="priTmplId">要删除的模板id</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static WxJsonResult DelTemplate(string accessToken, string priTmplId, int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/wxaapi/newtmpl/deltemplate?access_token={0}";
            var data = new Dictionary<string, string>()
            {
                {"priTmplId", priTmplId}
            };
            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }
        #endregion

        /// <summary>
        /// 发送订阅通知
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="toUser">接收者（用户）的 openid</param>
        /// <param name="templateId">所需下发的订阅模板id</param>
        /// <param name="data">模板内容，格式形如 { "key1": { "value": any }, "key2": { "value": any } }</param>
        /// <param name="page">跳转网页时填写</param>
        /// <param name="miniProgram">	跳转小程序时填写</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult BizSend(string accessTokenOrAppId, string toUser, string templateId,
            TemplateMessageData data, string page = null, TemplateModel_MiniProgram miniProgram = null,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/subscribe/bizsend?access_token={0}";
                var submitData = new
                {
                    touser = toUser,
                    template_id = templateId,
                    page = page,
                    data = data,
                    miniprogram = miniProgram,
                };
                return CommonJsonSend.Send(accessToken, urlFormat, submitData, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        #endregion


        #region 异步方法
        /// <summary>
        /// 【异步方法】发送订阅通知
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="toUser">接收者（用户）的 openid</param>
        /// <param name="templateId">所需下发的订阅模板id</param>
        /// <param name="data">模板内容，格式形如 { "key1": { "value": any }, "key2": { "value": any } }</param>
        /// <param name="page">跳转网页时填写</param>
        /// <param name="miniProgram">	跳转小程序时填写</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> BizSendAsync(string accessTokenOrAppId, string toUser, string templateId,
            TemplateMessageData data, string page = null, TemplateModel_MiniProgram miniProgram = null,
            int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/subscribe/bizsend?access_token={0}";
                var submitData = new
                {
                    touser = toUser,
                    template_id = templateId,
                    page = page,
                    data = data,
                    miniprogram = miniProgram,
                };
                return CommonJsonSend.SendAsync(accessToken, urlFormat, submitData, timeOut: timeOut);
            }, accessTokenOrAppId);
        }


        #region 模板快速设置
        /// <summary>
        /// 【异步方法】获取类目下的公共模板，可从中选用模板使用
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="ids">类目 id，多个用逗号隔开</param>
        /// <param name="start">用于分页，表示从 start 开始。从 0 开始计数。</param>
        /// <param name="limit">用于分页，表示拉取 limit 条记录。最大为 30。</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static async Task<GetPubTemplateTitlesJsonResult> GetPubTemplateTitlesAsync(string accessToken,
            string ids, int start, int limit, int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/wxaapi/newtmpl/getpubtemplatetitles?access_token={0}";
            urlFormat = $"{urlFormat}&ids={ids}&start={start}&limit={limit}";
            return await CommonJsonSend.SendAsync<GetPubTemplateTitlesJsonResult>(accessToken, urlFormat, null,
                CommonJsonSendType.GET, timeOut: timeOut);
        }

        /// <summary>
        /// 【异步方法】获取公共模板下的关键词列表
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="tid">模板标题 id，可通过接口获取</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static async Task<GetPubTemplateKeyWordsByIdJsonResult> GetPubTemplateKeyWordsByIdAsync(
            string accessToken, string tid, int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/wxaapi/newtmpl/getpubtemplatekeywords?access_token={0}";
            urlFormat = $"{urlFormat}&tid={tid}";
            return await CommonJsonSend.SendAsync<GetPubTemplateKeyWordsByIdJsonResult>(accessToken, urlFormat, null,
                CommonJsonSendType.GET, timeOut: timeOut);
        }


        /// <summary>
        /// 【异步方法】从公共模板库中选用模板，到私有模板库中
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="tid">模板标题 id，可通过getPubTemplateTitleList接口获取，也可登录公众号后台查看获取</param>
        /// <param name="kidList">开发者自行组合好的模板关键词列表，关键词顺序可以自由搭配（例如 [3,5,4] 或 [4,5,3]），最多支持5个，最少2个关键词组合</param>
        /// <param name="sceneDesc">服务场景描述，15个字以内</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        /// <returns></returns>
        public static async Task<AddTemplateJsonResult> AddTemplateAsync(string accessToken, string tid, int[] kidList,
            string sceneDesc = "", int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/wxaapi/newtmpl/addtemplate?access_token={0}";
            var data = new
            {
                tid = tid,
                kidList = kidList,
                sceneDesc = sceneDesc
            };

            return await CommonJsonSend.SendAsync<AddTemplateJsonResult>(accessToken, urlFormat, data,
                timeOut: timeOut);
        }
        #endregion


        #region 对已存在模板进行操作
        /// <summary>
        /// 【异步方法】获取私有的模板列表
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static async Task<GetTemplateListJsonResult> GetTemplateListAsync(string accessToken,
            int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/wxaapi/newtmpl/gettemplate?access_token={0}";
            return await CommonJsonSend.SendAsync<GetTemplateListJsonResult>(accessToken, urlFormat, null,
                CommonJsonSendType.GET, timeOut: timeOut);
        }




        /// <summary>
        ///【异步方法】 获取公众号所属类目，可用于查询类目下的公共模板
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static async Task<GetCategoryJsonResult> GetCategoryAsync(string accessToken,
            int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/wxaapi/newtmpl/getcategory?access_token={0}";
            return await CommonJsonSend.SendAsync<GetCategoryJsonResult>(accessToken, urlFormat, null,
                CommonJsonSendType.GET, timeOut: timeOut);
        }



        /// <summary>
        /// 【异步方法】删除私有模板库中的模板
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="priTmplId">要删除的模板id</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> DelTemplateAsync(string accessToken, string priTmplId,
            int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/wxaapi/newtmpl/deltemplate?access_token={0}";
            var data = new Dictionary<string, string>()
            {
                {"priTmplId", priTmplId}
            };
            return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }
        #endregion
        #endregion
    }
}