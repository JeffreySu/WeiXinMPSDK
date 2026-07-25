/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDocApi.Form.cs
    文件功能描述：企业微信收集表接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐收集表创建、编辑、信息、统计和答案接口

----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDoc
{
    public static partial class WeDocApi
    {
        private const string CreateFormPath = "/cgi-bin/wedoc/create_form";
        private const string ModifyFormPath = "/cgi-bin/wedoc/modify_form";
        private const string GetFormInfoPath = "/cgi-bin/wedoc/get_form_info";
        private const string GetFormStatisticPath = "/cgi-bin/wedoc/get_form_statistic";
        private const string GetFormAnswerPath = "/cgi-bin/wedoc/get_form_answer";

        /// <summary>
        /// 创建收集表。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/97462">企业微信官方文档</see></para>
        /// </summary>
        public static WeDocFormCreateResult CreateForm(string accessTokenOrAppKey, WeDocFormCreateRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<WeDocFormCreateResult>(accessTokenOrAppKey, CreateFormPath, request, timeOut);

        /// <summary>
        /// 异步创建收集表。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/97462">企业微信官方文档</see></para>
        /// </summary>
        public static Task<WeDocFormCreateResult> CreateFormAsync(string accessTokenOrAppKey,
            WeDocFormCreateRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocFormCreateResult>(accessTokenOrAppKey, CreateFormPath, request, timeOut);

        /// <summary>
        /// 编辑收集表。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/97462">企业微信官方文档</see></para>
        /// </summary>
        public static WorkJsonResult ModifyForm(string accessTokenOrAppKey, WeDocFormModifyRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, ModifyFormPath, request, timeOut);

        /// <summary>
        /// 异步编辑收集表。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/97462">企业微信官方文档</see></para>
        /// </summary>
        public static Task<WorkJsonResult> ModifyFormAsync(string accessTokenOrAppKey,
            WeDocFormModifyRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, ModifyFormPath, request, timeOut);

        /// <summary>
        /// 获取收集表信息。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/97817">企业微信官方文档</see></para>
        /// </summary>
        public static WeDocFormInfoResult GetFormInfo(string accessTokenOrAppKey, WeDocFormIdRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<WeDocFormInfoResult>(accessTokenOrAppKey, GetFormInfoPath, request, timeOut);

        /// <summary>
        /// 异步获取收集表信息。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/97817">企业微信官方文档</see></para>
        /// </summary>
        public static Task<WeDocFormInfoResult> GetFormInfoAsync(string accessTokenOrAppKey,
            WeDocFormIdRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocFormInfoResult>(accessTokenOrAppKey, GetFormInfoPath, request, timeOut);

        /// <summary>
        /// 批量获取收集表统计信息。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/97818">企业微信官方文档</see></para>
        /// </summary>
        public static WeDocFormStatisticResult GetFormStatistics(string accessTokenOrAppKey,
            IList<WeDocFormStatisticRequest> requests, int timeOut = Config.TIME_OUT)
            => Post<WeDocFormStatisticResult>(accessTokenOrAppKey, GetFormStatisticPath, requests, timeOut);

        /// <summary>
        /// 异步批量获取收集表统计信息。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/97818">企业微信官方文档</see></para>
        /// </summary>
        public static Task<WeDocFormStatisticResult> GetFormStatisticsAsync(string accessTokenOrAppKey,
            IList<WeDocFormStatisticRequest> requests, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocFormStatisticResult>(accessTokenOrAppKey, GetFormStatisticPath, requests, timeOut);

        /// <summary>
        /// 获取指定收集周期和答案 ID 的收集表答案。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/97818">企业微信官方文档</see></para>
        /// </summary>
        public static WeDocFormAnswerResult GetFormAnswers(string accessTokenOrAppKey,
            WeDocFormAnswerRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocFormAnswerResult>(accessTokenOrAppKey, GetFormAnswerPath, request, timeOut);

        /// <summary>
        /// 异步获取指定收集周期和答案 ID 的收集表答案。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/97818">企业微信官方文档</see></para>
        /// </summary>
        public static Task<WeDocFormAnswerResult> GetFormAnswersAsync(string accessTokenOrAppKey,
            WeDocFormAnswerRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocFormAnswerResult>(accessTokenOrAppKey, GetFormAnswerPath, request, timeOut);
    }
}
