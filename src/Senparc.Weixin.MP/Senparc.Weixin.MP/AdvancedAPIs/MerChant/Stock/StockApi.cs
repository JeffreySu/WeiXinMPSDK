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
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs.MerChant
{
    /// <summary>
    /// 微小店库存接口
    /// </summary>
    public static class StockApi
    {
        #region 同步方法
        /// <summary>
        /// 增加库存
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="addStockData">增加库存需要Post的数据</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StockApi.AddStock", true)]
        public static WxJsonResult AddStock(string accessToken, AddStockData addStockData)
        {
            var urlFormat = Config.ApiMpHost + "/merchant/stock/add?access_token={0}";

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, addStockData);
        }

        /// <summary>
        /// 减少库存
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="reduceStockData">减少库存需要Post的数据</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StockApi.ReduceStock", true)]
        public static WxJsonResult ReduceStock(string accessToken, ReduceStockData reduceStockData)
        {
            var urlFormat = Config.ApiMpHost + "/merchant/stock/reduce?access_token={0}";

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, reduceStockData);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 【异步方法】增加库存
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="addStockData">增加库存需要Post的数据</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StockApi.AddStockAsync", true)]
        public static async Task<WxJsonResult> AddStockAsync(string accessToken, AddStockData addStockData)
        {
            var urlFormat = Config.ApiMpHost + "/merchant/stock/add?access_token={0}";

            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, addStockData).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】减少库存
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="reduceStockData">减少库存需要Post的数据</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StockApi.ReduceStockAsync", true)]
        public static async Task<WxJsonResult> ReduceStockAsync(string accessToken, ReduceStockData reduceStockData)
        {
            var urlFormat = Config.ApiMpHost + "/merchant/stock/reduce?access_token={0}";

            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, reduceStockData).ConfigureAwait(false);
        }
        #endregion
    }
}
