﻿/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：Semantic.cs
    文件功能描述：语意理解接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
 
    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间
----------------------------------------------------------------*/

/*
    API：http://mp.weixin.qq.com/wiki/0/0ce78b3c9524811fee34aba3e33f3448.html
    文档下载：http://mp.weixin.qq.com/wiki/static/assets/f48efdb46b4bca35caed4f01ca92e7da.zip
 */

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

namespace Senparc.Weixin.MP.AdvancedAPIs.Semantic
{
    /// <summary>
    /// 语意理解接口
    /// </summary>
    public static class SemanticApi
    {
        /// <summary>
        /// 发送语义理解请求
        /// </summary>
        /// <typeparam name="T">语意理解返回的结果类型，在 AdvancedAPIs/Semantic/SemanticResult </typeparam>
        /// <param name="accessToken"></param>
        /// <param name="semanticPostData">语义理解请求需要post的数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static T SemanticSend<T>(string accessToken, SemanticPostData semanticPostData, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = "https://api.weixin.qq.com/semantic/semproxy/search?access_token={0}";

            //switch (semanticPostData.category)
            //{
            //    case "restaurant":
            //        BaseSemanticResultJson as Semantic_RestaurantResult;
            //}

            return CommonJsonSend.Send<T>(accessToken, urlFormat, semanticPostData, timeOut: timeOut);
        }
    }
}