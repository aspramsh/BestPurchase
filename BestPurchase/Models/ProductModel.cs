using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BestPurchase.ServiceLayer.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
        public string ProductCategory { get; set; }
        public int? Quantity { get; set; }
    }


    public class ProductsModel
    {
        public List<ProductModel> ProductList { get; set; }
        public ProductsModel()
        {
            ProductList = new List<ProductModel>();
        }
    }

    public enum CategoryModel
    {
        [Display(Name = "All Products")]
        AllProducts,

        [Display(Name = "Organic Products")]
        OrganicProducts,

        [Display(Name = "Meat Products")]
        MeatProducts,

        [Display(Name = "Fish And Sea Food")]
        FishAndSeaFood,

        [Display(Name = "Frozen Products")]
        FrozenProducts,
        Sweets
    }
}