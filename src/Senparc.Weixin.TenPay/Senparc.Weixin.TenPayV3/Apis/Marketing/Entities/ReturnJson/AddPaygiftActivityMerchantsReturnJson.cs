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
  
    文件名：AddPaygiftActivityMerchantsReturnJson.cs
    文件功能描述：新增活动发券商户号Json类
    
    
    创建标识：Senparc - 20210915
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    /// <summary>
    /// 新增活动发券商户号Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_7_8.shtml </para>
    /// </summary>
    public class AddPaygiftActivityMerchantsReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="activity_id">活动id  <para>活动id</para><para>示例值：126002309</para></param>
        /// <param name="invalid_merchant_id_list">校验失败的发券商户号 <para>未通过规则校验的发券商户号列表</para><para>可为null</para></param>
        /// <param name="add_time">添加时间  <para>成功添加发券商户号的时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>示例值：2015-05-20T13:29:35+08:00</para><para>可为null</para></param>
        public AddPaygiftActivityMerchantsReturnJson(string activity_id, Invalid_Merchant_Id_List[] invalid_merchant_id_list, string add_time)
        {
            this.activity_id = activity_id;
            this.invalid_merchant_id_list = invalid_merchant_id_list;
            this.add_time = add_time;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public AddPaygiftActivityMerchantsReturnJson()
        {
        }

        /// <summary>
        /// 活动id 
        /// <para>活动id </para>
        /// <para>示例值：126002309 </para>
        /// </summary>
        public string activity_id { get; set; }

        /// <summary>
        /// 校验失败的发券商户号
        /// <para>未通过规则校验的发券商户号列表 </para>
        /// <para>可为null</para>
        /// </summary>
        public Invalid_Merchant_Id_List[] invalid_merchant_id_list { get; set; }

        /// <summary>
        /// 添加时间 
        /// <para>成功添加发券商户号的时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。 </para>
        /// <para>示例值：2015-05-20T13:29:35+08:00 </para>
        /// <para>可为null</para>
        /// </summary>
        public string add_time { get; set; }

        #region 子数据类型
        public class Invalid_Merchant_Id_List
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="mchid">商户号  <para>商户号</para><para>示例值：1001234567</para><para>可为null</para></param>
            /// <param name="invalid_reason">无效原因  <para>活动参与商户校验失败的原因</para><para>示例值：商户与活动创建方不具备父子关系</para><para>可为null</para></param>
            public Invalid_Merchant_Id_List(string mchid, string invalid_reason)
            {
                this.mchid = mchid;
                this.invalid_reason = invalid_reason;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Invalid_Merchant_Id_List()
            {
            }

            /// <summary>
            /// 商户号 
            /// <para>商户号 </para>
            /// <para>示例值：1001234567</para>
            /// <para>可为null</para>
            /// </summary>
            public string mchid { get; set; }

            /// <summary>
            /// 无效原因 
            /// <para>活动参与商户校验失败的原因</para>
            /// <para>示例值：商户与活动创建方不具备父子关系</para>
            /// <para>可为null</para>
            /// </summary>
            public string invalid_reason { get; set; }

        }


        #endregion
    }




}


