/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：GetJsonResult.cs
    文件功能描述：微信开放平台帐号管理接口
    https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&id=open1498704804_iARAL&token=&lang=zh_CN

    创建标识：Senparc - 20170707
    
----------------------------------------------------------------*/
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Open.MpAPIs.Open
{
    /// <summary>
    /// 获取公众号/小程序所绑定的开放平台帐号
    /// </summary>
    public class GetJsonResult : WxJsonResult
    {
        /// <summary>
        /// 公众号或小程序所绑定的开放平台帐号的appid
        /// </summary>
        public string open_appid { get; set; }
    }
}