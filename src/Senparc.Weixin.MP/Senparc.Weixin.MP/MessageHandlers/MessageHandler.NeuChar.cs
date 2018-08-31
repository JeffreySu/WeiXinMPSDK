using Senparc.Weixin.MP.NeuChar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.MessageHandlers
{
    public abstract partial class MessageHandler<TC>
    {
        static MessageHandler()
        {
            Senparc.NeuChar.Register.RegisterNeuralNode("MessageHandlerNode", typeof(MessageHandlerNode));
        }
    }
}
