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
    
    文件名：TcbApi.cs
    文件功能描述：云函数。注意: HTTP API 途径触发云函数不包含用户信息。
    
    
    创建标识：lishewen - 20190530
   
----------------------------------------------------------------*/

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
        /// <summary>
        /// 新增集合
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="collection_name">集合名称</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.DatabaseCollectionAdd", true)]
        public static WxJsonResult DatabaseCollectionAdd(string accessTokenOrAppId, string env, string collection_name, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/databasecollectionadd?access_token={0}";
                var postBody = new
                {
                    env,
                    collection_name
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="collection_name">集合名称</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.DatabaseCollectionDelete", true)]
        public static WxJsonResult DatabaseCollectionDelete(string accessTokenOrAppId, string env, string collection_name, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/databasecollectiondelete?access_token={0}";
                var postBody = new
                {
                    env,
                    collection_name
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 获取特定云环境下集合信息
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="limit">获取数量限制</param>
        /// <param name="offset">偏移量</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.DatabaseCollectionGet", true)]
        public static WxDatabaseCollectionJsonResult DatabaseCollectionGet(string accessTokenOrAppId, string env, int limit = 10, int offset = 0, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/databasecollectionget?access_token={0}";
                var postBody = new
                {
                    env,
                    limit,
                    offset
                };
                return CommonJsonSend.Send<WxDatabaseCollectionJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 数据库插入记录
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="query">数据库操作语句</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.DatabaseAdd", true)]
        public static WxDatabaseAddJsonResult DatabaseAdd(string accessTokenOrAppId, string env, string query, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/databaseadd?access_token={0}";
                var postBody = new
                {
                    env,
                    query
                };
                return CommonJsonSend.Send<WxDatabaseAddJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 数据库删除记录
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="query">数据库操作语句</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.DatabaseDelete", true)]
        public static WxDatabaseDeleteJsonResult DatabaseDelete(string accessTokenOrAppId, string env, string query, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/databasedelete?access_token={0}";
                var postBody = new
                {
                    env,
                    query
                };
                return CommonJsonSend.Send<WxDatabaseDeleteJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 数据库更新记录
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="query">数据库操作语句</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.DatabaseUpdate", true)]
        public static WxDatabaseUpdateJsonResult DatabaseUpdate(string accessTokenOrAppId, string env, string query, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/databaseupdate?access_token={0}";
                var postBody = new
                {
                    env,
                    query
                };
                return CommonJsonSend.Send<WxDatabaseUpdateJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 数据库查询记录
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="query">数据库操作语句</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.DatabaseQuery", true)]
        public static WxDatabaseQueryJsonResult DatabaseQuery(string accessTokenOrAppId, string env, string query, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/databasequery?access_token={0}";
                var postBody = new
                {
                    env,
                    query
                };
                return CommonJsonSend.Send<WxDatabaseQueryJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 统计集合记录数或统计查询语句对应的结果记录数
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="query">数据库操作语句</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.DatabaseCount", true)]
        public static WxDatabaseCountJsonResult DatabaseCount(string accessTokenOrAppId, string env, string query, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/databasecount?access_token={0}";
                var postBody = new
                {
                    env,
                    query
                };
                return CommonJsonSend.Send<WxDatabaseCountJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 获取文件上传链接
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="path">上传路径</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.UploadFile", true)]
        public static WxUploadFileJsonResult UploadFile(string accessTokenOrAppId, string env, string path, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/uploadfile?access_token={0}";
                var postBody = new
                {
                    env,
                    path
                };
                return CommonJsonSend.Send<WxUploadFileJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 获取文件下载链接
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="file_list">文件列表</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.BatchDownloadFile", true)]
        public static WxDownloadFileJsonResult BatchDownloadFile(string accessTokenOrAppId, string env, IEnumerable<FileItem> file_list, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/batchdownloadfile?access_token={0}";
                var postBody = new
                {
                    env,
                    file_list
                };
                return CommonJsonSend.Send<WxDownloadFileJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="fileid_list">文件ID列表</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.BatchDeleteFile", true)]
        public static WxDeleteFileJsonResult BatchDeleteFile(string accessTokenOrAppId, string env, IEnumerable<string> fileid_list, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/batchdeletefile?access_token={0}";
                var postBody = new
                {
                    env,
                    fileid_list
                };
                return CommonJsonSend.Send<WxDeleteFileJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 获取腾讯云API调用凭证
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="lifespan">有效期（单位为秒，最大7200）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.GetQcloudToken", true)]
        public static WxQcloudTokenJsonResult GetQcloudToken(string accessTokenOrAppId, int lifespan, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/getqcloudtoken?access_token={0}";
                var postBody = new
                {
                    lifespan
                };
                return CommonJsonSend.Send<WxQcloudTokenJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

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
        /// <summary>
        /// 【异步方法】新增集合
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="collection_name">集合名称</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.DatabaseCollectionAddAsync", true)]
        public static async Task<WxJsonResult> DatabaseCollectionAddAsync(string accessTokenOrAppId, string env, string collection_name, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/databasecollectionadd?access_token={0}";
                var postBody = new
                {
                    env,
                    collection_name
                };
                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】删除集合
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="collection_name">集合名称</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.DatabaseCollectionDeleteAsync", true)]
        public static async Task<WxJsonResult> DatabaseCollectionDeleteAsync(string accessTokenOrAppId, string env, string collection_name, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/databasecollectiondelete?access_token={0}";
                var postBody = new
                {
                    env,
                    collection_name
                };
                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】获取特定云环境下集合信息
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="limit">获取数量限制</param>
        /// <param name="offset">偏移量</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.DatabaseCollectionGetAsync", true)]
        public static async Task<WxDatabaseCollectionJsonResult> DatabaseCollectionGetAsync(string accessTokenOrAppId, string env, int limit = 10, int offset = 0, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/databasecollectionget?access_token={0}";
                var postBody = new
                {
                    env,
                    limit,
                    offset
                };
                return await CommonJsonSend.SendAsync<WxDatabaseCollectionJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】数据库插入记录
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="query">数据库操作语句</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.DatabaseAddAsync", true)]
        public static async Task<WxDatabaseAddJsonResult> DatabaseAddAsync(string accessTokenOrAppId, string env, string query, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/databaseadd?access_token={0}";
                var postBody = new
                {
                    env,
                    query
                };
                return await CommonJsonSend.SendAsync<WxDatabaseAddJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】数据库删除记录
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="query">数据库操作语句</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.DatabaseDeleteAsync", true)]
        public static async Task<WxDatabaseDeleteJsonResult> DatabaseDeleteAsync(string accessTokenOrAppId, string env, string query, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/databasedelete?access_token={0}";
                var postBody = new
                {
                    env,
                    query
                };
                return await CommonJsonSend.SendAsync<WxDatabaseDeleteJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】数据库更新记录
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="query">数据库操作语句</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.DatabaseUpdateAsync", true)]
        public static async Task<WxDatabaseUpdateJsonResult> DatabaseUpdateAsync(string accessTokenOrAppId, string env, string query, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/databaseupdate?access_token={0}";
                var postBody = new
                {
                    env,
                    query
                };
                return await CommonJsonSend.SendAsync<WxDatabaseUpdateJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】数据库查询记录
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="query">数据库操作语句</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.DatabaseQueryAsync", true)]
        public static async Task<WxDatabaseQueryJsonResult> DatabaseQueryAsync(string accessTokenOrAppId, string env, string query, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/databasequery?access_token={0}";
                var postBody = new
                {
                    env,
                    query
                };
                return await CommonJsonSend.SendAsync<WxDatabaseQueryJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】统计集合记录数或统计查询语句对应的结果记录数
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="query">数据库操作语句</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.DatabaseCountAsync", true)]
        public static async Task<WxDatabaseCountJsonResult> DatabaseCountAsync(string accessTokenOrAppId, string env, string query, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/databasecount?access_token={0}";
                var postBody = new
                {
                    env,
                    query
                };
                return await CommonJsonSend.SendAsync<WxDatabaseCountJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】获取文件上传链接
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="path">上传路径</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.UploadFileAsync", true)]
        public static async Task<WxUploadFileJsonResult> UploadFileAsync(string accessTokenOrAppId, string env, string path, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/uploadfile?access_token={0}";
                var postBody = new
                {
                    env,
                    path
                };
                return await CommonJsonSend.SendAsync<WxUploadFileJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】获取文件下载链接
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="file_list">文件列表</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.BatchDownloadFileAsync", true)]
        public static async Task<WxDownloadFileJsonResult> BatchDownloadFileAsync(string accessTokenOrAppId, string env, IEnumerable<FileItem> file_list, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/batchdownloadfile?access_token={0}";
                var postBody = new
                {
                    env,
                    file_list
                };
                return await CommonJsonSend.SendAsync<WxDownloadFileJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】删除文件
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="env">云环境ID</param>
        /// <param name="fileid_list">文件ID列表</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.BatchDeleteFileAsync", true)]
        public static async Task<WxDeleteFileJsonResult> BatchDeleteFileAsync(string accessTokenOrAppId, string env, IEnumerable<string> fileid_list, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/batchdeletefile?access_token={0}";
                var postBody = new
                {
                    env,
                    fileid_list
                };
                return await CommonJsonSend.SendAsync<WxDeleteFileJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 获取腾讯云API调用凭证
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证</param>
        /// <param name="lifespan">有效期（单位为秒，最大7200）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "TcbApi.GetQcloudToken", true)]
        public static async Task<WxQcloudTokenJsonResult> GetQcloudTokenAsync(string accessTokenOrAppId, int lifespan, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/tcb/getqcloudtoken?access_token={0}";
                var postBody = new
                {
                    lifespan
                };
                return await CommonJsonSend.SendAsync<WxQcloudTokenJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        #endregion
    }
}
