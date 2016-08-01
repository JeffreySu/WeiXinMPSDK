/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：Article.cs
    文件功能描述：响应回复消息 图文类
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
    
    修改标识：Senparc - 20150708
    修改描述：增加注释
----------------------------------------------------------------*/

namespace Senparc.Weixin.QY.Entities
{
    public class Article
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 点击后跳转的链接
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80。如不填，在客户端不显示图片
        /// </summary>
        public string PicUrl { get; set; }
    }
}
