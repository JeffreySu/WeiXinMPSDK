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
  
    文件名：BuildPartnershipsRequestData.cs
    文件功能描述：建立合作关系接口请求数据
    
    
    创建标识：Senparc - 20210915
    
----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    /// <summary>
    /// 建立合作关系接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_5_1.shtml </para>
    /// </summary>
    public class BuildPartnershipsRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="partner">合作方信息 <para>body合作方相关的信息。</para></param>
        /// <param name="authorized_data">被授权数据 <para>body被授权的数据。</para></param>
        public BuildPartnershipsRequestData(Partner partner, Authorized_Data authorized_data)
        {
            this.partner = partner;
            this.authorized_data = authorized_data;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public BuildPartnershipsRequestData()
        {
        }

        /// <summary>
        /// 合作方信息
        /// <para>body 合作方相关的信息。 </para>
        /// </summary>
        public Partner partner { get; set; }

        /// <summary>
        /// 被授权数据
        /// <para>body 被授权的数据。</para>
        /// </summary>
        public Authorized_Data authorized_data { get; set; }

        #region 子数据类型
        public class Partner
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="type">合作方类别  <para>合作方类别，枚举值：APPID：合作方为APPIDMERCHANT：合作方为商户</para><para>示例值：APPID</para></param>
            /// <param name="appid">合作方APPID  <para>合作方APPID，合作方类别为APPID时必填。</para><para>示例值：wx4e1916a585d1f4e9</para><para>可为null</para></param>
            /// <param name="merchant_id">合作方商户ID  <para>合作方商户ID，合作方类别为MERCHANT时必填。</para><para>示例值：2480029552</para><para>可为null</para></param>
            public Partner(string type, string appid, string merchant_id)
            {
                this.type = type;
                this.appid = appid;
                this.merchant_id = merchant_id;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Partner()
            {
            }

            /// <summary>
            /// 合作方类别 
            /// <para>合作方类别，枚举值： APPID：合作方为APPID MERCHANT：合作方为商户 </para>
            /// <para>示例值：APPID </para>
            /// </summary>
            public string type { get; set; }

            /// <summary>
            /// 合作方APPID 
            /// <para>合作方APPID，合作方类别为APPID时必填。 </para>
            /// <para>示例值：wx4e1916a585d1f4e9 </para>
            /// <para>可为null</para>
            /// </summary>
            public string appid { get; set; }

            /// <summary>
            /// 合作方商户ID 
            /// <para>合作方商户ID，合作方类别为MERCHANT时必填。 </para>
            /// <para>示例值：2480029552 </para>
            /// <para>可为null</para>
            /// </summary>
            public string merchant_id { get; set; }

        }

        public class Authorized_Data
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="business_type">授权业务类别  <para>授权业务类别，枚举值：FAVOR_STOCK：代金券批次BUSIFAVOR_STOCK：商家券批次</para><para>示例值：FAVOR_STOCK</para></param>
            /// <param name="stock_id">授权批次ID  <para>授权批次ID，授权业务类别为商家券批次或代金券批次时，此参数必填。</para><para>示例值：2433405</para><para>可为null</para></param>
            public Authorized_Data(string business_type, string stock_id)
            {
                this.business_type = business_type;
                this.stock_id = stock_id;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Authorized_Data()
            {
            }

            /// <summary>
            /// 授权业务类别 
            /// <para>授权业务类别，枚举值： FAVOR_STOCK：代金券批次 BUSIFAVOR_STOCK：商家券批次 </para>
            /// <para>示例值：FAVOR_STOCK </para>
            /// </summary>
            public string business_type { get; set; }

            /// <summary>
            /// 授权批次ID 
            /// <para>授权批次ID，授权业务类别为商家券批次或代金券批次时，此参数必填。 </para>
            /// <para>示例值：2433405 </para>
            /// <para>可为null</para>
            /// </summary>
            public string stock_id { get; set; }

        }


        #endregion
    }

}
