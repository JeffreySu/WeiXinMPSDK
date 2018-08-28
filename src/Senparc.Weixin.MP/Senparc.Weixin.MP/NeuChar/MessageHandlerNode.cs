using Senparc.NeuChar;
using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.NeuChar
{
    /// <summary>
    /// MessageHandler 的神经节点
    /// </summary>
    public class MessageHandlerNode : BaseNeuralNode
    {
        public override string Version { get; set; }

        /// <summary>
        /// 获取响应消息
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <param name="defaultProcess"></param>
        /// <returns></returns>
        public IResponseMessageBase GetResponseMessage(IRequestMessageBase requestMessage,Func<IResponseMessageBase> defaultProcess)
        {
            IResponseMessageBase responseMessage = null;

            //TODO：添加逻辑

            if (responseMessage!=null)
            {
                responseMessage = defaultProcess();
            }

            return responseMessage;
        }

        //TODO：这是模拟的数据
    }
}
