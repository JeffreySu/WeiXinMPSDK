/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：MediaApi.cs
    文件功能描述：多媒体文件接口
    
    
    创建标识：Senparc - 20130313
    
    修改标识：Senparc - 20130313
    修改描述：整理接口
 
    修改标识：Senparc - 20130313
    修改描述：开放代理请求超时时间
----------------------------------------------------------------*/

/*
    接口详见：http://qydev.weixin.qq.com/wiki/index.php?title=%E7%AE%A1%E7%90%86%E5%A4%9A%E5%AA%92%E4%BD%93%E6%96%87%E4%BB%B6
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.QY.AdvancedAPIs.Media
{
    /// <summary>
    /// 多媒体文件接口
    /// </summary>
    public static class MediaApi
    {
        /// <summary>
        /// 上传媒体文件
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="type">媒体文件类型，分别有图片（image）、语音（voice）、视频（video），普通文件(file)</param>
        /// <param name="media">form-data中媒体文件标识，有filename、filelength、content-type等信息</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static UploadResultJson Upload(string accessToken, UploadMediaFileType type, string media, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}", accessToken, type.ToString());
            var fileDictionary = new Dictionary<string, string>();
            fileDictionary["media"] = media;
            return HttpUtility.Post.PostFileGetJson<UploadResultJson>(url, null, fileDictionary, null, timeOut);
        }

        /// <summary>
        /// 获取媒体文件
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="mediaId"></param>
        /// <param name="stream"></param>
        public static void Get(string accessToken, string mediaId, Stream stream)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}",
                accessToken, mediaId);
            HttpUtility.Get.Download(url, stream);//todo 异常处理
        }
    }
}
