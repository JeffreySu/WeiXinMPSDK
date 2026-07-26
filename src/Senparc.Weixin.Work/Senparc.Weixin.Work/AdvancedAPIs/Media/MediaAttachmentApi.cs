/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MediaAttachmentApi.cs
    文件功能描述：客户朋友圈与商品图册附件上传接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 新增客户朋友圈与商品图册附件上传接口

----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.HttpUtility;
using Senparc.Weixin.Work.AdvancedAPIs.Media;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>客户联系附件上传类型。</summary>
    public enum ExternalAttachmentType
    {
        /// <summary>客户朋友圈附件。</summary>
        Moment = 1,

        /// <summary>商品图册附件。</summary>
        ProductAlbum = 2
    }

    public static partial class MediaApi
    {
        /// <summary>上传客户朋友圈或商品图册附件。</summary>
        public static UploadTemporaryResultJson UploadAttachment(string accessTokenOrAppKey,
            UploadMediaFileType mediaType, ExternalAttachmentType attachmentType, string media,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/media/upload_attachment?access_token=" +
                          accessToken.AsUrlData() + "&media_type=" + mediaType.ToString().AsUrlData() +
                          "&attachment_type=" + ((int)attachmentType).ToString().AsUrlData();
                var files = new Dictionary<string, string>
                {
                    ["name"] = "media",
                    ["filename"] = media
                };
                return Post.PostFileGetJson<UploadTemporaryResultJson>(CommonDI.CommonSP, url, null, files,
                    timeOut: timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>异步上传客户朋友圈或商品图册附件。</summary>
        public static Task<UploadTemporaryResultJson> UploadAttachmentAsync(string accessTokenOrAppKey,
            UploadMediaFileType mediaType, ExternalAttachmentType attachmentType, string media,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/media/upload_attachment?access_token=" +
                          accessToken.AsUrlData() + "&media_type=" + mediaType.ToString().AsUrlData() +
                          "&attachment_type=" + ((int)attachmentType).ToString().AsUrlData();
                var files = new Dictionary<string, string>
                {
                    ["name"] = "media",
                    ["filename"] = media
                };
                return await Post.PostFileGetJsonAsync<UploadTemporaryResultJson>(CommonDI.CommonSP, url, null,
                    files, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey);
        }
    }
}
