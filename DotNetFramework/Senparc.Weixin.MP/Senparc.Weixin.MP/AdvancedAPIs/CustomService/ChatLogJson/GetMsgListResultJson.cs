using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.CustomService
{
    public class GetMsgListResultJson : WxJsonResult
    {
        public List<GetMsgList> recordList { get; set; }
        public int number { get; set; }
        public long msgid { get; set; }
    }

    public class GetMsgList
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 操作码，2002（客服发送信息），2003（客服接收消息）
        /// </summary>
        public int opercode { get; set; }
        /// <summary>
        /// 聊天记录
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 操作时间，unix时间戳
        /// </summary>
        public long time { get; set; }
        /// <summary>
        /// 完整客服帐号，格式为：帐号前缀@公众号微信号
        /// </summary>
        public string worker { get; set; }

        /// <summary>
        /// 客服发送信息
        /// </summary>
        public const int OperCodeResponse = 2002;
        /// <summary>
        /// 客服接收消息
        /// </summary>
        public const int OperCodeRequest = 2003;
    }
}
