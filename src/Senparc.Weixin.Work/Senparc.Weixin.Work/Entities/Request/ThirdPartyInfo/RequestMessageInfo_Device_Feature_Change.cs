/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：RequestMessageInfo_Device_Feature_Change.cs
    文件功能描述：硬件设备特征变更通知


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 增加硬件设备特征变更通知模型

----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 硬件设备特征变更通知。
    /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/90751"/></para>
    /// </summary>
    public class RequestMessageInfo_Device_Feature_Change : ThirdPartyInfoBase, IThirdPartyInfoBase
    {
        /// <summary>
        /// 第三方回调类型。
        /// </summary>
        public override ThirdPartyInfo InfoType => ThirdPartyInfo.DEVICE_FEATURE_CHANGE;

        /// <summary>
        /// 服务商 CorpId。
        /// </summary>
        public string ServiceCorpId { get; set; }

        /// <summary>
        /// 授权方企业 CorpId；未绑定授权企业时可能为空。
        /// </summary>
        public string AuthCorpId { get; set; }

        /// <summary>
        /// 设备序列号。
        /// </summary>
        public string DeviceSn { get; set; }
    }
}
