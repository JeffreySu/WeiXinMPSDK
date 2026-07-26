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

    文件名：MiniDramaAuditJson.cs
    文件功能描述：MiniDramaAuditJson 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.MiniDrama
{
    /// <summary>短剧演员信息。</summary>
    public class MiniDramaActor
    {
        /// <summary>演员姓名，最多 30 个字符。</summary>
        public string name { get; set; }

        /// <summary>演员照片临时素材 ID。</summary>
        public string photo_material_id { get; set; }

        /// <summary>饰演角色，最多 30 个字符。</summary>
        public string role { get; set; }

        /// <summary>演员简介，最多 100 个字符。</summary>
        public string profile { get; set; }
    }

    /// <summary>短剧演员列表容器。</summary>
    public class MiniDramaActorList
    {
        /// <summary>演员列表；真人短剧提审时需填写 2 至 5 位演员。</summary>
        public IList<MiniDramaActor> actor { get; set; }
    }

    /// <summary>短剧剧集替换关系。</summary>
    public class MiniDramaReplaceMediaItem
    {
        /// <summary>旧剧集媒资 ID。</summary>
        public long old { get; set; }

        /// <summary>新剧集媒资 ID。</summary>
        public long @new { get; set; }
    }

    /// <summary>短剧版权保护申请信息。</summary>
    public class MiniDramaCopyrightInfo
    {
        /// <summary>提审主体身份：1 剧目制作方，2 授权播出方或版权方。</summary>
        public int copyright_role { get; set; }

        /// <summary>是否申请版权保护：0 不申请，1 申请。</summary>
        public int apply_for_copyright_protection { get; set; }

        /// <summary>可选版权验证方式：1 基于证明材料，2 基于版权授权关系。</summary>
        public string copyright_verification { get; set; }

        /// <summary>剧目制作证明临时素材 ID 列表；官方将于 2026-07-28 调整为最多 10 个。</summary>
        public IList<string> proof_of_production { get; set; }

        /// <summary>版权采买或播出授权证明临时素材 ID 列表，最多 4 个。</summary>
        public IList<string> purchase_or_broadcast_authorization_certificate { get; set; }
    }

    /// <summary>短剧剧目提审请求。</summary>
    public class MiniDramaAuditDramaRequest
    {
        /// <summary>重新提审时必填的剧目 ID；首次提审不填。</summary>
        public long? drama_id { get; set; }

        /// <summary>剧目名称；首次提审必填。</summary>
        public string name { get; set; }

        /// <summary>剧集数；首次提审必填。</summary>
        public int? media_count { get; set; }

        /// <summary>剧集媒资 ID 列表；首次提审时数量必须与 <see cref="media_count"/> 一致。</summary>
        public IList<long> media_id_list { get; set; }

        /// <summary>剧目简介，最多 200 个字符。</summary>
        public string description { get; set; }

        /// <summary>可选剧目推荐语，最多 30 个字符。</summary>
        public string recommendations { get; set; }

        /// <summary>剧目海报临时素材 ID。</summary>
        public string cover_material_id { get; set; }

        /// <summary>可选推广海报临时素材 ID。</summary>
        public string promotion_poster_material_id { get; set; }

        /// <summary>剧目制作方。</summary>
        public string producer { get; set; }

        /// <summary>权利声明或播放授权材料 ID；官方计划自 2026-07-28 12:00 起弃用。</summary>
        public string authorized_material_id { get; set; }

        /// <summary>剧目资质类型：1 已取得许可证或备案号，2 未取得且制作成本小于 100 万元。</summary>
        public int? qualification_type { get; set; }

        /// <summary>资质类型为 1 时填写的剧目备案号或许可证编号。</summary>
        public string registration_number { get; set; }

        /// <summary>资质类型为 1 时填写的资质证明临时素材 ID。</summary>
        public string qualification_certificate_material_id { get; set; }

        /// <summary>资质类型为 2 时填写的成本配置比例情况报告临时素材 ID。</summary>
        public string cost_commitment_letter_material_id { get; set; }

        /// <summary>剧目制作成本，单位为万元，资质类型为 2 时填写 1 至 99 的整数。</summary>
        public int? cost_of_production { get; set; }

        /// <summary>是否加急：1 加急，0 或不填为不加急；仅首次提审生效。</summary>
        public int? expedited { get; set; }

        /// <summary>真人短剧演员信息。</summary>
        public MiniDramaActorList actor_list { get; set; }

        /// <summary>可选其他材料临时素材 ID，例如互动短剧剧情走线图。</summary>
        public string other_material_material_id { get; set; }

        /// <summary>重新提审时可选的剧集替换关系。</summary>
        public IList<MiniDramaReplaceMediaItem> replace_media_list { get; set; }

        /// <summary>版权保护相关信息。</summary>
        public MiniDramaCopyrightInfo copyright { get; set; }

        /// <summary>剧目类型：1 漫剧，2 真人，3 数字真人。</summary>
        public int? drama_type { get; set; }

        /// <summary>AI 内容声明：1 包含 AI 生成内容，0 或不填表示不包含。</summary>
        public int? content_declared { get; set; }

        /// <summary>AI 制作证明材料临时素材 ID；官方计划自 2026-07-28 12:00 起在声明含 AI 内容时必填。</summary>
        public string ai_software_ownership_proof { get; set; }

        /// <summary>其他平台发布证明临时素材 ID，最多 4 个；官方计划自 2026-07-28 12:00 起生效。</summary>
        public IList<string> other_platform_publication_proof { get; set; }
    }

    /// <summary>通用短剧分页请求。</summary>
    public class MiniDramaPageRequest
    {
        /// <summary>可选分页偏移量。</summary>
        public int? offset { get; set; }

        /// <summary>可选分页大小。</summary>
        public int? limit { get; set; }
    }

    /// <summary>短剧剧目 ID 请求。</summary>
    public class MiniDramaDramaIdRequest
    {
        /// <summary>剧目 ID。</summary>
        public long drama_id { get; set; }
    }

    /// <summary>提交替换剧集审核请求。</summary>
    public class MiniDramaSubmitReplaceMediasRequest : MiniDramaDramaIdRequest
    {
        /// <summary>待审核的剧集替换关系列表。</summary>
        public IList<MiniDramaReplaceMediaItem> replace_media_list { get; set; }
    }

    /// <summary>替换已审核通过剧集请求。</summary>
    public class MiniDramaReplaceMediaRequest : MiniDramaDramaIdRequest
    {
        /// <summary>旧剧集媒资 ID。</summary>
        public long old_media_id { get; set; }

        /// <summary>新剧集媒资 ID。</summary>
        public long new_media_id { get; set; }
    }

    /// <summary>修改短剧基本信息请求。</summary>
    public class MiniDramaModifyBasicInfoRequest : MiniDramaDramaIdRequest
    {
        /// <summary>可选剧目简介，最多 200 个字符。</summary>
        public string description { get; set; }

        /// <summary>可选剧目海报临时素材 ID。</summary>
        public string cover_material_id { get; set; }

        /// <summary>可选剧目推荐语。</summary>
        public string recommendations { get; set; }

        /// <summary>可选推广海报临时素材 ID。</summary>
        public string promotion_poster_material_id { get; set; }

        /// <summary>可选备用剧名。</summary>
        public string alternate_name { get; set; }

        /// <summary>可选完整演员信息；修改时需填写全部演员。</summary>
        public MiniDramaActorList actor_list { get; set; }

        /// <summary>可选剧目资质类型。</summary>
        public int? qualification_type { get; set; }

        /// <summary>可选备案号或许可证编号。</summary>
        public string registration_number { get; set; }

        /// <summary>可选资质证明临时素材 ID。</summary>
        public string qualification_certificate_material_id { get; set; }

        /// <summary>可选制作成本，单位为万元。</summary>
        public int? cost_of_production { get; set; }

        /// <summary>可选成本配置比例情况报告临时素材 ID。</summary>
        public string cost_commitment_letter_material_id { get; set; }

        /// <summary>可选其他材料临时素材 ID。</summary>
        public string other_material_material_id { get; set; }

        /// <summary>可选剧目制作方；申请版权保护时必填。</summary>
        public string producer { get; set; }

        /// <summary>可选版权保护信息。</summary>
        public MiniDramaCopyrightInfo copyright { get; set; }
    }

    /// <summary>查询剧目最后审核信息请求。</summary>
    public class MiniDramaGetLatestAuditInfoRequest : MiniDramaDramaIdRequest
    {
        /// <summary>审核类型：0 首次提审、1 再次提审、2 替换剧集、3 修改基本信息。</summary>
        public int audit_type { get; set; }
    }

    /// <summary>短剧剧目审核信息。</summary>
    public class MiniDramaAuditDetail
    {
        /// <summary>审核状态：0 无效、1 审核中、2 最终失败、3 通过、4 驳回重填。</summary>
        public int status { get; set; }

        /// <summary>审核类型：0 首次提审、1 再次提审、2 替换剧集、3 修改基本信息。</summary>
        public int? audit_type { get; set; }

        /// <summary>提审时间戳。</summary>
        public long create_time { get; set; }

        /// <summary>审核时间戳。</summary>
        public long audit_time { get; set; }
    }

    /// <summary>短剧剧集引用。</summary>
    public class MiniDramaMediaReference
    {
        /// <summary>媒资文件 ID。</summary>
        public long media_id { get; set; }
    }

    /// <summary>短剧剧目信息。</summary>
    public class MiniDramaDramaInfo
    {
        /// <summary>剧目 ID。</summary>
        public long drama_id { get; set; }

        /// <summary>创建时间戳。</summary>
        public long create_time { get; set; }

        /// <summary>剧名。</summary>
        public string name { get; set; }

        /// <summary>剧目海报链接。</summary>
        public string cover_url { get; set; }

        /// <summary>剧集数量。</summary>
        public int media_count { get; set; }

        /// <summary>制作方。</summary>
        public string producer { get; set; }

        /// <summary>编剧。</summary>
        public string playwright { get; set; }

        /// <summary>剧目简介。</summary>
        public string description { get; set; }

        /// <summary>广播电视节目制作经营许可证或备案信息。</summary>
        public string production_license { get; set; }

        /// <summary>审核信息。</summary>
        public MiniDramaAuditDetail audit_detail { get; set; }

        /// <summary>剧集信息列表。</summary>
        public IList<MiniDramaMediaReference> media_list { get; set; }

        /// <summary>是否加急：1 加急，0 或空为不加急。</summary>
        public int? expedited { get; set; }

        /// <summary>剧目推荐语。</summary>
        public string recommendations { get; set; }

        /// <summary>推广海报链接。</summary>
        public string promotion_poster { get; set; }

        /// <summary>演员信息。</summary>
        public MiniDramaActorList actor_list { get; set; }

        /// <summary>剧目状态：0 正常可播、1 审核中、2 审核失败、3 平台下架。</summary>
        public int status { get; set; }

        /// <summary>其他材料；官方返回示例包含该字段但参数表未列出。</summary>
        public string other_material { get; set; }
    }

    /// <summary>短剧提审结果。</summary>
    public class MiniDramaAuditDramaJsonResult : WxJsonResult
    {
        /// <summary>剧目 ID。</summary>
        public long drama_id { get; set; }
    }

    /// <summary>短剧剧目列表结果。</summary>
    public class MiniDramaListDramasJsonResult : WxJsonResult
    {
        /// <summary>剧目信息列表。</summary>
        public IList<MiniDramaDramaInfo> drama_info_list { get; set; }
    }

    /// <summary>短剧剧目详情结果。</summary>
    public class MiniDramaGetDramaJsonResult : WxJsonResult
    {
        /// <summary>剧目信息。</summary>
        public MiniDramaDramaInfo drama_info { get; set; }
    }

    /// <summary>短剧最后审核信息结果。</summary>
    public class MiniDramaGetLatestAuditInfoJsonResult : WxJsonResult
    {
        /// <summary>审核信息。</summary>
        public MiniDramaAuditDetail audit_detail { get; set; }
    }

    /// <summary>短剧 CDN 用量查询请求。</summary>
    public class MiniDramaGetCdnUsageDataRequest
    {
        /// <summary>起始时间戳。</summary>
        public long start_time { get; set; }

        /// <summary>截止时间戳。</summary>
        public long end_time { get; set; }

        /// <summary>时间粒度，单位为分钟，可取 5、60、1440。</summary>
        public string data_interval { get; set; }

        /// <summary>可选查询类型：0 全部、1 播放器定向流量、2 通用播放流量。</summary>
        public int? query_type { get; set; }
    }

    /// <summary>短剧 CDN 统计项。</summary>
    public class MiniDramaCdnUsageItem
    {
        /// <summary>数据时间区间的开始时间戳。</summary>
        public long time { get; set; }

        /// <summary>数据大小，单位为字节。</summary>
        public long value { get; set; }
    }

    /// <summary>短剧 CDN 用量查询结果。</summary>
    public class MiniDramaGetCdnUsageDataJsonResult : WxJsonResult
    {
        /// <summary>实际返回的时间粒度，单位为分钟。</summary>
        public int data_interval { get; set; }

        /// <summary>CDN 统计数据。</summary>
        public IList<MiniDramaCdnUsageItem> item_list { get; set; }
    }

    /// <summary>短剧 CDN 日志查询请求。</summary>
    public class MiniDramaGetCdnLogsRequest
    {
        /// <summary>起始时间戳。</summary>
        public long start_time { get; set; }

        /// <summary>结束时间戳，与起始时间跨度不能超过 48 小时。</summary>
        public long end_time { get; set; }

        /// <summary>已于 2024-03-29 废弃的分页大小，保留用于兼容旧调用。</summary>
        public int? limit { get; set; }

        /// <summary>已于 2024-03-29 废弃的分页偏移量，保留用于兼容旧调用。</summary>
        public int? offset { get; set; }

        /// <summary>可选查询类型：0 全部、1 播放器定向流量、2 通用播放流量。</summary>
        public int? query_type { get; set; }
    }

    /// <summary>短剧 CDN 日志下载信息。</summary>
    public class MiniDramaCdnLog
    {
        /// <summary>日志所属日期。官方参数表误标为 number，示例实际为 yyyy-MM-dd 字符串。</summary>
        public string date { get; set; }

        /// <summary>日志名称。官方参数表误标为 number，示例实际为字符串。</summary>
        public string name { get; set; }

        /// <summary>日志下载链接，24 小时内有效。官方参数表误标为 number。</summary>
        public string url { get; set; }

        /// <summary>日志起始时间戳。</summary>
        public long start_time { get; set; }

        /// <summary>日志结束时间戳。</summary>
        public long end_time { get; set; }
    }

    /// <summary>短剧 CDN 日志查询结果。</summary>
    public class MiniDramaGetCdnLogsJsonResult : WxJsonResult
    {
        /// <summary>日志下载链接总数。</summary>
        public int total_count { get; set; }

        /// <summary>国内 CDN 节点日志列表。官方参数表误标为 object，示例实际为数组。</summary>
        public IList<MiniDramaCdnLog> domestic_cdn_logs { get; set; }
    }

    /// <summary>短剧流量包查询请求。</summary>
    public class MiniDramaListPackagesRequest
    {
        /// <summary>流量包状态：0 不过滤、1 有效、2 无效、3 过期。</summary>
        public int status { get; set; }

        /// <summary>分页偏移量。</summary>
        public int offset { get; set; }

        /// <summary>分页大小，最大 100。</summary>
        public int limit { get; set; }

        /// <summary>可选查询类型：0 通用流量，1 定向流量。</summary>
        public int? query_type { get; set; }
    }

    /// <summary>短剧流量包信息。</summary>
    public class MiniDramaTrafficPackage
    {
        /// <summary>有效期起始时间戳。</summary>
        public long start_time { get; set; }

        /// <summary>有效期截止时间戳。</summary>
        public long end_time { get; set; }

        /// <summary>已消耗流量，单位为 MB。</summary>
        public long used { get; set; }

        /// <summary>流量包总额，单位为 MB。</summary>
        public long all { get; set; }

        /// <summary>订单号。官方定义为 string，示例使用超大整数。</summary>
        public string order_id { get; set; }

        /// <summary>流量包状态。</summary>
        public int status { get; set; }

        /// <summary>是否已删除或失效。</summary>
        public int is_deleted { get; set; }

        /// <summary>流量包编号。</summary>
        public string package_id { get; set; }
    }

    /// <summary>短剧流量包查询结果。</summary>
    public class MiniDramaListPackagesJsonResult : WxJsonResult
    {
        /// <summary>符合条件的流量包总数。</summary>
        public int total_count { get; set; }

        /// <summary>流量包详情列表。</summary>
        public IList<MiniDramaTrafficPackage> package_list { get; set; }
    }
}
