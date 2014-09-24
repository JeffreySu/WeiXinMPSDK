using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.Context
{
    public class MessageContainer<T> :Weixin.Context.MessageContainer<T> 
        //where T:IMessageBase
    {
        public MessageContainer(int maxRecordCount) : base(maxRecordCount)
        {
        }
    }
}
