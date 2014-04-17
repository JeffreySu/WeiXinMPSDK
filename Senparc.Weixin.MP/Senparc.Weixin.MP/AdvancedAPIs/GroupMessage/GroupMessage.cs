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
        /// 上传图文消息素材
        /// </summary>
        /// <param name="accessToken">Token</param>
        /// <param name="news">图文消息组</param>
        /// <returns></returns>
        public static UploadMediaFileResult UploadNews(string accessToken,params NewsModel[] news)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/media/uploadnews?access_token={0}";
            object data = new
            {
                articles = news
            };
            return CommonJsonSend.Send<UploadMediaFileResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 根据分组进行群发
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="groupId">群发到的分组的group_id</param>
        /// <param name="mediaId">用于群发的消息的media_id</param>
        /// <returns></returns>
        public static SendResult SendGroupMessageByGroupId(string accessToken, string groupId,string mediaId)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}";
            object data = new
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
        public static SendResult SendGroupMessageByOpenId(string accessToken, string mediaId,params string[] openIds)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}";

            object data = new
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
            const string urlFormat = "https://api.weixin.qq.com//cgi-bin/message/mass/delete?access_token={0}";

            object data = new
            {
                msgid = mediaId
            };
            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data);
        }
    }
}
