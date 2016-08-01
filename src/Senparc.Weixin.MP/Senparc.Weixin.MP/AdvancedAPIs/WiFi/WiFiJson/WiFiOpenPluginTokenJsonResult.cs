/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：WiFiOpenPluginTokenJsonResult.cs
    文件功能描述：第三方平台获取开插件wifi_token的返回结果
    
    创建标识：Senparc - 20160520
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.WiFi
{
    /// <summary>
    /// 第三方平台获取开插件wifi_token的返回结果
    /// </summary>
    public class WiFiOpenPluginTokenJsonResult : WxJsonResult 
    {
        public OpenPluginToken_Data data { get; set; }

        
    }
    public class OpenPluginToken_Data
    {
        /// <summary>
        /// 该公众号是否已开通微信连Wi-Fi插件，true-已开通，false-未开通
        /// </summary>
        public string is_open { get; set; }
        /// <summary>
        /// 开通插件的凭证，当is_open为false时才返回值
        /// </summary>
        public string wifi_token { get; set; }
    }
}
