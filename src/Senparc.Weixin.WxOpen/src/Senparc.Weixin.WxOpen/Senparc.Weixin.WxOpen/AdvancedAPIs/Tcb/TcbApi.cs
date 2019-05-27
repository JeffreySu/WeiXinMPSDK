using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.WxOpen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Tcb
{
    /// <summary>
    /// 云函数
    /// 注意: HTTP API 途径触发云函数不包含用户信息
    /// </summary>
    public static class TcbApi
    {
        #region 同步方法
        /// <summary>
        /// 触发云函数。注意：HTTP API 途径触发云函数不包含用户信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云开发环境ID</param>
        /// <param name="name">云函数名称</param>
        /// <param name="postBody">云函数的传入参数，具体结构由开发者定义。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.InvokeCloudFunction", true)]
        public static WxCloudFunctionJsonResult InvokeCloudFunction(string accessTokenOrAppId, string env, string name, object postBody, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/invokecloudfunction?access_token={0}&env=" + env + "&name=" + name;
                return CommonJsonSend.Send<WxCloudFunctionJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 数据库导入
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="collection">导入collection名</param>
        /// <param name="file_path">导入文件路径(导入文件需先上传到同环境的存储中，可使用开发者工具或 HTTP API的上传文件 API上传）</param>
        /// <param name="file_type">导入文件类型，文件格式参考数据库导入指引中的文件格式部分</param>
        /// <param name="stop_on_error">是否在遇到错误时停止导入</param>
        /// <param name="conflict_mode">冲突处理模式</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.DatabaseMigrateImport", true)]
        public static WxDatabaseMigrateJsonResult DatabaseMigrateImport(string accessTokenOrAppId, string env, string collection, string file_path, FileType file_type, bool stop_on_error,
            ConflictMode conflict_mode, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/databasemigrateimport?access_token={0}";
                var postBody = new
                {
                    env,
                    collection,
                    file_path,
                    file_type,
                    stop_on_error,
                    conflict_mode
                };
                return CommonJsonSend.Send<WxDatabaseMigrateJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 数据库导出
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="file_path">导出文件路径（文件会导出到同环境的云存储中，可使用获取下载链接 API 获取下载链接）</param>
        /// <param name="file_type">导出文件类型，文件格式参考数据库导入指引中的文件格式部分</param>
        /// <param name="query">导出条件</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.DatabaseMigrateExport", true)]
        public static WxDatabaseMigrateJsonResult DatabaseMigrateExport(string accessTokenOrAppId, string env, string file_path, FileType file_type, string query, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/databasemigrateexport?access_token={0}";
                var postBody = new
                {
                    env,
                    file_path,
                    file_type,
                    query
                };
                return CommonJsonSend.Send<WxDatabaseMigrateJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 数据库迁移状态查询
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="job_id">迁移任务ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.DatabaseMigrateQueryInfo", true)]
        public static WxDatabaseMigrateQueryInfoJsonResult DatabaseMigrateQueryInfo(string accessTokenOrAppId, string env, int job_id, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/databasemigratequeryinfo?access_token={0}";
                var postBody = new
                {
                    env,
                    job_id
                };
                return CommonJsonSend.Send<WxDatabaseMigrateQueryInfoJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 【异步方法】触发云函数。注意：HTTP API 途径触发云函数不包含用户信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云开发环境ID</param>
        /// <param name="name">云函数名称</param>
        /// <param name="postBody">云函数的传入参数，具体结构由开发者定义。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.InvokeCloudFunctionAsync", true)]
        public static async Task<WxCloudFunctionJsonResult> SendTemplateMessageAsync(string accessTokenOrAppId, string env, string name, object postBody, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/invokecloudfunction?access_token={0}&env=" + env + "&name=" + name;
                return await CommonJsonSend.SendAsync<WxCloudFunctionJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】数据库导入
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="collection">导入collection名</param>
        /// <param name="file_path">导入文件路径(导入文件需先上传到同环境的存储中，可使用开发者工具或 HTTP API的上传文件 API上传）</param>
        /// <param name="file_type">导入文件类型，文件格式参考数据库导入指引中的文件格式部分</param>
        /// <param name="stop_on_error">是否在遇到错误时停止导入</param>
        /// <param name="conflict_mode">冲突处理模式</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.DatabaseMigrateImportAsync", true)]
        public static async Task<WxDatabaseMigrateJsonResult> DatabaseMigrateImportAsync(string accessTokenOrAppId, string env, string collection, string file_path, FileType file_type, bool stop_on_error,
            ConflictMode conflict_mode, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/databasemigrateimport?access_token={0}";
                var postBody = new
                {
                    env,
                    collection,
                    file_path,
                    file_type,
                    stop_on_error,
                    conflict_mode
                };
                return await CommonJsonSend.SendAsync<WxDatabaseMigrateJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】数据库导出
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="file_path">导出文件路径（文件会导出到同环境的云存储中，可使用获取下载链接 API 获取下载链接）</param>
        /// <param name="file_type">导出文件类型，文件格式参考数据库导入指引中的文件格式部分</param>
        /// <param name="query">导出条件</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.DatabaseMigrateExportAsync", true)]
        public static async Task<WxDatabaseMigrateJsonResult> DatabaseMigrateExportAsync(string accessTokenOrAppId, string env, string file_path, FileType file_type, string query, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/databasemigrateexport?access_token={0}";
                var postBody = new
                {
                    env,
                    file_path,
                    file_type,
                    query
                };
                return await CommonJsonSend.SendAsync<WxDatabaseMigrateJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】数据库迁移状态查询
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="job_id">迁移任务ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.DatabaseMigrateQueryInfoAsync", true)]
        public static async Task<WxDatabaseMigrateQueryInfoJsonResult> DatabaseMigrateQueryInfoAsync(string accessTokenOrAppId, string env, int job_id, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/databasemigratequeryinfo?access_token={0}";
                var postBody = new
                {
                    env,
                    job_id
                };
                return await CommonJsonSend.SendAsync<WxDatabaseMigrateQueryInfoJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        #endregion
    }
}
