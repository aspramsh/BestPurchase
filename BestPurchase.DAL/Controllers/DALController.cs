using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
    }
}
