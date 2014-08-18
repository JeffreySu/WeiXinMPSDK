using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 微小店接口，官方API：http://mp.weixin.qq.com/wiki/index.php?title=%E5%BE%AE%E4%BF%A1%E5%B0%8F%E5%BA%97%E6%8E%A5%E5%8F%A3
    /// </summary>
    public static class WeixinShopStock
    {
        /// <summary>
        /// 增加库存
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="addStockData">增加库存需要Post的数据</param>
        /// <returns></returns>
        public static AddStockResult AddStock(string accessToken, AddStockData addStockData)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/stock/add?access_token={0}";

            return CommonJsonSend.Send<AddStockResult>(accessToken, urlFormat, addStockData);
        }

        /// <summary>
        /// 减少库存
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="reduceStockData">减少库存需要Post的数据</param>
        /// <returns></returns>
        public static ReduceStockResult ReduceStock(string accessToken, ReduceStockData reduceStockData)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/stock/reduce?access_token={0}";

            return CommonJsonSend.Send<ReduceStockResult>(accessToken, urlFormat, reduceStockData);
        }
    }
}
