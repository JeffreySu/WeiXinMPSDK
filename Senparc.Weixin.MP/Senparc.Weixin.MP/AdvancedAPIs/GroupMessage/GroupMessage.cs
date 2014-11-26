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
        /// <param name="type"></param>
        /// <returns></returns>
        public static SendResult SendGroupMessageByGroupId(string accessToken, string groupId, string mediaId, GroupMessageType type)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}";
            
            BaseGroupMessageDataByGroupId baseData = null;
            switch (type)
            {
                case GroupMessageType.image:
                    baseData = new GroupMessageByGroupId_ImageData()
                        {
                            filter = new GroupMessageByGroupId_GroupId()
                                {
                                  group_id  = groupId
                                },
                            image = new GroupMessageByGroupId_MediaId()
                                 {
                                     media_id = mediaId
                                 },
                             msgtype = "image"
                        };
                    break;
                case GroupMessageType.voice:
                    baseData = new GroupMessageByGroupId_VoiceData()
                    {
                        filter = new GroupMessageByGroupId_GroupId()
                        {
                            group_id = groupId
                        },
                        voice = new GroupMessageByGroupId_MediaId()
                        {
                            media_id = mediaId
                        },
                        msgtype = "voice"
                    };
                    break;
                case GroupMessageType.mpnews:
                    baseData = new GroupMessageByGroupId_MpNewsData()
                    {
                        filter = new GroupMessageByGroupId_GroupId()
                        {
                            group_id = groupId
                        },
                        mpnews = new GroupMessageByGroupId_MediaId()
                        {
                            media_id = mediaId
                        },
                        msgtype = "mpnews"
                    };
                    break;
                case GroupMessageType.video:
                    baseData = new GroupMessageByGroupId_MpVideoData()
                    {
                        filter = new GroupMessageByGroupId_GroupId()
                        {
                            group_id = groupId
                        },
                        mpvideo = new GroupMessageByGroupId_MediaId()
                        {
                            media_id = mediaId
                        },
                        msgtype = "mpvideo"
                    };
                    break;
                case GroupMessageType.text:
                    throw new Exception("发送文本信息请使用SendTextGroupMessageByGroupId方法。");
                    break;
                default:
                    throw new Exception("参数错误。");
                    break;
            }

            return CommonJsonSend.Send<SendResult>(accessToken, urlFormat, baseData);
        }

        /// <summary>
        /// 根据GroupId进行群发文本信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="groupId">群发到的分组的group_id</param>
        /// <param name="content">用于群发文本消息的content</param>
        /// <returns></returns>
        public static SendResult SendTextGroupMessageByGroupId(string accessToken, string groupId, string content)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}";

            BaseGroupMessageDataByGroupId baseData = new GroupMessageByGroupId_TextData()
                    {
                        filter = new GroupMessageByGroupId_GroupId()
                        {
                            group_id = groupId
                        },
                        text = new GroupMessageByGroupId_Content()
                        {
                            content = content
                        },
                        msgtype = "text"
                    };

            return CommonJsonSend.Send<SendResult>(accessToken, urlFormat, baseData);
        }

        /// <summary>
        /// 根据OpenId进行群发
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="mediaId">用于群发的消息的media_id</param>
        /// <param name="type"></param>
        /// <param name="openIds">openId字符串数组</param>
        /// 注意mediaId和content不可同时为空
        /// <returns></returns>
        public static SendResult SendGroupMessageByOpenId(string accessToken, GroupMessageType type, string mediaId, params string[] openIds)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}";

            BaseGroupMessageDataByOpenId baseData = null;
            switch (type)
            {
                case GroupMessageType.image:
                    baseData = new GroupMessageByOpenId_ImageData()
                    {
                        filter = new GroupMessageByOpenId_GroupId()
                        {
                            touser = openIds
                        },
                        image = new GroupMessageByOpenId_MediaId()
                        {
                            media_id = mediaId
                        },
                        msgtype = "image"
                    };
                    break;
                case GroupMessageType.voice:
                    baseData = new GroupMessageByOpenId_VoiceData()
                    {
                        filter = new GroupMessageByOpenId_GroupId()
                        {
                            touser = openIds
                        },
                        voice = new GroupMessageByOpenId_MediaId()
                        {
                            media_id = mediaId
                        },
                        msgtype = "voice"
                    };
                    break;
                case GroupMessageType.mpnews:
                    baseData = new GroupMessageByOpenId_MpNewsData()
                    {
                        filter = new GroupMessageByOpenId_GroupId()
                        {
                            touser = openIds
                        },
                        mpnews = new GroupMessageByOpenId_MediaId()
                        {
                            media_id = mediaId
                        },
                        msgtype = "mpnews"
                    };
                    break;
                case GroupMessageType.video:
                    throw new Exception("发送视频信息请使用SendVideoGroupMessageByOpenId方法。");
                    break;
                case GroupMessageType.text:
                    throw new Exception("发送文本信息请使用SendTextGroupMessageByOpenId方法。");
                    break;
                default:
                    throw new Exception("参数错误。");
                    break;
            }
            return CommonJsonSend.Send<SendResult>(accessToken, urlFormat, baseData);
        }

        /// <summary>
        /// 根据OpenId进行群发文本消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="content"></param>
        /// <param name="openIds">openId字符串数组</param>
        /// 注意mediaId和content不可同时为空
        /// <returns></returns>
        public static SendResult SendTextGroupMessageByOpenId(string accessToken, string content, params string[] openIds)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}";

            BaseGroupMessageDataByOpenId baseData = new GroupMessageByOpenId_TextData()
                    {
                        filter = new GroupMessageByOpenId_GroupId()
                        {
                            touser = openIds
                        },
                        text = new GroupMessageByOpenId_Content()
                        {
                            content = content
                        },
                        msgtype = "text"
                    };
                    
            return CommonJsonSend.Send<SendResult>(accessToken, urlFormat, baseData);
        }

        /// <summary>
        /// 根据OpenId进行群发视频消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="title"></param>
        /// <param name="mediaId"></param>
        /// <param name="openIds">openId字符串数组</param>
        /// <param name="description"></param>
        /// 注意mediaId和content不可同时为空
        /// <returns></returns>
        public static SendResult SendVideoGroupMessageByOpenId(string accessToken, string title, string description, string mediaId, params string[] openIds)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}";

            BaseGroupMessageDataByOpenId baseData = new GroupMessageByOpenId_MpVideoData()
            {
                filter = new GroupMessageByOpenId_GroupId()
                {
                    touser = openIds
                },
                video = new GroupMessageByOpenId_Video()
                {
                    title = title,
                    description = description,
                    media_id = mediaId
                },
                msgtype = "video"
            };

            return CommonJsonSend.Send<SendResult>(accessToken, urlFormat, baseData);
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