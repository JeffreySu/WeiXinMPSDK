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
    public class GetDomainConfirmFileResult : WxJsonResult
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string file_name { get; set; }
        /// <summary>
        /// 文件内容
        /// </summary>
        public string file_content { get; set; }
    }
}
