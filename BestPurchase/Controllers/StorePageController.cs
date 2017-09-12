using BestPurchase.DataModel;
using BestPurchase.ServiceLayer.Functionals;
using BestPurchase.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


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
            ShoppingCart Cart = Manager.Instance().ConvertCartModelToCart(cart);
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
            ShoppingCartCollection cartCollection = Manager.Instance().GetShoppingCartContent();
            CartsCollection carts = Manager.Instance().ConvertBLCartsToSLCarts(cartCollection);
            return View("GetShoppingCartContent", carts.ProductList);
        }
        public ActionResult DeleteItemFromCart(int productId)
        {
            ShoppingCart cart = new ShoppingCart();
            cart.Added.Id = productId;
            Manager.Instance().DeleteItemFromCart(cart);
            ShoppingCartCollection cartCollection = Manager.Instance().GetShoppingCartContent();
            CartsCollection carts = Manager.Instance().ConvertBLCartsToSLCarts(cartCollection);
            return View("GetShoppingCartContent", carts.ProductList);
        }
    }
}