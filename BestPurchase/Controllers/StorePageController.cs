using BestPurchase.DataModel;
using BestPurchase.ServiceLayer.Functionals;
using BestPurchase.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BestPurchase.ServiceLayer.Controllers
{
    public class StorePageController : Controller
    {
        // GET: StorePage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllProducts()
        {
            return View("GetAllProducts");
        }
        public PartialViewResult ShowFilteredCategory(int category)
        {
            Products products = Manager.Instance().GetProducts();
            ProductsModel prods = Manager.Instance().ConvertToServiceLayerEntity(products);
            if (category == 0)
            {
                return PartialView("ShowFilteredCategory", prods.ProductList);
            }
            Category cat = (Category)category;
            var filteredProducts = prods.ProductList.Where(c => c.ProductCategory ==
            Manager.Instance().ConvertFromEnumToString(cat)).ToList();
            return PartialView("ShowFilteredCategory", filteredProducts);
        }
        
        [HttpPost]
        public ActionResult AddToCart(CartModel cart)
        {
            var Cart = ShoppingCart.GetCart(this.HttpContext);
            Cart.Added.Id = cart.ProductId;
            Cart.Quantity = cart.Quantity;
            Manager.Instance().AddProductToCart(Cart);
            return View("AddToCart");
        }
        public PartialViewResult SelectQuantity(int Id)
        {
            CartModel cart = new CartModel();
            cart.ProductId = Id;
            return PartialView("SelectQuantity", cart);
        }
        public ActionResult GetShoppingCartContent()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            ShoppingCartCollection cartCollection = Manager.Instance().GetShoppingCartContent(cart.CartId);
            CartsCollection carts = Manager.Instance().ConvertBLCartsToSLCarts(cartCollection);
            return View("GetShoppingCartContent", carts.ProductList);
        }
        public ActionResult DeleteItemFromCart(int productId)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.Added.Id = productId;
            Manager.Instance().DeleteItemFromCart(cart);
            ShoppingCartCollection cartCollection = Manager.Instance().GetShoppingCartContent(cart.CartId);
            CartsCollection carts = Manager.Instance().ConvertBLCartsToSLCarts(cartCollection);
            return View("GetShoppingCartContent", carts.ProductList);
        }
        public ActionResult AddOrder()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            OrderModel order = new OrderModel();
            ShoppingCartCollection cartCollection = Manager.Instance().GetShoppingCartContent(cart.CartId);
            CartsCollection carts = Manager.Instance().ConvertBLCartsToSLCarts(cartCollection);
            foreach (var item in carts.ProductList)
            {
                order.Cart.Add(item);
            }
            return View("AddOrder", order);
        }

        [HttpPost]
        public ActionResult AddOrder(OrderModel order)
        {
            Manager.Instance().AddOrder(order);
            var Cart = ShoppingCart.GetCart(this.HttpContext);
            foreach (var item in order.Cart)
            {
                ShoppingCart cart = Manager.Instance().ConvertCartModelToCart(item);
                cart.CartId = Cart.CartId;
                Manager.Instance().DeleteItemFromCart(cart);
            }
            ShoppingCartCollection cartCollection = Manager.Instance().GetShoppingCartContent(Cart.CartId);
            CartsCollection carts = Manager.Instance().ConvertBLCartsToSLCarts(cartCollection);
            return View("GetShoppingCartContent", carts.ProductList);
        }
        
        public ActionResult GetProductDetails(int productId)
        {
            Product product = Manager.Instance().GetProductById(productId);
            ProductModel pro = Manager.Instance().ConvertProductToProductModel(product);
            return View("GetProductDetails", pro);
        }
    }
}