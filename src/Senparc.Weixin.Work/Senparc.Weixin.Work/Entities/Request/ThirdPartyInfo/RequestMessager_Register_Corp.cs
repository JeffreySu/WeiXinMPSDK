/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：RequestMessager_Register_Corp.cs
    文件功能描述：推广二维码注册企业微信完成通知
    
    
    创建标识：Billzjh - 20201210
    
    修改标识：Senparc - 20230905
    修改描述：v4.15.0 完善“第三方服务商小程序备案”接口

----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 推广二维码注册企业微信完成通知
    /// </summary>
    public class RequestMessager_Register_Corp : ThirdPartyInfoBase, IThirdPartyInfoBase
    {
        //public override RequestMsgType MsgType
        //{
        //    get { return RequestMsgType.Unknown; }
        //}

        public override ThirdPartyInfo InfoType => ThirdPartyInfo.REGISTER_CORP;

        /// <summary>
        /// 服务商corpid
        /// </summary>
        public string ServiceCorpId { get; set; }
        /// <summary>
        /// 创建企业对应的注册码
        /// </summary>
        public string RegisterCode { get; set; }
        /// <summary>
        /// 注册成功的企业corpid
        /// </summary>
        public string AuthCorpId { get; set; }
        /// <summary>
        /// 授权管理员的信息
        /// </summary>
        public ContactSyncToken ContactSync { get; set; }
        /// <summary>
        /// 授权管理员的userid
        /// </summary>
        public AuthUserInfoModel AuthUserInfo { get; set; }
        /// <summary>
        /// 用户自定义的状态值，参数值由接口 获取注册码 指定。若未指定，则无该字段
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 推广包ID
        /// </summary>
        public string TemplateId { get; set; }
    }
}
