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
  
    文件名：ResponseRequestData.cs
    文件功能描述：提交回复接口请求数据
    
    
    创建标识：Senparc - 20210925

----------------------------------------------------------------*/



using Newtonsoft.Json;

namespace Senparc.Weixin.TenPayV3.Apis.Complaint
{
    /// <summary>
    /// 提交回复接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_14.shtml </para>
    /// </summary>
    public class ResponseRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="complaint_id">投诉单号 <para>path投诉单对应的投诉单号</para><para>示例值：200201820200101080076610000</para></param>
        /// <param name="complainted_mchid">被诉商户号 <para>body投诉单对应的被诉商户号</para><para>示例值：1900012181</para></param>
        /// <param name="response_content">回复内容 <para>body具体的投诉处理方案，限制200个字符以内。</para><para>示例值：已与用户沟通解决</para></param>
        /// <param name="response_images">回复图片 <para>body传入调用商户上传反馈图片接口返回的media_id，最多上传4张图片凭证</para><para>示例值：file23578_21798531.jpg</para><para>可为null</para></param>
        public ResponseRequestData(string complaint_id, string complainted_mchid, string response_content, string[] response_images)
        {
            this.complaint_id = complaint_id;
            this.complainted_mchid = complainted_mchid;
            this.response_content = response_content;
            this.response_images = response_images;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public ResponseRequestData()
        {
        }

        /// <summary>
        /// 投诉单号
        /// <para>path投诉单对应的投诉单号 </para>
        /// <para>示例值：200201820200101080076610000</para>
        /// </summary>
        [JsonIgnore]
        public string complaint_id { get; set; }

        /// <summary>
        /// 被诉商户号
        /// <para>body投诉单对应的被诉商户号 </para>
        /// <para>示例值：1900012181</para>
        /// </summary>
        public string complainted_mchid { get; set; }

        /// <summary>
        /// 回复内容
        /// <para>body具体的投诉处理方案，限制200个字符以内。 </para>
        /// <para>示例值：已与用户沟通解决</para>
        /// </summary>
        public string response_content { get; set; }

        /// <summary>
        /// 回复图片
        /// <para>body传入调用商户上传反馈图片接口返回的media_id，最多上传4张图片凭证 </para>
        /// <para>示例值：file23578_21798531.jpg</para>
        /// <para>可为null</para>
        /// </summary>
        public string[] response_images { get; set; }

    }


}
