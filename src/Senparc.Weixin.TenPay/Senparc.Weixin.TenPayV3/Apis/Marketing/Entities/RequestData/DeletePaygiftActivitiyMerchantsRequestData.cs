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
  
    文件名：DeletePaygiftActivitiyMerchantsRequestData.cs
    文件功能描述：删除活动发券商户号接口请求数据
    
    
    创建标识：Senparc - 20210915
    
----------------------------------------------------------------*/


using Newtonsoft.Json;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    /// <summary>
    /// 删除活动发券商户号接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_7_10.shtml </para>
    /// </summary>
    public class DeletePaygiftActivitiyMerchantsRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="activity_id">活动id  <para>path活动id</para><para>示例值：126002309</para></param>
        /// <param name="merchant_id_list">删除的发券商户号  <para>body从活动已有的发券商户号中移除的商户号列表</para><para>特殊规则：最小字符长度为8，最大为15。</para><para>条目个数限制：[1,500]</para><para>示例值：100123456,100123457</para><para>可为null</para></param>
        /// <param name="delete_request_no">请求业务单据号 <para>body商户创建批次凭据号（格式：商户id+日期+流水号），商户侧需保持唯一性，可包含英文字母，数字，｜，_，*，-等内容，不允许出现其他不合法符号。</para><para>示例值：100002322019090134234sfdf</para><para>可为null</para></param>
        public DeletePaygiftActivitiyMerchantsRequestData(string activity_id, string[] merchant_id_list, string delete_request_no)
        {
            this.activity_id = activity_id;
            this.merchant_id_list = merchant_id_list;
            this.delete_request_no = delete_request_no;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public DeletePaygiftActivitiyMerchantsRequestData()
        {
        }

        /// <summary>
        /// 活动id 
        /// <para>path 活动id </para>
        /// <para>示例值：126002309 </para>
        /// </summary>
        [JsonIgnore]
        public string activity_id { get; set; }

        /// <summary>
        /// 删除的发券商户号 
        /// <para>body 从活动已有的发券商户号中移除的商户号列表 </para>
        /// <para>特殊规则：最小字符长度为8，最大为15。</para>
        /// <para>条目个数限制：[1, 500] </para>
        /// <para>示例值：100123456, 100123457</para>
        /// <para>可为null</para>
        /// </summary>
        public string[] merchant_id_list { get; set; }

        /// <summary>
        /// 请求业务单据号
        /// <para>body 商户创建批次凭据号（格式：商户id+日期+流水号），商户侧需保持唯一性，可包含英文字母，数字，｜，_，*，-等内容，不允许出现其他不合法符号。 </para>
        /// <para>示例值：100002322019090134234sfdf</para>
        /// <para>可为null</para>
        /// </summary>
        public string delete_request_no { get; set; }

    }

}
