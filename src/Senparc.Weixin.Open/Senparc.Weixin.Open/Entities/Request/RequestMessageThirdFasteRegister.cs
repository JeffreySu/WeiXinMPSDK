/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageThirdFasteRegister.cs
    文件功能描述：注册审核事件推送
    
    
    创建标识：Senparc - 20190103
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

}
