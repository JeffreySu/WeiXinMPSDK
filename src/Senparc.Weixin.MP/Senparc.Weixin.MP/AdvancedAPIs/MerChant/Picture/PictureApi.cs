#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc
    
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
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.HttpUtility;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.MP.AdvancedAPIs.MerChant
{
    /// <summary>
    /// 微小店图片接口
    /// </summary>
    public static class PictureApi
    {
        #region 同步方法
        public static PictureResult UploadImg(string accessToken, string fileName)
        {
            var urlFormat = Config.ApiMpHost + "/merchant/common/upload_img?access_token={0}&filename={1}";
            var url = string.IsNullOrEmpty(accessToken) ? urlFormat : string.Format(urlFormat, accessToken.AsUrlData(), fileName.AsUrlData());

            var json = new PictureResult();

            using (var fs = FileHelper.GetFileStream(fileName))
            {
                var jsonText = RequestUtility.HttpPost(url, null, fs);
                json = Senparc.Weixin.HttpUtility.Post.GetResult<PictureResult>(jsonText);
            }
            return json;
        }
        #endregion

#if !NET35 && !NET40
        #region 异步方法
        public static async Task<PictureResult> UploadImgAsync(string accessToken, string fileName)
        {
            var urlFormat = Config.ApiMpHost + "/merchant/common/upload_img?access_token={0}&filename={1}";
            var url = string.IsNullOrEmpty(accessToken) ? urlFormat : string.Format(urlFormat, accessToken.AsUrlData(), fileName.AsUrlData());

            var json = new PictureResult();

            using (var fs = FileHelper.GetFileStream(fileName))
            {
                var jsonText = await RequestUtility.HttpPostAsync(url, null, fs);
                json = Senparc.Weixin.HttpUtility.Post.GetResult<PictureResult>(jsonText);
            }
            return json;
        }
        #endregion
#endif
    }
}
