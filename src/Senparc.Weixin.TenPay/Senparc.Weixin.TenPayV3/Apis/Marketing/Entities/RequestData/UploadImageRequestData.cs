#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2024 Senparc
  
    文件名：UploadImageRequestData.cs
    文件功能描述：图片上传接口请求数据
    
    
    创建标识：Senparc - 20210922
    
----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    /// <summary>
    /// 图片上传接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_0_1.shtml </para>
    /// </summary>
    public class UploadImageRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="file">图片文件  <para>body将媒体图片进行二进制转换，得到的媒体图片二进制内容，在请求body中上传此二进制内容。媒体图片只支持JPG、BMP、PNG格式，文件大小不能超过2M。</para><para>示例值：pic1</para></param>
        /// <param name="meta">媒体文件元信息 <para>body媒体文件元信息，使用json表示，包含两个对象：filename、sha256。</para></param>
        public UploadImageRequestData(string/*message*/ file, Meta meta)
        {
            this.file = file;
            this.meta = meta;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public UploadImageRequestData()
        {
        }

        /// <summary>
        /// 图片文件 
        /// <para>body 将媒体图片进行二进制转换，得到的媒体图片二进制内容，在请求body中上传此二进制内容。媒体图片只支持JPG、BMP、PNG格式，文件大小不能超过2M。</para>
        /// <para>示例值：pic1 </para>
        /// </summary>
        public string file { get; set; }

        /// <summary>
        /// 媒体文件元信息
        /// <para>body 媒体文件元信息，使用json表示，包含两个对象：filename、sha256。</para>
        /// </summary>
        public Meta meta { get; set; }

        #region 子数据类型
        public class Meta
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="filename">文件名称  <para>商户上传的媒体图片的名称，商户自定义，必须以JPG、BMP、PNG为后缀。</para><para>示例值：filea.jpg</para></param>
            /// <param name="sha256">文件摘要  <para>图片文件的文件摘要，即对图片文件的二进制内容进行sha256计算得到的值。</para><para>示例值：hjkahkjsjkfsjk78687dhjahdajhk</para></param>
            public Meta(string filename, string sha256)
            {
                this.filename = filename;
                this.sha256 = sha256;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Meta()
            {
            }

            /// <summary>
            /// 文件名称 
            /// <para>商户上传的媒体图片的名称，商户自定义，必须以JPG、BMP、PNG为后缀。 </para>
            /// <para>示例值：filea.jpg</para>
            /// </summary>
            public string filename { get; set; }

            /// <summary>
            /// 文件摘要 
            /// <para>图片文件的文件摘要，即对图片文件的二进制内容进行sha256计算得到的值。 </para>
            /// <para>示例值：hjkahkjsjkfsjk78687dhjahdajhk</para>
            /// </summary>
            public string sha256 { get; set; }

        }
        #endregion
    }




}
