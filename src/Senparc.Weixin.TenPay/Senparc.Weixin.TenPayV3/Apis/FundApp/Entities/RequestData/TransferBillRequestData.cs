#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2026 Senparc
  
    文件名：TransferBillRequestData.cs
    文件功能描述：商家转账 - 发起转账API 请求数据
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.TenPayV3.Apis.FundApp
{
    /// <summary>
    /// 商家转账 - 发起转账API 请求数据
    /// <para>https://pay.weixin.qq.com/doc/v3/merchant/4012716434</para>
    /// </summary>
    public class TransferBillRequestData
    {
        /// <summary>
        /// 商户AppID
        /// <para>是微信开放平台和微信公众平台为开发者的应用程序(APP、小程序、公众号、企业号corpid即为此AppID)提供的一个唯一标识。此处，可以填写这四种类型中的任意一种APPID，但请确保该appid与商户号有绑定关系。</para>
        /// <para>示例值：wxf636efh567hg4356</para>
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 商户单号
        /// <para>商户系统内部的商家单号，要求此参数只能由数字、大小写字母组成，在商户系统内部唯一</para>
        /// <para>示例值：plfk2020042013</para>
        /// </summary>
        public string out_bill_no { get; set; }

        /// <summary>
        /// 转账场景ID
        /// <para>该笔转账使用的转账场景，可前往"商户平台-产品中心-商家转账"中申请。如：1000（现金营销），1006（企业报销）等</para>
        /// <para>示例值：1000</para>
        /// </summary>
        public string transfer_scene_id { get; set; }

        /// <summary>
        /// 收款用户OpenID
        /// <para>用户在商户appid下的唯一标识。发起转账前需获取到用户的OpenID，获取方式详见参数说明。</para>
        /// <para>示例值：o-MYE42l80oelYMDE34nYD456Xoy</para>
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 收款用户姓名
        /// <para>收款方真实姓名。需要加密传入，支持标准RSA算法和国密算法，公钥由微信侧提供。</para>
        /// <para>转账金额 >= 2,000元时，该笔明细必须填写</para>
        /// <para>若商户传入收款用户姓名，微信支付会校验收款用户与输入姓名是否一致，并提供电子回单</para>
        /// <para>示例值：757b340b45ebef5467rter35gf464344v3542sdf4t6re4tb4f54ty45t4yyry45</para>
        /// </summary>
        public string user_name { get; set; }

        /// <summary>
        /// 转账金额
        /// <para>转账金额单位为"分"。</para>
        /// <para>示例值：400000</para>
        /// </summary>
        public int transfer_amount { get; set; }

        /// <summary>
        /// 转账备注
        /// <para>转账备注，用户收款时可见该备注信息，UTF8编码，最多允许32个字符</para>
        /// <para>示例值：新会员开通有礼</para>
        /// </summary>
        public string transfer_remark { get; set; }

        /// <summary>
        /// 通知地址
        /// <para>异步接收微信支付结果通知的回调地址，通知url必须为公网可访问的URL，必须为HTTPS，不能携带参数。</para>
        /// <para>示例值：https://www.weixin.qq.com/wxpay/pay.php</para>
        /// </summary>
        public string notify_url { get; set; }

        /// <summary>
        /// 用户收款感知
        /// <para>用户收款时感知到的收款原因将根据转账场景自动展示默认内容。如有其他展示需求，可在本字段传入。各场景展示的默认内容和支持传入的内容，可查看产品文档了解。</para>
        /// <para>示例值：现金奖励</para>
        /// </summary>
        public string user_recv_perception { get; set; }

        /// <summary>
        /// 转账场景报备信息
        /// <para>各转账场景下需报备的内容，商户需要按照所属转账场景规则传参，详见转账场景报备信息字段说明。</para>
        /// </summary>
        public Transfer_Scene_Report_Info[] transfer_scene_report_infos { get; set; }
    }

    /// <summary>
    /// 转账场景报备信息
    /// </summary>
    public class Transfer_Scene_Report_Info
    {
        /// <summary>
        /// 信息类型
        /// <para>不能超过15个字符，商户所属转账场景下的信息类型，此字段内容为固定值，需严格按照转账场景报备信息字段说明传参。</para>
        /// <para>示例值：活动名称</para>
        /// </summary>
        public string info_type { get; set; }

        /// <summary>
        /// 信息内容
        /// <para>不能超过32个字符，商户所属转账场景下的信息内容，商户可按实际业务场景自定义传参，需严格按照转账场景报备信息字段说明传参。</para>
        /// <para>示例值：新会员有礼</para>
        /// </summary>
        public string info_content { get; set; }
    }
}

