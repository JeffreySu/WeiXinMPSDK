/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：MpNewsArticle.cs
    文件功能描述：响应回复消息 MpNewsArticle
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.QY.Entities
{
    public class MpNewsArticle
    {
        public string Title { get; set; }
        public string ThumbMediaId { get; set; }
        public string Author { get; set; }
        public string ContentSourceUrl { get; set; }
        public string Content { get; set; }
        public string Digest { get; set; }
        public string ShowCoverPic { get; set; }
    }
}
