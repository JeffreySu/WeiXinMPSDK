/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：MobileApiResult.cs
    文件功能描述：移动端SDK返回结果
     
    
    创建标识：Senparc - 20181009

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Mobile
{

    /// <summary>
    /// 获取电子发票ticket返回结果
    /// </summary>
    public class GetTicketResultJson : WorkJsonResult
    {
        public string ticket { get; set; }
        public int expires_in { get; set; }
    }



}
