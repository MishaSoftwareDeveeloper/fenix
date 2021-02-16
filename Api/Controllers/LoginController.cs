using Api.Helpers;
using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    public class LoginController : ApiController
    {
        // GET: api/Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Login
        public IHttpActionResult Post([FromBody] User user)
        {
            User fuser = null;
            string token = string.Empty;
            
            if (user != null)
            {
                fuser = new User() { 
                    id=1,
                    name = "Test",
                    mail=user.mail,
                    password = string.Empty,
                };
                token = TokenHelper.GenerateJSONWebToken(fuser);
            }

            return Ok(new { user = fuser, tokeninfo = token });
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
