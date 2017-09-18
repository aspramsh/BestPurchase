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
        public ProductModel ConvertProductToProductModel(Product product)
        {
            ProductModel Product = new ProductModel();
            Product.Id = product.Id;
            Product.Name = product.Name;
            Product.Price = product.Price;
            Product.ProductCategory = ConvertFromEnumToString(product.ProductCategory);
            Product.Description = product.Description;
            Product.ImageStream = product.ImageSource;
            return Product;
        }
        public ProductsModel ConvertToServiceLayerEntity(Products products)
        {
            ProductsModel prs = new ProductsModel();
            foreach (var item in products.ProductList)
            {
                ProductModel product = this.ConvertProductToProductModel(item);
                prs.ProductList.Add(product);
            }
            return prs;
        }

        public string ConvertFromEnumToString(Category category)
        {
            string productCategory = Regex.Replace(category.ToString(), "(?<=[^A-Z])(?=[A-Z])", " "); ;
            return productCategory;
        }
        public Product GetProductById(int Id)
        {
            Product product = new Product();
            string baseUrl = ConfigurationManager.ConnectionStrings["MLServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.GetProductById + Id.ToString();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";

            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                MemoryStream memStr = new MemoryStream();
                stream.CopyTo(memStr);
                memStr.Flush();
                memStr.Position = 0;
                memStr.ToArray();
                BinaryFormatter bfd = new BinaryFormatter();
                product = bfd.Deserialize(memStr) as Product;
                return product;            }
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

        public ShoppingCartCollection GetShoppingCartContent(string cartId)
        {
            ShoppingCartCollection carts = new ShoppingCartCollection();
            string baseUrl = ConfigurationManager.ConnectionStrings["MLServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.GetShoppingCartContent + cartId;
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

        #region Order
        private User CreateUserFromUserModel(OrderModel order)
        {
            User user = new User();
            user.FirstName = order.FirstName;
            user.LastName = order.LastName;
            user.Address =order.Address;
            user.City = order.City;
            user.State = order.State;
            user.Country = order.Country;
            user.PostalCode = order.PostalCode;
            user.Phone = order.Phone;
            user.Email = order.Email;
            user.UserName = order.UserName;
            return user;
        }
        private Order CreateOrderFromCartContent(List<CartModel> cartContent)
        {
            Order order = new Order();
            order.OrderDate = DateTime.Now;
            foreach (var item in cartContent)
            {
                order.Ordered.ProductList.Add(ConvertCartModelToCart(item).Added);
                order.Quantity.Add(item.Quantity);
            }
            return order;
        }
        public string AddOrder(OrderModel ord)
        {
            User user = this.CreateUserFromUserModel(ord);
            Order order = this.CreateOrderFromCartContent(ord.Cart);
            order.Orderer = user;
            string baseUrl = ConfigurationManager.ConnectionStrings["MLServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.AddOrder;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "POST";
            request.ContentType = "application/octet-stream";

            Stream requestStream = request.GetRequestStream();
            byte[] bytes = Formatter.Serialize(order);
            requestStream.Write(bytes, 0, bytes.Length);

            WebResponse response = request.GetResponse();
            Stream str = response.GetResponseStream();
            return "Added";
        }
        #endregion
    }
}