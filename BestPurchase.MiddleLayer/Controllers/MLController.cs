using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
    }
}
