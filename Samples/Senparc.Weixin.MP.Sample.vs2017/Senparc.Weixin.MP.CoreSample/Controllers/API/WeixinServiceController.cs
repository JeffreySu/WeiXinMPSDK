using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Routing;

namespace Senparc.Weixin.MP.CoreSample.Controllers
{
    using Microsoft.AspNetCore.Routing;
    using Senparc.Weixin.MP;
    using Senparc.Weixin.MP.Entities;

    //TODO:由于微信对接受数据格式要求很严格，这里暂时放弃ApiController，使用普通Controller，见WeixinController.cs

    //public class WeixinServiceController : ApiController
    //{
    //    // GET api/weixinservice
    //    public object Get(/*string signature, string timestamp, string nonce, string echostr*/)
    //    {
    //        var routeDataTokens = base.ControllerContext.RouteData.Values;
    //        var signature = routeDataTokens["signature"];
    //        //string timestamp, string nonce, string echostr
    //        return signature;
    //        //if (CheckSignature.Check(signature, timestamp, nonce))
    //        //{
    //        //    return new StringContent(echostr);
    //        //    return echostr;
    //        //}
    //        //else
    //        //{
    //        //    return new StringContent(echostr);
    //        //}
    //    }

    //    // POST api/weixinservice
    //    public string Post([FromBody]string value, string signature, string timestamp, string nonce, string echostr)
    //    {
    //        return "OK";//XML
    //    }
    //}
}
