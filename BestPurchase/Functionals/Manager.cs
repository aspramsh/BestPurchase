using BestPurchase.DataModel;
using BestPurchase.ServiceLayer.Models;
using BestPurchase.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Web;

namespace BestPurchase.ServiceLayer.Functionals
{
    public class Manager
    {
        // Making the class singleton
        public static Manager instance { get; }

        public static Manager Instance()
        {
            return instance != null ? instance : new Manager();
        }

        #region Products

        public Products GetProducts()
        {
            Products products = new Products();
            string baseUrl = ConfigurationManager.ConnectionStrings["MLServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.GetProducts;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";

            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                MemoryStream memStr = new MemoryStream();
                stream.CopyTo(memStr);
                memStr.Flush();
                memStr.Position = 0;
                BinaryFormatter bfd = new BinaryFormatter();
                products = bfd.Deserialize(memStr) as Products;
            }
            return products;
        }
        public ProductsModel ConvertToServiceLayerEntity(Products products)
        {
            ProductsModel prs = new ProductsModel();
            foreach (var item in products.ProductList)
            {
                ProductModel product = new ProductModel();
                product.Id = item.Id;
                product.Name = item.Name;
                product.Price = item.Price;
                product.ProductCategory = ConvertFromEnumToString(item.ProductCategory);
                product.Description = item.Description;
                prs.ProductList.Add(product);
            }
            return prs;
        }

        public string ConvertFromEnumToString(Category category)
        {
            string productCategory = Regex.Replace(category.ToString(), "(?<=[^A-Z])(?=[A-Z])", " "); ;
            return productCategory;
        }
        #endregion

        #region Shopping Cart
        public string AddProductToCart(ShoppingCart cart)
        {
            string baseUrl = ConfigurationManager.ConnectionStrings["MLServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.AddProductToCart;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "POST";
            request.ContentType = "application/octet-stream";

            Stream requestStream = request.GetRequestStream();
            byte[] bytes = Formatter.Serialize<ShoppingCart>(cart);
            requestStream.Write(bytes, 0, bytes.Length);

            WebResponse response = request.GetResponse();
            Stream str = response.GetResponseStream();
            return "Added";
        }

        public ShoppingCart ConvertCartModelToCart(CartModel cart)
        {
            ShoppingCart Cart = new ShoppingCart();
            Cart.Added.Id = cart.ProductId;
            Cart.Quantity = cart.Quantity;
            return Cart;
        }
        public ShoppingCartCollection GetShoppingCartContent()
        {
            ShoppingCartCollection carts = new ShoppingCartCollection();
            string baseUrl = ConfigurationManager.ConnectionStrings["MLServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.GetShoppingCartContent;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";

            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                MemoryStream memStr = new MemoryStream();
                stream.CopyTo(memStr);
                memStr.Flush();
                memStr.Position = 0;
                BinaryFormatter bfd = new BinaryFormatter();
                carts = bfd.Deserialize(memStr) as ShoppingCartCollection;
            }
            return carts;
        }
        public CartsCollection ConvertBLCartsToSLCarts(ShoppingCartCollection carts)
        {
            CartsCollection Carts = new CartsCollection();
            foreach (var item in carts.ListOfCarts)
            {
                CartModel cart = new CartModel();
                cart.ProductId = item.Added.Id;
                cart.ProductName = item.Added.Name;
                cart.Price = item.Added.Price;
                cart.Quantity = item.Quantity;
                Carts.ProductList.Add(cart);
            }
            return Carts;
        }

        public string DeleteItemFromCart(ShoppingCart cart)
        {
            string baseUrl = ConfigurationManager.ConnectionStrings["MLServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.DeleteProductFromCart;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "POST";
            request.ContentType = "application/octet-stream";

            Stream requestStream = request.GetRequestStream();
            byte[] bytes = Formatter.Serialize<ShoppingCart>(cart);
            requestStream.Write(bytes, 0, bytes.Length);

            WebResponse response = request.GetResponse();
            Stream str = response.GetResponseStream();
            return "Added";
        }
        #endregion

    }
}