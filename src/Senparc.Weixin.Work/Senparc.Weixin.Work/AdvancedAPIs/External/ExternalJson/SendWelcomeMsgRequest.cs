using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    /// <summary>
    /// “发送新客户欢迎语”接口请求信息
    /// </summary>
    public class SendWelcomeMsgRequest
    {
        /// <summary>
        /// 通过<see hef="https://developer.work.weixin.qq.com/document/path/92137#15260/%E6%B7%BB%E5%8A%A0%E5%A4%96%E9%83%A8%E8%81%94%E7%B3%BB%E4%BA%BA%E4%BA%8B%E4%BB%B6">添加外部联系人事件</see>推送给企业的发送欢迎语的凭证，有效期为20秒
        /// </summary>
        public string welcome_code { get; set; }
        /// <summary>
        /// 消息文本
        /// </summary>
        public Text text { get; set; }
        public Attachment[] attachments { get; set; }


        public class Text
        {
            /// <summary>
            /// 消息文本内容,最长为4000字节
            /// </summary>
            public string content { get; set; }
        }

        public class Attachment
        {
            /// <summary>
            /// 附件类型，可选image、link、miniprogram或者video
            /// </summary>
            public string msgtype { get; set; }
            public Image image { get; set; }
            public Link link { get; set; }
            public Miniprogram miniprogram { get; set; }
            public Video video { get; set; }
            public File file { get; set; }
        }

        public class Image
        {
            /// <summary>
            /// 图片的media_id，可以通过<see href="https://developer.work.weixin.qq.com/document/path/90253">素材管理接口</see>获得
            /// </summary>
            public string media_id { get; set; }
            /// <summary>
            /// 图片的链接，仅可使用<see href="https://developer.work.weixin.qq.com/document/path/92135#13219">上传图片</see>接口得到的链接
            /// </summary>
            public string pic_url { get; set; }
        }

        public class Link
        {
            /// <summary>
            /// 图文消息标题，最长128个字节
            /// </summary>
            public string title { get; set; }
            /// <summary>
            /// 图文消息封面的url，最长2048个字节
            /// </summary>
            public string picurl { get; set; }
            /// <summary>
            /// 图文消息的描述，最多512个字节
            /// </summary>
            public string desc { get; set; }
            /// <summary>
            /// 图文消息的链接，最长2048个字节
            /// </summary>
            public string url { get; set; }
        }

        public class Miniprogram
        {
            /// <summary>
            /// 小程序消息标题，最多64个字节
            /// </summary>
            public string title { get; set; }
            /// <summary>
            /// 小程序消息封面的mediaid，封面图建议尺寸为520*416
            /// </summary>
            public string pic_media_id { get; set; }
            /// <summary>
            /// 小程序appid（可以在微信公众平台上查询），必须是关联到企业的小程序应用
            /// </summary>
            public string appid { get; set; }
            /// <summary>
            /// 小程序page路径
            /// </summary>
            public string page { get; set; }
        }

        public class Video
        {
            /// <summary>
            /// 视频的media_id，可以通过<see href="https://developer.work.weixin.qq.com/document/path/90253">素材管理接口</see>接口获得
            /// </summary>
            public string media_id { get; set; }
        }

        public class File
        {
            /// <summary>
            /// 文件的media_id，可以通过<see href="https://developer.work.weixin.qq.com/document/path/90253">素材管理接口</see>接口获得
            /// </summary>
            public string media_id { get; set; }
        }

    }

}
