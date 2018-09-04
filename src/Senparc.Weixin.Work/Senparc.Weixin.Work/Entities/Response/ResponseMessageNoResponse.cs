/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：ResponseMessageNoResponse.cs
    文件功能描述：无需响应（回复空字符串）的响应类型
    
    
    创建标识：Senparc - 20150505

    修改标识：Senparc - 20180901
    修改描述：支持 NeuChar   
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 当MessageHandler接收到IResponseNothing的返回类型参数时，只会向微信服务器返回空字符串，等同于return null
    /// </summary>
    public class ResponseMessageNoResponse : WorkResponseMessageBase, IResponseMessageNoResponse
    {
    }
}
