using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.QY.Entities.Request
{
    /// <summary>
    /// 微信企业号服务器Post过来的参数集合（不包括PostData）
    /// </summary>
    public class PostModel
    {
        public string Msg_Signature { get; set; }
        public string Timestamp { get; set; }
        public string Nonce { get; set; }

        //以下信息不会出现在微信发过来的信息中，都是企业号后台需要设置（获取的）的信息，用于扩展传参使用
        public string Token { get; set; }
        public string EncodingAESKey { get; set; }
        public string CorpId { get; set; }
    }
}
