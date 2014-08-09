using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 微小店接口，官方API：https://mp.weixin.qq.com/paymch/readtemplate?t=mp/business/course2_tmpl&lang=zh_CN&token=25857919#4
    /// </summary>
    public static class WeixinShop
    {
        /// <summary>
        /// 增加商品
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <param name="addProductData">提交到接口的数据（AddProductData）</param>
        /// <returns></returns>
        public static AddProductResult AddProduct(string appId, ProductData addProductData)
        {
            var accessToken = AccessTokenContainer.GetToken(appId);

            var urlFormat = "https://api.weixin.qq.com/merchant/create?access_token={0}";

            return CommonJsonSend.Send<AddProductResult>(accessToken, urlFormat, addProductData);
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <param name="product_Id">商品的Id</param>
        /// <returns></returns>
        public static WeixinShopResult DeleteProduct(string appId, string product_Id)
        {
            var accessToken = AccessTokenContainer.GetToken(appId);

            var urlFormat = "https://api.weixin.qq.com/merchant/del?access_token={0}";

            return CommonJsonSend.Send<WeixinShopResult>(accessToken, urlFormat, product_Id);
        }

        /// <summary>
        /// 修改商品
        /// product_id表示要更新的商品的ID，其他字段说明请参考增加商品接口。
        /// 从未上架的商品所有信息均可修改，否则商品的名称(name)、商品分类(category)、商品属性(property)这三个字段不可修改。
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <param name="reviseProduct">修改商品的信息</param>
        /// <returns></returns>
        public static WeixinShopResult ReviseProduct(string appId, ReviseProductData reviseProduct)
        {
            var accessToken = AccessTokenContainer.GetToken(appId);

            var urlFormat = "https://api.weixin.qq.com/merchant/update?access_token={0}";

            return CommonJsonSend.Send<WeixinShopResult>(accessToken, urlFormat, reviseProduct);
        }
    }
}
