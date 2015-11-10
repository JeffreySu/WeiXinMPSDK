/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：MessageBase.cs
    文件功能描述：所有Request和Response消息的基类
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.Entities
{
    public interface IMessageBase : IEntityBase
    {
        string ToUserName { get; set; }
        string FromUserName { get; set; }
        DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 所有Request和Response消息的基类
    /// </summary>
    public class MessageBase : /*EntityBase, */IMessageBase
    {
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public DateTime CreateTime { get; set; }

        public override string ToString()
        {
            //TODO:直接输出XML


            return base.ToString();
        }
    }
}
