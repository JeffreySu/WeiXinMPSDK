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
  
    文件名：QueryRequestData.cs
    文件功能描述：查询订单请求数据
    
    
    创建标识：Senparc - 20230421

    
----------------------------------------------------------------*/


using Newtonsoft.Json;

namespace Senparc.Weixin.TenPayV3.Apis.BasePay
{
    public class QueryRequestData
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public QueryRequestData() { }

        /// <summary>
        /// 含参构造函数(商家模式)
        /// </summary>
        /// <param name="mchid">直连商户的商户号，由微信支付生成并下发。 示例值：1230000109</param>
        /// <param name="order_no">微信支付订单号 / 商户系统内部订单号</param>
        public QueryRequestData(string mchid, string order_no)
        {
            this.mchid = mchid;
            this.order_no = order_no;
        }


        /// <summary>
        /// 含参构造函数(服务商模式)
        /// </summary>
        /// <param name="sp_mchid">服务商户号，由微信支付生成并下发</param>
        /// <param name="sub_mchid">子商户的商户号，由微信支付生成并下发。</param>
        /// <param name="order_no">微信支付订单号 / 商户系统内部订单号</param>
        public QueryRequestData(string sp_mchid, string sub_mchid, string order_no)
        {
            this.sp_mchid = sp_mchid;
            this.sub_mchid = sub_mchid;
            this.order_no = order_no;
        }

        #region 商户
        /// <summary>
        /// 直连商户号
        /// 直连商户的商户号，由微信支付生成并下发。
        /// 示例值：1230000109
        /// </summary>
        public string mchid { get; set; }
        #endregion

        #region 服务商
        /// <summary>
        /// 服务商户号
        /// 服务商户号，由微信支付生成并下发
        /// 示例值：1230000109
        /// </summary>
        public string sp_mchid { get; set; }

        /// <summary>
        /// 子商户号
        /// 子商户的商户号，由微信支付生成并下发。
        /// 示例值：1900000109
        /// </summary>
        public string sub_mchid { get; set; }
        #endregion

        /// <summary>
        /// 1. 
        /// 微信支付订单号
        /// 微信支付系统生成的订单号
        /// 示例值：1217752501201407033233368018
        /// 
        /// 2. 商户订单号
        /// 商户系统内部订单号，只能是数字、大小写字母_-*且在同一个商户号下唯一。
        /// 特殊规则：最小字符长度为6
        /// 示例值：1217752501201407033233368018
        /// </summary>
        [JsonIgnore]
        public string order_no { get; set; }
    }
}

