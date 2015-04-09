/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：EncryptPostModel.cs
    文件功能描述：加解密消息统一基类 接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin
{
    /// <summary>
    /// 接收解密信息统一接口
    /// </summary>
    public interface IEncryptPostModel
    {
    }

    /// <summary>
    /// 接收加密信息统一基类
    /// </summary>
    public class EncryptPostModel : IEncryptPostModel
    {
    }
}
