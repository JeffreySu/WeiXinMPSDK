/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：SubmerChantSubmitJsonResult.cs
    文件功能描述：创建子商户接口的返回结果
    
    
    创建标识：Senparc - 20160520
    
    修改标识：Senparc - 20160520
    修改描述：整理接口
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 创建子商户接口的返回结果
    /// </summary>
   public class SubmerChantSubmitJsonResult : WxJsonResult
    {
       public SubmerChantSubmit_info info { get; set; }

       
    }
   public class SubmerChantSubmit_info
   {
       /// <summary>
       /// 子商户id，对于一个母商户公众号下唯一。创建卡券时需填入该id号，字段结构如下：base_info：｛sub_merchant_info:｛merchant_id：｝｝，详情见创建卡券接口
       /// </summary>
       public string merchant_id { get; set; }
       /// <summary>
       /// 子商户信息创建时间
       /// </summary>
       public int create_time { get; set; }
       /// <summary>
       /// 子商户信息更新时间
       /// </summary>
       public int update_time { get; set; }
       /// <summary>
       /// 子商户名称（12个汉字内），该名称将在制券时填入并显示在卡券页面上。
       /// </summary>
       public string brand_name { get; set; }
       /// <summary>
       /// 子商户logo，可通过上传logo接口获取。该logo将在制券时填入并显示在卡券页面上
       /// </summary>
       public string logo_url { get; set; }
       /// <summary>
       /// 子商户状态，"CHECKING" 审核中, "APPROVED" , 已通过；"REJECTED"被驳回, "EXPIRED"协议已过期
       /// </summary>
       public string status { get; set; }
       /// <summary>
       /// 创建时间（非协议开始时间）
       /// </summary>
       public int begin_time { get; set; }
       /// <summary>
       /// 授权函有效期截止时间（东八区时间，单位为秒）
       /// </summary>
       public int end_time { get; set; }
       /// <summary>
       /// 子商户一级类目
       /// </summary>
       public int primary_category_id { get; set; }
       /// <summary>
       /// 子商户二级类目
       /// </summary>
       public int secondary_category_id { get; set; }
   }
}
