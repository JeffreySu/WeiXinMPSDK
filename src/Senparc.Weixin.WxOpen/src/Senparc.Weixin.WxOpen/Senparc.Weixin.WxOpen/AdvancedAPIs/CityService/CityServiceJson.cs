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

    文件名：CityServiceJson.cs
    文件功能描述：CityServiceJson 强类型数据模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.CityService
{
    /// <summary>城市服务限定页面附加参数。</summary>
    public class CityServicePathExtraParameter
    {
        /// <summary>参数名称；服务列表关键词固定使用 <c>keyword</c>。</summary>
        public string key { get; set; }

        /// <summary>参数值或城市服务标签名称。</summary>
        public string value { get; set; }
    }

    /// <summary>获取城市服务限定页面链接请求。</summary>
    public class CityServiceGetServicePathRequest
    {
        /// <summary>页面类型：0 服务主页，1 首页，3 专题页，5 服务列表页。</summary>
        public int page_type { get; set; }

        /// <summary>来源渠道：0 公众号，1 小程序，2 短信，3 其他，5 厂商。</summary>
        public int src_channel { get; set; }

        /// <summary>获取 H5 URL 时填写 1；OpenSDK 场景无需填写。</summary>
        public int? need_path_type { get; set; }

        /// <summary>获取 H5 URL 时填写 2；OpenSDK 场景无需填写。</summary>
        public int? device_type { get; set; }

        /// <summary>地级市名称；页面类型为 1、3、5 时必填。</summary>
        public string city_name { get; set; }

        /// <summary>专题页名称；页面类型为 3 时必填。</summary>
        public string content_name { get; set; }

        /// <summary>附加参数；页面类型为 5 时必填。</summary>
        public IList<CityServicePathExtraParameter> ext_params { get; set; }

        /// <summary>服务 ID；页面类型为 0 时必填。</summary>
        public long? service_id { get; set; }

        /// <summary>服务主页透传参数的 JSON 数组字符串，最多包含 10 个 key/value 对。</summary>
        /// <remarks>官方协议要求该字段本身为字符串，而不是直接发送数组。</remarks>
        public string @params { get; set; }

        /// <summary>与腾讯城市编码一致的城市 ID；页面类型为 0 时可填写。</summary>
        public string city_id { get; set; }
    }

    /// <summary>获取城市服务限定页面链接结果。</summary>
    public class CityServiceGetServicePathJsonResult : WxJsonResult
    {
        /// <summary>结果页面路径。</summary>
        public string path { get; set; }

        /// <summary>业务类型。</summary>
        public string business_type { get; set; }

        /// <summary>官方专题页返回示例误拼写的业务类型字段。</summary>
        public string bussiness_type { get; set; }

        /// <summary>目标小程序 AppId；页面类型为 5 时返回。</summary>
        public string app_id { get; set; }

        /// <summary>目标小程序原始 ID；页面类型为 5 时返回。</summary>
        public string username { get; set; }

        /// <summary>页面路径参数；页面类型为 0 时返回。</summary>
        public string query_string { get; set; }
    }

    /// <summary>城市服务消息通路模板字段。</summary>
    public class CityServiceMessageTemplateField
    {
        /// <summary>字段展示值。</summary>
        public string value { get; set; }

        /// <summary>可选展示颜色；仅服务通知生效。</summary>
        public string color { get; set; }
    }

    /// <summary>城市服务消息通路发送请求。</summary>
    /// <typeparam name="TData">城市服务分配模板对应的数据结构。</typeparam>
    public class CityServiceSendMessageDataRequest<TData>
    {
        /// <summary>用户 OpenId；通过小程序提供服务时仍使用小程序 OpenId。</summary>
        public string openid { get; set; }

        /// <summary>城市服务分配的模板 ID。</summary>
        public string biz_template_id { get; set; }

        /// <summary>结果页样式 ID；消息包含结果页时必填。</summary>
        public string result_page_style_id { get; set; }

        /// <summary>办事记录样式 ID；消息包含办事记录时必填。</summary>
        public string deal_msg_style_id { get; set; }

        /// <summary>页卡样式 ID；消息包含页卡时必填。</summary>
        public string card_style_id { get; set; }

        /// <summary>业务订单号；同一订单号的办事记录会合并。</summary>
        public string order_no { get; set; }

        /// <summary>服务通知、结果页或待办提醒的跳转链接。</summary>
        public string url { get; set; }

        /// <summary>城市服务模板数据。</summary>
        public TData data { get; set; }
    }

    /// <summary>城市服务消息通路发送结果。</summary>
    public class CityServiceSendMessageDataJsonResult : WxJsonResult
    {
        /// <summary>城市服务结果页 URL；未传结果页样式 ID 时为空。</summary>
        public string result_page_url { get; set; }
    }

    /// <summary>校验城市服务实名信息请求。</summary>
    public class CityServiceCheckRealNameRequest
    {
        /// <summary>用户在申请接口权限的小程序下的 OpenId。</summary>
        public string openid { get; set; }

        /// <summary>待校验姓名。</summary>
        public string real_name { get; set; }

        /// <summary>待校验证件号。</summary>
        public string cred_id { get; set; }

        /// <summary>证件类型，当前仅支持字符串 <c>1</c>（身份证）。</summary>
        public string cred_type { get; set; }

        /// <summary>小程序回跳获得的实名校验 Code。</summary>
        public string code { get; set; }
    }

    /// <summary>校验城市服务实名信息结果。</summary>
    public class CityServiceCheckRealNameJsonResult : WxJsonResult
    {
        /// <summary>OpenId 实名状态，可由多个以分号连接的结果值组成。</summary>
        public string verify_openid { get; set; }

        /// <summary>姓名与证件号校验结果。</summary>
        public string verify_real_name { get; set; }
    }

    /// <summary>获取交通出行仿原生页面请求。</summary>
    public class CityServiceBusinessViewRequest
    {
        /// <summary>页面类型：0 欢迎页，1 乘车码，2 已开通路线，3 个人中心，4 乘车记录，5 帮助，6 欠费记录。</summary>
        public int path_type { get; set; }
    }

    /// <summary>获取交通出行仿原生页面结果。</summary>
    public class CityServiceBusinessViewJsonResult : WxJsonResult
    {
        /// <summary>仿原生业务类型。</summary>
        public string business_type { get; set; }

        /// <summary>调用仿原生小程序时使用的参数。</summary>
        public string query_string { get; set; }

        /// <summary>查询参数到期 Unix 时间戳。</summary>
        public long expire_at { get; set; }
    }

    /// <summary>微信就医助手消息请求。</summary>
    /// <typeparam name="TBusinessInfo">当前消息状态对应的业务字段结构。</typeparam>
    public class CityServiceMedicalMessageRequest<TBusinessInfo>
    {
        /// <summary>消息子状态，例如 1501001 表示预约挂号成功。</summary>
        public int status { get; set; }

        /// <summary>用户在公众号或小程序下的 OpenId。</summary>
        public string open_id { get; set; }

        /// <summary>业务方唯一订单 ID；同次就医流程应保持一致。</summary>
        public string order_id { get; set; }

        /// <summary>消息唯一标识，同用户、同订单下必须唯一。</summary>
        public string msg_id { get; set; }

        /// <summary>已开通就医助手的公众号 AppId。</summary>
        public string app_id { get; set; }

        /// <summary>业务 ID，固定为 150。</summary>
        public int business_id { get; set; }

        /// <summary>当前消息状态对应的业务字段。</summary>
        public TBusinessInfo business_info { get; set; }
    }

    /// <summary>预约挂号类就医助手业务字段。</summary>
    public class CityServiceMedicalBusinessInfo
    {
        /// <summary>患者在医院的就诊卡号。</summary>
        public string pat_hospital_id { get; set; }

        /// <summary>患者姓名。</summary>
        public string pat_name { get; set; }

        /// <summary>医生姓名或医生级别。</summary>
        public string doc_name { get; set; }

        /// <summary>科室名称，可包含楼栋和楼层。</summary>
        public string department_name { get; set; }

        /// <summary>科室位置。</summary>
        public string department_location { get; set; }

        /// <summary>预约时间。</summary>
        public string appointment_time { get; set; }

        /// <summary>就医须知。</summary>
        public string memo { get; set; }

        /// <summary>医院普通页面跳转信息。</summary>
        public CityServiceMedicalRedirectPage redirect_page { get; set; }

        /// <summary>医院适老化页面跳转信息。</summary>
        public CityServiceMedicalRedirectPage elder_redirect_page { get; set; }
    }

    /// <summary>就医助手页面跳转信息。</summary>
    public class CityServiceMedicalRedirectPage
    {
        /// <summary>页面类型：<c>web</c> 或 <c>mini_program</c>。</summary>
        public string page_type { get; set; }

        /// <summary>网页地址；页面类型为 web 时必填。</summary>
        public string url { get; set; }

        /// <summary>目标小程序 AppId；页面类型为 mini_program 时必填。</summary>
        public string app_id { get; set; }

        /// <summary>目标小程序完整路径；页面类型为 mini_program 时必填。</summary>
        public string fullpath { get; set; }
    }

    /// <summary>查询长辈就医实名信息请求。</summary>
    public class CityServiceGetMedicalRealNameRequest
    {
        /// <summary>业务方 AppId。</summary>
        public string app_id { get; set; }

        /// <summary>微信用户 OpenId。</summary>
        public string open_id { get; set; }

        /// <summary>实名信息授权 Code，有效期十分钟。</summary>
        public string wxmed_authcode { get; set; }
    }

    /// <summary>查询长辈就医实名信息结果。</summary>
    public class CityServiceGetMedicalRealNameJsonResult : WxJsonResult
    {
        /// <summary>Base64 编码的加密实名信息。</summary>
        public string cipher_real_name { get; set; }

        /// <summary>加密算法，默认 AES_256_ECB_PKCS7Padding。</summary>
        public string cipher_algorithm { get; set; }

        /// <summary>实名信息加密密钥版本号。</summary>
        public int key_version { get; set; }

        /// <summary>业务方 AppId。</summary>
        public string app_id { get; set; }

        /// <summary>微信用户 OpenId。</summary>
        public string open_id { get; set; }

        /// <summary>官方返回示例误写的 OpenId 字段。</summary>
        public string openid_id { get; set; }
    }

    /// <summary>解密后的长辈就医实名信息结构。</summary>
    public class CityServiceMedicalRealNameInfo
    {
        /// <summary>姓名。</summary>
        public string real_name { get; set; }

        /// <summary>证件号码。</summary>
        public string id_card_no { get; set; }

        /// <summary>证件类型：1 身份证，4 澳门通行证，5 台湾通行证，6 香港通行证。</summary>
        public int id_card_type { get; set; }

        /// <summary>电话号码。</summary>
        public string phone { get; set; }

        /// <summary>数据生成 Unix 时间戳。</summary>
        public long timestamp { get; set; }

        /// <summary>电话号码国家或地区代码。</summary>
        public string phone_country_code { get; set; }
    }

    /// <summary>查询长辈就医开通状态请求。</summary>
    public class CityServiceGetMessageRelationRequest
    {
        /// <summary>业务 ID，长辈就医固定为 130；官方参数表误标为 string，示例使用 number。</summary>
        public int business_id { get; set; }

        /// <summary>微信用户 OpenId。</summary>
        public string open_id { get; set; }
    }

    /// <summary>查询长辈就医开通状态结果。</summary>
    public class CityServiceGetMessageRelationJsonResult : WxJsonResult
    {
        /// <summary>官方示例使用的下划线错误码字段。</summary>
        public int err_code { get; set; }

        /// <summary>官方示例使用的下划线错误信息字段。</summary>
        public string err_msg { get; set; }

        /// <summary>是否已经开通长辈就医订阅；官方表为 boolean，示例以 0/1 返回。</summary>
        public bool is_subscribed { get; set; }
    }

    /// <summary>查询医院公告请求。</summary>
    public class CityServiceGetHospitalNoticeListRequest
    {
        /// <summary>业务方公众号 AppId。</summary>
        public string app_id { get; set; }

        /// <summary>公告类型：1 挂号前就医须知，2 来院须知。</summary>
        public int notice_type { get; set; }
    }

    /// <summary>医院公告信息。</summary>
    public class CityServiceHospitalNotice
    {
        /// <summary>公告 ID。</summary>
        public long notice_id { get; set; }

        /// <summary>公告内容。</summary>
        public string content { get; set; }

        /// <summary>公告状态：DRAFT 草稿，PUBLIC 已发布。</summary>
        public string status { get; set; }

        /// <summary>可以预览草稿的用户 OpenId；官方表将该字段误列在结果顶层，示例放在公告项内。</summary>
        public IList<string> preview_openid { get; set; }
    }

    /// <summary>查询医院公告结果。</summary>
    public class CityServiceGetHospitalNoticeListJsonResult : WxJsonResult
    {
        /// <summary>最近五条医院公告。</summary>
        public IList<CityServiceHospitalNotice> notice_list { get; set; }

        /// <summary>兼容官方参数表列出的顶层预览用户列表。</summary>
        public IList<string> preview_openid { get; set; }
    }

    /// <summary>设置医院公告草稿预览权限请求。</summary>
    public class CityServicePreviewHospitalNoticeRequest
    {
        /// <summary>业务方公众号 AppId。</summary>
        public string app_id { get; set; }

        /// <summary>公告类型：1 挂号前就医须知，2 来院须知。</summary>
        public int notice_type { get; set; }

        /// <summary>公告 ID；官方参数表误标为 string，示例和返回均为 number。</summary>
        public long notice_id { get; set; }

        /// <summary>获得或移除预览权限的用户微信号。</summary>
        public string preview_username { get; set; }

        /// <summary>操作类型：1 删除预览权限，2 添加预览权限。</summary>
        public int operation { get; set; }
    }

    /// <summary>发布医院公告请求。</summary>
    public class CityServicePublishHospitalNoticeRequest
    {
        /// <summary>业务方公众号 AppId。</summary>
        public string app_id { get; set; }

        /// <summary>公告类型：1 挂号前就医须知，2 来院须知。</summary>
        public int notice_type { get; set; }

        /// <summary>待发布的公告 ID。</summary>
        public long notice_id { get; set; }
    }

    /// <summary>新增或覆盖医院公告草稿请求。</summary>
    public class CityServiceSetHospitalNoticeRequest
    {
        /// <summary>业务方公众号 AppId。</summary>
        public string app_id { get; set; }

        /// <summary>公告类型：1 挂号前就医须知，2 来院须知。</summary>
        public int notice_type { get; set; }

        /// <summary>公告富文本内容，最长 3000 个字符。</summary>
        public string notice_content { get; set; }

        /// <summary>待覆盖草稿的公告 ID；不填写时新增草稿。</summary>
        public long? notice_id { get; set; }
    }

    /// <summary>医院公告操作结果。</summary>
    public class CityServiceNoticeIdJsonResult : WxJsonResult
    {
        /// <summary>新增、修改、预览或发布的公告 ID。</summary>
        public long notice_id { get; set; }
    }
}
