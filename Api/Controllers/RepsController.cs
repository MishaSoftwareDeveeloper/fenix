using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Services;

namespace Api.Controllers
{
    public class RepsController : ApiController
    {
        public RepsController()
        {
            if(DB.repositories == null)
            {
                DB.repositories = new List<Repo>();
            }
        }

        // GET: api/Reps
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Reps/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Reps
        [WebMethod(EnableSession = true)]
        public void Post([FromBody]Repo r)
        {

            DB.repositories.Add(r);
            HttpContext.Current.Session["repositories"] = DB.repositories;
        }

        // PUT: api/Reps/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Reps/5
        public void Delete(int id)
        {
        }
    }
}
