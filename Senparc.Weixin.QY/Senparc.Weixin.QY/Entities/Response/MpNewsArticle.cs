using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.QY.Entities
{
    public class MpNewsArticle
    {
        public string ThumbMediaId { get; set; }
        public string Author { get; set; }
        public string ContentSourceUrl { get; set; }
        public string Content { get; set; }
        public string Digest { get; set; }
        public string ShowCoverPic { get; set; }
    }
}
