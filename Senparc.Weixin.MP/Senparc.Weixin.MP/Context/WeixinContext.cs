using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Context;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.Context
{
    public class WeixinContext : Weixin.Context.WeixinContext<MessageContext<IRequestMessageBase, IResponseMessageBase>, IRequestMessageBase, IResponseMessageBase>
    {

    }
}
