﻿using BestPurchase.DataModel;
using BestPurchase.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace BestPurchase.MiddleLayer.Functionals
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
            string baseUrl = ConfigurationManager.ConnectionStrings["DALServerName"].ConnectionString;
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
        public byte[] SerializeProducts()
        {
            Products products = this.GetProducts();
            byte[] Bytes = Formatter.Serialize<Products>(products);
            return Bytes;
        }
        public byte[] GetProcuctById(int Id)
        {
            string baseUrl = ConfigurationManager.ConnectionStrings["DALServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.GetProductById + Id;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";

            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                MemoryStream memStr = new MemoryStream();
                stream.CopyTo(memStr);
                memStr.Flush();
                memStr.Position = 0;
                return memStr.ToArray();
            }
        }
        #endregion

        #region ShoppingCart
        public string AddProductToCart(byte[] byteArray)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(byteArray);
            var cart = bf.Deserialize(stream) as ShoppingCart;
            string baseUrl = ConfigurationManager.ConnectionStrings["DALServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.AddProductToCart;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "POST";
            request.ContentType = "application/octet-stream";

            Stream requestStream = request.GetRequestStream();
            byte[] bytes = Formatter.Serialize<ShoppingCart>(cart);
            requestStream.Write(bytes, 0, bytes.Length);

            WebResponse response = request.GetResponse();
            Stream str = response.GetResponseStream();
            return "Done";
        }


        public ShoppingCartCollection GetShoppingCartContent(string cartId)
        {
            ShoppingCartCollection carts = new ShoppingCartCollection();
            string baseUrl = ConfigurationManager.ConnectionStrings["DALServerName"].ConnectionString;
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
        public byte[] SerializeShoppingCartContent(string cartId)
        {
            ShoppingCartCollection carts = this.GetShoppingCartContent(cartId);
            byte[] Bytes = Formatter.Serialize(carts);
            return Bytes;
        }
        public string DeleteProductFromCart(byte[] byteArray)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(byteArray);
            var cart = bf.Deserialize(stream) as ShoppingCart;
            string baseUrl = ConfigurationManager.ConnectionStrings["DALServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.DeleteProductFromCart;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "POST";
            request.ContentType = "application/octet-stream";

            Stream requestStream = request.GetRequestStream();
            byte[] bytes = Formatter.Serialize<ShoppingCart>(cart);
            requestStream.Write(bytes, 0, bytes.Length);

            WebResponse response = request.GetResponse();
            Stream str = response.GetResponseStream();
            return "Done";
        }

        #endregion

        #region Order
        public string AddOrder(byte[] byteArray)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(byteArray);
            var order = bf.Deserialize(stream) as Order;
            string baseUrl = ConfigurationManager.ConnectionStrings["DALServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.AddOrder;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "POST";
            request.ContentType = "application/octet-stream";

            Stream requestStream = request.GetRequestStream();
            byte[] bytes = Formatter.Serialize(order);
            requestStream.Write(bytes, 0, bytes.Length);

            WebResponse response = request.GetResponse();
            Stream str = response.GetResponseStream();
            return "Done";
        }
        #endregion
    }
}