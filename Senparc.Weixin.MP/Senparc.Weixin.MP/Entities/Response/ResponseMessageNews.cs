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
            set
            {
                value = value;//这里开放set只为了逆向从Response的Xml转成实体的操作一致性，没有实际意义。
            }
        }

        public List<Article> Articles { get; set; }

        public ResponseMessageNews()
        {
            Articles = new List<Article>();
        }
    }
}
