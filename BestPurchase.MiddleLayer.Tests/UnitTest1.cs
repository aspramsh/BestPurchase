using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BestPurchase.MiddleLayer.Functionals;
using BestPurchase.DataModel;

namespace BestPurchase.MiddleLayer.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string cartId = "55";
            byte[] bytes = Manager.Instance().SerializeShoppingCartContent(cartId);
        }
    }
}
