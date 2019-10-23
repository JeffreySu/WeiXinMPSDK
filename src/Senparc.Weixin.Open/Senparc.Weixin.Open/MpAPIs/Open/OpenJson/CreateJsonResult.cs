/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：CreateJsonResult.cs
    文件功能描述：微信开放平台帐号管理接口
    https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&id=open1498704804_iARAL&token=&lang=zh_CN

    创建标识：Senparc - 20170707
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Open.MpAPIs.Open
{
    /// <summary>
    /// 创建开放平台帐号并绑定公众号/小程序接口返回结果
    /// </summary>
    [Serializable]
    public class CreateJsonResult : WxJsonResult
    {
        /// <summary>
        /// 所创建的开放平台帐号的appid
        /// </summary>
        public string open_appid { get; set; }
    }
}
