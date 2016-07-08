/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：ConcernApi.cs
    文件功能描述：二次验证接口
    
    
    创建标识：Senparc - 20150313
    
    修改标识：MysticBoy - 20150414
    修改描述：TwoVerification接口没有参数
----------------------------------------------------------------*/

/*
    官方文档：http://qydev.weixin.qq.com/wiki/index.php?title=%E5%85%B3%E6%B3%A8%E4%B8%8E%E5%8F%96%E6%B6%88%E5%85%B3%E6%B3%A8
 */

using Senparc.Weixin.Entities;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.QY.AdvancedAPIs
{
    /// <summary>
    /// 关注与取消关注
    /// </summary>
    public static class ConcernApi
    {
        /// <summary>
        /// 二次验证
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="userId">员工UserID</param>
        /// <returns></returns>
        public static QyJsonResult TwoVerification(string accessToken, string userId)
        {
            var url =string.Format ( "https://qyapi.weixin.qq.com/cgi-bin/user/authsucc?access_token={0}&userid={1}",accessToken.AsUrlData(), userId.AsUrlData());
            return Get.GetJson<QyJsonResult>(url);
        }
    }
}
