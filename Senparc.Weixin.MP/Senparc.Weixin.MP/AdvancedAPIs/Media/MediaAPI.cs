/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：MediaAPI.cs
    文件功能描述：多媒体文件接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

/*
    接口详见：http://mp.weixin.qq.com/wiki/index.php?title=%E4%B8%8A%E4%BC%A0%E4%B8%8B%E8%BD%BD%E5%A4%9A%E5%AA%92%E4%BD%93%E6%96%87%E4%BB%B6
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.AdvancedAPIs.GroupMessage;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.MP.AdvancedAPIs.Media
{
    /// <summary>
    /// 多媒体文件接口
    /// </summary>
    public static class MediaApi
    {
        /// <summary>
        /// 上传媒体文件
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="type"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static UploadResultJson Upload(string accessToken, UploadMediaFileType type, string file)
        {
            var url = string.Format("http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}", accessToken, type.ToString());
            var fileDictionary = new Dictionary<string, string>();
            fileDictionary["media"] = file;
            return HttpUtility.Post.PostFileGetJson<UploadResultJson>(url, null, fileDictionary, null);
        }

        /// <summary>
        /// 下载媒体文件
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="mediaId"></param>
        /// <param name="stream"></param>
        public static void Get(string accessToken, string mediaId, Stream stream)
        {
            var url = string.Format("http://file.api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}",
                accessToken, mediaId);
            HttpUtility.Get.Download(url, stream);
        }

        /// <summary>
        /// 上传图文消息素材
        /// </summary>
        /// <param name="accessToken">Token</param>
        /// <param name="news">图文消息组</param>
        /// <returns></returns>
        public static UploadMediaFileResult UploadNews(string accessToken, params NewsModel[] news)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/media/uploadnews?access_token={0}";
            var data = new
            {
                articles = news
            };
            return CommonJsonSend.Send<UploadMediaFileResult>(accessToken, urlFormat, data);
        }


    }
}
