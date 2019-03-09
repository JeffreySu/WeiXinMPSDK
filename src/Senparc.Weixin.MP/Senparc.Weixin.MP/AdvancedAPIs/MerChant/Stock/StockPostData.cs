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

namespace Senparc.Weixin.MP.AdvancedAPIs.MerChant
{
    public class BaseStockData
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public string product_id { get; set; }
        /// <summary>
        /// sku信息,格式"id1:vid1;id2:vid2",如商品为统一规格，则此处赋值为空字符串即可
        /// </summary>
        public string sku_info { get; set; }
        /// <summary>
        /// 增加的库存数量
        /// </summary>
        public int quantity { get; set; }
    }

    /// <summary>
    /// 增加库存
    /// </summary>
    public class AddStockData : BaseStockData
    {
    }

    /// <summary>
    /// 减少库存
    /// </summary>
    public class ReduceStockData : BaseStockData
    {
    }
}
