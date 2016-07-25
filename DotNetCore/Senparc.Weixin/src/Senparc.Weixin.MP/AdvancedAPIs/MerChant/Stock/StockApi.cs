/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：ProductApi.cs
    文件功能描述：微小店商品接口
    
    
    创建标识：Senparc - 20150827

    修改标识：Senparc - 20160719
    修改描述：增加其接口的异步方法
----------------------------------------------------------------*/

/* 
   微小店接口，官方API：http://mp.weixin.qq.com/wiki/index.php?title=%E5%BE%AE%E4%BF%A1%E5%B0%8F%E5%BA%97%E6%8E%A5%E5%8F%A3
*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs.MerChant
{
    /// <summary>
    /// 微小店库存接口
    /// </summary>
    public static class StockApi
    {
        #region 同步请求
        /// <summary>
        /// 增加库存
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="addStockData">增加库存需要Post的数据</param>
        /// <returns></returns>
        public static WxJsonResult AddStock(string accessToken, AddStockData addStockData)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/stock/add?access_token={0}";

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, addStockData);
        }

        /// <summary>
        /// 减少库存
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="reduceStockData">减少库存需要Post的数据</param>
        /// <returns></returns>
        public static WxJsonResult ReduceStock(string accessToken, ReduceStockData reduceStockData)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/stock/reduce?access_token={0}";

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, reduceStockData);
        }
        #endregion
        #region 异步请求
        /// <summary>
        /// 【异步方法】增加库存
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="addStockData">增加库存需要Post的数据</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> AddStockAsync(string accessToken, AddStockData addStockData)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/stock/add?access_token={0}";

            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, addStockData);
        }

        /// <summary>
        /// 【异步方法】减少库存
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="reduceStockData">减少库存需要Post的数据</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> ReduceStockAsync(string accessToken, ReduceStockData reduceStockData)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/stock/reduce?access_token={0}";

            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, reduceStockData);
        }
        #endregion
        
    }
}
