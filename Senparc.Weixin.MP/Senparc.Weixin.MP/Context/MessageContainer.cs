using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Senparc.Weixin.MP.Context
{
    public class MessageContainer<T> :Weixin.Context.MessageContainer<T>
    {
        public MessageContainer(int maxRecordCount) : base(maxRecordCount)
        {
        }
    }
}
