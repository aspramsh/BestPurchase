using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BestPurchase.ServiceLayer.Controllers;
using System.Web.Mvc;
using System.Web;
using BestPurchase.DataModel;

namespace BestPurchase.ServiceLayer.Testing
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGetProductDetails()
        {
            var controller = new StorePageController();
            var result = controller.GetProductDetails(1) as ViewResult;
            var product = (Product)result.ViewData.Model;
            Assert.AreEqual("3 Types Of Meat", product.Name);
        }
    }
}
