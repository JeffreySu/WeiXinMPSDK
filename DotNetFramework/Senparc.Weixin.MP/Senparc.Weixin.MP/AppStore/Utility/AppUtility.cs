/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
  
    文件名：AppUtility.cs
    文件功能描述：获取RequestMessage中ToUserName中的信息
    
    
    创建标识：Senparc - 20150319
----------------------------------------------------------------*/

using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AppStore.Utility
{
    /// <summary>
    /// 微信请求中ToUserName包含的信息
    /// </summary>
    public class WeixinRequestInfo
    {
        /// <summary>
        /// 使用此应用的微信账号ID（在微微嗨平台上的唯一ID）
        /// </summary>
        public int WeixinId { get; set; }

        /// <summary>
        /// 被请求应用的唯一ID
        /// </summary>
        public int AppId { get; set; }
    }

    public static class AppUtility
    {
        /// <summary>
        /// 获取RequestMessage中ToUserName中的信息（这条信息由微微嗨平台向APP发出）
        /// </summary>
        /// <param name="toUserName">RequestMessage中的ToUserName属性</param>
        /// <returns></returns>
        public static WeixinRequestInfo GetWeixinRequestInfo(string toUserName)
        {
            var info = new WeixinRequestInfo();
            try
            {
                var data = toUserName.Split('_');
                info.WeixinId = int.Parse(data[1]);
                info.AppId = int.Parse(data[2]);
            }
            catch
            {
            }
            return info;
        }

        /// <summary>
        /// 获取RequestMessage中ToUserName中的信息（这条信息由微微嗨平台向APP发出）
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public static WeixinRequestInfo GetWeixinRequestInfo(this IRequestMessageBase requestMessage)
        {
            return GetWeixinRequestInfo(requestMessage.ToUserName);
        }
    }
}
