/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：RequestMessageInfo_Device_Data_Auth_Change.cs
    文件功能描述：设备数据授权变更通知


    创建标识：Senparc - 20260726

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 增加设备数据授权变更通知模型

----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 设备数据授权变更通知。
    /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96103"/></para>
    /// </summary>
    public class RequestMessageInfo_Device_Data_Auth_Change : ThirdPartyInfoBase, IThirdPartyInfoBase
    {
        /// <summary>
        /// 第三方回调类型。
        /// </summary>
        public override ThirdPartyInfo InfoType => ThirdPartyInfo.DEVICE_DATA_AUTH_CHANGE;

        /// <summary>
        /// 授权方企业的 CorpId。
        /// </summary>
        public string AuthCorpId { get; set; }
    }
}
