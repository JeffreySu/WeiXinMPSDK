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
  
    文件名：SetBusifavorCouponCodesRequestData.cs
    文件功能描述：设置商家券的Code码接口请求数据
    
    
    创建标识：Senparc - 20210912
    
----------------------------------------------------------------*/


using Newtonsoft.Json;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    /// <summary>
    /// 设置商家券的Code码接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_6.shtml </para>
    /// </summary>
    public class SetBusifavorCouponCodesRequestData
    {
        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="stock_id">批次号  <para>path微信为每个商家券批次分配的唯一ID</para><para>示例值：98065001</para></param>
        /// <param name="coupon_code_list">券code列表  <para>body商户上传的券code列表，code允许包含的字符有0-9、a-z、A-Z、-、_、\、/、=、|。特殊规则：单个券code长度为【1，32】，条目个数限制为【1，200】。</para><para>示例值：ABC9588200，ABC9588201</para><para>可为null</para></param>
        /// <param name="upload_request_no">请求业务单据号  <para>body商户上传code的凭据号，商户侧需保持唯一性。</para><para>示例值：100002322019090134234sfdf</para></param>
        public SetBusifavorCouponCodesRequestData(string stock_id, string[] coupon_code_list, string upload_request_no)
        {
            this.stock_id = stock_id;
            this.coupon_code_list = coupon_code_list;
            this.upload_request_no = upload_request_no;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public SetBusifavorCouponCodesRequestData()
        {
        }

        /// <summary>
        /// 批次号 
        /// <para>path 微信为每个商家券批次分配的唯一ID </para>
        /// <para>示例值：98065001 </para>
        /// </summary>
        [JsonIgnore]
        public string stock_id { get; set; }

        /// <summary>
        /// 券code列表 
        /// <para>body 商户上传的券code列表，code允许包含的字符有0-9、a-z、A-Z、-、_、\、/、=、|。 特殊规则：单个券code长度为【1，32】，条目个数限制为【1，200】。 </para>
        /// <para>示例值：ABC9588200，ABC9588201 </para>
        /// <para>可为null</para>
        /// </summary>
        public string[] coupon_code_list { get; set; }

        /// <summary>
        /// 请求业务单据号 
        /// <para>body 商户上传code的凭据号，商户侧需保持唯一性。 </para>
        /// <para>示例值：100002322019090134234sfdf </para>
        /// </summary>
        public string upload_request_no { get; set; }
    }
}
