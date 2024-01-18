/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：RequestMessageInfo_Reset_Permanent_Code.cs
    文件功能描述：推广二维码注册企业微信完成通知
    
    
    创建标识：dukecheng - 2023-09-11
    
----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 代开发应用=>授权客户=>应用Secret 重新获取通知
    /// </summary>
    public class RequestMessageInfo_Reset_Permanent_Code : ThirdPartyInfoBase, IThirdPartyInfoBase
    {
        public override ThirdPartyInfo InfoType => ThirdPartyInfo.RESET_PERMANENT_CODE;

        public string AuthCode { get; set; }
    }
}
