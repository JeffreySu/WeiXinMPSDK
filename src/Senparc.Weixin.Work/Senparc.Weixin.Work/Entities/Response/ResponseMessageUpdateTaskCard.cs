using Senparc.NeuChar;
using Senparc.NeuChar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.Entities.Response
{
    public class ResponseMessageUpdateTaskCard : WorkResponseMessageBase, IResponseMessageUpdateTaskCard
    {
        public new virtual ResponseMsgType MsgType
        {
            get { return ResponseMsgType.TaskCard; }
        }

        public Image Image { get; set; }

        public ResponseMessageUpdateTaskCard()
        {
            Image = new Image();
        }
    }
}
