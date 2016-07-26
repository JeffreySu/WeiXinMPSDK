﻿/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：OrderApi.cs
    文件功能描述：微小店图片接口
    
    
    创建标识：Senparc - 20150827
 
    修改标识：Senparc - 20160721
    修改描述：增加UploadImg的异步方法
----------------------------------------------------------------*/

/* 
   微小店接口，官方API：http://mp.weixin.qq.com/wiki/index.php?title=%E5%BE%AE%E4%BF%A1%E5%B0%8F%E5%BA%97%E6%8E%A5%E5%8F%A3
*/

using System.Threading.Tasks;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.MP.AdvancedAPIs.MerChant
{
    /// <summary>
    /// 微小店图片接口
    /// </summary>
    public static class PictureApi
    {
        #region 同步请求
        public static PictureResult UploadImg(string accessToken, string fileName)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/common/upload_img?access_token={0}&filename={1}";
            var url = string.IsNullOrEmpty(accessToken) ? urlFormat : string.Format(urlFormat, accessToken.AsUrlData(), fileName.AsUrlData());

            var json = new PictureResult();

            using (var fs = FileHelper.GetFileStream(fileName))
            {
                var jsonText = RequestUtility.HttpPost(url, null, fs);
                json = Post.GetResult<PictureResult>(jsonText);
            }
            return json;
        }
        #endregion
        #region 异步请求
        public static async Task<PictureResult> UploadImgAsync(string accessToken, string fileName)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/common/upload_img?access_token={0}&filename={1}";
            var url = string.IsNullOrEmpty(accessToken) ? urlFormat : string.Format(urlFormat, accessToken.AsUrlData(), fileName.AsUrlData());

            var json = new PictureResult();

            using (var fs = FileHelper.GetFileStream(fileName))
            {
                var jsonText = await RequestUtility.HttpPostAsync( url, null, fs);
                json = Post.GetResult<PictureResult>(jsonText);
            }
            return json;
        }
        #endregion

    }
}
