using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.Sample.CommonService.AI.MessageHandlers
{
    namespace Senparc.Weixin.Sample.CommonService.CustomMessageHandler
    {
        /// <summary>
        /// 自定义MessageHandler（公众号）
        /// </summary>
        public partial class CustomMessageHandler
        {
            private async Task AIChat(RequestMessageBase requestMessage)
            {
                if (requestMessage is RequestMessageText)
                {
                   
                }
            }
        }
    }
}