/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：AppApi.cs
    文件功能描述：管理企业号应用接口
    
    
    创建标识：Senparc - 20150316
  
    增加功能：获取应用概况列表
    修改标识：Bemguin - 20150614 
 
    修改标识：Senparc - 20160720
    修改描述：增加其接口的异步方法

    修改标识：Senparc - 20170313
    修改描述：v4.2.3 AppApi.SetApp()方法改为POST请求方式

    修改标识：Senparc - 20170712
    修改描述：v14.5.1 AccessToken HandlerWaper改造

----------------------------------------------------------------*/

/*
    官方文档：http://qydev.weixin.qq.com/wiki/index.php?title=%E7%AE%A1%E7%90%86%E4%BC%81%E4%B8%9A%E5%8F%B7%E5%BA%94%E7%94%A8
 */

using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.QY.AdvancedAPIs.App;

namespace Senparc.Weixin.QY.AdvancedAPIs
{
    /// <summary>
    /// 管理企业号应用
    /// </summary>
    public static class AppApi
    {
        #region 同步请求


        /// <summary>
        /// 获取企业号应用信息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetAppInfoResult GetAppInfo(string accessTokenOrAppKey, int agentId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/agent/get?access_token={0}&agentid={1}", accessToken.AsUrlData(), agentId.ToString("d").AsUrlData());

                return Get.GetJson<GetAppInfoResult>(url);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 设置企业号应用
        /// 此App只能修改现有的并且有权限管理的应用，无法创建新应用（因为新应用没有权限）
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="data">设置应用需要Post的数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static QyJsonResult SetApp(string accessTokenOrAppKey, SetAppPostData data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/agent/set?access_token={0}", accessToken.AsUrlData());

                return Post.PostGetJson<QyJsonResult>(url, formData: null);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 获取应用概况列表
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetAppListResult GetAppList(string accessTokenOrAppKey, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/agent/list?access_token={0}", accessToken.AsUrlData());

                return Get.GetJson<GetAppListResult>(url);
            }, accessTokenOrAppKey);


        }
        #endregion

#if !NET35 && !NET40
        #region 异步请求
        /// <summary>
        /// 【异步方法】获取企业号应用信息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetAppInfoResult> GetAppInfoAsync(string accessTokenOrAppKey, int agentId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/agent/get?access_token={0}&agentid={1}", accessToken.AsUrlData(), agentId.ToString("d").AsUrlData());

                return await Get.GetJsonAsync<GetAppInfoResult>(url);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】设置企业号应用
        /// 此App只能修改现有的并且有权限管理的应用，无法创建新应用（因为新应用没有权限）
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="data">设置应用需要Post的数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<QyJsonResult> SetAppAsync(string accessTokenOrAppKey, SetAppPostData data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/agent/set?access_token={0}", accessToken.AsUrlData());

                return await Post.PostGetJsonAsync<QyJsonResult>(url, formData: null);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】获取应用概况列表
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetAppListResult> GetAppListAsync(string accessTokenOrAppKey, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/agent/list?access_token={0}", accessToken.AsUrlData());

                return await Get.GetJsonAsync<GetAppListResult>(url);
            }, accessTokenOrAppKey);


        }
        #endregion
#endif
    }
}
