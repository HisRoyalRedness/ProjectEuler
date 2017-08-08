using EulerWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EulerWeb.Controllers
{
    [RoutePrefix("api/testapi")]
    public class TestApiController : ApiController
    {
        // GET: api/Test
        [AcceptVerbs("Get")]
        public HttpResponseMessage /*IEnumerable<TestModel>*/ GetModels(int index = 0)
        {
            var models =  new TestModel[] { new TestModel(index), new TestModel(2) };
            return Request.CreateResponse(HttpStatusCode.OK, models);
        }

        // GET: api/Test
        [HttpGet]
        [Route("getmodel/{index:int}")]
        public HttpResponseMessage GetModel(int index = 0)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new TestModel(index));
        }
    }
}
