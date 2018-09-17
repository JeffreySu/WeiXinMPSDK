using Senparc.NeuChar.Entities;
using Senparc.NeuChar.MessageHandlers;
using Senparc.Weixin.WxOpen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.MessageHandlers
{
   public class WxOpenMessageEntityEnlightener
    {
        public static MessageEntityEnlightener Instance = new MessageEntityEnlightener(NeuChar.PlatformType.WeChat_MiniProgram)
        {
            NewRequestMessageText = () => new RequestMessageText(),
            NewRequestMessageImage = () => new RequestMessageImage(),

            //NewResponseMessageText = () => new SuccessResponseMessage();
            //NewResponseMessageImage = () => new SuccessResponseMessage();
    };
    }
}
