using System;
using System.Collections.Generic;
using System.Linq;
/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：RefreshTokenResult.cs
    文件功能描述：通过RefreshToken接口返回的结果
    
    
    创建标识：Senparc - 20161019
    
----------------------------------------------------------------*/

using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.OAuth
{
    /// <summary>
    /// 通过RefreshToken接口返回的结果
    /// </summary>
    public class RefreshTokenResult : OAuthAccessTokenResult
    {
        //public string access_token { get; set; }
        //public int expires_in { get; set; }
        //public  string refresh_token { get; set; }

        //public string  openId { get; set; }
    }
}
