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
  
    文件名：GoldplanChangeCustomPageStatusRequestData.cs
    文件功能描述：微信支付V3商家小票管理请求数据
    
    
    创建标识：Senparc - 20230602
    
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
    /// 微信支付V3商家小票管理请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter8_5_2.shtml </para>
    /// </summary>
    public class GoldplanChangeCustomPageStatusRequestData
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public GoldplanChangeCustomPageStatusRequestData()
        {
        }

        /// <summary>
        /// 含参构造函数(服务商模式)
        /// </summary>
        /// <param name="sub_mchid">特约商户号  <para>开通或关闭“商家自定义小票”的特约商户商户号，由微信支付生成并下发。</para><para>示例值：1234567890</para></param>
        /// <param name="operation_type">操作类型 <para>开通或关闭“商家自定义小票”的动作，枚举值：OPEN：表示开通点金计划 CLOSE：表示关闭点金计划</para></param>
        public GoldplanChangeCustomPageStatusRequestData(string sub_mchid, string operation_type)
        { 
            this.sub_mchid = sub_mchid;
            this.operation_type = operation_type;
        }

        #region 服务商
        /// <summary>
        /// 子商户号 
        /// 服务商模式需要
        /// </summary>
        public string sub_mchid { get; set; }
        #endregion

        /// <summary>
        /// 操作类型
        /// <para>开通或关闭点金计划的动作，枚举值：</para>
        /// <para>OPEN：表示开通点金计划</para>
        /// <para>CLOSE：表示关闭点金计划</para>
        /// <para>示例值：OPEN</para>
        /// </summary>
        public string operation_type { get; set; }

    }
}
