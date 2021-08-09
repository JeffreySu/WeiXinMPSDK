using Senparc.NeuChar;
using Senparc.NeuChar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.Entities.Response
{
    public class ResponseMessageUpdateTaskCard : WorkResponseMessageBase, IResponseMessageTaskCard
    {
        public new virtual ResponseMsgType MsgType
        {
            get { return ResponseMsgType.TaskCard; }
        }

        public TaskCard TaskCard { get; set; }

        public ResponseMessageUpdateTaskCard()
        {
            TaskCard = new TaskCard();
        }
    }
}
