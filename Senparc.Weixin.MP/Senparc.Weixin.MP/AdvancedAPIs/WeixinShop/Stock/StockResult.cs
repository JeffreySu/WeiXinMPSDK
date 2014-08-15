using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 基础返回结果
    /// </summary>
    public class StockResult
    {
        public int errcode { get; set; }//错误码
        public string errmsg { get; set; }//错误信息
    }

    /// <summary>
    /// 增加库存返回结果
    /// </summary>
    public class AddStockResult : StockResult
    {
    }

    /// <summary>
    /// 减少库存返回结果
    /// </summary>
    public class ReduceStockResult : StockResult
    {
    }
}


