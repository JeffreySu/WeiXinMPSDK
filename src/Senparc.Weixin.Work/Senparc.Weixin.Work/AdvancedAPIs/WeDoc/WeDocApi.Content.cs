/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDocApi.Content.cs
    文件功能描述：企业微信在线文档内容读取与编辑接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐在线文档内容读取与编辑接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDoc
{
    public static partial class WeDocApi
    {
        private const string GetDocumentDataPath = "/cgi-bin/wedoc/get_doc_data";
        private const string ModifyDocumentContentPath = "/cgi-bin/wedoc/mod_doc";

        /// <summary>
        /// 获取在线文档内容数据，并保留不同文档块类型对应的 JSON 结构。
        /// <para>官方接口：<c>POST /cgi-bin/wedoc/get_doc_data</c>。</para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档标识及可选的内容范围参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>文档内容及分页信息。</returns>
        public static WeDocContentResult GetDocumentData(string accessTokenOrAppKey,
            WeDocContentRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocContentResult>(accessTokenOrAppKey, GetDocumentDataPath, request, timeOut);

        /// <summary>
        /// 异步获取在线文档内容数据，并保留不同文档块类型对应的 JSON 结构。
        /// <para>官方接口：<c>POST /cgi-bin/wedoc/get_doc_data</c>。</para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档标识及可选的内容范围参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>文档内容及分页信息。</returns>
        public static Task<WeDocContentResult> GetDocumentDataAsync(string accessTokenOrAppKey,
            WeDocContentRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocContentResult>(accessTokenOrAppKey, GetDocumentDataPath, request, timeOut);

        /// <summary>
        /// 按顺序执行一组在线文档块级编辑操作。
        /// <para>官方接口：<c>POST /cgi-bin/wedoc/mod_doc</c>。</para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档标识及块级编辑操作列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static WorkJsonResult ModifyDocumentContent(string accessTokenOrAppKey,
            WeDocContentModifyRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, ModifyDocumentContentPath, request, timeOut);

        /// <summary>
        /// 异步按顺序执行一组在线文档块级编辑操作。
        /// <para>官方接口：<c>POST /cgi-bin/wedoc/mod_doc</c>。</para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档标识及块级编辑操作列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static Task<WorkJsonResult> ModifyDocumentContentAsync(string accessTokenOrAppKey,
            WeDocContentModifyRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, ModifyDocumentContentPath, request, timeOut);
    }
}
