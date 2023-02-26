namespace Senparc.Weixin.Work.Entities.JsonResult
{
    public class ExternalProfile
    {
        public string external_corp_name { get; set; }
        public ExternalAttribute external_attr { get; set; }
    }
    
    public class ExternalAttribute
    {
        /// <summary>
        /// 属性类型: 0-文本 1-网页 2-小程序
        /// </summary>
        public ExternalAttributeType type { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 文本类型的属性
        /// </summary>
        public ExternalAttrText text { get; set; }
        /// <summary>
        /// 网页类型的属性，url和title字段要么同时为空表示清除该属性，要么同时不为空
        /// </summary>
        public ExternalAttrWeb web { get; set; }
        /// <summary>
        /// 小程序类型的属性，appid和title字段要么同时为空表示清除改属性，要么同时不为空
        /// </summary>
        public ExternalAttrMiniprogram miniprogram { get; set; }
    }

    /// <summary>
    /// 文本类型的属性
    /// </summary>
    public class ExternalAttrText
    {
        /// <summary>
        /// 文本属性内容,长度限制12个UTF8字符
        /// </summary>
        public string value { get; set; }
    }

    /// <summary>
    /// 网页类型的属性
    /// </summary>
    public class ExternalAttrWeb
    {
        /// <summary>
        /// 网页的url,必须包含http或者https头
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 网页的展示标题,长度限制12个UTF8字符
        /// </summary>
        public string title { get; set; }
    }

    /// <summary>
    /// 小程序类型的属性
    /// </summary>
    public class ExternalAttrMiniprogram
    {
        /// <summary>
        /// 小程序appid，必须是有在本企业安装授权的小程序，否则会被忽略 	
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 小程序的页面路径
        /// </summary>
        public string pagepath { get; set; }
        /// <summary>
        /// 小程序的展示标题,长度限制12个UTF8字符
        /// </summary>
        public string title { get; set; }
    }
}