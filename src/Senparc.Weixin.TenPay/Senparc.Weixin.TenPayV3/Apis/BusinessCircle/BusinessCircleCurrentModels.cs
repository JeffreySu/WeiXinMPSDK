#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：BusinessCircleCurrentModels.cs
    文件功能描述：微信支付 V3 智慧商圈积分提交状态与停车状态模型


    创建标识：Senparc - 20260728

    修改标识：Senparc - 20260728
    修改描述：v2.6.0 新增积分提交状态查询及停车状态同步模型

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.BusinessCircle
{
    /// <summary>顾客积分提交状态查询结果。</summary>
    public class QueryPointsCommitStatusReturnJson : ReturnJsonBase
    {
        /// <summary>积分提交状态，以微信支付官方返回枚举为准。</summary>
        public string points_commit_status { get; set; }
    }

    /// <summary>
    /// 智慧商圈停车状态同步请求。
    /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/merchant/4012535502</para>
    /// </summary>
    public class BusinessCircleParkingRequestData
    {
        /// <summary>服务商模式下的子商户号。</summary>
        public string sub_mchid { get; set; }

        /// <summary>微信支付分配的品牌 ID。</summary>
        public long brandid { get; set; }

        /// <summary>顾客授权时使用的小程序 AppID。</summary>
        public string appid { get; set; }

        /// <summary>顾客在小程序 AppID 下的 OpenID。</summary>
        public string openid { get; set; }

        /// <summary>车辆车牌号。</summary>
        public string plate_number { get; set; }

        /// <summary>停车状态：IN 表示入场，OUT 表示离场。</summary>
        public string state { get; set; }

        /// <summary>入场或离场时间，格式为 RFC 3339。</summary>
        public string time { get; set; }
    }
}
