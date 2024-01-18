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
    文件功能描述：查询退款订单请求数据
    
    
    创建标识：Senparc - 20230421

    修改标识：Senparc - 20230905
    修改描述：v0.7.11 RefundQueryRequestData.sub_mchid 属性添加 [JsonIgnore] 标签 #2905
    
----------------------------------------------------------------*/

using Newtonsoft.Json;

namespace Senparc.Weixin.TenPayV3.Apis.BasePay
{
    public class RefundQueryRequestData
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public RefundQueryRequestData() { }

        /// <summary>
        /// 含参构造函数(商家模式)
        /// </summary>
        /// <param name="out_refund_no">商户系统内部的退款单号，商户系统内部唯一，只能是数字、大小写字母_-|*@ ，同一退款单号多次请求只退一笔。</param>
        public RefundQueryRequestData(string out_refund_no)
        {
            this.out_refund_no = out_refund_no;
        }


        /// <summary>
        /// 含参构造函数(服务商模式)
        /// </summary>
        /// <param name="sub_mchid">子商户的商户号，由微信支付生成并下发。</param>
        /// <param name="out_refund_no">商户系统内部的退款单号，商户系统内部唯一，只能是数字、大小写字母_-|*@ ，同一退款单号多次请求只退一笔。</param>
        public RefundQueryRequestData(string sub_mchid, string out_refund_no)
        {
            this.sub_mchid = sub_mchid;
            this.out_refund_no = out_refund_no;
        }

        #region 商户
        #endregion

        #region 服务商
        /// <summary>
        /// 子商户号
        /// 子商户的商户号，由微信支付生成并下发。
        /// 示例值：1900000109
        /// </summary>
        public string sub_mchid { get; set; }
        #endregion

        /// <summary>
        /// 商户退款单号
        /// 商户系统内部的退款单号，商户系统内部唯一，只能是数字、大小写字母_-|*@ ，同一退款单号多次请求只退一笔。
        /// 示例值：1217752501201407033233368018
        /// </summary>
        [JsonIgnore]
        public string out_refund_no { get; set; }
    }
}

