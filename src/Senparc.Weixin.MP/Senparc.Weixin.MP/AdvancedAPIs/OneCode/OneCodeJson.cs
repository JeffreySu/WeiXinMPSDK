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

    文件名：OneCodeJson.cs
    文件功能描述：OneCodeJson 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v16.25.1 补齐公众号 openApi、统计、图像、医疗、非税和一物一码官方接口

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.OneCode
{
    /// <summary>
    /// 申请一物一码营销二维码请求。
    /// </summary>
    public class ApplyCodeRequest
    {
        /// <summary>
        /// 申请数量，必须为 10000 的整数倍，范围为 10000 至 20000000。
        /// </summary>
        public long code_count { get; set; }

        /// <summary>
        /// 调用方外部单号；相同外部单号视为同一申请单。
        /// </summary>
        public string isv_application_id { get; set; }
    }

    /// <summary>
    /// 申请一物一码营销二维码结果。
    /// </summary>
    public class ApplyCodeJsonResult : WxJsonResult
    {
        /// <summary>
        /// 微信生成的申请单号。
        /// </summary>
        public long application_id { get; set; }
    }

    /// <summary>
    /// 查询二维码申请单请求。
    /// </summary>
    public class ApplyCodeQueryRequest
    {
        /// <summary>
        /// 微信申请单号；与 <see cref="isv_application_id"/> 至少填写一项。
        /// </summary>
        public long? application_id { get; set; }

        /// <summary>
        /// 调用方外部单号；与 <see cref="application_id"/> 至少填写一项。
        /// </summary>
        public string isv_application_id { get; set; }
    }

    /// <summary>
    /// 查询二维码申请单结果。
    /// </summary>
    public class ApplyCodeQueryJsonResult : WxJsonResult
    {
        /// <summary>
        /// 申请单状态，例如 <c>FINISH</c>。
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 微信申请单号。
        /// </summary>
        public long application_id { get; set; }

        /// <summary>
        /// 调用方外部单号。
        /// </summary>
        public string isv_application_id { get; set; }

        /// <summary>
        /// 已生成的二维码码段列表。
        /// </summary>
        public List<OneCodeRange> code_generate_list { get; set; }

        /// <summary>
        /// 申请单创建时间，Unix 时间戳。
        /// </summary>
        public long create_time { get; set; }

        /// <summary>
        /// 申请单更新时间，Unix 时间戳。
        /// </summary>
        public long update_time { get; set; }
    }

    /// <summary>
    /// 一物一码二维码码段。
    /// </summary>
    public class OneCodeRange
    {
        /// <summary>
        /// 码段起始偏移量，包含该值。
        /// </summary>
        public long code_start { get; set; }

        /// <summary>
        /// 码段结束偏移量，包含该值。
        /// </summary>
        public long code_end { get; set; }
    }

    /// <summary>
    /// 下载二维码数据包请求。
    /// </summary>
    public class ApplyCodeDownloadRequest
    {
        /// <summary>
        /// 微信申请单号。
        /// </summary>
        public long application_id { get; set; }

        /// <summary>
        /// 需要下载的码段起始偏移量。
        /// </summary>
        public long code_start { get; set; }

        /// <summary>
        /// 需要下载的码段结束偏移量。
        /// </summary>
        public long code_end { get; set; }
    }

    /// <summary>
    /// 下载二维码数据包结果。
    /// </summary>
    public class ApplyCodeDownloadJsonResult : WxJsonResult
    {
        /// <summary>
        /// Base64 编码的文件内容；需先 Base64 解码，再按官方规则解密。
        /// </summary>
        public string buffer { get; set; }
    }

    /// <summary>
    /// 激活二维码请求。
    /// </summary>
    public class CodeActiveRequest
    {
        /// <summary>
        /// 微信申请单号。
        /// </summary>
        public long application_id { get; set; }

        /// <summary>
        /// 活动名称，是数据分析中的活动区分依据。
        /// </summary>
        public string activity_name { get; set; }

        /// <summary>
        /// 商品品牌，是数据分析中的品牌区分依据。
        /// </summary>
        public string product_brand { get; set; }

        /// <summary>
        /// 商品标题，是数据分析中的商品区分依据。
        /// </summary>
        public string product_title { get; set; }

        /// <summary>
        /// 商品 EAN 条码。
        /// </summary>
        public string product_code { get; set; }

        /// <summary>
        /// 扫码后跳转的小程序 AppId。
        /// </summary>
        public string wxa_appid { get; set; }

        /// <summary>
        /// 扫码后跳转的小程序页面路径。
        /// </summary>
        public string wxa_path { get; set; }

        /// <summary>
        /// 小程序版本：0 正式版，1 开发版，2 体验版；不传时默认为 0。
        /// </summary>
        public int? wxa_type { get; set; }

        /// <summary>
        /// 激活码段起始偏移量，包含该值。
        /// </summary>
        public long code_start { get; set; }

        /// <summary>
        /// 激活码段结束偏移量，包含该值。
        /// </summary>
        public long code_end { get; set; }
    }

    /// <summary>
    /// 查询二维码激活状态请求。
    /// </summary>
    public class CodeActiveQueryRequest
    {
        /// <summary>
        /// 微信申请单号；使用此查询方式时必须同时填写 <see cref="code_index"/>。
        /// </summary>
        public long? application_id { get; set; }

        /// <summary>
        /// 二维码在申请批次中的偏移量。
        /// </summary>
        public long? code_index { get; set; }

        /// <summary>
        /// 28 位普通码字符；与 <see cref="code"/> 二选一。
        /// </summary>
        public string code_url { get; set; }

        /// <summary>
        /// 9 位字符串原始码；与 <see cref="code_url"/> 二选一。
        /// </summary>
        public string code { get; set; }
    }

    /// <summary>
    /// 二维码激活信息结果基类。
    /// </summary>
    public class OneCodeInfoJsonResult : WxJsonResult
    {
        /// <summary>
        /// 二维码原始码。
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 微信申请单号。
        /// </summary>
        public long application_id { get; set; }

        /// <summary>
        /// 调用方外部单号。
        /// </summary>
        public string isv_application_id { get; set; }

        /// <summary>
        /// 活动名称。
        /// </summary>
        public string activity_name { get; set; }

        /// <summary>
        /// 商品品牌。
        /// </summary>
        public string product_brand { get; set; }

        /// <summary>
        /// 商品标题。
        /// </summary>
        public string product_title { get; set; }

        /// <summary>
        /// 商品 EAN 条码。官方返回参数表未列出该字段，但返回示例包含该字段。
        /// </summary>
        public string product_code { get; set; }

        /// <summary>
        /// 关联小程序 AppId。
        /// </summary>
        public string wxa_appid { get; set; }

        /// <summary>
        /// 关联小程序页面路径。
        /// </summary>
        public string wxa_path { get; set; }

        /// <summary>
        /// 小程序版本：0 正式版，1 开发版，2 体验版。
        /// </summary>
        public int? wxa_type { get; set; }

        /// <summary>
        /// 激活码段起始偏移量。
        /// </summary>
        public long code_start { get; set; }

        /// <summary>
        /// 激活码段结束偏移量。
        /// </summary>
        public long code_end { get; set; }
    }

    /// <summary>
    /// 查询二维码激活状态结果。
    /// </summary>
    public class CodeActiveQueryJsonResult : OneCodeInfoJsonResult
    {
    }

    /// <summary>
    /// CODE_TICKET 换 CODE 请求。
    /// </summary>
    public class TicketToCodeRequest
    {
        /// <summary>
        /// 扫码用户的 OpenId。
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 扫码跳转参数中的 code_ticket。
        /// </summary>
        public string code_ticket { get; set; }
    }

    /// <summary>
    /// CODE_TICKET 换 CODE 结果。
    /// </summary>
    public class TicketToCodeJsonResult : OneCodeInfoJsonResult
    {
    }
}
