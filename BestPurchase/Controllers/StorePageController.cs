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
            CategoryModel cat = (CategoryModel)category;
            var filteredProducts = prods.ProductList.Where(c => c.ProductCategory == cat).ToList();
            return PartialView("ShowFilteredCategory", filteredProducts);
        }
    }
}