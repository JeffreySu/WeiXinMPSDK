/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc
    
    文件名：RequestMessageFastVerifyBetaApp.cs
    文件功能描述：试用小程序快速认证事件推送
    
    
    创建标识：mc7246 - 20220329

    修改标识：Senparc - 20261024
    修改描述：兼容 System.Text.Json 枚举字符串并保持旧 JSON 契约

----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using System;

namespace Senparc.Weixin.Open
{
    /// <summary>
    /// 试用小程序快速认证事件推送
    /// </summary>
    public class RequestMessageFastVerifyBetaApp : RequestMessageBase
    {
        public override RequestInfoType InfoType
        {
            get { return RequestInfoType.notify_third_fastverifybetaapp; }
        }

        /// <summary>
        /// 创建小程序appid
        /// </summary>
        public string appid { get; set; }

        [System.Text.Json.Serialization.JsonConverter(typeof(Senparc.Weixin.Helpers.Serializers.JsonStringOrNumberEnumConverterFactory))]
        public ReturnCode status { get; set; }

        public string msg { get; set; }
        
        /// <summary>
        /// 注册时提交的资料
        /// </summary>
        public ThirdFasteRegisterInfo info {get;set;}
    }
}

