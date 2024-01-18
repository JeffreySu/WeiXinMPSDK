/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：RequestMessageFastRegisterBetaAppApp.cs
    文件功能描述：创建试用小程序事件推送
    
    
    创建标识：mc7246 - 20220329

----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using System;

namespace Senparc.Weixin.Open
{
    /// <summary>
    /// 创建试用小程序事件推送
    /// </summary>
    public class RequestMessageFastRegisterBetaAppApp : RequestMessageBase
    {
        public override RequestInfoType InfoType
        {
            get { return RequestInfoType.notify_third_fastregisterbetaapp; }
        }

        /// <summary>
        /// 创建小程序appid
        /// </summary>
        public string appid { get; set; }

        public ReturnCode status { get; set; }

        public string msg { get; set; }
        
        public ThirdFastRegisterBetaAppInfo info {get;set;}
    }

    public class ThirdFastRegisterBetaAppInfo
    {
        /// <summary>
        /// 唯一标识符
        /// </summary>
        public string unique_id { get; set; }

        /// <summary>
        /// 小程序名称
        /// </summary>
        public string name { get; set; }
    }
}
