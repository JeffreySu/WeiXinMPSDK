/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：OpenHardwareApi.Visitor.cs
    文件功能描述：企业微信智慧硬件访客数据占位接口


    创建标识：Senparc - 20260726

    修改标识：Senparc - 20260726
    修改描述：v3.32.1 保留无权限访客接口名称并显式阻止未知协议请求

----------------------------------------------------------------*/

using System;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.OpenHardware
{
    /// <summary>
    /// 企业微信智慧硬件访客数据接口。
    /// </summary>
    public static partial class OpenHardwareApi
    {
        private const string VisitorApiUnavailableReason =
            "企业微信官方文档在已登录账号下仍显示“暂无权限查看”，" +
            "SDK 无法核验接口路径、鉴权方式、请求字段和响应字段；" +
            "当前仅保留接口名称，不会发起未经验证的网络请求。";

        /// <summary>
        /// 全量获取访客数据。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96060"/></para>
        /// <para>当前官方正文无权访问，本方法仅作为接口名称占位，调用时始终抛出异常。</para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证；当前不会被发送。</param>
        /// <param name="data">全量访客数据占位请求；当前不会被发送。</param>
        /// <param name="timeOut">请求超时时间；当前不会发起网络请求。</param>
        /// <returns>此占位方法不会返回结果。</returns>
        /// <exception cref="NotSupportedException">
        /// 官方文档 96060 的正文无权访问，无法安全实现接口协议。
        /// </exception>
        public static OpenHardwareGetVisitorByPageResult GetVisitorByPage(
            string deviceAccessToken, OpenHardwareGetVisitorByPageRequest data,
            int timeOut = Config.TIME_OUT)
            => throw CreateVisitorApiUnavailableException(
                "全量获取访客数据", "96060");

        /// <summary>
        /// 异步全量获取访客数据。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96060"/></para>
        /// <para>当前官方正文无权访问，本方法仅作为接口名称占位，等待任务时始终抛出异常。</para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证；当前不会被发送。</param>
        /// <param name="data">全量访客数据占位请求；当前不会被发送。</param>
        /// <param name="timeOut">请求超时时间；当前不会发起网络请求。</param>
        /// <returns>包含异常的任务。</returns>
        /// <exception cref="NotSupportedException">
        /// 官方文档 96060 的正文无权访问，无法安全实现接口协议。
        /// </exception>
        public static Task<OpenHardwareGetVisitorByPageResult> GetVisitorByPageAsync(
            string deviceAccessToken, OpenHardwareGetVisitorByPageRequest data,
            int timeOut = Config.TIME_OUT)
            => Task.FromException<OpenHardwareGetVisitorByPageResult>(
                CreateVisitorApiUnavailableException(
                    "全量获取访客数据", "96060"));

        /// <summary>
        /// 获取指定访客数据。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96061"/></para>
        /// <para>当前官方正文无权访问，本方法仅作为接口名称占位，调用时始终抛出异常。</para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证；当前不会被发送。</param>
        /// <param name="data">指定访客数据占位请求；当前不会被发送。</param>
        /// <param name="timeOut">请求超时时间；当前不会发起网络请求。</param>
        /// <returns>此占位方法不会返回结果。</returns>
        /// <exception cref="NotSupportedException">
        /// 官方文档 96061 的正文无权访问，无法安全实现接口协议。
        /// </exception>
        public static OpenHardwareGetVisitorByIdsResult GetVisitorByIds(
            string deviceAccessToken, OpenHardwareGetVisitorByIdsRequest data,
            int timeOut = Config.TIME_OUT)
            => throw CreateVisitorApiUnavailableException(
                "获取指定访客数据", "96061");

        /// <summary>
        /// 异步获取指定访客数据。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96061"/></para>
        /// <para>当前官方正文无权访问，本方法仅作为接口名称占位，等待任务时始终抛出异常。</para>
        /// </summary>
        /// <param name="deviceAccessToken">设备调用凭证；当前不会被发送。</param>
        /// <param name="data">指定访客数据占位请求；当前不会被发送。</param>
        /// <param name="timeOut">请求超时时间；当前不会发起网络请求。</param>
        /// <returns>包含异常的任务。</returns>
        /// <exception cref="NotSupportedException">
        /// 官方文档 96061 的正文无权访问，无法安全实现接口协议。
        /// </exception>
        public static Task<OpenHardwareGetVisitorByIdsResult> GetVisitorByIdsAsync(
            string deviceAccessToken, OpenHardwareGetVisitorByIdsRequest data,
            int timeOut = Config.TIME_OUT)
            => Task.FromException<OpenHardwareGetVisitorByIdsResult>(
                CreateVisitorApiUnavailableException(
                    "获取指定访客数据", "96061"));

        private static NotSupportedException CreateVisitorApiUnavailableException(
            string interfaceName, string documentPath)
            => new NotSupportedException(
                $"企业微信智慧硬件接口“{interfaceName}”（官方文档 {documentPath}）" +
                $"暂未支持。{VisitorApiUnavailableReason}");
    }
}
