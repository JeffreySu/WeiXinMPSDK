/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：ConcernApi.cs
    文件功能描述：二次验证接口
    
    
    创建标识：Senparc - 20150313
    
    修改标识：MysticBoy - 20150414
    修改描述：TwoVerification接口没有参数
 
    修改标识：Senparc - 20160720
    修改描述：增加其接口的异步方法

    修改标识：Senparc - 20170712
    修改描述：v14.5.1 AccessToken HandlerWaper改造
----------------------------------------------------------------*/

/*
    官方文档：http://qydev.weixin.qq.com/wiki/index.php?title=%E5%85%B3%E6%B3%A8%E4%B8%8E%E5%8F%96%E6%B6%88%E5%85%B3%E6%B3%A8
 */

using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Entities;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 关注与取消关注
    /// </summary>
    public static class ConcernApi
    {
        #region 同步方法

        /// <summary>
        /// 二次验证
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="userId">员工UserID</param>
        /// <returns></returns>
        public static WorkJsonResult TwoVerification(string accessTokenOrAppKey, string userId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/user/authsucc?access_token={0}&userid={1}", accessToken.AsUrlData(), userId.AsUrlData());
                return Get.GetJson<WorkJsonResult>(url);
            }, accessTokenOrAppKey);


        }
        #endregion

#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        /// 【异步方法】二次验证
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="userId">员工UserID</param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> TwoVerificationAsync(string accessTokenOrAppKey, string userId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/user/authsucc?access_token={0}&userid={1}", accessToken.AsUrlData(), userId.AsUrlData());
                return await Get.GetJsonAsync<WorkJsonResult>(url);
            }, accessTokenOrAppKey);


        }
        #endregion
#endif
    }
}
