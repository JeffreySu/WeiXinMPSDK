/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：DeviceDataApi.cs
    文件功能描述：企业微信设备数据接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐设备授权、设备数据和门禁规则接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.DeviceData
{
    /// <summary>
    /// 企业微信设备数据接口。
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Work, true)]
    public static class DeviceDataApi
    {
        private const string GetAuthInfoPath = "/cgi-bin/devicedata/get_auth_info";
        private const string GetCheckinDataPath = "/cgi-bin/devicedata/get_checkin_data";
        private const string GetTemperatureDataPath = "/cgi-bin/devicedata/get_temperature_data";
        private const string GetAccessControlDataPath = "/cgi-bin/devicedata/get_accesscontrol_data";
        private const string GetAccessControlRulePath = "/cgi-bin/devicedata/get_accesscontrol_rule";
        private const string AddAccessControlRulePath = "/cgi-bin/devicedata/add_accesscontrol_rule";
        private const string ModifyAccessControlRulePath = "/cgi-bin/devicedata/mod_accesscontrol_rule";
        private const string DeleteAccessControlRulePath = "/cgi-bin/devicedata/del_accesscontrol_rule";

        /// <summary>
        /// 获取应用已授权的设备列表。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96097"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">设备数据应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">应用 ID。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>应用已授权的设备列表。</returns>
        public static DeviceDataGetAuthInfoResult GetAuthInfo(string accessTokenOrAppKey,
            DeviceDataGetAuthInfoRequest data, int timeOut = Config.TIME_OUT)
            => Post<DeviceDataGetAuthInfoResult>(accessTokenOrAppKey, GetAuthInfoPath, data, timeOut);

        /// <summary>
        /// 异步获取应用已授权的设备列表。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96097"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">设备数据应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">应用 ID。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>应用已授权的设备列表。</returns>
        public static Task<DeviceDataGetAuthInfoResult> GetAuthInfoAsync(string accessTokenOrAppKey,
            DeviceDataGetAuthInfoRequest data, int timeOut = Config.TIME_OUT)
            => PostAsync<DeviceDataGetAuthInfoResult>(accessTokenOrAppKey, GetAuthInfoPath, data, timeOut);

        /// <summary>
        /// 分页获取设备上传的打卡数据。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96027"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">设备数据应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">时间范围、筛选条件和分页参数。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>设备打卡数据和下一页游标。</returns>
        public static DeviceDataGetCheckinDataResult GetCheckinData(string accessTokenOrAppKey,
            DeviceDataQueryRequest data, int timeOut = Config.TIME_OUT)
            => Post<DeviceDataGetCheckinDataResult>(accessTokenOrAppKey, GetCheckinDataPath, data, timeOut);

        /// <summary>
        /// 异步分页获取设备上传的打卡数据。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96027"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">设备数据应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">时间范围、筛选条件和分页参数。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>设备打卡数据和下一页游标。</returns>
        public static Task<DeviceDataGetCheckinDataResult> GetCheckinDataAsync(
            string accessTokenOrAppKey, DeviceDataQueryRequest data, int timeOut = Config.TIME_OUT)
            => PostAsync<DeviceDataGetCheckinDataResult>(accessTokenOrAppKey,
                GetCheckinDataPath, data, timeOut);

        /// <summary>
        /// 分页获取设备上传的测温数据。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96028"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">设备数据应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">时间范围、筛选条件和分页参数。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>设备测温数据和下一页游标。</returns>
        public static DeviceDataGetTemperatureDataResult GetTemperatureData(
            string accessTokenOrAppKey, DeviceDataQueryRequest data, int timeOut = Config.TIME_OUT)
            => Post<DeviceDataGetTemperatureDataResult>(accessTokenOrAppKey,
                GetTemperatureDataPath, data, timeOut);

        /// <summary>
        /// 异步分页获取设备上传的测温数据。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96028"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">设备数据应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">时间范围、筛选条件和分页参数。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>设备测温数据和下一页游标。</returns>
        public static Task<DeviceDataGetTemperatureDataResult> GetTemperatureDataAsync(
            string accessTokenOrAppKey, DeviceDataQueryRequest data, int timeOut = Config.TIME_OUT)
            => PostAsync<DeviceDataGetTemperatureDataResult>(accessTokenOrAppKey,
                GetTemperatureDataPath, data, timeOut);

        /// <summary>
        /// 分页获取设备上传的门禁通行数据。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96029"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">设备数据应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">时间范围、筛选条件和分页参数。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>设备门禁通行数据和下一页游标。</returns>
        public static DeviceDataGetAccessControlDataResult GetAccessControlData(
            string accessTokenOrAppKey, DeviceDataQueryRequest data, int timeOut = Config.TIME_OUT)
            => Post<DeviceDataGetAccessControlDataResult>(accessTokenOrAppKey,
                GetAccessControlDataPath, data, timeOut);

        /// <summary>
        /// 异步分页获取设备上传的门禁通行数据。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96029"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">设备数据应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">时间范围、筛选条件和分页参数。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>设备门禁通行数据和下一页游标。</returns>
        public static Task<DeviceDataGetAccessControlDataResult> GetAccessControlDataAsync(
            string accessTokenOrAppKey, DeviceDataQueryRequest data, int timeOut = Config.TIME_OUT)
            => PostAsync<DeviceDataGetAccessControlDataResult>(accessTokenOrAppKey,
                GetAccessControlDataPath, data, timeOut);

        /// <summary>
        /// 获取指定设备的门禁规则。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96030"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">设备数据应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">设备序列号。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>普通门禁和远程开门规则。</returns>
        public static DeviceDataGetAccessControlRuleResult GetAccessControlRule(
            string accessTokenOrAppKey, DeviceDataGetAccessControlRuleRequest data,
            int timeOut = Config.TIME_OUT)
            => Post<DeviceDataGetAccessControlRuleResult>(accessTokenOrAppKey,
                GetAccessControlRulePath, data, timeOut);

        /// <summary>
        /// 异步获取指定设备的门禁规则。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96030"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">设备数据应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">设备序列号。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>普通门禁和远程开门规则。</returns>
        public static Task<DeviceDataGetAccessControlRuleResult> GetAccessControlRuleAsync(
            string accessTokenOrAppKey, DeviceDataGetAccessControlRuleRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<DeviceDataGetAccessControlRuleResult>(accessTokenOrAppKey,
                GetAccessControlRulePath, data, timeOut);

        /// <summary>
        /// 新增门禁规则。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96031"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">设备数据应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">规则名称、设备范围和通行规则。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>新增规则 ID 和无效成员列表。</returns>
        public static DeviceDataAddAccessControlRuleResult AddAccessControlRule(
            string accessTokenOrAppKey, DeviceDataAddAccessControlRuleRequest data,
            int timeOut = Config.TIME_OUT)
            => Post<DeviceDataAddAccessControlRuleResult>(accessTokenOrAppKey,
                AddAccessControlRulePath, data, timeOut);

        /// <summary>
        /// 异步新增门禁规则。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96031"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">设备数据应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">规则名称、设备范围和通行规则。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>新增规则 ID 和无效成员列表。</returns>
        public static Task<DeviceDataAddAccessControlRuleResult> AddAccessControlRuleAsync(
            string accessTokenOrAppKey, DeviceDataAddAccessControlRuleRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<DeviceDataAddAccessControlRuleResult>(accessTokenOrAppKey,
                AddAccessControlRulePath, data, timeOut);

        /// <summary>
        /// 修改门禁规则。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96221"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">设备数据应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">规则 ID、设备范围和更新后的通行规则。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>无效成员列表。</returns>
        public static DeviceDataModifyAccessControlRuleResult ModifyAccessControlRule(
            string accessTokenOrAppKey, DeviceDataModifyAccessControlRuleRequest data,
            int timeOut = Config.TIME_OUT)
            => Post<DeviceDataModifyAccessControlRuleResult>(accessTokenOrAppKey,
                ModifyAccessControlRulePath, data, timeOut);

        /// <summary>
        /// 异步修改门禁规则。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96221"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">设备数据应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">规则 ID、设备范围和更新后的通行规则。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>无效成员列表。</returns>
        public static Task<DeviceDataModifyAccessControlRuleResult> ModifyAccessControlRuleAsync(
            string accessTokenOrAppKey, DeviceDataModifyAccessControlRuleRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<DeviceDataModifyAccessControlRuleResult>(accessTokenOrAppKey,
                ModifyAccessControlRulePath, data, timeOut);

        /// <summary>
        /// 删除门禁规则。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96227"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">设备数据应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">待删除的规则 ID。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信接口结果。</returns>
        public static DeviceDataDeleteAccessControlRuleResult DeleteAccessControlRule(
            string accessTokenOrAppKey, DeviceDataDeleteAccessControlRuleRequest data,
            int timeOut = Config.TIME_OUT)
            => Post<DeviceDataDeleteAccessControlRuleResult>(accessTokenOrAppKey,
                DeleteAccessControlRulePath, data, timeOut);

        /// <summary>
        /// 异步删除门禁规则。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96227"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">设备数据应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">待删除的规则 ID。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信接口结果。</returns>
        public static Task<DeviceDataDeleteAccessControlRuleResult> DeleteAccessControlRuleAsync(
            string accessTokenOrAppKey, DeviceDataDeleteAccessControlRuleRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<DeviceDataDeleteAccessControlRuleResult>(accessTokenOrAppKey,
                DeleteAccessControlRulePath, data, timeOut);

        private static T Post<T>(string accessTokenOrAppKey, string path, object data, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", data,
                CommonJsonSendType.POST, timeOut: timeOut), accessTokenOrAppKey);

        private static Task<T> PostAsync<T>(string accessTokenOrAppKey, string path, object data,
            int timeOut) where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", data,
                CommonJsonSendType.POST, timeOut: timeOut), accessTokenOrAppKey);
    }
}
