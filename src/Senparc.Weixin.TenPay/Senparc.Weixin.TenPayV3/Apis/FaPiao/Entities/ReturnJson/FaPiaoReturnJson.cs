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
  
    文件名：FapiaoReturnJson.cs
    文件功能描述：电子发票 - API 返回信息
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.TenPayV3.Apis.Fapiao
{
    /// <summary>
    /// 电子发票 - 检查子商户开票功能状态API 返回信息
    /// </summary>
    public class CheckFapiaoStatusReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 子商户号
        /// <para>微信支付分配的子商户号</para>
        /// <para>示例值：1900000109</para>
        /// </summary>
        public string sub_mchid { get; set; }

        /// <summary>
        /// 开票功能状态
        /// <para>子商户的开票功能状态</para>
        /// <para>AVAILABLE: 可用；UNAVAILABLE: 不可用</para>
        /// <para>示例值：AVAILABLE</para>
        /// </summary>
        public string fapiao_status { get; set; }
    }

    /// <summary>
    /// 电子发票 - 创建电子发票卡券模板API 返回信息
    /// </summary>
    public class CreateFapiaoCardTemplateReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 卡券模板ID
        /// <para>微信支付系统生成的卡券模板ID</para>
        /// <para>示例值：template_123456789</para>
        /// </summary>
        public string template_id { get; set; }

        /// <summary>
        /// 创建时间
        /// <para>卡券模板创建时间</para>
        /// <para>示例值：2021-06-10T13:29:35.120+08:00</para>
        /// </summary>
        public DateTime create_time { get; set; }
    }

    /// <summary>
    /// 电子发票 - 查询电子发票API 返回信息
    /// </summary>
    public class QueryFapiaoReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 发票申请单号
        /// <para>微信支付系统生成的发票申请单号</para>
        /// <para>示例值：2020112611140011234567890</para>
        /// </summary>
        public string fapiao_apply_id { get; set; }

        /// <summary>
        /// 发票状态
        /// <para>发票的当前状态</para>
        /// <para>APPLYING: 开票中；SUCCESS: 开票成功；FAILED: 开票失败；REVERSED: 已冲红</para>
        /// <para>示例值：SUCCESS</para>
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 发票号码
        /// <para>税务局分配的发票号码</para>
        /// <para>示例值：033002000712</para>
        /// </summary>
        public string fapiao_number { get; set; }

        /// <summary>
        /// 发票代码
        /// <para>税务局分配的发票代码</para>
        /// <para>示例值：033002000712</para>
        /// </summary>
        public string fapiao_code { get; set; }

        /// <summary>
        /// 开票时间
        /// <para>发票的开具时间</para>
        /// <para>示例值：2021-06-10T13:29:35.120+08:00</para>
        /// </summary>
        public DateTime fapiao_time { get; set; }

        /// <summary>
        /// 发票金额
        /// <para>发票金额，单位为分</para>
        /// <para>示例值：100</para>
        /// </summary>
        public int fapiao_amount { get; set; }
    }

    /// <summary>
    /// 电子发票 - 获取抬头填写链接API 返回信息
    /// </summary>
    public class GetTitleUrlReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 抬头填写链接
        /// <para>用户填写抬头信息的链接</para>
        /// <para>示例值：https://fapiao.wechatpay.cn/title?token=xxxxxxxxx</para>
        /// </summary>
        public string title_url { get; set; }

        /// <summary>
        /// 链接有效期
        /// <para>链接的有效期，单位为秒</para>
        /// <para>示例值：3600</para>
        /// </summary>
        public int expires_in { get; set; }
    }

    /// <summary>
    /// 电子发票 - 获取用户填写的抬头API 返回信息
    /// </summary>
    public class GetUserTitleReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 抬头类型
        /// <para>发票抬头的类型</para>
        /// <para>PERSONAL: 个人；CORPORATE: 企业</para>
        /// <para>示例值：PERSONAL</para>
        /// </summary>
        public string title_type { get; set; }

        /// <summary>
        /// 抬头名称
        /// <para>发票抬头的名称</para>
        /// <para>示例值：张三</para>
        /// </summary>
        public string title_name { get; set; }

        /// <summary>
        /// 纳税人识别号
        /// <para>企业的纳税人识别号</para>
        /// <para>示例值：123456789012345678</para>
        /// </summary>
        public string taxpayer_id { get; set; }

        /// <summary>
        /// 地址
        /// <para>企业的地址</para>
        /// <para>示例值：北京市海淀区xxx</para>
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 电话
        /// <para>企业的电话</para>
        /// <para>示例值：010-12345678</para>
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 开户银行
        /// <para>企业的开户银行</para>
        /// <para>示例值：中国银行</para>
        /// </summary>
        public string bank_name { get; set; }

        /// <summary>
        /// 银行账号
        /// <para>企业的银行账号</para>
        /// <para>示例值：1234567890123456789</para>
        /// </summary>
        public string bank_account { get; set; }
    }

    /// <summary>
    /// 电子发票 - 获取商户开票基础信息API 返回信息
    /// </summary>
    public class GetMerchantInfoReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 子商户号
        /// <para>微信支付分配的子商户号</para>
        /// <para>示例值：1900000109</para>
        /// </summary>
        public string sub_mchid { get; set; }

        /// <summary>
        /// 商户名称
        /// <para>商户的名称</para>
        /// <para>示例值：示例商户</para>
        /// </summary>
        public string merchant_name { get; set; }

        /// <summary>
        /// 纳税人识别号
        /// <para>商户的纳税人识别号</para>
        /// <para>示例值：123456789012345678</para>
        /// </summary>
        public string taxpayer_id { get; set; }
    }

    /// <summary>
    /// 电子发票 - 开具电子发票API 返回信息
    /// </summary>
    public class CreateFapiaoReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 发票申请单号
        /// <para>微信支付系统生成的发票申请单号</para>
        /// <para>示例值：2020112611140011234567890</para>
        /// </summary>
        public string fapiao_apply_id { get; set; }

        /// <summary>
        /// 发票状态
        /// <para>发票的当前状态</para>
        /// <para>APPLYING: 开票中；SUCCESS: 开票成功；FAILED: 开票失败</para>
        /// <para>示例值：APPLYING</para>
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 创建时间
        /// <para>发票申请的创建时间</para>
        /// <para>示例值：2021-06-10T13:29:35.120+08:00</para>
        /// </summary>
        public DateTime create_time { get; set; }
    }

    /// <summary>
    /// 电子发票 - 冲红电子发票API 返回信息
    /// </summary>
    public class ReverseFapiaoReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 发票申请单号
        /// <para>微信支付系统生成的发票申请单号</para>
        /// <para>示例值：2020112611140011234567890</para>
        /// </summary>
        public string fapiao_apply_id { get; set; }

        /// <summary>
        /// 冲红申请单号
        /// <para>微信支付系统生成的冲红申请单号</para>
        /// <para>示例值：2020112611140011234567891</para>
        /// </summary>
        public string reverse_apply_id { get; set; }

        /// <summary>
        /// 冲红状态
        /// <para>冲红的当前状态</para>
        /// <para>APPLYING: 冲红中；SUCCESS: 冲红成功；FAILED: 冲红失败</para>
        /// <para>示例值：APPLYING</para>
        /// </summary>
        public string reverse_status { get; set; }

        /// <summary>
        /// 冲红时间
        /// <para>冲红申请的创建时间</para>
        /// <para>示例值：2021-06-10T13:29:35.120+08:00</para>
        /// </summary>
        public DateTime reverse_time { get; set; }
    }

    /// <summary>
    /// 电子发票 - 获取发票下载信息API 返回信息
    /// </summary>
    public class GetFapiaoFileReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 发票申请单号
        /// <para>微信支付系统生成的发票申请单号</para>
        /// <para>示例值：2020112611140011234567890</para>
        /// </summary>
        public string fapiao_apply_id { get; set; }

        /// <summary>
        /// 发票文件下载链接
        /// <para>发票PDF文件的下载链接</para>
        /// <para>示例值：https://fapiao.wechatpay.cn/download?token=xxxxxxxxx</para>
        /// </summary>
        public string download_url { get; set; }

        /// <summary>
        /// 文件哈希值
        /// <para>发票文件的哈希值</para>
        /// <para>示例值：9d2de93dc39bf6a72ad52e5dc1eeaf0c</para>
        /// </summary>
        public string file_hash { get; set; }

        /// <summary>
        /// 链接有效期
        /// <para>下载链接的有效期，单位为秒</para>
        /// <para>示例值：3600</para>
        /// </summary>
        public int expires_in { get; set; }
    }
}
