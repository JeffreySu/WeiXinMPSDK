#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：QrCodeAPI.cs
    文件功能描述：二维码接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
 
    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间
 
    修改标识：Senparc - 20150623
    修改描述：添加 用字符串类型创建二维码 接口
 
    修改标识：Senparc - 20160719
    修改描述：增加其接口的异步方法

    修改标识：Senparc - 20160901
    修改描述：v14.3.7 修改Create方法（及对应异步方法），匹配最新的官方文档，删除CreateByStr方法（及对应异步方法）

    修改标识：Senparc - 20170707
    修改描述：v14.5.1 完善异步方法async/await

    修改标识：Senparc - 20170715
    修改描述：v14.5.3 添加 QrCode_ActionName.QR_STR_SCENE

----------------------------------------------------------------*/

/*
    API：http://mp.weixin.qq.com/wiki/index.php?title=%E7%94%9F%E6%88%90%E5%B8%A6%E5%8F%82%E6%95%B0%E7%9A%84%E4%BA%8C%E7%BB%B4%E7%A0%81
 */

using System;
using System.IO;
using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.CO2NET.HttpUtility;
using Senparc.Weixin.MP.AdvancedAPIs.QrCode;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 二维码接口
    /// </summary>
    public static class QrCodeApi
    {
        #region 同步方法

        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="expireSeconds">临时二维码有效时间，以秒为单位。最大不超过2592000（即30天），此字段如果不填，则默认有效期为30秒,永久二维码将忽略此参数</param>
        /// <param name="sceneId">场景值ID，临时二维码时为32位非0整型，永久二维码时最大值为100000（目前参数只支持1--100000）</param>
        /// <param name="sceneStr">场景字符串，字符串类型，长度限制为1到64，仅actionName为QR_LIMIT_STR_SCENE时有效</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="actionName">二维码类型，当actionName为QR_LIMIT_STR_SCENE时，sceneId会被忽略</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "QrCodeApi.Create", true)]
        public static CreateQrCodeResult Create(string accessTokenOrAppId, int expireSeconds, int sceneId, QrCode_ActionName actionName, string sceneStr = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/qrcode/create?access_token={0}";
                object data = null;

                switch (actionName)
                {
                    case QrCode_ActionName.QR_SCENE:
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
                        break;
                    case QrCode_ActionName.QR_LIMIT_SCENE:
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
                        break;
                    case QrCode_ActionName.QR_LIMIT_STR_SCENE:
                        data = new
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
                        break;
                    case QrCode_ActionName.QR_STR_SCENE:
                        data = new
                        {
                            expire_seconds = expireSeconds,
                            action_name = "QR_STR_SCENE",
                            action_info = new
                            {
                                scene = new
                                {
                                    scene_str = sceneStr
                                }
                            }
                        };
                        break;
                    default:
                        //throw new ArgumentOutOfRangeException(nameof(actionName), actionName, null);
                        throw new ArgumentOutOfRangeException(actionName.GetType().Name, actionName, null);
                }

                return CommonJsonSend.Send<CreateQrCodeResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        ///// <summary>
        ///// 用字符串类型创建二维码（永久）
        ///// </summary>
        ///// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        ///// <param name="sceneStr">场景值ID（字符串形式的ID），字符串类型，长度限制为1到64，仅永久二维码支持此字段</param>
        ///// <param name="timeOut"></param>
        ///// <returns></returns>
        //public static CreateQrCodeResult CreateByStr(string accessTokenOrAppId, string sceneStr, int timeOut = Config.TIME_OUT)
        //{
        //    return ApiHandlerWapper.TryCommonApi(accessToken =>
        //    {
        //        var urlFormat = Config.ApiMpHost + "/cgi-bin/qrcode/create?access_token={0}";
        //        var data = new
        //        {
        //            action_name = "QR_LIMIT_STR_SCENE", action_info = new
        //            {
        //                scene = new
        //                {
        //                    scene_str = sceneStr
        //                }
        //            }
        //        };
        //        return CommonJsonSend.Send<CreateQrCodeResult>(accessToken, urlFormat, data, timeOut: timeOut);
        //    }, accessTokenOrAppId);
        //}

        /*此接口无异步方法*/

        /// <summary>
        /// 获取下载二维码的地址
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "QrCodeApi.GetShowQrCodeUrl", true)]
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
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "QrCodeApi.ShowQrCode", true)]
        public static void ShowQrCode(string ticket, Stream stream)
        {
            var url = GetShowQrCodeUrl(ticket);
            Get.Download(url, stream);
        }

        #endregion

        #region 异步方法

        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="expireSeconds">临时二维码有效时间，以秒为单位。最大不超过2592000（即30天），此字段如果不填，则默认有效期为30秒,永久二维码将忽略此参数</param>
        /// <param name="sceneId">场景值ID，临时二维码时为32位非0整型，永久二维码时最大值为100000（目前参数只支持1--100000）</param>
        /// <param name="sceneStr">场景字符串，字符串类型，长度限制为1到64，仅actionName为QR_LIMIT_STR_SCENE时有效</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="actionName">二维码类型，当actionName为QR_LIMIT_STR_SCENE时，sceneId会被忽略</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "QrCodeApi.CreateAsync", true)]
        public static async Task<CreateQrCodeResult> CreateAsync(string accessTokenOrAppId, int expireSeconds, int sceneId, QrCode_ActionName actionName, string sceneStr = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/qrcode/create?access_token={0}";
                object data = null;

                switch (actionName)
                {
                    case QrCode_ActionName.QR_SCENE:
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
                        break;
                    case QrCode_ActionName.QR_LIMIT_SCENE:
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
                        break;
                    case QrCode_ActionName.QR_LIMIT_STR_SCENE:
                        data = new
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
                        break;
                    case QrCode_ActionName.QR_STR_SCENE:
                        data = new
                        {
                            expire_seconds = expireSeconds,
                            action_name = "QR_STR_SCENE",
                            action_info = new
                            {
                                scene = new
                                {
                                    scene_str = sceneStr
                                }
                            }
                        };
                        break;
                    default:
                        //throw new ArgumentOutOfRangeException(nameof(actionName), actionName, null);
                        throw new ArgumentOutOfRangeException(actionName.GetType().Name, actionName, null);
                }

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<CreateQrCodeResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        ///// <summary>
        ///// 【异步方法】用字符串类型创建二维码
        ///// </summary>
        ///// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        ///// <param name="sceneStr">场景值ID（字符串形式的ID），字符串类型，长度限制为1到64，仅永久二维码支持此字段</param>
        ///// <param name="timeOut"></param>
        ///// <returns></returns>
        //public static async Task<CreateQrCodeResult> CreateByStrAsync(string accessTokenOrAppId, string sceneStr, int timeOut = Config.TIME_OUT)
        //{
        //    return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
        //    {
        //        var urlFormat = Config.ApiMpHost + "/cgi-bin/qrcode/create?access_token={0}";
        //        var data = new
        //        {
        //            action_name = "QR_LIMIT_STR_SCENE", action_info = new
        //            {
        //                scene = new
        //                {
        //                    scene_str = sceneStr
        //                }
        //            }
        //        };
        //        return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<CreateQrCodeResult>(accessToken, urlFormat, data, timeOut: timeOut);
        //    }, accessTokenOrAppId);
        //}


        /// <summary>
        ///【异步方法】 获取二维码（不需要AccessToken）
        /// 错误情况下（如ticket非法）返回HTTP错误码404。
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="stream"></param>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "QrCodeApi.ShowQrCodeAsync", true)]
        public static async Task ShowQrCodeAsync(string ticket, Stream stream)
        {
            var url = GetShowQrCodeUrl(ticket);
            await Get.DownloadAsync(url, stream).ConfigureAwait(false);
        }

        #endregion
    }
}