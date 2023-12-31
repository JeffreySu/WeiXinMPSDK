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
  
    文件名：QueryProfitsharingBillsRequestData.cs
    文件功能描述：申请分账账单API请求数据
    
    
    创建标识：Senparc - 20210915

----------------------------------------------------------------*/

using Newtonsoft.Json;

namespace Senparc.Weixin.TenPayV3.Apis.Profitsharing
{
    /// <summary>
    /// 申请分账账单API请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_11.shtml </para>
    /// </summary>
    public class QueryProfitsharingBillsRequestData
    {

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public QueryProfitsharingBillsRequestData()
        {
        }

        /// <summary>
        /// 含参构造函数(服务商模式)
        /// </summary>
        /// <param name="sub_mchid">子商户号 <para>不填则默认返回服务商下的所有分账账单。</para><para>如需下载某个子商户下的分账账单，则填指定的子商户号。</para>示例值：19000000001<para></para></param>
        /// <param name="bill_date">账单日期 <para>格式yyyy-MM-DD</para><para>仅支持三个月内的账单下载申请。</para>示例值：2019-06-11<para></para></param>
        /// <param name="tar_type">压缩类型 <para>不填则默认是数据流。</para><para>枚举值：GZIP：返回格式为.gzip的压缩包账单</para>示例值：GZIP<para></para></param>
        public QueryProfitsharingBillsRequestData(string sub_mchid, string bill_date, string tar_type)
        {
            this.sub_mchid = sub_mchid;
            this.bill_date = bill_date;
            this.tar_type = tar_type;
        }

        #region 服务商
        /// <summary>
        /// 子商户号 
        /// <para>不填则默认返回服务商下的所有分账账单。</para>
        /// <para>如需下载某个子商户下的分账账单，则填指定的子商户号。</para>
        /// <para>示例值：19000000001</para>
        /// </summary>
        public string sub_mchid { get; set; }
        #endregion

        /// <summary>
        /// 账单日期
        /// <para>格式yyyy-MM-DD</para>
        /// <para>仅支持三个月内的账单下载申请。</para>
        /// <para>示例值：2019-06-11</para>
        /// </summary>
        [JsonIgnore]
        public string bill_date { get; set; }

        /// <summary>
        /// 压缩类型
        /// <para>不填则默认是数据流</para>
        /// <para>枚举值：GZIP：返回格式为.gzip的压缩包账单</para>
        /// <para>示例值：GZIP</para>
        /// </summary>
        public string tar_type { get; set; }

    }
}
