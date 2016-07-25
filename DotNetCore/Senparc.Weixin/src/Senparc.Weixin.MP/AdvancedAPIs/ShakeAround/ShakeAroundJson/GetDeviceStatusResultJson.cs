/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

    文件名：GetDeviceStatusResultJson.cs
    文件功能描述：查询审核状态返回结果


    创建标识：Senparc - 20160424

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.ShakeAround
{
    /// <summary>
    /// 查询审核状态返回结果
    /// </summary>
    public class GetDeviceStatusResultJson : WxJsonResult
    {
        public GetDeviceStatusData data { get; set; }
    }

    public class GetDeviceStatusData
    {
        /// <summary>
        /// 提交申请的时间戳
        /// </summary>
        public long apply_time { get; set; }
        /// <summary>
        /// 审核备注，包括审核不通过的原因
        /// </summary>
        public string audit_comment { get; set; }
        /// <summary>
        /// 审核状态。0：审核未通过、1：审核中、2：审核已通过；审核会在三个工作日内完成
        /// TODO：可以做成枚举
        /// </summary>
        public int audit_status { get; set; }
        /// <summary>
        /// 确定审核结果的时间戳；若状态为审核中，则该时间值为0
        /// </summary>
        public long audit_time { get; set; }
    }
}
