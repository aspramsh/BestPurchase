﻿using BestPurchase.DataModel;
using BestPurchase.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
        #region Products
        private DataModel.Product ConvertDBProductToBLProduct(Product product)
        {
            DataModel.Product Product = new DataModel.Product();
            Product.Id = product.Id;
            Product.Name = product.Name;
            Product.Price = product.Price;
            Product.Description = product.Description;
            Product.ProductCategory = (DataModel.Category)Enum.Parse(typeof(DataModel.Category), product.Category.ProductCategory);
            return Product;
        }
        public Products ConvertDBProductsToBLProducts(List<DAL.Product> dbList)
        {
            Products products = new Products();
            foreach (var item in dbList)
            {
                DataModel.Product product = this.ConvertDBProductToBLProduct(item);
                products.ProductList.Add(product);
            }
            return products;
        }
        public byte[] GetAllProducts()
        {
            BestPurchaseDBEntities db = new BestPurchaseDBEntities();
            var dbList = db.Products.ToList();
            Products products = ConvertDBProductsToBLProducts(dbList);
            return Formatter.Serialize<Products>(products);
        }
        #endregion

        #region Shopping Cart
        public ShoppingCart ConvertBLCartToDBCart(BestPurchase.DataModel.ShoppingCart cart)
        {
            ShoppingCart Cart = new ShoppingCart();
            Cart.ProductId = cart.Added.Id;
            Cart.Quantity = cart.Quantity;
            return Cart;
        }

        public string AddProductToCart(byte[] byteArray)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(byteArray);
            var cart = bf.Deserialize(stream) as BestPurchase.DataModel.ShoppingCart;
            ShoppingCart Cart = this.ConvertBLCartToDBCart(cart);
            using (var context = new BestPurchaseDBEntities())
            {
                context.ShoppingCarts.Add(Cart);
                context.SaveChanges();
            }
            return "Done";
        }

        public byte[] GetShoppingCartContent()
        {
            BestPurchaseDBEntities db = new BestPurchaseDBEntities();
            var dbList = db.ShoppingCarts.ToList();
            ShoppingCartCollection carts = ConvertDBCartsToBLCarts(dbList, db);
            return Formatter.Serialize(carts);
        }

        private ShoppingCartCollection ConvertDBCartsToBLCarts(List<ShoppingCart> dbList, BestPurchaseDBEntities db)
        {
            ShoppingCartCollection carts = new ShoppingCartCollection();
            foreach (var item in dbList)
            {
                DataModel.ShoppingCart cart = new DataModel.ShoppingCart();
                Product product = db.Products.FirstOrDefault(c => c.Id == item.ProductId);
                DataModel.Product pro = ConvertDBProductToBLProduct(product);
                cart.Added = pro;
                cart.Quantity = item.Quantity;
                carts.ListOfCarts.Add(cart);
            }
            return carts;
        }
        public string DeleteProductFromCart(byte[] byteArray)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(byteArray);
            var cart = bf.Deserialize(stream) as BestPurchase.DataModel.ShoppingCart;
            ShoppingCart Cart = this.ConvertBLCartToDBCart(cart);
            using (var context = new BestPurchaseDBEntities())
            {
                var itemToRemove = context.ShoppingCarts.SingleOrDefault(x => 
                x.ProductId == Cart.ProductId);
                context.ShoppingCarts.Remove(itemToRemove);
                context.SaveChanges();
            }
            return "Done";
        }
        #endregion
    }
}