using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 高级群发接口
    /// </summary>
    public static class GroupMessage
    {
        /// <summary>
        /// 根据分组进行群发
        /// 
        /// 请注意：
        /// 1、该接口暂时仅提供给已微信认证的服务号
        /// 2、虽然开发者使用高级群发接口的每日调用限制为100次，但是用户每月只能接收4条，请小心测试
        /// 3、无论在公众平台网站上，还是使用接口群发，用户每月只能接收4条群发消息，多于4条的群发将对该用户发送失败。
        /// 
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="groupId">群发到的分组的group_id</param>
        /// <param name="mediaId">用于群发的消息的media_id</param>
        /// <returns></returns>
        public static SendResult SendGroupMessageByGroupId(string accessToken, string groupId, string mediaId)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}";
            var data = new
            {
                filter = new
                {
                    group_id = groupId
                },
                mpnews = new
                {
                    media_id = mediaId
                },
                msgtype = "mpnews"
            };
            return CommonJsonSend.Send<SendResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 根据OpenId进行群发
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="mediaId">用于群发的消息的media_id</param>
        /// <param name="openIds">openId字符串数组</param>
        /// <returns></returns>
        public static SendResult SendGroupMessageByOpenId(string accessToken, string mediaId, params string[] openIds)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}";

            var data = new
            {
                touser = openIds,
                mpnews = new
                {
                    media_id = mediaId
                },
                msgtype = "mpnews"
            };
            return CommonJsonSend.Send<SendResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 删除群发消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="mediaId">发送出去的消息ID</param>
        /// <returns></returns>
        public static WxJsonResult DeleteSendMessage(string accessToken, string mediaId)
        {
            //官方API地址为https://api.weixin.qq.com//cgi-bin/message/mass/delete?access_token={0}，应该是多了一个/
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/delete?access_token={0}";

            var data = new
            {
                msgid = mediaId
            };
            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data);
        }
    }
}