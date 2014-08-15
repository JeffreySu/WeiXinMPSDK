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
    public static class WeixinShopShelves
    {
        /// <summary>
        /// 增加货架
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <param name="addStockData">增加货架需要Post的数据</param>
        /// <returns></returns>
        public static AddShelvesResult AddShelves(string appId, AddShelvesData addStockData)
        {
            var accessToken = AccessTokenContainer.GetToken(appId);

            var urlFormat = "https://api.weixin.qq.com/merchant/shelf/add?access_token={0}";

            return CommonJsonSend.Send<AddShelvesResult>(accessToken, urlFormat, addStockData);
        }

        /// <summary>
        /// 删除货架
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <param name="shelfId">货架Id</param>
        /// <returns></returns>
        public static DeleteShelvesResult DeleteShelves(string appId, int shelfId)
        {
            var accessToken = AccessTokenContainer.GetToken(appId);

            var urlFormat = "https://api.weixin.qq.com/merchant/shelf/del?access_token={0}";

            return CommonJsonSend.Send<DeleteShelvesResult>(accessToken, urlFormat, shelfId);
        }

        /// <summary>
        /// 修改货架
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <param name="modShelvesData">修改货架需要Post的数据</param>
        /// <returns></returns>
        public static ModShelvesResult ModShelves(string appId, ModShelvesData modShelvesData)
        {
            var accessToken = AccessTokenContainer.GetToken(appId);

            var urlFormat = "https://api.weixin.qq.com/merchant/shelf/mod?access_token={0}";

            return CommonJsonSend.Send<ModShelvesResult>(accessToken, urlFormat, modShelvesData);
        }


        public static ModShelvesResult GetallShelves(string appId)
        {
            var accessToken = AccessTokenContainer.GetToken(appId);

            var urlFormat = "https://api.weixin.qq.com/merchant/shelf/getall?access_token=ACCESS_TOKEN";

            return CommonJsonSend.Send<ModShelvesResult>(accessToken, urlFormat, null,CommonJsonSendType.GET);
        }
    }
}
