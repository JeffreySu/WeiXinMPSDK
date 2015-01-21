using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.QY.AdvancedAPIs
{
    //接口详见：http://qydev.weixin.qq.com/wiki/index.php?title=%E7%AE%A1%E7%90%86%E5%A4%9A%E5%AA%92%E4%BD%93%E6%96%87%E4%BB%B6

    /// <summary>
    /// 多媒体文件接口
    /// </summary>
    public static class Media
    {
        /// <summary>
        /// 上传媒体文件
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="type">媒体文件类型，分别有图片（image）、语音（voice）、视频（video），普通文件(file)</param>
        /// <param name="media">form-data中媒体文件标识，有filename、filelength、content-type等信息</param>
        /// <returns></returns>
        public static UploadResultJson Upload(string accessToken, UploadMediaFileType type, string media)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}", accessToken, type.ToString());
            var fileDictionary = new Dictionary<string, string>();
            fileDictionary["media"] = media;
            return HttpUtility.Post.PostFileGetJson<UploadResultJson>(url, null, fileDictionary, null);
        }

        /// <summary>
        /// 获取媒体文件
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="mediaId"></param>
        public static void Get(string accessToken, string mediaId, Stream stream)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}",
                accessToken, mediaId);
            HttpUtility.Get.Download(url, stream);//todo 异常处理
        }
    }
}
