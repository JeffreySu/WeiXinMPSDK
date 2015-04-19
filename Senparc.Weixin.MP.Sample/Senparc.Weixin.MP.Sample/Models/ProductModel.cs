using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Senparc.Weixin.MP.Sample.Models
{
    /// <summary>
    /// 商品实体类
    /// </summary>
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ProductModel()
        {
        }

        public ProductModel(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }


        private static List<ProductModel> ProductList { get; set; }

        public static List<ProductModel> GetFakeProductList()
        {
            var list = ProductList ?? new List<ProductModel>()
            {
                new ProductModel(1,"产品1",(decimal)1.00),
                new ProductModel(2,"产品2",(decimal)1.00),
                new ProductModel(3,"产品3",(decimal)1.00),
                new ProductModel(4,"产品4",(decimal)1.00),
            };
            ProductList = ProductList ?? list;

            return list;
        }
    }
}