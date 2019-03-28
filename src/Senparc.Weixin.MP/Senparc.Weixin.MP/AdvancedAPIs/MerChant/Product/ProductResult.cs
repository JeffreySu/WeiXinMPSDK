#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：ProductResult.cs
    文件功能描述：商品结果


    创建标识：Senparc - 20150827

    修改标识：Senparc - 20160610
    修改描述：修改PropertyValue的id类型为int
  
    修改标识：Senparc - 20160825
    修改描述;将Sku和Value中的id的int类型改为string类型

----------------------------------------------------------------*/


using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.MerChant
{
    /// <summary>
    /// 增加商品返回结果
    /// </summary>
    public class AddProductResult : WxJsonResult
    {
        public string product_id { get; set; }
    }

    /// <summary>
    /// 查询商品返回结果
    /// </summary>
    public class GetProductResult : WxJsonResult
    {
        //商品详细信息
        public ProductInfoData product_info { get; set; }
    }

    /// <summary>
    /// 修改商品信息
    /// </summary>
    public class ProductInfoData : BaseProductData
    {
        public string product_id { get; set; }
    }

    /// <summary>
    /// 获取指定状态的所有商品返回结果
    /// </summary>
    public class GetByStatusResult : WxJsonResult
    {
        public List<GetByStatusProductInfo> products_info { get; set; }
    }

    public class GetByStatusProductInfo : BaseProductData
    {
        public string product_id { get; set; }
        public int status { get; set; }
    }

    /// <summary>
    /// 获取指定分类的所有子分类返回结果
    /// </summary>
    public class GetSubResult : WxJsonResult
    {
        public List<CateItem> cate_list { get; set; }
    }

    public class CateItem
    {
        /// <summary>
        /// 子分类ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 子分类名称
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// 获取指定子分类的所有SKU返回结果
    /// </summary>
    public class GetSkuResult : WxJsonResult
    {
        public List<Sku> sku_table { get; set; }
    }

    public class Sku
    {
        /// <summary>
        /// sku id 将int类型修改为string类型
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// sku 名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// sku vid列表
        /// </summary>
        public List<Value> value_list { get; set; }
    }

    public class Value
    {
        /// <summary>
        /// vid
        /// 将id的int类型改为string类型
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// vid名称
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// 获取指定分类的所有属性返回结果
    /// </summary>
    public class GetPropertyResult : WxJsonResult
    {
        public List<PropertyItem> properties { get; set; }
    }

    public class PropertyItem
    {
        /// <summary>
        /// 属性id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        public List<PropertyValue> property_value { get; set; }
    }

    public class PropertyValue
    {
        /// <summary>
        /// 属性值id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 属性值名称
        /// </summary>
        public string name { get; set; }
    }
}
