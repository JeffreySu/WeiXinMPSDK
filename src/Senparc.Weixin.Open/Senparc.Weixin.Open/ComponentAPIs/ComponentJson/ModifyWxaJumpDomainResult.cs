/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc

    文件名：ModifyWxaServerDomainResult.cs
    文件功能描述：设置第三方平台服务器域名 返回结果


    创建标识：Senparc - 20230109

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.ComponentAPIs
{
    /// <summary>
    /// 设置第三方平台业务域名 返回结果
    /// </summary>
    public class ModifyWxaJumpDomainResult : WxJsonResult
    {
        /// <summary>
        /// 目前生效的 “全网发布版”第三方平台“小程序业务域名”。如果修改失败，该字段不会返回。如果没有已发布的第三方平台，该字段也不会返回。
        /// </summary>
        public string published_wxa_jump_h5_domain { get; set; }
        /// <summary>
        /// 目前生效的 “测试版”第三方平台“小程序业务域名”。如果修改失败，该字段不会返回
        /// </summary>
        public string testing_wxa_jump_h5_domain { get; set; }
        /// <summary>
        /// 未通过验证的域名。如果不存在未通过验证的域名，该字段不会返回。
        /// </summary>
        public string invalid_wxa_jump_h5_domain { get; set; }
    }
}
