using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;

namespace Senparc.Weixin.QY.CommonAPIs
{
    public static class AccessTokenHandlerWapper
    {
        /// <summary>
        /// 使用AccessToken进行操作时，如果遇到AccessToken错误的情况，重新获取AccessToken一次，并重试
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="fun">第一个参数为accessToken</param>
        /// <param name="retryIfFaild"></param>
        /// <returns></returns>
        public static T Do<T>(string appId, string appSecret, Func<string, T> fun, bool retryIfFaild = true) where T : WxJsonResult
        {
            T result = null;
            try
            {
                var accessToken = AccessTokenContainer.TryGetToken(appId, appSecret, false);
                result = fun(accessToken);
            }
            catch (ErrorJsonResultException ex)
            {
                if (retryIfFaild && ex.JsonResult.errcode == ReturnCode.验证失败)
                {
                    //尝试重新验证
                    var accessToken = AccessTokenContainer.TryGetToken(appId, appSecret, true);
                    result = Do<T>(appId, appSecret, fun, false);
                }
            }
            return result;
        }
    }
}
