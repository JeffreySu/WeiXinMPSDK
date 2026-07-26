/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WorkBenchApi.Batch.cs
    文件功能描述：企业微信批量设置成员工作台数据接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 增加批量设置成员工作台数据接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.WorkBench.WorkBenchJson;

namespace Senparc.Weixin.Work.AdvancedAPIs.WorkBench
{
    public partial class WorkBenchApi
    {
        private const string BatchSetWorkBenchDataPath = "/cgi-bin/agent/batch_set_workbench_data";

        /// <summary>
        /// 批量设置成员在应用工作台展示的数据。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/92535">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">应用、成员列表及工作台模板数据。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>批量设置结果。</returns>
        public static WorkJsonResult BatchSetWorkBenchData(string accessTokenOrAppKey,
            BatchSetWorkBenchDataModel data, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<WorkJsonResult>(accessToken,
                Config.ApiWorkHost + BatchSetWorkBenchDataPath + "?access_token={0}", data,
                CommonJsonSendType.POST, timeOut), accessTokenOrAppKey);

        /// <summary>
        /// 异步批量设置成员在应用工作台展示的数据。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/92535">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">应用、成员列表及工作台模板数据。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>批量设置结果。</returns>
        public static Task<WorkJsonResult> BatchSetWorkBenchDataAsync(string accessTokenOrAppKey,
            BatchSetWorkBenchDataModel data, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<WorkJsonResult>(
                accessToken, Config.ApiWorkHost + BatchSetWorkBenchDataPath + "?access_token={0}", data,
                CommonJsonSendType.POST, timeOut), accessTokenOrAppKey);
    }
}
