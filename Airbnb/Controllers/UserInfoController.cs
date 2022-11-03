using Airbnb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Airbnb.Controllers
{
    //User info controller
    public class UserInfoController : ApiController
    {
        [HttpGet]
        [Route("api/UserInfo/Getinfo")]
        //Routing for Getinfo
        public List<UserInfo> Get()
        {
            UserInfo ui = new UserInfo();
            return ui.GetInfo();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}