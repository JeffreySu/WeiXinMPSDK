/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc

    文件名：OpenApi.cs
    文件功能描述：微信开放平台帐号管理接口
    https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&id=open1498704804_iARAL&token=&lang=zh_CN

    创建标识：Senparc - 20170629
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.Open.MpAPIs.Open
{
    public static class OpenApi
    {
        #region 同步接口

        /// <summary>
        /// 创建开放平台帐号并绑定公众号/小程序。
        /// 该API用于创建一个开放平台帐号，并将一个尚未绑定开放平台帐号的公众号/小程序绑定至该开放平台帐号上。新创建的开放平台帐号的主体信息将设置为与之绑定的公众号或小程序的主体。
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public static CreateJsonResult Create(string accessToken, string appId)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/open/create?access_token={0}";
            var data = new { appid = appId };
            return CommonJsonSend.Send<CreateJsonResult>(accessToken, urlFormat, data);
        }

        #endregion

        #region 异步接口



        #endregion
    }
}
