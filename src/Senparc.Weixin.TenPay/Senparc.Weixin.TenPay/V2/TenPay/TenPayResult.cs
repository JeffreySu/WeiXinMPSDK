#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc
 
    文件名：TenPayResult.cs
    文件功能描述：微信支付返回结果
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.TenPay.V2
{
    /// <summary>
    /// 订单查询
    /// </summary>
    public class OrderqueryResult : WxJsonResult
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public OrderInfo orderInfo { get; set; }

        public class OrderInfo
        {
            public string ret_code { get; set; }
            public string ret_msg { get; set; }
            public string input_charset { get; set; }
            public string trade_state { get; set; }
            public string trade_mode { get; set; }
            public string partner { get; set; }
            public string bank_type { get; set; }
            public string bank_billno { get; set; }
            public string total_fee { get; set; }
            public string fee_type { get; set; }
            public string transaction_id { get; set; }
            public string out_trade_no { get; set; }
            public string is_split { get; set; }
            public string is_refund { get; set; }
            public string attach { get; set; }
            public string time_end { get; set; }
            public string transport_fee { get; set; }
            public string product_fee { get; set; }
            public string discount { get; set; }
            public string rmb_total_fee { get; set; }
        }
    }
}
