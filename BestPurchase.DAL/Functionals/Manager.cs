using BestPurchase.DataModel;
using BestPurchase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BestPurchase.DAL.Functionals
{
    public class Manager
    {
        private static Manager instance;

        public static Manager Instance()
        {
            if (instance == null)
            { instance = new Manager(); }
            return instance;
        }
        public Products ConvertDBProductToBLProduct(List<DAL.Product> dbList)
        {
            Products products = new Products();
            foreach (var item in dbList)
            {
                DataModel.Product product = new DataModel.Product();
                product.Id = item.Id;
                product.Name = item.Name;
                product.Price = item.Price;
                product.Description = item.Description;
                product.ProductCategory = (DataModel.Category)Enum.Parse(typeof(DataModel.Category), item.Category.ProductCategory);
                products.ProductList.Add(product);
            }
            return products;
        }
        public byte[] GetAllProducts()
        {
            BestPurchaseDataBaseEntities db = new BestPurchaseDataBaseEntities();
            var dbList = db.Products.ToList();
            Products products = ConvertDBProductToBLProduct(dbList);
            return Formatter.Serialize<Products>(products);
        }
    }
}