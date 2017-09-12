using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BestPurchase.ServiceLayer.Models
{
    public class CartModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? Price { get; set; }
        public int? Quantity { get; set; }
    }
    public class CartsCollection
    {
        public List<CartModel> ProductList { get; set; }
        public CartsCollection()
        {
            ProductList = new List<CartModel>();
        }
    }
}