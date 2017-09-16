using BestPurchase.DAL.Functionals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace BestPurchase.DAL.Controllers
{
    public class DALController : ApiController
    {
        [Route("api/DAL/GetProducts")]
        [HttpGet]
        public HttpResponseMessage GetProducts()
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Functionals.Manager.Instance().GetAllProducts())
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
        [Route("api/DAL/AddProductToCart")]
        [HttpPost]
        public string AddProductToCart()
        {
            Task<byte[]> task = this.Request.Content.ReadAsByteArrayAsync();

            task.Wait();

            byte[] byteArray = task.Result;
            Manager.Instance().AddProductToCart(byteArray);

            return "success.";
        }
        [Route("api/DAL/GetShoppingCartContent")]
        [HttpGet]
        public HttpResponseMessage GetShoppingCartContent(string cartId)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Functionals.Manager.Instance().GetShoppingCartContent(cartId))
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
        [Route("api/DAL/DeleteProductFromCart")]
        [HttpPost]
        public string DeleteProductFromCart()
        {
            Task<byte[]> task = this.Request.Content.ReadAsByteArrayAsync();

            task.Wait();

            byte[] byteArray = task.Result;
            Manager.Instance().DeleteProductFromCart(byteArray);

            return "success.";
        }
        [Route("api/DAL/AddOrder")]
        [HttpPost]
        public string AddOrder()
        {
            Task<byte[]> task = this.Request.Content.ReadAsByteArrayAsync();

            task.Wait();

            byte[] byteArray = task.Result;
            Manager.Instance().AddOrder(byteArray);

            return "success.";
        }
        [Route("api/DAL/GetProductById")]
        [HttpGet]
        public HttpResponseMessage DownLoadAttachments(int Id)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Functionals.Manager.Instance().GetProductById(Id))
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
    }
}
