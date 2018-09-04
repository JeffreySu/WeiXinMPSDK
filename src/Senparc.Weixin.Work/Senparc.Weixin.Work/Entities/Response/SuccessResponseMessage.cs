/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：SuccessResponseMessage.cs
    文件功能描述：只返回"success"等指定字符串的响应信息
    
    
    创建标识：Senparc - 20170106

    修改标识：Senparc - 20180901
    修改描述：支持 NeuChar
----------------------------------------------------------------*/


using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 只返回"success"成功字符串的响应信息
    /// </summary>
    public class SuccessResponseMessage : SuccessResponseMessageBase, Senparc.Weixin.Work.Entities.IWorkResponseMessageBase
    {
        public ResponseMsgType MsgType
        {
            get { return ResponseMsgType.SuccessResponse; }
        }
    }
}
