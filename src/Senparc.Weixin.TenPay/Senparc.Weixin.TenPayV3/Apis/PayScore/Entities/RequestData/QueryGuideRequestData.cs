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
  
    文件名：AssignGuideRequestData.cs
    文件功能描述：微信支付V3服务人员分配接口请求数据
    
    
    创建标识：Senparc - 20210925
    
----------------------------------------------------------------*/


using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.PayScore
{
    /// <summary>
    /// 微信支付V3服务人员查询请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_4_3.shtml </para>
    /// </summary>
    public class QueryGuideRequestData
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public QueryGuideRequestData()
        {
        }

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="store_id">门店ID</param>
        /// <param name="userid">企业微信的员工ID</param>
        /// <param name="mobile">手机号码</param>
        /// <param name="work_id">工号</param>
        /// <param name="limit">最大资源条数</param>
        /// <param name="offset">请求资源起始位置</param>
        public QueryGuideRequestData(string store_id, string userid, string mobile, string work_id, int limit = 0, int offset = 0)
        {
            this.store_id = store_id;
            this.userid = userid;
            this.mobile = mobile;
            this.work_id = work_id;
            this.limit = limit;
            this.offset = offset;
        }

        /// <summary>
        /// 含参构造函数(服务商模式)
        /// </summary>
        /// <param name="sub_mchid">服务人员所属商户的商户ID</param>
        /// <param name="store_id">门店ID</param>
        /// <param name="userid">企业微信的员工ID</param>
        /// <param name="mobile">手机号码</param>
        /// <param name="work_id">工号</param>
        /// <param name="limit">最大资源条数</param>
        /// <param name="offset">请求资源起始位置</param>
        public QueryGuideRequestData(string sub_mchid, string store_id, string userid, string mobile, string work_id, int limit = 0, int offset = 0)
        {
            this.sub_mchid = sub_mchid;
            this.store_id = store_id;
            this.userid = userid;
            this.mobile = mobile;
            this.work_id = work_id;
            this.limit = limit;
            this.offset = offset;
        }

        #region 服务商
        /// <summary>
        /// 子商户号 
        /// 服务商模式需要
        /// </summary>
        public string sub_mchid { get; set; }
        #endregion

        /// <summary>
        /// 门店ID
        /// 门店在微信支付商户平台的唯一标识
        /// </summary>
        public string store_id { get; set; }

        /// <summary>
        /// 企业微信的员工ID
        /// 员工在商户企业微信通讯录使用的唯一标识，企业微信商家可传入该字段查询单个服务人员信息；不传则查询整个门店下的服务人员信息
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 手机号码
        /// 服务人员通过小程序注册时填写的手机号码，企业微信/个人微信商家可传入该字段查询单个服务人员信息；不传则查询整个门店下的服务人员信息。
        /// 该字段需进行加密处理，加密方法详见敏感信息加密说明 
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/wechatpay/wechatpay4_3.shtml
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 工号
        /// 服务人员通过小程序注册时填写的工号，个人微信商家可传入该字段查询单个服务人员信息；不传则查询整个门店下的服务人员信息
        /// </summary>
        public string work_id { get; set; }

        /// <summary>
        /// 最大资源条数
        /// 商家自定义字段，该次请求可返回的最大资源条数，不大于10
        /// </summary>
        public int limit { get; set; }

        /// <summary>
        /// 请求资源起始位置
        /// 商家自定义字段，该次请求资源的起始位置，默认值为0
        /// </summary>
        public int offset { get; set; }

    }
}
