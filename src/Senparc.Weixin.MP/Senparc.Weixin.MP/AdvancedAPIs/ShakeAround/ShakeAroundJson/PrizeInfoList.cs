/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：PrizeInfoList.cs
    文件功能描述：录入红包信息
    创建标识：Senparc - 20160520
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.ShakeAround
{
    /// <summary>
    /// 红包ticket列表，如果红包数较多，可以一次传入多个红包，批量调用该接口设置红包信息。每次请求传入的红包个数上限为100
    /// </summary>
   public class PrizeInfoList
    {
       /// <summary>
        /// 预下单时返回的红包ticket，单个活动红包ticket数量上限为100000个，可添加多次。    
       /// </summary>
       public string ticket { get; set; }
    }
}
