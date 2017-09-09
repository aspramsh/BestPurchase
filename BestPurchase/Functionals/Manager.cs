using BestPurchase.DataModel;
using BestPurchase.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
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
                product.ProductCategory = (CategoryModel)Enum.Parse(typeof(CategoryModel), item.ProductCategory.ToString());
                product.Description = item.Description;
                prs.ProductList.Add(product);
            }
            return prs;
        }
    }
}