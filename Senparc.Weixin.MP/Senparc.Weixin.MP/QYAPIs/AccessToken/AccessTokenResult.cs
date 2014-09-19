using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.QYPIs
{
    /// <summary>
    /// 获取AccessToken的结果
    /// 如果错误，返回结果{"errcode": 43003,"errmsg":"require https"}
    /// </summary>
    public class AccessTokenResult : WxJsonResult
    {
        public string access_token { get; set; }
    }
}
