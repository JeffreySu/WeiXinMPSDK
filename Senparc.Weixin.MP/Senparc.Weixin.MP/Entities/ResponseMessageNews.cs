using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public class ResponseMessageNews : ResponseMessageBase, IResponseMessageBase
    {
        public string Content { get; set; }
        
        public int ArticleCount
        {
            get { return (Articles ?? new List<Article>()).Count; }
        }

        public List<Article> Articles { get; set; }

        public ResponseMessageNews()
        {
            Articles = new List<Article>();
        }
    }
}
