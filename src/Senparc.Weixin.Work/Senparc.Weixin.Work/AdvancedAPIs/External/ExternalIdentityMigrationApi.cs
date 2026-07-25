/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ExternalIdentityMigrationApi.cs
    文件功能描述：企业微信客户联系身份转换与迁移接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐客户联系身份转换、可见范围与迁移接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Work.AdvancedAPIs.External;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 企业微信客户联系身份转换、获客助手范围与迁移接口。
    /// </summary>
    public static partial class ExternalApi
    {
        private const string ExternalContactUnionIdConvertPath =
            "/cgi-bin/externalcontact/unionid_to_external_userid";
        private const string BatchMobileToExternalUserIdPath =
            "/cgi-bin/externalcontact/batch_to_external_userid";
        private const string ToServiceExternalUserIdPath =
            "/cgi-bin/externalcontact/to_service_external_userid";
        private const string CustomerAcquisitionAppPermitPath =
            "/cgi-bin/externalcontact/customer_acquisition_app/get_permit";
        private const string GetNewExternalUserIdPath =
            "/cgi-bin/externalcontact/get_new_external_userid";
        private const string GetNewGroupChatExternalUserIdPath =
            "/cgi-bin/externalcontact/groupchat/get_new_external_userid";

        /// <summary>
        /// 将微信 UnionId 转换为企业微信客户联系的 ExternalUserId。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93274"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">待转换的微信 UnionId。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>客户联系 ExternalUserId。</returns>
        public static ExternalContactUnionIdConvertResult ConvertExternalContactUnionId(
            string accessTokenOrAppKey, ExternalContactUnionIdConvertRequest request,
            int timeOut = Config.TIME_OUT)
            => PostP1<ExternalContactUnionIdConvertResult>(accessTokenOrAppKey,
                ExternalContactUnionIdConvertPath, request, timeOut);

        /// <summary>
        /// 异步将微信 UnionId 转换为企业微信客户联系的 ExternalUserId。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93274"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">待转换的微信 UnionId。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>客户联系 ExternalUserId。</returns>
        public static Task<ExternalContactUnionIdConvertResult> ConvertExternalContactUnionIdAsync(
            string accessTokenOrAppKey, ExternalContactUnionIdConvertRequest request,
            int timeOut = Config.TIME_OUT)
            => PostP1Async<ExternalContactUnionIdConvertResult>(accessTokenOrAppKey,
                ExternalContactUnionIdConvertPath, request, timeOut);

        /// <summary>
        /// 批量将手机号转换为家校场景中的 ExternalUserId。
        /// <see href="https://developer.work.weixin.qq.com/document/path/92506"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">待转换的手机号列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>手机号转换成功和失败的逐项结果。</returns>
        public static BatchMobileToExternalUserIdResult BatchMobileToExternalUserId(
            string accessTokenOrAppKey, BatchMobileToExternalUserIdRequest request,
            int timeOut = Config.TIME_OUT)
            => PostP1<BatchMobileToExternalUserIdResult>(accessTokenOrAppKey,
                BatchMobileToExternalUserIdPath, request, timeOut);

        /// <summary>
        /// 异步批量将手机号转换为家校场景中的 ExternalUserId。
        /// <see href="https://developer.work.weixin.qq.com/document/path/92506"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">待转换的手机号列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>手机号转换成功和失败的逐项结果。</returns>
        public static Task<BatchMobileToExternalUserIdResult> BatchMobileToExternalUserIdAsync(
            string accessTokenOrAppKey, BatchMobileToExternalUserIdRequest request,
            int timeOut = Config.TIME_OUT)
            => PostP1Async<BatchMobileToExternalUserIdResult>(accessTokenOrAppKey,
                BatchMobileToExternalUserIdPath, request, timeOut);

        /// <summary>
        /// 将代开发自建应用的 ExternalUserId 转换为服务商范围 ExternalUserId。
        /// <see href="https://developer.work.weixin.qq.com/document/path/95195"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">代开发自建应用的 ExternalUserId。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>服务商范围的 ExternalUserId。</returns>
        public static ServiceExternalUserIdConvertResult ConvertToServiceExternalUserId(
            string accessTokenOrAppKey, ServiceExternalUserIdConvertRequest request,
            int timeOut = Config.TIME_OUT)
            => PostP1<ServiceExternalUserIdConvertResult>(accessTokenOrAppKey,
                ToServiceExternalUserIdPath, request, timeOut);

        /// <summary>
        /// 异步将代开发自建应用的 ExternalUserId 转换为服务商范围 ExternalUserId。
        /// <see href="https://developer.work.weixin.qq.com/document/path/95195"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">代开发自建应用的 ExternalUserId。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>服务商范围的 ExternalUserId。</returns>
        public static Task<ServiceExternalUserIdConvertResult> ConvertToServiceExternalUserIdAsync(
            string accessTokenOrAppKey, ServiceExternalUserIdConvertRequest request,
            int timeOut = Config.TIME_OUT)
            => PostP1Async<ServiceExternalUserIdConvertResult>(accessTokenOrAppKey,
                ToServiceExternalUserIdPath, request, timeOut);

        /// <summary>
        /// 获取当前获客助手应用可使用的成员、部门和标签范围。
        /// <see href="https://developer.work.weixin.qq.com/document/path/101146"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>获客助手应用的成员、部门和标签可见范围。</returns>
        public static CustomerAcquisitionAppPermitResult GetCustomerAcquisitionAppPermit(
            string accessTokenOrAppKey, int timeOut = Config.TIME_OUT)
            => GetP1<CustomerAcquisitionAppPermitResult>(accessTokenOrAppKey,
                CustomerAcquisitionAppPermitPath, string.Empty, timeOut);

        /// <summary>
        /// 异步获取当前获客助手应用可使用的成员、部门和标签范围。
        /// <see href="https://developer.work.weixin.qq.com/document/path/101146"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>获客助手应用的成员、部门和标签可见范围。</returns>
        public static Task<CustomerAcquisitionAppPermitResult> GetCustomerAcquisitionAppPermitAsync(
            string accessTokenOrAppKey, int timeOut = Config.TIME_OUT)
            => GetP1Async<CustomerAcquisitionAppPermitResult>(accessTokenOrAppKey,
                CustomerAcquisitionAppPermitPath, string.Empty, timeOut);

        /// <summary>
        /// 批量获取企业合并后的新 ExternalUserId。
        /// <see href="https://developer.work.weixin.qq.com/document/path/95327"/>
        /// <see href="https://developer.work.weixin.qq.com/document/path/95435"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">企业合并前的 ExternalUserId 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新旧 ExternalUserId 对应关系。</returns>
        public static NewExternalUserIdResult GetNewExternalUserId(
            string accessTokenOrAppKey, NewExternalUserIdRequest request,
            int timeOut = Config.TIME_OUT)
            => PostP1<NewExternalUserIdResult>(accessTokenOrAppKey,
                GetNewExternalUserIdPath, request, timeOut);

        /// <summary>
        /// 异步批量获取企业合并后的新 ExternalUserId。
        /// <see href="https://developer.work.weixin.qq.com/document/path/95327"/>
        /// <see href="https://developer.work.weixin.qq.com/document/path/95435"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">企业合并前的 ExternalUserId 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新旧 ExternalUserId 对应关系。</returns>
        public static Task<NewExternalUserIdResult> GetNewExternalUserIdAsync(
            string accessTokenOrAppKey, NewExternalUserIdRequest request,
            int timeOut = Config.TIME_OUT)
            => PostP1Async<NewExternalUserIdResult>(accessTokenOrAppKey,
                GetNewExternalUserIdPath, request, timeOut);

        /// <summary>
        /// 按客户群批量获取企业合并后的新 ExternalUserId。
        /// <see href="https://developer.work.weixin.qq.com/document/path/95327"/>
        /// <see href="https://developer.work.weixin.qq.com/document/path/95435"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">客户群 ID 和企业合并前的 ExternalUserId 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>客户群中新旧 ExternalUserId 对应关系。</returns>
        public static NewExternalUserIdResult GetNewGroupChatExternalUserId(
            string accessTokenOrAppKey, NewGroupChatExternalUserIdRequest request,
            int timeOut = Config.TIME_OUT)
            => PostP1<NewExternalUserIdResult>(accessTokenOrAppKey,
                GetNewGroupChatExternalUserIdPath, request, timeOut);

        /// <summary>
        /// 异步按客户群批量获取企业合并后的新 ExternalUserId。
        /// <see href="https://developer.work.weixin.qq.com/document/path/95327"/>
        /// <see href="https://developer.work.weixin.qq.com/document/path/95435"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">客户群 ID 和企业合并前的 ExternalUserId 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>客户群中新旧 ExternalUserId 对应关系。</returns>
        public static Task<NewExternalUserIdResult> GetNewGroupChatExternalUserIdAsync(
            string accessTokenOrAppKey, NewGroupChatExternalUserIdRequest request,
            int timeOut = Config.TIME_OUT)
            => PostP1Async<NewExternalUserIdResult>(accessTokenOrAppKey,
                GetNewGroupChatExternalUserIdPath, request, timeOut);
    }
}
