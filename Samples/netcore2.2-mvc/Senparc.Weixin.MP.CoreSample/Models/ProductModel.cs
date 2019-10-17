using System;
using System.Collections.Generic;
using System.Linq;

namespace Senparc.Weixin.MP.CoreSample.Models
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
                new ProductModel(1,"产品1",(decimal)0.01),
                new ProductModel(2,"产品2",(decimal)2.00),
                new ProductModel(3,"产品3",(decimal)3.00),
                new ProductModel(4,"产品4",(decimal)4.00),
                new ProductModel(5,"捐赠1",(decimal)10.00),
                new ProductModel(6,"捐赠2",(decimal)50.00),
                new ProductModel(7,"捐赠3",(decimal)100.00),
                new ProductModel(8,"捐赠4",(decimal)500.00),
            };
            ProductList = ProductList ?? list;

            return list;
        }
    }
}