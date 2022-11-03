using Airbnb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Airbnb.Controllers
{
    //Apartment info controller
    public class AptInfoController : ApiController
    {
        [HttpGet]
        [Route("api/AptInfo/Getinfo")]
        //Routing for Getinfo
        public List<AptInfo> Get()
        {
            AptInfo ai = new AptInfo();
            return ai.GetInfo();
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