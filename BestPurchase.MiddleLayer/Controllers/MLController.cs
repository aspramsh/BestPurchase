using BestPurchase.MiddleLayer.Functionals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace BestPurchase.MiddleLayer.Controllers
{
    public class MLController : ApiController
    {
        [Route("api/ML/GetProducts")]
        [HttpGet]
        public HttpResponseMessage GetProducts()
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Functionals.Manager.Instance().SerializeProducts())
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }

        [Route("api/ML/AddProductToCart")]
        [HttpPost]
        public string AddProductToCart()
        {
            Task<byte[]> task = this.Request.Content.ReadAsByteArrayAsync();

            task.Wait();

            byte[] byteArray = task.Result;
            Manager.Instance().AddProductToCart(byteArray);

            return "success.";
        }
        [Route("api/ML/GetShoppingCartContent")]
        [HttpGet]
        public HttpResponseMessage GetShoppingCartContent()
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Manager.Instance().SerializeShoppingCartContent())
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
        [Route("api/ML/DeleteProductFromCart")]
        [HttpPost]
        public string DeleteProductFromCart()
        {
            Task<byte[]> task = this.Request.Content.ReadAsByteArrayAsync();

            task.Wait();

            byte[] byteArray = task.Result;
            Manager.Instance().DeleteProductFromCart(byteArray);

            return "success.";
        }
    }
}
