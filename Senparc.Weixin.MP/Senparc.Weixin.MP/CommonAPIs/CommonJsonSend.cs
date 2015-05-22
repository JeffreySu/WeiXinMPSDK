/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：CommonJsonSend.cs
    文件功能描述：向需要AccessToken的API发送消息的公共方法
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
 
    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.MP.CommonAPIs
{
    public enum CommonJsonSendType
    {
        GET,
        POST
    }

    public static class CommonJsonSend
    {
        /// <summary>
        /// 向需要AccessToken的API发送消息的公共方法
        /// </summary>
        /// <param name="accessToken">这里的AccessToken是通用接口的AccessToken，非OAuth的。如果不需要，可以为null，此时urlFormat不要提供{0}参数</param>
        /// <param name="urlFormat"></param>
        /// <param name="data">如果是Get方式，可以为null</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult Send(string accessToken, string urlFormat, object data, CommonJsonSendType sendType = CommonJsonSendType.POST, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessToken, urlFormat, data, sendType, timeOut);
        }

        /// <summary>
        /// 向需要AccessToken的API发送消息的公共方法
        /// </summary>
        /// <param name="accessToken">这里的AccessToken是通用接口的AccessToken，非OAuth的。如果不需要，可以为null，此时urlFormat不要提供{0}参数</param>
        /// <param name="urlFormat"></param>
        /// <param name="data">如果是Get方式，可以为null</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static T Send<T>(string accessToken, string urlFormat, object data, CommonJsonSendType sendType = CommonJsonSendType.POST, int timeOut = Config.TIME_OUT)
        {
            var url = string.IsNullOrEmpty(accessToken) ? urlFormat : string.Format(urlFormat, accessToken);
            switch (sendType)
            {
                case CommonJsonSendType.GET:
                    return Get.GetJson<T>(url);
                case CommonJsonSendType.POST:
                    SerializerHelper serializerHelper = new SerializerHelper();
                    var jsonString = serializerHelper.GetJsonString(data);
                    //jsonString = "{ \"card\": {   \"card_type\": \"GROUPON\",   \"groupon\": {       \"base_info\": {           \"logo_url\": \"http://mmbiz.qpic.cn/mmbiz/iaL1LJM1mF9aRKPZJkmG8xXhiaHqkKSVMMWeN3hLut7X7hicFNjakmxibMLGWpXrEXB33367o7zHN0CwngnQY7zb7g/0\",           \"brand_name\":\"海底捞\",           \"code_type\":\"CODE_TYPE_TEXT\",           \"title\": \"132元双人火锅套餐\",           \"sub_title\": \"周末狂欢必备\",           \"color\": \"Color010\",           \"notice\": \"使用时向服务员出示此券\",           \"service_phone\": \"020-88888888\",           \"description\": \"不可与其他优惠同享\n如需团购券发票，请在消费时向商户提出\n店内均可使用，仅限堂食\",           \"date_info\": {               \"type\": 1,               \"begin_timestamp\": 1431648000 ,               \"end_timestamp\": 1431734400           },           \"sku\": {               \"quantity\": 50000000           },           \"get_limit\": 3,           \"use_custom_code\": false,           \"bind_openid\": false,           \"can_share\": true,         \"can_give_friend\": true,           \"location_id_list\" : [123, 12321, 345345],           \"custom_url_name\": \"立即使用\",           \"custom_url\": \"http://www.qq.com\",           \"custom_url_sub_title\": \"6个汉字tips\",           \"promotion_url_name\": \"更多优惠\",         \"promotion_url\": \"http://www.qq.com\",           \"source\": \"大众点评\"          },       \"deal_detail\": \"以下锅底2选1（有菌王锅、麻辣锅、大骨锅、番茄锅、清补凉锅、酸菜鱼锅可选）：\n大锅1份 12元\n小锅2份 16元 \"} }}";
                    
                    using (MemoryStream ms = new MemoryStream())
                    {
                        var bytes = Encoding.UTF8.GetBytes(jsonString);
                        ms.Write(bytes, 0, bytes.Length);
                        ms.Seek(0, SeekOrigin.Begin);

                        return Post.PostGetJson<T>(url, null, ms, timeOut: timeOut);
                    }
                default:
                    throw new ArgumentOutOfRangeException("sendType");
            }
        }
    }
}