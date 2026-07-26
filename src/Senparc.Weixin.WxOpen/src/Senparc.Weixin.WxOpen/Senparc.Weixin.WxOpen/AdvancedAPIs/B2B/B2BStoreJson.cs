#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License
is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：B2BStoreJson.cs
    文件功能描述：B2BStoreJson 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.B2B
{
    #region 门店授权

    /// <summary>
    /// 申请开通 B2B 门店助手请求。
    /// </summary>
    public class B2BRetailBusinessApplyRequest
    {
        /// <summary>
        /// 主营商品类型列表，例如“食品”“饮料”“其他”。
        /// </summary>
        public IList<string> goods_type_list { get; set; }

        /// <summary>
        /// 主要线下销售渠道列表，例如“杂货店”“便利店”“超市”。
        /// </summary>
        public IList<string> goods_sale_list { get; set; }

        /// <summary>
        /// 门店覆盖数，可选值为“0-5千”“5千-1万”“1万-10万”“10万-50万”“50万以上”。
        /// </summary>
        public string cover_num { get; set; }

        /// <summary>
        /// 所需服务类型列表，例如“门店订货”“门店促销”“门店活动执行”“门店直播”“其他”。
        /// </summary>
        public IList<string> service_list { get; set; }

        /// <summary>
        /// 小程序方案概述，长度为 21 至 100 个中文字符。
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 联系人姓名，长度为 1 至 7 个中文字符。
        /// </summary>
        public string contact_name { get; set; }

        /// <summary>
        /// 联系人手机号。
        /// </summary>
        public string contact_phone { get; set; }

        /// <summary>
        /// 联系人邮箱。
        /// </summary>
        public string contact_email { get; set; }
    }

    /// <summary>
    /// 批量预录入门店请求；单次最多包含 100 家门店。
    /// </summary>
    public class B2BBatchCreateRetailRequest
    {
        /// <summary>
        /// 需要预录入的门店信息列表。
        /// </summary>
        public IList<B2BRetailPreEntry> retail_info_list { get; set; }
    }

    /// <summary>
    /// 预录入的单家门店信息。
    /// </summary>
    public class B2BRetailPreEntry
    {
        /// <summary>
        /// 门店负责人手机号。
        /// </summary>
        public string mobile_phone { get; set; }

        /// <summary>
        /// 门店名称，长度为 1 至 100 个字符，一个中文字符按两个字符计算。
        /// </summary>
        public string retail_name { get; set; }

        /// <summary>
        /// 可选的一级门店类型。
        /// </summary>
        public string retail_type { get; set; }

        /// <summary>
        /// 可选的二级门店类型；一级门店类型为“其他”时必填。
        /// </summary>
        public string sub_retail_type { get; set; }

        /// <summary>
        /// 门店地址所在省。
        /// </summary>
        public string address_province { get; set; }

        /// <summary>
        /// 门店地址所在市。
        /// </summary>
        public string address_city { get; set; }

        /// <summary>
        /// 门店地址所在区县。
        /// </summary>
        public string address_region { get; set; }

        /// <summary>
        /// 门店街道详细地址。
        /// </summary>
        public string address_street { get; set; }

        /// <summary>
        /// 营业执照注册号。官方参数表标记为必填，但首个请求示例未填写，调用方应按实际门店资质提供。
        /// </summary>
        public string registration_number { get; set; }

        /// <summary>
        /// 可选的企业名称。
        /// </summary>
        public string biz_name { get; set; }

        /// <summary>
        /// 可选的法人姓名。
        /// </summary>
        public string corporation_name { get; set; }

        /// <summary>
        /// 可选的门店纬度。
        /// </summary>
        public double? latitude { get; set; }

        /// <summary>
        /// 可选的门店经度。
        /// </summary>
        public double? longitude { get; set; }

        /// <summary>
        /// 可选的一级主营商品列表。
        /// </summary>
        public IList<string> business_type { get; set; }

        /// <summary>
        /// 可选的二级主营商品；一级主营商品包含“其他”时必填。
        /// </summary>
        public string other_business_type { get; set; }
    }

    /// <summary>
    /// 门店预录入失败记录。
    /// </summary>
    public class B2BRetailFailureRecord
    {
        /// <summary>
        /// 预录入门店的手机号。
        /// </summary>
        public string mobile_phone { get; set; }

        /// <summary>
        /// 营业执照注册号。
        /// </summary>
        public string registration_number { get; set; }

        /// <summary>
        /// 失败码：2 手机号无效，3 门店类型无效，4 地址解析失败，5 手机号已录入，6 门店名称无效，7 主营商品无效。
        /// </summary>
        public int failure_code { get; set; }
    }

    /// <summary>
    /// 批量预录入门店结果。
    /// </summary>
    public class B2BBatchCreateRetailJsonResult : WxJsonResult
    {
        /// <summary>
        /// 成功预录入的门店数。
        /// </summary>
        public int num_success { get; set; }

        /// <summary>
        /// 预录入失败的门店数。
        /// </summary>
        public int num_failure { get; set; }

        /// <summary>
        /// 预录入失败记录列表。
        /// </summary>
        public IList<B2BRetailFailureRecord> failure_record_list { get; set; }
    }

    /// <summary>
    /// 查询门店信息请求。
    /// </summary>
    public class B2BGetRetailInfoRequest
    {
        /// <summary>
        /// 可选的门店管理员或员工 OpenId；与手机号二选一，都填写时优先使用 OpenId。
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 可选的门店负责人手机号；与 OpenId 二选一。
        /// </summary>
        public string mobile_phone { get; set; }
    }

    /// <summary>
    /// 门店管理员或员工信息。
    /// </summary>
    public class B2BRetailStaff
    {
        /// <summary>
        /// 管理员或员工 OpenId。
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 角色：1 管理员，2 员工。
        /// </summary>
        public int role { get; set; }

        /// <summary>
        /// 员工加入时间，Unix 秒级时间戳；管理员可能不返回。
        /// </summary>
        public long? create_time { get; set; }
    }

    /// <summary>
    /// 已授权门店信息。
    /// </summary>
    public class B2BRetailInfo
    {
        /// <summary>
        /// 门店负责人手机号。
        /// </summary>
        public string mobile_phone { get; set; }

        /// <summary>
        /// 一级门店类型。
        /// </summary>
        public string retail_type { get; set; }

        /// <summary>
        /// 二级门店类型。
        /// </summary>
        public string sub_retail_type { get; set; }

        /// <summary>
        /// 完整门店地址。
        /// </summary>
        public string retail_address { get; set; }

        /// <summary>
        /// 门店名称。
        /// </summary>
        public string retail_name { get; set; }

        /// <summary>
        /// 营业执照注册号。
        /// </summary>
        public string identification { get; set; }

        /// <summary>
        /// 企业名称。
        /// </summary>
        public string principal { get; set; }

        /// <summary>
        /// 法人姓名。
        /// </summary>
        public string legal_person_name { get; set; }

        /// <summary>
        /// 门店管理员或员工 OpenId；按手机号查询时返回管理员 OpenId。
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 角色：1 管理员，2 员工。官方参数表误标为字符串，因此使用可空整数兼容实际枚举。
        /// </summary>
        public int? role { get; set; }

        /// <summary>
        /// 认证状态，1 表示已经完成认证。
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 认证时间，Unix 秒级时间戳。
        /// </summary>
        public long auth_time { get; set; }

        /// <summary>
        /// 授权时间，Unix 秒级时间戳。
        /// </summary>
        public long grant_time { get; set; }

        /// <summary>
        /// 可选的门店经度；未提交定位信息时不返回。
        /// </summary>
        public double? longitude { get; set; }

        /// <summary>
        /// 可选的门店纬度；未提交定位信息时不返回。
        /// </summary>
        public double? latitude { get; set; }

        /// <summary>
        /// 一级主营商品列表。
        /// </summary>
        public IList<string> business_type { get; set; }

        /// <summary>
        /// 二级主营商品。
        /// </summary>
        public string other_business_type { get; set; }

        /// <summary>
        /// 员工列表，包含管理员。
        /// </summary>
        public IList<B2BRetailStaff> staff_list { get; set; }
    }

    /// <summary>
    /// 门店信息查询结果。
    /// </summary>
    /// <remarks>官方返回参数表直接列出了门店字段，返回示例实际使用 <c>info</c> 数组作为外层包装，本模型按真实示例建模。</remarks>
    public class B2BGetRetailInfoJsonResult : WxJsonResult
    {
        /// <summary>
        /// 匹配的门店信息列表。
        /// </summary>
        public IList<B2BRetailInfo> info { get; set; }
    }

    /// <summary>
    /// 分页查询全量授权门店 OpenId 请求。
    /// </summary>
    public class B2BGetRetailOpenIdListRequest
    {
        /// <summary>
        /// 每页最大返回数量，取值范围为 1 至 100。
        /// </summary>
        public int limit { get; set; }

        /// <summary>
        /// 分页上下文；首次调用传空字符串，后续传上次返回值。
        /// </summary>
        public string page_context { get; set; }
    }

    /// <summary>
    /// 全量授权门店 OpenId 查询结果。
    /// </summary>
    public class B2BGetRetailOpenIdListJsonResult : WxJsonResult
    {
        /// <summary>
        /// 已授权门店管理员或员工 OpenId 列表。
        /// </summary>
        public IList<string> openid_list { get; set; }

        /// <summary>
        /// 下一页分页上下文。
        /// </summary>
        public string page_context { get; set; }
    }

    #endregion

    #region 门店消息

    /// <summary>
    /// 向门店负责人发送 B2B 模板消息请求。
    /// </summary>
    public class B2BSendRetailNotificationRequest
    {
        /// <summary>
        /// B2B 门店助手模板消息类型。
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 接收消息的门店负责人 OpenId 列表，单次最多 200 个。
        /// </summary>
        public IList<string> to_user_list { get; set; }

        /// <summary>
        /// 可选的消息内容；应传入对应模板要求的 JSON 字符串。
        /// </summary>
        public string content { get; set; }
    }

    /// <summary>
    /// 查询 B2B 门店消息效果数据请求。
    /// </summary>
    public class B2BGetRetailMessageListRequest
    {
        /// <summary>
        /// 分页起始位置，从 0 开始。
        /// </summary>
        public int start { get; set; }

        /// <summary>
        /// 每页数量，最大为 1000，默认值为 20。
        /// </summary>
        public int offset { get; set; }

        /// <summary>
        /// 查询开始日期，格式为 yyyy-MM-dd。
        /// </summary>
        public string begin_date { get; set; }

        /// <summary>
        /// 查询结束日期，格式为 yyyy-MM-dd。
        /// </summary>
        public string end_date { get; set; }
    }

    /// <summary>
    /// 单条 B2B 门店消息效果数据。
    /// </summary>
    public class B2BRetailMessageData
    {
        /// <summary>
        /// 微信消息 ID。
        /// </summary>
        public long msg_id { get; set; }

        /// <summary>
        /// 消息类型。
        /// </summary>
        public int msg_type { get; set; }

        /// <summary>
        /// 消息日期。
        /// </summary>
        public string date { get; set; }

        /// <summary>
        /// 消息发送时间。
        /// </summary>
        public string msg_time { get; set; }

        /// <summary>
        /// 发送人数。
        /// </summary>
        public int send_uv { get; set; }

        /// <summary>
        /// 进入人数。
        /// </summary>
        public int entry_uv { get; set; }

        /// <summary>
        /// 业务方自定义消息 ID。
        /// </summary>
        public string business_msg_id { get; set; }
    }

    /// <summary>
    /// B2B 门店消息效果数据查询结果。
    /// </summary>
    public class B2BGetRetailMessageListJsonResult : WxJsonResult
    {
        /// <summary>
        /// 查询到的数据总数。
        /// </summary>
        public int total_num { get; set; }

        /// <summary>
        /// 消息效果数据列表。
        /// </summary>
        public IList<B2BRetailMessageData> data_line { get; set; }
    }

    #endregion
}
