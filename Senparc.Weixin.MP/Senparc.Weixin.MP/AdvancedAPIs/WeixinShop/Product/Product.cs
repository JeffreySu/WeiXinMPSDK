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
    public static class WeixinShopProduct
    {
        /// <summary>
        /// 增加商品
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <param name="addProductData">提交到接口的数据（AddProductData）</param>
        /// <returns></returns>
        public static AddProductResult AddProduct(string appId, AddProductData addProductData)
        {
            var accessToken = AccessTokenContainer.GetToken(appId);

            var urlFormat = "https://api.weixin.qq.com/merchant/create?access_token={0}";

            return CommonJsonSend.Send<AddProductResult>(accessToken, urlFormat, addProductData);
        }

        /// <summary>
        /// 删除商品              
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <param name="productId">商品的Id</param>
        /// <returns></returns>
        public static DeleteProductResult DeleteProduct(string appId, string productId)
        {
            var accessToken = AccessTokenContainer.GetToken(appId);

            var urlFormat = "https://api.weixin.qq.com/merchant/del?access_token={0}";

            return CommonJsonSend.Send<DeleteProductResult>(accessToken, urlFormat, productId);
        }

        /// <summary>
        /// 修改商品
        /// product_id表示要更新的商品的ID，其他字段说明请参考增加商品接口。
        /// 从未上架的商品所有信息均可修改，否则商品的名称(name)、商品分类(category)、商品属性(property)这三个字段不可修改。
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <param name="reviseProduct">修改商品的信息</param>
        /// <returns></returns>
        public static UpdateProductResult UpDateProduct(string appId, UpdateProductData reviseProduct)
        {
            var accessToken = AccessTokenContainer.GetToken(appId);

            var urlFormat = "https://api.weixin.qq.com/merchant/update?access_token={0}";

            return CommonJsonSend.Send<UpdateProductResult>(accessToken, urlFormat, reviseProduct);
        }

        /// <summary>
        /// 查询商品
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <param name="productId">商品的Id</param>
        /// <returns></returns>
        public static GetProductResult GetProduct(string appId, string productId)
        {
            var accessToken = AccessTokenContainer.GetToken(appId);

            var urlFormat = "https://api.weixin.qq.com/merchant/get?access_token={0}";

            return CommonJsonSend.Send<GetProductResult>(accessToken, urlFormat, productId);
        }

        /// <summary>
        /// 获取指定状态的所有商品
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <param name="state">商品状态(0-全部, 1-上架, 2-下架)</param>
        /// <returns></returns>
        public static GetByStatusResult Getbystatus(string appId, int state)
        {
            var accessToken = AccessTokenContainer.GetToken(appId);

            var urlFormat = "https://api.weixin.qq.com/merchant/getbystatus?access_token={0}";

            return CommonJsonSend.Send<GetByStatusResult>(accessToken, urlFormat, state);
        }

        /// <summary>
        /// 商品上下架
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <param name="status">商品上下架标识(0-下架, 1-上架)</param>
        /// <returns></returns>
        public static ModProductStatusResult ModProductStatus(string appId, int status)
        {
            var accessToken = AccessTokenContainer.GetToken(appId);

            var urlFormat = "https://api.weixin.qq.com/merchant/modproductstatus?access_token={0}";

            return CommonJsonSend.Send<ModProductStatusResult>(accessToken, urlFormat, status);
        }

        /// <summary>
        /// 获取指定分类的所有子分类
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <param name="cateId">大分类ID(根节点分类id为1)</param>
        /// <returns></returns>
        public static GetSubResult GetSub(string appId, int cateId)
        {
            var accessToken = AccessTokenContainer.GetToken(appId);

            var urlFormat = "https://api.weixin.qq.com/merchant/category/getsub?access_token={0}";

            return CommonJsonSend.Send<GetSubResult>(accessToken, urlFormat, cateId);
        }

        /// <summary>
        /// 获取指定子分类的所有SKU
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <param name="cateId">商品子分类ID</param>
        /// <returns></returns>
        public static GetSkuResult GetSku(string appId, int cateId)
        {
            var accessToken = AccessTokenContainer.GetToken(appId);

            var urlFormat = "https://api.weixin.qq.com/merchant/category/getsku?access_token={0}";

            return CommonJsonSend.Send<GetSkuResult>(accessToken, urlFormat, cateId);
        }

        /// <summary>
        /// 获取指定分类的所有属性
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <param name="cateId">分类ID</param>
        /// <returns></returns>
        public static GetPropertyResult GetProperty(string appId, int cateId)
        {
            var accessToken = AccessTokenContainer.GetToken(appId);

            var urlFormat = "https://api.weixin.qq.com/merchant/category/getproperty?access_token={0}";

            return CommonJsonSend.Send<GetPropertyResult>(accessToken, urlFormat, cateId);
        }
    }
}
