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

    文件名：OrderIncrementJson.cs
    文件功能描述：OrderIncrementJson 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Collections.Generic;
using Newtonsoft.Json;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Sec
{
    /// <summary>
    /// 特殊发货报备请求。
    /// </summary>
    public class SpecialOrderRequest
    {
        /// <summary>
        /// 需要报备的订单号，可填写微信支付单号或商户单号。
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 报备类型：1 预售商品订单，2 测试订单。
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 预计发货时间，Unix 秒级时间戳；预售商品订单必填，测试订单可省略。
        /// </summary>
        public long? delay_to { get; set; }
    }

    /// <summary>
    /// 小程序品牌申请请求。
    /// </summary>
    public class FamousBrandApplyRequest
    {
        /// <summary>
        /// 品牌申请信息。微信官方请求字段首字母大写，序列化时保持为 Application。
        /// </summary>
        [JsonProperty("Application")]
        public FamousBrandApplication Application { get; set; }
    }

    /// <summary>
    /// 小程序品牌申请信息。
    /// </summary>
    public class FamousBrandApplication
    {
        /// <summary>
        /// 品牌申请类型：1 知名品牌，2 已接入小店商品组件。
        /// </summary>
        public int apply_for { get; set; }

        /// <summary>
        /// 品牌审核材料；申请知名品牌时必填。
        /// </summary>
        public FamousBrandAuditInfo audit_info { get; set; }
    }

    /// <summary>
    /// 小程序品牌审核材料。
    /// </summary>
    public class FamousBrandAuditInfo
    {
        /// <summary>
        /// 品牌名称。
        /// </summary>
        public string brand_name { get; set; }

        /// <summary>
        /// 品牌类型：1 工信部消费品名单、2 中国连锁经营协会网络零售、3 中华老字号、4 电商平台旗舰店、5 全球 500 强、6 中国 500 强、7 驰名或著名商标、8 工信部消费名品成长企业名单。
        /// </summary>
        public int brand_type { get; set; }

        /// <summary>
        /// 旗舰店所在电商平台；<see cref="brand_type"/> 为 4 时必填。
        /// </summary>
        public string flagship_in_which_ec_platform { get; set; }

        /// <summary>
        /// 电商平台官方旗舰店佐证图片的临时素材 media_id 列表；<see cref="brand_type"/> 为 4 时必填。
        /// </summary>
        public IList<string> ec_platform_proof_list { get; set; }

        /// <summary>
        /// 其他补充材料图片的临时素材 media_id 列表。
        /// </summary>
        public IList<string> other_material_list { get; set; }

        /// <summary>
        /// 有关部门认定的驰名或著名商标佐证图片的临时素材 media_id 列表；<see cref="brand_type"/> 为 7 时必填。
        /// </summary>
        public IList<string> authority_certified_proof_list { get; set; }
    }

    /// <summary>
    /// 小程序品牌申请进度。
    /// </summary>
    public class FamousBrandProgress
    {
        /// <summary>
        /// 品牌申请状态：1 平台审核中，2 审核驳回，3 审核通过。官方示例暂使用 0 表示尚无有效进度。
        /// </summary>
        public int status { get; set; }
    }

    /// <summary>
    /// 小程序品牌申请状态中的审核信息。
    /// </summary>
    public class FamousBrandStatusAuditInfo
    {
        /// <summary>
        /// 审核原因，仅审核驳回时有值。
        /// </summary>
        public string audit_reason { get; set; }
    }

    /// <summary>
    /// 小程序品牌申请状态中的申请信息。
    /// </summary>
    public class FamousBrandStatusApplication
    {
        /// <summary>
        /// 申请类型：1 知名品牌，2 已接入小店商品组件。
        /// </summary>
        public int apply_for { get; set; }

        /// <summary>
        /// 审核信息。
        /// </summary>
        public FamousBrandStatusAuditInfo audit_info { get; set; }

        /// <summary>
        /// 品牌申请状态：1 平台审核中，2 审核驳回，3 审核通过。官方示例暂使用 0 表示尚无有效状态。
        /// </summary>
        public int status { get; set; }
    }

    /// <summary>
    /// 查询小程序品牌申请状态结果。
    /// </summary>
    public class FamousBrandStatusJsonResult : WxJsonResult
    {
        /// <summary>
        /// 整体申请进度。
        /// </summary>
        public FamousBrandProgress progress { get; set; }

        /// <summary>
        /// 当前品牌申请信息和审核状态。
        /// </summary>
        public FamousBrandStatusApplication application { get; set; }
    }

    /// <summary>
    /// 小程序交易类型变更申请请求。
    /// </summary>
    public class TradeTypeChangeRequest
    {
        /// <summary>
        /// 目标交易类型：1 综合类，2 实物电商类，3 线下服务类，4 在线服务类。
        /// </summary>
        public int trade_type { get; set; }

        /// <summary>
        /// 申请材料列表，最多 10 个，其中视频最多 3 个。
        /// </summary>
        public IList<TradeTypeMaterial> material_list { get; set; }

        /// <summary>
        /// 申请理由。
        /// </summary>
        public string reason { get; set; }
    }

    /// <summary>
    /// 小程序交易类型变更申请材料。
    /// </summary>
    public class TradeTypeMaterial
    {
        /// <summary>
        /// 材料类型：1 图片，2 视频。
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 通过“新增临时素材”接口上传后获得的 media_id。
        /// </summary>
        public string media_id { get; set; }
    }
}
