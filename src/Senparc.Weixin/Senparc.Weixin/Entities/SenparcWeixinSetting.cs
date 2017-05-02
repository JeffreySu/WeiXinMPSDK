/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：SenparcWeixinSetting.cs
    文件功能描述：Senparc.Weixin JSON 配置
    
    
    创建标识：Senparc - 20170302
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.Entities
{
    /// <summary>
    /// Senparc.Weixin JSON 配置
    /// </summary>
    public class SenparcWeixinSetting
    {
        public string Token { get; set; }
        public string EncodingAESKey { get; set; }
        public string WeixinAppId { get; set; }
        public string WeixinAppSecret { get; set; }

    }
}
