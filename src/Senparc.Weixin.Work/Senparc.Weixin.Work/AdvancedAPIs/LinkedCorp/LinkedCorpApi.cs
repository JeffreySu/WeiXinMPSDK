/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：LinkedCorpApi.cs
    文件功能描述：企业微信互联企业通讯录接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 新增互联企业权限、成员和部门查询接口

----------------------------------------------------------------*/

using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.LinkedCorp
{
    /// <summary>
    /// 企业微信互联企业通讯录接口。
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Work, true)]
    public static class LinkedCorpApi
    {
        private const string GetAgentPermissionListPath = "/cgi-bin/linkedcorp/agent/get_perm_list";
        private const string GetUserPath = "/cgi-bin/linkedcorp/user/get";
        private const string GetSimpleUserListPath = "/cgi-bin/linkedcorp/user/simplelist";
        private const string GetUserListPath = "/cgi-bin/linkedcorp/user/list";
        private const string GetDepartmentListPath = "/cgi-bin/linkedcorp/department/list";

        /// <summary>
        /// 获取应用的可见范围。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93172"/></para>
        /// </summary>
        /// <param name="accessToken">互联企业应用的调用凭证。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>可见成员和部门列表。</returns>
        public static LinkedCorpAgentPermissionListResult GetAgentPermissionList(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + GetAgentPermissionListPath + "?access_token={0}";
            return CommonJsonSend.Send<LinkedCorpAgentPermissionListResult>(accessToken, urlFormat,
                new LinkedCorpAgentPermissionListRequest(), CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 获取互联企业成员详情。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93171"/></para>
        /// </summary>
        /// <param name="accessToken">互联企业应用的调用凭证。</param>
        /// <param name="data">成员账号查询参数。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>互联企业成员详情。</returns>
        public static LinkedCorpUserGetResult GetUser(string accessToken, LinkedCorpUserGetRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + GetUserPath + "?access_token={0}";
            return CommonJsonSend.Send<LinkedCorpUserGetResult>(accessToken, urlFormat, data,
                CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 获取互联企业部门成员简要列表。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93168"/></para>
        /// </summary>
        /// <param name="accessToken">互联企业应用的调用凭证。</param>
        /// <param name="data">部门和递归查询参数。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>互联企业成员简要列表。</returns>
        public static LinkedCorpSimpleUserListResult GetSimpleUserList(string accessToken, LinkedCorpUserListRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + GetSimpleUserListPath + "?access_token={0}";
            return CommonJsonSend.Send<LinkedCorpSimpleUserListResult>(accessToken, urlFormat, data,
                CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 获取互联企业部门成员详情列表。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93169"/></para>
        /// </summary>
        /// <param name="accessToken">互联企业应用的调用凭证。</param>
        /// <param name="data">部门和递归查询参数。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>互联企业成员详情列表。</returns>
        public static LinkedCorpUserListResult GetUserList(string accessToken, LinkedCorpUserListRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + GetUserListPath + "?access_token={0}";
            return CommonJsonSend.Send<LinkedCorpUserListResult>(accessToken, urlFormat, data,
                CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 获取互联企业部门列表。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93170"/></para>
        /// </summary>
        /// <param name="accessToken">互联企业应用的调用凭证。</param>
        /// <param name="data">互联企业部门查询参数。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>互联企业部门列表。</returns>
        public static LinkedCorpDepartmentListResult GetDepartmentList(string accessToken, LinkedCorpDepartmentListRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + GetDepartmentListPath + "?access_token={0}";
            return CommonJsonSend.Send<LinkedCorpDepartmentListResult>(accessToken, urlFormat, data,
                CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 异步获取应用的可见范围。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93172"/></para>
        /// </summary>
        /// <param name="accessToken">互联企业应用的调用凭证。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>可见成员和部门列表。</returns>
        public static async Task<LinkedCorpAgentPermissionListResult> GetAgentPermissionListAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + GetAgentPermissionListPath + "?access_token={0}";
            return await CommonJsonSend.SendAsync<LinkedCorpAgentPermissionListResult>(accessToken, urlFormat,
                new LinkedCorpAgentPermissionListRequest(), CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步获取互联企业成员详情。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93171"/></para>
        /// </summary>
        /// <param name="accessToken">互联企业应用的调用凭证。</param>
        /// <param name="data">成员账号查询参数。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>互联企业成员详情。</returns>
        public static async Task<LinkedCorpUserGetResult> GetUserAsync(string accessToken, LinkedCorpUserGetRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + GetUserPath + "?access_token={0}";
            return await CommonJsonSend.SendAsync<LinkedCorpUserGetResult>(accessToken, urlFormat, data,
                CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步获取互联企业部门成员简要列表。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93168"/></para>
        /// </summary>
        /// <param name="accessToken">互联企业应用的调用凭证。</param>
        /// <param name="data">部门和递归查询参数。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>互联企业成员简要列表。</returns>
        public static async Task<LinkedCorpSimpleUserListResult> GetSimpleUserListAsync(string accessToken, LinkedCorpUserListRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + GetSimpleUserListPath + "?access_token={0}";
            return await CommonJsonSend.SendAsync<LinkedCorpSimpleUserListResult>(accessToken, urlFormat, data,
                CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步获取互联企业部门成员详情列表。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93169"/></para>
        /// </summary>
        /// <param name="accessToken">互联企业应用的调用凭证。</param>
        /// <param name="data">部门和递归查询参数。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>互联企业成员详情列表。</returns>
        public static async Task<LinkedCorpUserListResult> GetUserListAsync(string accessToken, LinkedCorpUserListRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + GetUserListPath + "?access_token={0}";
            return await CommonJsonSend.SendAsync<LinkedCorpUserListResult>(accessToken, urlFormat, data,
                CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步获取互联企业部门列表。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93170"/></para>
        /// </summary>
        /// <param name="accessToken">互联企业应用的调用凭证。</param>
        /// <param name="data">互联企业部门查询参数。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>互联企业部门列表。</returns>
        public static async Task<LinkedCorpDepartmentListResult> GetDepartmentListAsync(string accessToken, LinkedCorpDepartmentListRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + GetDepartmentListPath + "?access_token={0}";
            return await CommonJsonSend.SendAsync<LinkedCorpDepartmentListResult>(accessToken, urlFormat, data,
                CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);
        }
    }
}
