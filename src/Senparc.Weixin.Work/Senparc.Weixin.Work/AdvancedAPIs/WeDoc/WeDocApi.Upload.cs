/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDocApi.Upload.cs
    文件功能描述：企业微信文档图片上传接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐文档编辑图片上传接口

----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDoc
{
    public static partial class WeDocApi
    {
        private const string UploadDocumentImagePath = "/cgi-bin/wedoc/upload_doc_image";

        /// <summary>上传在线文档编辑时使用的图片资源。</summary>
        public static WeDocImageUploadResult UploadDocumentImage(string accessTokenOrAppKey,
            string imageFilePath, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + UploadDocumentImagePath + "?access_token=" +
                          accessToken.AsUrlData();
                var files = new Dictionary<string, string>
                {
                    ["name"] = "media",
                    ["filename"] = imageFilePath
                };
                return Senparc.CO2NET.HttpUtility.Post.PostFileGetJson<WeDocImageUploadResult>(
                    CommonDI.CommonSP, url, null, files,
                    timeOut: timeOut);
            }, accessTokenOrAppKey);

        /// <summary>异步上传在线文档编辑时使用的图片资源。</summary>
        public static Task<WeDocImageUploadResult> UploadDocumentImageAsync(string accessTokenOrAppKey,
            string imageFilePath, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + UploadDocumentImagePath + "?access_token=" +
                          accessToken.AsUrlData();
                var files = new Dictionary<string, string>
                {
                    ["name"] = "media",
                    ["filename"] = imageFilePath
                };
                return await Senparc.CO2NET.HttpUtility.Post.PostFileGetJsonAsync<WeDocImageUploadResult>(
                    CommonDI.CommonSP, url, null, files, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey);
    }
}
