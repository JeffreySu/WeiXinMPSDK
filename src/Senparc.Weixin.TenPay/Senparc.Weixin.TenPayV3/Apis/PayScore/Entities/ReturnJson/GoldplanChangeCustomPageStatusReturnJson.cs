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
  
    文件名：GoldplanChangeGoldplanStatusReturnJson.cs
    文件功能描述：商家小票管理返回模型
    
    
    创建标识：Senparc - 20230602
    
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
    /// 商家小票管理返回模型
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter8_5_2.shtml </para>
    /// </summary>
    public class GoldplanChangeCustomPageStatusReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public GoldplanChangeCustomPageStatusReturnJson()
        {

        }

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="sub_mchid">特约商户号  <para>开通或关闭“商家自定义小票”的特约商户商户号，由微信支付生成并下发。</para><para>示例值：1234567890</para></param>
        public GoldplanChangeCustomPageStatusReturnJson(string sub_mchid)
        {
            this.sub_mchid = sub_mchid;
        }

        /// <summary>
        /// 特约商户号 
        /// <para>开通或关闭“商家自定义小票”的特约商户商户号，由微信支付生成并下发。 </para>
        /// <para>示例值：1234567890</para>
        /// </summary>
        public string sub_mchid { get; set; }
    }
}
