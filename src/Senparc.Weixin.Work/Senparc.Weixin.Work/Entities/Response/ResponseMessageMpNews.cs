/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ResponseMessageMpNews.cs
    文件功能描述：响应回复MpNews消息
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

using Senparc.NeuChar;
using Senparc.NeuChar.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.Entities
{
    public class ResponseMessageMpNews : WorkResponseMessageBase, IResponseMessageMpNews
    {
        public new virtual ResponseMsgType MsgType
        {
            get { return ResponseMsgType.MpNews; }
        }

        public int MpNewsArticleCount
        {
            get
            {
                return MpNewsArticles == null ? 0 : MpNewsArticles.Count;
            }
            set
            {
                //这里开放set只为了逆向从Response的Xml转成实体的操作一致性，没有实际意义。
            }
        }

        /// <summary>
        /// 文章列表，微信客户端只能输出前10条（可能未来数字会有变化，出于视觉效果考虑，建议控制在8条以内）
        /// </summary>
        public List<MpNewsArticle> MpNewsArticles { get; set; }

        public ResponseMessageMpNews()
        {
            MpNewsArticles = new List<MpNewsArticle>();
        }
    }
}
