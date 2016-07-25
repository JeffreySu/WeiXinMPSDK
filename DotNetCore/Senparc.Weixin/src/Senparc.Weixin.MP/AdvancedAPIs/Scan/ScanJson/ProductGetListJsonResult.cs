/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：ProductGetListJsonResult.cs
    文件功能描述：批量查询商品返回结果
    
    
    创建标识：Senparc - 20160520
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Scan
{
    /// <summary>
    /// 批量查询商品返回结果
    /// </summary>
   public  class ProductGetListJsonResult : WxJsonResult 
    {
       /// <summary>
        /// 命中筛选条件的商品总数。
       /// </summary>
       public int total { get; set; }
       /// <summary>
       /// 商品信息列表。
       /// </summary>
       public List<ProductGetList_KeyList> key_list { get; set; }

      
    }
   public class ProductGetList_KeyList
   {
       /// <summary>
       /// 商品编码标准。
       /// </summary>
       public string keystandard { get; set; }
       /// <summary>
       /// 商品编码内容。
       /// </summary>
       public string keystr { get; set; }
       /// <summary>
       /// 商品类目ID。
       /// </summary>
       public string category_id { get; set; }
       /// <summary>
       /// 商品类目名称。
       /// </summary>
       public string category_name { get; set; }
       /// <summary>
       /// 商品信息的最后更新时间（整型）。
       /// </summary>
       public string update_time { get; set; }
       /// <summary>
       /// 商品主页的状态，on为发布状态，off为未发布状态，check为审核中状态，reject为审核未通过状态。
       /// </summary>
       public string status { get; set; }
   }
}
