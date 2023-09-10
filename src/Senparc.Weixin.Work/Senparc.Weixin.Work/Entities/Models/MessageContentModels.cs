namespace Senparc.Weixin.Work.Entities.Models
{
    /// <summary>
    /// 文本消息
    /// </summary>
    public class MessageText
    {
        /// <summary>
        /// 文本消息内容,最长为4000字节
        /// </summary>
        public string content { get; set; }
    }

    /// <summary>
    /// 图片消息
    /// </summary>
    public class MessageImage
    {
        /// <summary>
        /// 图片的media_id，可以通过素材管理接口获得
        /// </summary>
        public string media_id { get; set; }
        /// <summary>
        /// 图片的链接，仅可使用上传图片接口得到的链接
        /// </summary>
        public string pic_url { get; set; }
    }

    /// <summary>
    /// 图文消息
    /// </summary>
    public class MessageLink
    {
        /// <summary>
        /// 图文消息标题，最长为128字节
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 图文消息封面的url
        /// </summary>
        public string picurl { get; set; }
        /// <summary>
        /// 图文消息的描述，最长为512字节
        /// </summary>
        public string desc { get; set; }
        /// <summary>
        /// 图文消息的链接
        /// </summary>
        public string url { get; set; }
    }

    /// <summary>
    /// 小程序消息
    /// </summary>
    public class MessageMiniprogram
    {
        /// <summary>
        /// 小程序消息标题，最长为64字节
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 小程序消息封面的mediaid，封面图建议尺寸为520*416
        /// </summary>
        public string pic_media_id { get; set; }
        /// <summary>
        /// 小程序appid，必须是关联到企业的小程序应用
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 小程序page路径
        /// </summary>
        public string page { get; set; }
    }
}
