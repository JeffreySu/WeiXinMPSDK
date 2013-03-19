using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public class ResponseMessageMusic : ResponseMessageBase, IResponseMessageBase
    {
        public Music Music { get; set; }

        public ResponseMessageMusic()
        {
            Music = new Music();
        }
    }
}
