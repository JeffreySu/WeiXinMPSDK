/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：ShakeAroundApi.cs
    文件功能描述：摇一摇周边接口
    
    
    创建标识：Senparc - 20150921
----------------------------------------------------------------*/

/*
    官方文档：http://qydev.weixin.qq.com/wiki/index.php?title=%E8%8E%B7%E5%8F%96%E8%AE%BE%E5%A4%87%E5%8F%8A%E7%94%A8%E6%88%B7%E4%BF%A1%E6%81%AF
 */

using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.QY.AdvancedAPIs.ShakeAround;
using Senparc.Weixin.QY.CommonAPIs;

namespace Senparc.Weixin.QY.AdvancedAPIs
{
    public static class ShakeAroundApi
    {
        /// <summary>
        /// 获取设备及用户信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="ticket">摇周边业务的ticket，可在摇到的URL中得到，ticket生效时间为30分钟，每一次摇都会重新生成新的ticket</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetShakeInfoResult GetSuiteToken(string accessToken, string ticket, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/shakearound/getshakeinfo?access_token={0}", accessToken.AsUrlData());

            var data = new
            {
                ticket = ticket
            };

            return CommonJsonSend.Send<GetShakeInfoResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
    }
}
