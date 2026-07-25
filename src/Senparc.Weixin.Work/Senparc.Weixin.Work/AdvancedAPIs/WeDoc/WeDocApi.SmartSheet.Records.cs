/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDocApi.SmartSheet.Records.cs
    文件功能描述：企业微信智能表格记录接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐智能表格记录增删改查接口及必要注释

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDoc
{
    public static partial class WeDocApi
    {
        private const string GetSmartSheetRecordsPath = "/cgi-bin/wedoc/smartsheet/get_records";
        private const string AddSmartSheetRecordsPath = "/cgi-bin/wedoc/smartsheet/add_records";
        private const string DeleteSmartSheetRecordsPath = "/cgi-bin/wedoc/smartsheet/delete_records";
        private const string UpdateSmartSheetRecordsPath = "/cgi-bin/wedoc/smartsheet/update_records";

        /// <summary>
        /// 获取智能表格记录，支持按记录、字段、视图、排序及筛选条件查询。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99915"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识、查询条件和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>记录列表及分页信息。</returns>
        public static WeDocSmartSheetGetRecordsResult GetSmartSheetRecords(string accessTokenOrAppKey,
            WeDocSmartSheetGetRecordsRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSmartSheetGetRecordsResult>(accessTokenOrAppKey, GetSmartSheetRecordsPath, request, timeOut);

        /// <summary>
        /// 异步获取智能表格记录，支持按记录、字段、视图、排序及筛选条件查询。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99915"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识、查询条件和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>记录列表及分页信息。</returns>
        public static Task<WeDocSmartSheetGetRecordsResult> GetSmartSheetRecordsAsync(string accessTokenOrAppKey,
            WeDocSmartSheetGetRecordsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSmartSheetGetRecordsResult>(accessTokenOrAppKey, GetSmartSheetRecordsPath, request, timeOut);

        /// <summary>
        /// 批量新增智能表格记录。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99907"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识、键类型及待新增记录列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新增后的记录 ID 列表。</returns>
        public static WeDocSmartSheetAddRecordsResult AddSmartSheetRecords(string accessTokenOrAppKey,
            WeDocSmartSheetAddRecordsRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSmartSheetAddRecordsResult>(accessTokenOrAppKey, AddSmartSheetRecordsPath, request, timeOut);

        /// <summary>
        /// 异步批量新增智能表格记录。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99907"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识、键类型及待新增记录列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新增后的记录 ID 列表。</returns>
        public static Task<WeDocSmartSheetAddRecordsResult> AddSmartSheetRecordsAsync(string accessTokenOrAppKey,
            WeDocSmartSheetAddRecordsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSmartSheetAddRecordsResult>(accessTokenOrAppKey, AddSmartSheetRecordsPath, request, timeOut);

        /// <summary>
        /// 批量删除智能表格记录。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99908"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识及待删除记录 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static WorkJsonResult DeleteSmartSheetRecords(string accessTokenOrAppKey,
            WeDocSmartSheetDeleteRecordsRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, DeleteSmartSheetRecordsPath, request, timeOut);

        /// <summary>
        /// 异步批量删除智能表格记录。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99908"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识及待删除记录 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static Task<WorkJsonResult> DeleteSmartSheetRecordsAsync(string accessTokenOrAppKey,
            WeDocSmartSheetDeleteRecordsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, DeleteSmartSheetRecordsPath, request, timeOut);

        /// <summary>
        /// 批量更新智能表格记录的单元格值。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99909"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识、键类型及待更新记录列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static WorkJsonResult UpdateSmartSheetRecords(string accessTokenOrAppKey,
            WeDocSmartSheetUpdateRecordsRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, UpdateSmartSheetRecordsPath, request, timeOut);

        /// <summary>
        /// 异步批量更新智能表格记录的单元格值。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99909"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识、键类型及待更新记录列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static Task<WorkJsonResult> UpdateSmartSheetRecordsAsync(string accessTokenOrAppKey,
            WeDocSmartSheetUpdateRecordsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, UpdateSmartSheetRecordsPath, request, timeOut);
    }
}
