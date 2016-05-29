/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：InfoList.cs
    文件功能描述：创建子商户的功能的数据
    
    
    创建标识：Senparc - 20160520
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 创建子商户的功能的数据
    /// </summary>
    public class InfoList
    {
        /// <summary>
        /// 子商户名称（12个汉字内），该名称将在制券时填入并显示在卡券页面上
        /// </summary>
        public string brand_name { get; set; }
        /// <summary>
        /// 子商户的公众号app_id，配置后子商户卡券券面上的app_id为该app_id。注意：该app_id须经过认证
        /// </summary>
        public string app_id { get; set; }
        /// <summary>
        /// 子商户名称（12个汉字内），该名称将在制券时填入并显示在卡券页面上
        /// </summary>
        public string logo_url { get; set; }
        /// <summary>
        /// 授权函ID，即通过上传临时素材接口上传授权函后获得的meida_id
        /// </summary>
        public string protocol { get; set; }
        /// <summary>
        /// 营业执照或个体工商户营业执照彩照或扫描件
        /// </summary>
        public string agreement_media_id { get; set; }
        /// <summary>
        /// 营业执照内登记的经营者身份证彩照或扫描件
        /// </summary>
        public string operator_media_id { get; set; }
        /// <summary>
        /// 授权函有效期截止时间（东八区时间，单位为秒），需要与提交的扫描件一致
        /// </summary>
        public string end_time { get; set; }
        /// <summary>
        /// 一级类目id,可以通过本文档中接口查询
        /// </summary>
        public string primary_category_id { get; set; }
        /// <summary>
        /// 二级类目id，可以通过本文档中接口查询
        /// </summary>
        public string secondary_category_id { get; set; }
    }
}
