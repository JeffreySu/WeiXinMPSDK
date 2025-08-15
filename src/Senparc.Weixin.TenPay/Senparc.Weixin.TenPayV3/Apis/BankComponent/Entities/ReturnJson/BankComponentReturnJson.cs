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
  
    文件名：BankComponentReturnJson.cs
    文件功能描述：银行组件 - API 返回信息
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.TenPayV3.Apis.BankComponent
{
    /// <summary>
    /// 银行组件 - 获取对私银行卡号开户银行API 返回信息
    /// </summary>
    public class QueryBankReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 银行别名编码
        /// <para>银行的别名编码</para>
        /// <para>示例值：ICBC</para>
        /// </summary>
        public string bank_alias_code { get; set; }

        /// <summary>
        /// 银行别名
        /// <para>银行的别名</para>
        /// <para>示例值：工商银行</para>
        /// </summary>
        public string bank_alias { get; set; }

        /// <summary>
        /// 账户类型
        /// <para>银行卡的账户类型</para>
        /// <para>CREDIT：信用卡；DEBIT：借记卡</para>
        /// <para>示例值：DEBIT</para>
        /// </summary>
        public string account_type { get; set; }
    }

    /// <summary>
    /// 银行组件 - 查询支持个人业务的银行列表API 返回信息
    /// </summary>
    public class QueryBankListReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 银行信息列表
        /// <para>支持个人业务的银行信息列表</para>
        /// </summary>
        public BankInfo[] data { get; set; }

        /// <summary>
        /// 总数量
        /// <para>银行总数量</para>
        /// <para>示例值：100</para>
        /// </summary>
        public int total_count { get; set; }
    }

    /// <summary>
    /// 银行信息
    /// </summary>
    public class BankInfo
    {
        /// <summary>
        /// 银行别名编码
        /// <para>银行的别名编码</para>
        /// <para>示例值：ICBC</para>
        /// </summary>
        public string bank_alias_code { get; set; }

        /// <summary>
        /// 银行别名
        /// <para>银行的别名</para>
        /// <para>示例值：工商银行</para>
        /// </summary>
        public string bank_alias { get; set; }

        /// <summary>
        /// 需要填写支行
        /// <para>该银行是否需要填写支行信息</para>
        /// <para>示例值：true</para>
        /// </summary>
        public bool need_branch { get; set; }
    }

    /// <summary>
    /// 银行组件 - 查询省份列表API 返回信息
    /// </summary>
    public class QueryProvinceListReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 省份信息列表
        /// <para>省份信息列表</para>
        /// </summary>
        public ProvinceInfo[] data { get; set; }
    }

    /// <summary>
    /// 省份信息
    /// </summary>
    public class ProvinceInfo
    {
        /// <summary>
        /// 省份编码
        /// <para>省份的编码</para>
        /// <para>示例值：110000</para>
        /// </summary>
        public string province_code { get; set; }

        /// <summary>
        /// 省份名称
        /// <para>省份的名称</para>
        /// <para>示例值：北京市</para>
        /// </summary>
        public string province_name { get; set; }
    }

    /// <summary>
    /// 银行组件 - 查询城市列表API 返回信息
    /// </summary>
    public class QueryCityListReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 城市信息列表
        /// <para>城市信息列表</para>
        /// </summary>
        public CityInfo[] data { get; set; }
    }

    /// <summary>
    /// 城市信息
    /// </summary>
    public class CityInfo
    {
        /// <summary>
        /// 城市编码
        /// <para>城市的编码</para>
        /// <para>示例值：110100</para>
        /// </summary>
        public string city_code { get; set; }

        /// <summary>
        /// 城市名称
        /// <para>城市的名称</para>
        /// <para>示例值：北京市</para>
        /// </summary>
        public string city_name { get; set; }
    }

    /// <summary>
    /// 银行组件 - 查询支行列表API 返回信息
    /// </summary>
    public class QueryBranchListReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 支行信息列表
        /// <para>支行信息列表</para>
        /// </summary>
        public BranchInfo[] data { get; set; }

        /// <summary>
        /// 总数量
        /// <para>支行总数量</para>
        /// <para>示例值：100</para>
        /// </summary>
        public int total_count { get; set; }
    }

    /// <summary>
    /// 支行信息
    /// </summary>
    public class BranchInfo
    {
        /// <summary>
        /// 支行编码
        /// <para>支行的编码</para>
        /// <para>示例值：102100000123</para>
        /// </summary>
        public string branch_code { get; set; }

        /// <summary>
        /// 支行名称
        /// <para>支行的名称</para>
        /// <para>示例值：工商银行北京分行</para>
        /// </summary>
        public string branch_name { get; set; }
    }
}
