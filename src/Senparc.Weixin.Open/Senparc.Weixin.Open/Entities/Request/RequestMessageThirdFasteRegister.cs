/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageThirdFasteRegister.cs
    文件功能描述：注册审核事件推送
    
    
    创建标识：Senparc - 20190103


    修改标识：Senparc - 20190530
    修改描述：v4.5.4.1 GetAuditStatusResultJson 改名为 GetAuditResultJson，保持全局命名唯一性

    修改标识：Senparc - 20190530
    修改描述：v4.5.4.1 GetAuditStatusResultJson 改名为 GetAuditResultJson，保持全局命名唯一性
----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using System;

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

        public ReturnCode status { get; set; }
        /// <summary>
        /// 第三方授权码
        /// </summary>
        public string auth_code { get; set; }

        public string msg { get; set; }
        
        /// <summary>
        /// 注册时提交的资料
        /// </summary>
        public ThirdFasteRegisterInfo info {get;set;}
    }

    ///// <summary>
    ///// 注册时提交的资料信息
    ///// </summary>
    //public class info
    //{
    //    /// <summary>
    //    /// 企业名称
    //    /// </summary>
    //    public string name { get; set; }

    //    /// <summary>
    //    /// 企业代码
    //    /// </summary>
    //    public string code { get; set; }
    //    /// <summary>
    //    /// 企业代码类型 
    //    /// </summary>
    //    public CodeType code_type { get; set; }
    //    /// <summary>
    //    /// 法人微信号
    //    /// </summary>
    //    public string legal_persona_wechat { get; set; }
    //    /// <summary>
    //    /// 法人姓名
    //    /// </summary>
    //    public string legal_persona_name { get; set; }
    //    /// <summary>
    //    /// 第三方联系电话
    //    /// </summary>
    //    public string component_phone { get; set; }
    //}
}
