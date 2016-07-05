/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：QrCodeAPI.cs
    文件功能描述：二维码接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
 
    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间
 
    修改标识：Senparc - 20150623
    修改描述：添加 用字符串类型创建二维码 接口
----------------------------------------------------------------*/

/*
    API：http://mp.weixin.qq.com/wiki/index.php?title=%E7%94%9F%E6%88%90%E5%B8%A6%E5%8F%82%E6%95%B0%E7%9A%84%E4%BA%8C%E7%BB%B4%E7%A0%81
 */

using System.IO;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.AdvancedAPIs.QrCode;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 二维码接口
    /// </summary>
    public static class QrCodeApi
    {
        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="expireSeconds">该二维码有效时间，以秒为单位。 最大不超过1800。0时为永久二维码</param>
        /// <param name="sceneId">场景值ID，临时二维码时为32位整型，永久二维码时最大值为1000</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CreateQrCodeResult Create(string accessTokenOrAppId, int expireSeconds, int sceneId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}";
                object data = null;
                if (expireSeconds > 0)
                {
                    data = new
                    {
                        expire_seconds = expireSeconds,
                        action_name = "QR_SCENE",
                        action_info = new
                        {
                            scene = new
                            {
                                scene_id = sceneId
                            }
                        }
                    };
                }
                else
                {
                    data = new
                    {
                        action_name = "QR_LIMIT_SCENE",
                        action_info = new
                        {
                            scene = new
                            {
                                scene_id = sceneId
                            }
                        }
                    };
                }
                return CommonJsonSend.Send<CreateQrCodeResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 用字符串类型创建二维码
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="sceneStr">场景值ID（字符串形式的ID），字符串类型，长度限制为1到64，仅永久二维码支持此字段</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static CreateQrCodeResult CreateByStr(string accessTokenOrAppId, string sceneStr, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}";
                var data = new
                {
                    action_name = "QR_LIMIT_STR_SCENE",
                    action_info = new
                    {
                        scene = new
                        {
                            scene_str = sceneStr
                        }
                    }
                };
                return CommonJsonSend.Send<CreateQrCodeResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取下载二维码的地址
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public static string GetShowQrCodeUrl(string ticket)
        {
            var urlFormat = "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket={0}";
            return string.Format(urlFormat, ticket.AsUrlData());
        }

        /// <summary>
        /// 获取二维码（不需要AccessToken）
        /// 错误情况下（如ticket非法）返回HTTP错误码404。
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="stream"></param>
        public static void ShowQrCode(string ticket, Stream stream)
        {
            var url = GetShowQrCodeUrl(ticket);
            Get.Download(url, stream);
        }

    }
}