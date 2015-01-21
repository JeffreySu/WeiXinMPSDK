using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 语意理解接口
    /// </summary>
    public static class Semantic
    {
        /// <summary>
        /// 发送语义理解请求
        /// </summary>
        /// <returns></returns>
        public static SearchResultJson SemanticUnderStand(string accessToken, string query, string category, string city, string appid)
        {
            var urlFormat = "https://api.weixin.qq.com/semantic/semproxy/search?access_token={0}";
            var data = new
            {
                query = query,
                category = category,
                city = city,
                appid = appid
            };
            return CommonJsonSend.Send<SearchResultJson>(accessToken, urlFormat, data);
        }
    }
}
