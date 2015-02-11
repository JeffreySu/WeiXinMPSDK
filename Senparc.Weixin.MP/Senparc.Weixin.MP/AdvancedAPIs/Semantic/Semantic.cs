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
        /// <param name="accessToken"></param>
        /// <param name="semanticPostData">语义理解请求需要post的数据</param>
        /// <returns></returns>
        public static BaseSemanticResultJson SemanticSend(string accessToken, SemanticPostData semanticPostData)
        {
            var urlFormat = "https://api.weixin.qq.com/semantic/semproxy/search?access_token={0}";

            //switch (semanticPostData.category)
            //{
            //    case "restaurant":
            //        BaseSemanticResultJson as
            //}

            return CommonJsonSend.Send<Semantic_RestaurantResult>(accessToken, urlFormat, semanticPostData);
        }
    }
}
