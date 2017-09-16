using BestPurchase.DataModel;
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
        public byte[] GetProductById(int productId)
        {
            BestPurchaseDBEntities db = new BestPurchaseDBEntities();
            Product product = db.Products.FirstOrDefault(c => c.Id == productId);
            DataModel.Product blProduct = ConvertDBProductToBLProduct(product);

            // Getting image as byte array
            MemoryStream memoryStream = new MemoryStream();
            byte[] fileContents;
            var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Pictures"), 
                product.ImageSource);
            fileContents = File.ReadAllBytes(path);
            blProduct.ImageSource = fileContents;
            return Formatter.Serialize(blProduct);
        }
        #endregion

        #region Shopping Cart
        public ShoppingCart ConvertBLCartToDBCart(BestPurchase.DataModel.ShoppingCart cart)
        {
            ShoppingCart Cart = new ShoppingCart();
            Cart.Id = cart.CartId;
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
                var cartItem = context.ShoppingCarts.SingleOrDefault(
                c => c.Id == Cart.Id
                && c.ProductId == Cart.ProductId);

                // Creating new shopping cart
                if (cartItem == null)
                {
                    context.ShoppingCarts.Add(Cart);
                }
                else
                {
                    // Incrementing quantity if the item is already in the cart
                    cartItem.Quantity += Cart.Quantity;
                }
                // Save changes
                context.SaveChanges();

            }
            return "Done";
        }

        public byte[] GetShoppingCartContent(string cartId)
        {
            BestPurchaseDBEntities db = new BestPurchaseDBEntities();
            var dbList = db.ShoppingCarts.Where(c => c.Id == cartId).ToList();
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
                cart.CartId = item.Id;
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
                cart.CartId == x.Id && x.ProductId == Cart.ProductId);
                context.ShoppingCarts.Remove(itemToRemove);
                context.SaveChanges();
            }
            return "Done";
        }
        #endregion

        #region Order
        public string AddOrder(byte[] byteArray)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(byteArray);
            var order = bf.Deserialize(stream) as BestPurchase.DataModel.Order;
            Order dbOrder = ConvertBLOrderToDBOrder(order);
            using (var context = new BestPurchaseDBEntities())
            {
                context.Orders.Add(dbOrder);
                context.SaveChanges();
            }
            return "Added";
        }

        private Order ConvertBLOrderToDBOrder(DataModel.Order order)
        {
            Order dbOrder = new Order();
            dbOrder.Date = order.OrderDate;
            for (int i = 0; i < order.Ordered.ProductList.Count(); ++i)
            {
                OrderDetail details = new OrderDetail();
                details.ProductId = order.Ordered.ProductList[i].Id;
                details.Quantity = order.Quantity[i];
                dbOrder.OrderDetails.Add(details);
            }
            dbOrder.User = ConvertBLUserToDbUser(order.Orderer);
            return dbOrder;
        }

        private User ConvertBLUserToDbUser(DataModel.User orderer)
        {
            User user = new User();
            user.FirstName = orderer.FirstName;
            user.LastName = orderer.LastName;
            user.Address = orderer.Address;
            user.City = orderer.City;
            user.State = orderer.State;
            user.Country = orderer.Country;
            user.Phone = orderer.Phone;
            user.PostalCode = orderer.PostalCode;
            user.Email = orderer.Email;
            user.Username = orderer.UserName;
            return user;
        }
        #endregion
    }
}