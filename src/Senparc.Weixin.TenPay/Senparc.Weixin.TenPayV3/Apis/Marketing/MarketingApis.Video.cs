#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2025 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2025 Senparc
  
    文件名：MarketingApis.Video.cs
    文件功能描述：微信支付V3营销工具接口 - 视频上传
    
    
    创建标识：Senparc - 20250813
    
----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Trace;
using Senparc.Weixin.TenPayV3.Apis.Marketing;
using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis
{
    /// <summary>
    /// 微信支付V3营销工具接口 - 视频上传
    /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter3_1_1.shtml 下的【营销工具】所有接口 &gt; 【视频上传接口】
    /// </summary>
    public partial class MarketingApis
    {
        #region 视频上传接口
        /*
        /// <summary>
        /// 视频上传API
        /// <para>通过本接口上传视频文件，获取视频MediaID</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_0_1.shtml</para>
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="fileStream">文件流</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public async Task<UploadVideoReturnJson> UploadVideoAsync(string fileName, Stream fileStream, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/merchant/media/video_upload");

            //文件信息
            var fileBytes = fileStream.ReadFully();
            var fileSha256 =  SHA256.Create().ComputeHash(fileBytes).ToHex();

            var formData = new
            {
                filename = fileName,
                sha256 = fileSha256
            };

            //multipart/form-data
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<UploadVideoReturnJson>(url, null, timeOut, 
                apiRequestMethod: ApiRequestMethod.POST, 
                fileBytes: fileBytes, 
                fileName: fileName, 
                formData: formData);
        }


        /// <summary>
        /// 视频上传API（通过文件路径）
        /// <para>通过本接口上传视频文件，获取视频MediaID</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_0_1.shtml</para>
        /// </summary>
        /// <param name="filePath">视频文件路径</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public async Task<UploadVideoReturnJson> UploadVideoAsync(string filePath, int timeOut = Config.TIME_OUT)
        {
            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                var fileName = Path.GetFileName(filePath);
                return await UploadVideoAsync(fileName, fs, timeOut);
            }
        }
        */

        #endregion
    }
}


