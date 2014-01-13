using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mvc.Web.Controllers {
    public class NewsController : ApiController {
        // GET api/news
        public IEnumerable<string> Get() {
            return new string[] { "Title1", "Title2", "Title3" };
        }

        // GET api/news/5
        public string Get(int id) {
            return "value";
        }

        // POST api/news
        public void Post([FromBody]string value) {
        }

        // PUT api/news/5
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE api/news/5
        public void Delete(int id) {
        }
    }
}
