/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：JsCode2JsonResult.cs
    文件功能描述：JsCode2Json接口结果
    
    
    创建标识：Senparc - 20170105

----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Sns
{
    /// <summary>
    /// JsCode2Json接口结果
    /// </summary>
    public class JsCode2JsonResult:WxJsonResult
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 会话密钥
        /// </summary>
        public string session_key { get; set; }
    }
}
