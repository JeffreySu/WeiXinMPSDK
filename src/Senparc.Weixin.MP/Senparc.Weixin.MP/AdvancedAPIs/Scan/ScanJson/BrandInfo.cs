using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.Scan
{

       public class BrandInfo_BaseInfo
       {
           /// <summary>
           /// 
           /// </summary>
           public string title { get; set; }
           /// <summary>
           /// 
           /// </summary>
           public string thumb_url { get; set; }
           /// <summary>
           /// 
           /// </summary>
           public string brand_tag { get; set; }
           /// <summary>
           /// 
           /// </summary>
           public int category_id { get; set; }
           /// <summary>
           /// 
           /// </summary>
           public string store_mgr_type { get; set; }
           /// <summary>
           /// 
           /// </summary>
           public List<string> store_vendorid_list { get; set; }
           /// <summary>
           /// 
           /// </summary>
           public string color { get; set; }
       }


       public class BrandInfo_DetailInfo
       {
           /// <summary>
           /// 
           /// </summary>
           public List<string> banner_list { get; set; }
           public List<string> detail_list { get; set; }
       }
       public class BrandInfo_ActionInfo
       {
           /// <summary>
           /// 商品主页中可设置多个服务栏。
           /// </summary>
           public List<BrandInfo_ActionList> action_list { get; set; }
        }
       public class BrandInfo_ActionList
       {
           /// <summary>
           /// 服务栏的类型，Media,视频播放；Text，文字介绍；Link，图片跳转；Link，普通链接；User，公众号；Card，微信卡券；Price，建议零售价；Product，微信小店；Store，电商链接；recommend，商品推荐。
           /// </summary>
           public string type { get; set; }
       }
   public class BrandInfo
    {
       public BrandInfo_BaseInfo base_info { get; set; }

       public BrandInfo_DetailInfo detail_info { get; set; }

      
       public BrandInfo_ActionInfo action_info { get; set; }

      
       public BrandInfo_ModuleInfo module_info { get; set; }

      
    }
   public class BrandInfo_ModuleInfo
   {
       /// <summary>
       /// 未来可设置多个组件，目前仅支持防伪组件。
       /// </summary>
       public List<BrandInfo_ModuleList> module_list { get; set; }
    }
   public class BrandInfo_ModuleList
   {
       /// <summary>
       /// 组件的类型，目前仅包括防伪组件anti_fake。
       /// </summary>
       public string type { get; set; }
       /// <summary>
       /// 设置为true时，防伪结果使用微信提供的弹窗页面展示，商户仅需调用“商品管理”部分的组件消息接口回传产品真假信息。设置为false时，无防伪弹窗效果。
       /// </summary>
       public string native_show { get; set; }
       /// <summary>
       /// 商户提供的防伪查询链接，当native_show设置为false时必填。
       /// </summary>
       public string anti_fake_url { get; set; }
   }
}
