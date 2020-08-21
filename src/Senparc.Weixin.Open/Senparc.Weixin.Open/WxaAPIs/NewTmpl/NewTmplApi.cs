#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2020 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2020 Senparc
    
    文件名：TemplateApi.cs
    文件功能描述：小程序模板消息
    
    创建标识：Senparc - 20200731

    修改标识：Senparc - 20200731
    修改描述：v3.8.502.1 小程序订阅消息模板的管理

----------------------------------------------------------------*/

using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Open.WxaAPIs.NewTmpl.NewTmplJson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs.NewTmpl
{
    //文档：https://developers.weixin.qq.com/miniprogram/dev/api-backend/open-api/subscribe-message/subscribeMessage.addTemplate.html

    /// <summary>
    /// 小程序订阅消息模板的管理
    /// </summary>
    public static class NewTmplApi
    {
        #region 同步方法


        #region 模板快速设置
        /// <summary>
        /// 获取小程序模板库标题列表
        /// subscribeMessage.getPubTemplateTitleList
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="ids">类目 id，多个用逗号隔开</param>
        /// <param name="start">用于分页，表示从 start 开始。从 0 开始计数。</param>
        /// <param name="limit">用于分页，表示拉取 limit 条记录。最大为 30。</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "NewTmplApi.GetPubTemplateTitles", true)]
        public static GetPubTemplateTitlesJsonResult GetPubTemplateTitles(string accessToken, string ids, int start, int limit, int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/wxaapi/newtmpl/getpubtemplatetitles?access_token={0}";
            urlFormat = $"{urlFormat}&ids={ids}&start={start}&limit={limit}";
            return CommonJsonSend.Send<GetPubTemplateTitlesJsonResult>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
        }

        /// <summary>
        /// 获取模板库某个模板标题下关键词库
        /// subscribeMessage.getPubTemplateKeyWordsById
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="tid">模板标题 id，可通过接口获取</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "NewTmplApi.GetPubTemplateKeyWordsById", true)]
        public static GetPubTemplateKeyWordsByIdJsonResult GetPubTemplateKeyWordsById(string accessToken, string tid, int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/wxaapi/newtmpl/getpubtemplatekeywords?access_token={0}";
            urlFormat = $"{urlFormat}&tid={tid}";
            return CommonJsonSend.Send<GetPubTemplateKeyWordsByIdJsonResult>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
        }


        /// <summary>
        /// 组合模板并添加至帐号下的个人模板库
        /// subscribeMessage.addTemplate
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="id">模板标题id，可通过接口获取，也可登录小程序后台查看获取</param>
        /// <param name="keywordIdList">开发者自行组合好的模板关键词列表，关键词顺序可以自由搭配（例如[3,5,4]或[4,5,3]），最多支持10个关键词组合</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "NewTmplApi.AddTemplate", true)]
        public static AddTemplateJsonResult AddTemplate(string accessToken, string tid, int[] kidList, string sceneDesc = "", int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/wxaapi/newtmpl/addtemplate?access_token={0}";
            var data = new Dictionary<string, string>()
            {
                { "tid", tid },
                { "sceneDesc", sceneDesc },
            };
            for (int i = 0; i < kidList.Length; i++)
            {
                data.Add($"kidList[{i}]", $"{kidList[i]}");
            }
            return CommonJsonSend.Send<AddTemplateJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }

        #endregion


        #region 对已存在模板进行操作

        /// <summary>
        /// 获取帐号下已存在的模板列表
        /// subscribeMessage.getTemplateList
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "NewTmplApi.GetTemplateList", true)]
        public static GetTemplateListJsonResult GetTemplateList(string accessToken, int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/wxaapi/newtmpl/gettemplate?access_token={0}";
            return CommonJsonSend.Send<GetTemplateListJsonResult>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
        }

        /// <summary>
        /// 删除帐号下的某个模板
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="priTmplId">要删除的模板id</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "NewTmplApi.DelTemplate", true)]
        public static WxJsonResult DelTemplate(string accessToken, string priTmplId, int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/wxaapi/newtmpl/deltemplate?access_token={0}";
            var data = new Dictionary<string, string>()
            {
                { "priTmplId", priTmplId }
            };
            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }


        #endregion

        #endregion


        #region 异步方法


        #region 模板快速设置
        /// <summary>
        /// 【异步方法】获取小程序模板库标题列表
        /// subscribeMessage.getPubTemplateTitleList
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="ids">类目 id，多个用逗号隔开</param>
        /// <param name="start">用于分页，表示从 start 开始。从 0 开始计数。</param>
        /// <param name="limit">用于分页，表示拉取 limit 条记录。最大为 30。</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "NewTmplApi.GetPubTemplateTitlesAsync", true)]
        public static async Task<GetPubTemplateTitlesJsonResult> GetPubTemplateTitlesAsync(string accessToken, string ids, int start, int limit, int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/wxaapi/newtmpl/getpubtemplatetitles?access_token={0}";
            urlFormat = $"{urlFormat}&ids={ids}&start={start}&limit={limit}";
            return await CommonJsonSend.SendAsync<GetPubTemplateTitlesJsonResult>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
        }

        /// <summary>
        /// 【异步方法】获取模板库某个模板标题下关键词库
        /// subscribeMessage.getPubTemplateKeyWordsById
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="tid">模板标题 id，可通过接口获取</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "NewTmplApi.GetPubTemplateKeyWordsByIdAsync", true)]
        public static async Task<GetPubTemplateKeyWordsByIdJsonResult> GetPubTemplateKeyWordsByIdAsync(string accessToken, string tid, int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/wxaapi/newtmpl/getpubtemplatekeywords?access_token={0}";
            urlFormat = $"{urlFormat}&tid={tid}";
            return await CommonJsonSend.SendAsync<GetPubTemplateKeyWordsByIdJsonResult>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
        }


        /// <summary>
        /// 【异步方法】组合模板并添加至帐号下的个人模板库
        /// subscribeMessage.addTemplate
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="id">模板标题id，可通过接口获取，也可登录小程序后台查看获取</param>
        /// <param name="keywordIdList">开发者自行组合好的模板关键词列表，关键词顺序可以自由搭配（例如[3,5,4]或[4,5,3]），最多支持10个关键词组合</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "NewTmplApi.AddTemplateAsync", true)]
        public static async Task<AddTemplateJsonResult> AddTemplateAsync(string accessToken, string tid, int[] kidList, string sceneDesc = "", int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/wxaapi/newtmpl/addtemplate?access_token={0}";
            var data = new Dictionary<string, string>()
            {
                { "tid", tid },
                { "sceneDesc", sceneDesc },
            };
            for (int i = 0; i < kidList.Length; i++)
            {
                data.Add($"kidList[{i}]", $"{kidList[i]}");
            }
            return await CommonJsonSend.SendAsync<AddTemplateJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }

        #endregion


        #region 对已存在模板进行操作

        /// <summary>
        /// 【异步方法】获取帐号下已存在的模板列表
        /// subscribeMessage.getTemplateList
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "NewTmplApi.GetTemplateListAsync", true)]
        public static async Task<GetTemplateListJsonResult> GetTemplateListAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/wxaapi/newtmpl/gettemplate?access_token={0}";
            return await CommonJsonSend.SendAsync<GetTemplateListJsonResult>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
        }

        /// <summary>
        /// 【异步方法】删除帐号下的某个模板
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="priTmplId">要删除的模板id</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "NewTmplApi.DelTemplateAsync", true)]
        public static async Task<WxJsonResult> DelTemplateAsync(string accessToken, string priTmplId, int timeOut = Config.TIME_OUT)
        {
            string urlFormat = Config.ApiMpHost + "/wxaapi/newtmpl/deltemplate?access_token={0}";
            var data = new Dictionary<string, string>()
            {
                { "priTmplId", priTmplId }
            };
            return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }


        #endregion


        #endregion
    }
}