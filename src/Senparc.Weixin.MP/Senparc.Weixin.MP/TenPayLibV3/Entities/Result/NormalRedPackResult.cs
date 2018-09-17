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
  
    文件名：SendRedPackResult.cs
    文件功能描述：获取普通现金红包发送接口的结果
    
    
    创建标识：Yu XiaoChou - 20160107

    修改标识：Senparc - 20170110
    修改描述：v14.3.118 修改return_code和result_code的数据类型
----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.MP.TenPayLibV3
{
    /// <summary>
    /// 获取普通现金红包发送接口的结果
    /// </summary>
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public class NormalRedPackResult
    {
        /// <summary>
        /// 返回状态码
        /// SUCCESS/FAIL，此字段是通信标识，非交易标识，交易是否成功需要查看result_code来判断
        /// </summary>
        public string return_code { get; set; }

        /// <summary>
        /// 判断return_code是否为SUCCESS返回值
        /// </summary>
        public bool ReturnCodeSuccess { get { return return_code.ToUpper() == "SUCCESS"; } }

        /// <summary>
        /// 返回信息，返回信息，如非空，为错误原因，签名失败，参数格式校验错误
        /// </summary>
        public string return_msg { get; set; }

        /// <summary>
        /// 业务结果,SUCCESS/FAIL
        /// </summary>
        public string result_code { get; set; }

        /// <summary>
        /// 判断result_code是否为SUCCESS返回值
        /// </summary>
        public bool ResultCodeSuccess { get { return result_code.ToUpper() == "SUCCESS"; } }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string err_code { get; set; }

        /// <summary>
        /// 错误代码描述
        /// </summary>
        public string err_code_des { get; set; }

        /// <summary>
        /// 商户订单号（每个订单号必须唯一） 组成：mch_id+yyyymmdd+10位一天内不能重复的数字
        /// </summary>
        public string mch_billno { get; set; }

        /// <summary>
        /// 商户号，微信支付分配的商户号
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 公众账号appid。商户appid，接口传入的所有appid应该为公众号的appid（在mp.weixin.qq.com申请的），不能为APP的appid（在open.weixin.qq.com申请的）。
        /// </summary>
        public string wxappid { get; set; }

        /// <summary>
        /// 用户openid
        /// </summary>
        public string re_openid { get; set; }

        /// <summary>
        /// 付款金额，单位分
        /// </summary>
        public string total_amount { get; set; }

        /// <summary>
        /// 发放成功时间,格式20150520102602
        /// </summary>
        public string send_time { get; set; }

        /// <summary>
        /// 红包订单的微信单号
        /// </summary>
        public string send_listid { get; set; }
    }
}
