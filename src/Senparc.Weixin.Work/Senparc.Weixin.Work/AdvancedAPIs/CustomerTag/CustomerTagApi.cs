using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.CustomerTag
{
    /// <summary>
    /// 客户标签管理Api
    /// </summary>
    public class CustomerTagApi
    {
        
        #region 同步方法

        /// <summary>
        /// 获取企业标签库
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "CustomerTagApi.GetCustomerTagList", true)]
        public static GetCorpTagListResult GetCustomerTagList(string accessTokenOrAppKey, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/get_corp_tag_list?access_token={0}", accessToken.AsUrlData());

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<GetCorpTagListResult>(accessToken, url, null, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 添加企业客户标签
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "CustomerTagApi.AddCorpCustomerTag", true)]
        public static AddCorpCustomerTagResult AddCorpCustomerTag(string accessTokenOrAppKey, CorpTagGroup data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/add_corp_tag?access_token={0}", accessToken.AsUrlData());

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<AddCorpCustomerTagResult>(accessToken, url, data, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 编辑企业客户标签
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "CustomerTagApi.EditCorpCustomerTag", true)]
        public static WorkJsonResult EditCorpCustomerTag(string accessTokenOrAppKey, EditCorpCustomerTagRequest data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/edit_corp_tag?access_token={0}", accessToken.AsUrlData());

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<WorkJsonResult>(accessToken, url, data, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 删除企业客户标签
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "CustomerTagApi.DeleteCorpCustomerTag", true)]
        public static WorkJsonResult DeleteCorpCustomerTag(string accessTokenOrAppKey, DeleteCorpCustomerTagRequest data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/del_corp_tag?access_token={0}", accessToken.AsUrlData());

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<WorkJsonResult>(accessToken, url, data, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 编辑客户企业标签标签
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "CustomerTagApi.ExternalContactMarkTag", true)]
        public static WorkJsonResult ExternalContactMarkTag(string accessTokenOrAppKey, ExternalContactMarkTagRequest data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/mark_tag?access_token={0}", accessToken.AsUrlData());

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<WorkJsonResult>(accessToken, url, data, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }

        #endregion

        #region 异步方法

        /// <summary>
        /// 获取企业标签库
        /// </summary>
        /// <param name="accessTokenOrAppkey"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "CustomerTagApi.GetCustomerTagListAsync", true)]
        public static async Task<GetCorpTagListResult> GetCustomerTagListAsync(string accessTokenOrAppkey, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/get_corp_tag_list?access_token={0}", accessToken.AsUrlData());

                return await Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetCorpTagListResult>(accessToken, url, null, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppkey).ConfigureAwait(false);
        }

        /// <summary>
        /// 添加企业客户标签
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "CustomerTagApi.AddCorpCustomerTagAsync", true)]
        public static async Task<AddCorpCustomerTagResult> AddCorpCustomerTagAsync(string accessTokenOrAppKey, CorpTagGroup data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/add_corp_tag?access_token={0}", accessToken.AsUrlData());

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AddCorpCustomerTagResult>(accessToken, url, data, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 编辑企业客户标签
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "CustomerTagApi.EditCorpCustomerTagAsync", true)]
        public static async Task<WorkJsonResult> EditCorpCustomerTagAsync(string accessTokenOrAppKey, EditCorpCustomerTagRequest data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/edit_corp_tag?access_token={0}", accessToken.AsUrlData());

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, url, data, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 删除企业客户标签
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "CustomerTagApi.DeleteCorpCustomerTagAsync", true)]
        public static async Task<WorkJsonResult> DeleteCorpCustomerTagAsync(string accessTokenOrAppKey, DeleteCorpCustomerTagRequest data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/del_corp_tag?access_token={0}", accessToken.AsUrlData());

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, url, data, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 编辑客户企业标签标签
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "CustomerTagApi.ExternalContactMarkTagAsync", true)]
        public static async Task<WorkJsonResult> ExternalContactMarkTagAsync(string accessTokenOrAppKey, ExternalContactMarkTagRequest data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/mark_tag?access_token={0}", accessToken.AsUrlData());

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, url, data, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        #endregion

    }
}