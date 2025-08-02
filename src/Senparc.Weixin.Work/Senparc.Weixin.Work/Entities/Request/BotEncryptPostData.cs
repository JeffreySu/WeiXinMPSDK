/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：BotEncryptPostData.cs
    文件功能描述：企业微信智能机器人原始加密信息
    
    
    创建标识：Wang Qian - 20250802
----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities
{
    public class BotEncryptPostData
    {
        /// <summary>
        /// 消息结构体加密后的字符串，对应encrypt
        /// </summary>
        public string Encrypt { get; set; }
    }
}