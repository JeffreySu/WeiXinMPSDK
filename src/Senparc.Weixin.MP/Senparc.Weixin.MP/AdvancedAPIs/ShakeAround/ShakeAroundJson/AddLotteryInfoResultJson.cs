/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：AddLotteryInfoResultJson.cs
    文件功能描述：创建红包活动的返回结果
    
    
    创建标识：Senparc - 20160520
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.ShakeAround
{
    /// <summary>
    /// 创建红包活动的返回结果
    /// </summary>
   public class AddLotteryInfoResultJson : WxJsonResult 
    {
       /// <summary>
        /// 生成的红包活动id
       /// </summary>
       public string lottery_id { get; set; }
       /// <summary>
       /// 生成的模板页面ID  
       /// </summary>
       public int page_id { get; set; }
    }
}
