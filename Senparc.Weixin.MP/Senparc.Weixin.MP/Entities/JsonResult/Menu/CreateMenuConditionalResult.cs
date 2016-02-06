/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：CreateMenuConditionalResult.cs.cs
    文件功能描述：创建个性化菜单结果
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Entities.Menu;

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// CreateMenuConditional返回的Json结果
    /// </summary>
    public class CreateMenuConditionalResult : WxJsonResult
    {
        /* JSON:
        {"menuid":401654628}
        */
        /// <summary>
        /// menuid
        /// </summary>
        public long menuid { get; set; }
    }
}
