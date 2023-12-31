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
  
    文件名：QueryGuideReturnJson.cs
    文件功能描述：服务人员查询返回Json类
    
    
    创建标识：Senparc - 20210925
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.PayScore
{
    /// <summary>
    /// 服务人员查询返回Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_4_3.shtml </para>
    /// </summary>
    public class QueryGuideReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="data">服务人员列表 <para>服务人员列表</para></param>
        /// <param name="total_count">服务人员数量 <para>符合条件筛选的服务人员数量，当服务人员列表返回为空时，服务人员数量返回0；当服务人员列表有返回值时，服务人员数量按查询到的值返回。</para><para>示例值：10</para></param>
        /// <param name="limit">最大资源条数 <para>该次请求可返回的最大资源条数，不大于10</para><para>示例值：5</para></param>
        /// <param name="offset">请求资源起始位置 <para>该次请求资源的起始位置，默认值为0</para><para>示例值：0</para></param>
        public QueryGuideReturnJson(Data[] data, int total_count, int limit, int offset)
        {
            this.data = data;
            this.total_count = total_count;
            this.limit = limit;
            this.offset = offset;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public QueryGuideReturnJson()
        {
        }

        /// <summary>
        /// 服务人员列表
        /// <para>服务人员列表</para>
        /// </summary>
        public Data[] data { get; set; }

        /// <summary>
        /// 服务人员数量
        /// <para>符合条件筛选的服务人员数量，当服务人员列表返回为空时，服务人员数量返回0；当服务人员列表有返回值时，服务人员数量按查询到的值返回。 </para>
        /// <para>示例值：10</para>
        /// </summary>
        public int total_count { get; set; }

        /// <summary>
        /// 最大资源条数
        /// <para>该次请求可返回的最大资源条数，不大于10 </para>
        /// <para>示例值：5</para>
        /// </summary>
        public int limit { get; set; }

        /// <summary>
        /// 请求资源起始位置
        /// <para>该次请求资源的起始位置，默认值为0 </para>
        /// <para>示例值：0</para>
        /// </summary>
        public int offset { get; set; }

        #region 子数据类型
        public class Data
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="guide_id">服务人员ID <para>服务人员在服务人员系统中的唯一标识</para><para>示例值：LLA3WJ6DSZUfiaZDS79FH5Wm5m4X69TBic</para></param>
            /// <param name="store_id">门店ID <para>门店在微信支付商户平台的唯一标识（查找路径：登录商户平台—>营销中心—>门店管理，若无门店则需先创建门店）</para><para>示例值：12345678</para></param>
            /// <param name="name">服务人员姓名 <para>员工在商户个人/企业微信通讯录上的姓名,需使用微信支付平台公钥加密该字段需进行加密处理，加密方法详见敏感信息加密说明。</para><para>特殊规则：加密前字段长度限制为64个字节</para><para>示例值：pVd1HJ6v/69bDnuC4EL5Kz4jBHLiCa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg==</para></param>
            /// <param name="mobile">服务人员手机号码 <para>员工在商户个人/企业微信通讯录上设置的手机号码，使用微信支付平台公钥加密该字段需进行加密处理，加密方法详见敏感信息加密说明。</para><para>特殊规则：加密前字段长度限制为32个字节</para><para>示例值：pVd1HJ6v/69bDnuC4EL5Kz4jBHLiCa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg==</para></param>
            /// <param name="userid">企业微信的员工ID <para>员工在商户企业微信通讯录使用的唯一标识，使用企业微信商家时返回</para><para>示例值：robert</para></param>
            /// <param name="work_id">工号 <para>服务人员通过小程序注册时填写的工号，使用个人微信商家时返回</para><para>示例值：robert</para>TODO:多选一</param>
            public Data(string guide_id, int store_id, string name, string mobile, string userid, string work_id)
            {
                this.guide_id = guide_id;
                this.store_id = store_id;
                this.name = name;
                this.mobile = mobile;
                this.userid = userid;
                this.work_id = work_id;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Data()
            {
            }

            /// <summary>
            /// 服务人员ID
            /// <para>服务人员在服务人员系统中的唯一标识 </para>
            /// <para>示例值：LLA3WJ6DSZUfiaZDS79FH5Wm5m4X69TBic</para>
            /// </summary>
            public string guide_id { get; set; }

            /// <summary>
            /// 门店ID
            /// <para>门店在微信支付商户平台的唯一标识 （查找路径：登录商户平台—>营销中心—>门店管理，若无门店则需先创建门店）</para>
            /// <para>示例值：12345678</para>
            /// </summary>
            public int store_id { get; set; }

            /// <summary>
            /// 服务人员姓名
            /// <para>员工在商户个人/企业微信通讯录上的姓名,需使用微信支付平台公钥加密 该字段需进行加密处理，加密方法详见敏感信息加密说明。</para>
            /// <para>特殊规则：加密前字段长度限制为64个字节</para>
            /// <para>示例值：pVd1HJ6v/69bDnuC4EL5Kz4jBHLiCa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg== </para>
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 服务人员手机号码
            /// <para>员工在商户个人/企业微信通讯录上设置的手机号码，使用微信支付平台公钥加密 该字段需进行加密处理，加密方法详见敏感信息加密说明。</para>
            /// <para>特殊规则：加密前字段长度限制为32个字节</para>
            /// <para>示例值：pVd1HJ6v/69bDnuC4EL5Kz4jBHLiCa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg== </para>
            /// </summary>
            public string mobile { get; set; }

            /// <summary>
            /// 企业微信的员工ID
            /// <para>员工在商户企业微信通讯录使用的唯一标识，使用企业微信商家时返回</para>
            /// <para>示例值：robert</para>
            /// </summary>
            public string userid { get; set; }

            /// <summary>
            /// 工号
            /// <para>服务人员通过小程序注册时填写的工号，使用个人微信商家时返回 </para>
            /// <para>示例值：robert </para>TODO: 多选一
            /// </summary>
            public string work_id { get; set; }

        }


        #endregion
    }
}
