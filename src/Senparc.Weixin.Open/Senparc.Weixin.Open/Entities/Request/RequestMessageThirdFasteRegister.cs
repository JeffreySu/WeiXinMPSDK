/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：RequestMessageThirdFasteRegister.cs
    文件功能描述：注册审核事件推送
    
    
    创建标识：Senparc - 20190103


    修改标识：Senparc - 20190530
    修改描述：v4.5.4.1 GetAuditStatusResultJson 改名为 GetAuditResultJson，保持全局命名唯一性

    修改标识：Senparc - 20190530
    修改描述：v4.5.4.1 GetAuditStatusResultJson 改名为 GetAuditResultJson，保持全局命名唯一性

    修改标识：Senparc - 20231026
    修改描述：v4.16.0 1、处理三方推送消息枚举
		              2、添加“小程序发货信息管理服务”

----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.Open
{
    /// <summary>
    /// 注册审核事件推送
    /// </summary>
    public class RequestMessageThirdFasteRegister : RequestMessageBase
    {
        public override RequestInfoType InfoType
        {
            get { return RequestInfoType.notify_third_fasteregister; }
        }

        /// <summary>
        /// 创建小程序appid
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ReturnCode status { get; set; }

        /// <summary>
        /// 第三方授权码
        /// </summary>
        public string auth_code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }
        
        /// <summary>
        /// 注册时提交的资料
        /// </summary>
        public ThirdFasteRegisterInfo info {get;set;}
    }
}
