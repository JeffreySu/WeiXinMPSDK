using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities.Request;

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageVideo : RequestMessageBase, IRequestMessageBase
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.Video; }
        }


        /* 
         * 官方文档和实际发送的不一致，
         * 实际发送没有Video这个对象，直接是MediaId和ThumbMediaId，
         * 但是文档中这两项是包含在Video节点下面的，
         * 这里为了防止官方调整，专门添加了Video这个对象，以防不测。
         */
        public Video Video { get; set; }

        public string MediaId
        {
            get
            {
                return Video.MediaId;
            }
            set
            {
                Video.MediaId = value;
            }
        }
        public string ThumbMediaId
        {
            get
            {
                return Video.ThumbMediaId;
            }
            set
            {
                Video.ThumbMediaId = value;
            }
        }

        public RequestMessageVideo()
        {
            Video = new Video();
        }
    }
}
