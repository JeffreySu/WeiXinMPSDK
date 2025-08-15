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
  
    文件名：BankComponentRequestData.cs
    文件功能描述：银行组件 - API 请求数据
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.TenPayV3.Apis.BankComponent
{
    /// <summary>
    /// 银行组件 - 获取对私银行卡号开户银行API 请求数据
    /// </summary>
    public class QueryBankRequestData
    {
        /// <summary>
        /// 银行卡号
        /// <para>用于查询的银行卡号，需要进行加密处理</para>
        /// <para>示例值：6225880137005***</para>
        /// </summary>
        public string account_number { get; set; }
    }

    /// <summary>
    /// 银行组件 - 查询支持个人业务的银行列表API 请求数据
    /// </summary>
    public class QueryBankListRequestData
    {
        /// <summary>
        /// 偏移量
        /// <para>分页查询的偏移量</para>
        /// <para>示例值：0</para>
        /// </summary>
        public int? offset { get; set; }

        /// <summary>
        /// 限制数量
        /// <para>分页查询的限制数量，最大值为50</para>
        /// <para>示例值：20</para>
        /// </summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 银行组件 - 查询城市列表API 请求数据
    /// </summary>
    public class QueryCityListRequestData
    {
        /// <summary>
        /// 省份编码
        /// <para>省份的编码，用于查询该省份下的城市列表</para>
        /// <para>示例值：110000</para>
        /// </summary>
        public string province_code { get; set; }
    }

    /// <summary>
    /// 银行组件 - 查询支行列表API 请求数据
    /// </summary>
    public class QueryBranchListRequestData
    {
        /// <summary>
        /// 银行别名编码
        /// <para>银行的别名编码</para>
        /// <para>示例值：ICBC</para>
        /// </summary>
        public string bank_alias_code { get; set; }

        /// <summary>
        /// 城市编码
        /// <para>城市的编码</para>
        /// <para>示例值：110100</para>
        /// </summary>
        public string city_code { get; set; }

        /// <summary>
        /// 偏移量
        /// <para>分页查询的偏移量</para>
        /// <para>示例值：0</para>
        /// </summary>
        public int? offset { get; set; }

        /// <summary>
        /// 限制数量
        /// <para>分页查询的限制数量，最大值为50</para>
        /// <para>示例值：20</para>
        /// </summary>
        public int? limit { get; set; }
    }
}
