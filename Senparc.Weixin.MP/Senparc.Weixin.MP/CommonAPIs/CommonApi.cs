/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：CommonApi.cs
    文件功能描述：通用接口(用于和微信服务器通讯，一般不涉及自有网站服务器的通讯)
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

/*
    API：http://mp.weixin.qq.com/wiki/index.php?title=%E6%8E%A5%E5%8F%A3%E6%96%87%E6%A1%A3&oldid=103
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Menu;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.MP.CommonAPIs
{
    /// <summary>
    /// 通用接口
    /// 通用接口用于和微信服务器通讯，一般不涉及自有网站服务器的通讯
    /// </summary>
    public partial class CommonApi
    {
        /// <summary>
        /// 获取凭证接口
        /// </summary>
        /// <param name="grant_type">获取access_token填写client_credential</param>
        /// <param name="appid">第三方用户唯一凭证</param>
        /// <param name="secret">第三方用户唯一凭证密钥，既appsecret</param>
        /// <returns></returns>
        public static AccessTokenResult GetToken(string appid, string secret, string grant_type = "client_credential")
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type={0}&appid={1}&secret={2}",
                                    grant_type, appid, secret);

            AccessTokenResult result = Get.GetJson<AccessTokenResult>(url);
            return result;
        }

        /// <summary>
        /// 用户信息接口
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static WeixinUserInfoResult GetUserInfo(string accessToken, string openId)
        {
            var url = string.Format("http://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}",
                                    accessToken, openId);
            WeixinUserInfoResult result = Get.GetJson<WeixinUserInfoResult>(url);
            return result;
        }


        //此方法已经过期，新的媒体文件上传方法请见  AdvancedAPIs/Media
        ///// <summary>
        ///// 媒体文件上传接口
        /////注意事项
        /////1.上传的媒体文件限制：
        /////图片（image) : 1MB，支持JPG格式
        /////语音（voice）：1MB，播放长度不超过60s，支持MP4格式
        /////视频（video）：10MB，支持MP4格式
        /////缩略图（thumb)：64KB，支持JPG格式
        /////2.媒体文件在后台保存时间为3天，即3天后media_id失效
        ///// </summary>
        ///// <param name="accessToken"></param>
        ///// <param name="type">上传文件类型</param>
        ///// <param name="fileName">上传文件完整路径+文件名</param>
        ///// <returns></returns>
        //public static UploadMediaFileResult UploadMediaFile(string accessToken, UploadMediaFileType type, string fileName)
        //{
        //    var cookieContainer = new CookieContainer();
        //    var fileStream = FileHelper.GetFileStream(fileName);

        //    var url = string.Format("http://api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}&filename={2}&filelength={3}",
        //        accessToken, type.ToString(), Path.GetFileName(fileName), fileStream != null ? fileStream.Length : 0);
        //    UploadMediaFileResult result = Post.PostGetJson<UploadMediaFileResult>(url, cookieContainer, fileStream);
        //    return result;
        //}

        /// <summary>
        /// 获取调用微信JS接口的临时票据
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="secret"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static JsApiTicketResult GetTicket(string appId, string secret, string type = "jsapi")
        {
            var accessToken = AccessTokenContainer.TryGetToken(appId, secret);

            var url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type={1}",
                                    accessToken, type);

            JsApiTicketResult result = Get.GetJson<JsApiTicketResult>(url);
            return result;
        }
    }
}
