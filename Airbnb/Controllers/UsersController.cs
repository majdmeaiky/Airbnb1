using Airbnb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Airbnb.Controllers
{
    //Users controller
    public class UsersController : ApiController
    {
        [HttpGet]
        // GET api/<controller>/5

        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>

        [HttpPost]
        public int Post([FromBody]User user)
        {
            if (user.Insertuser() == -10)
            {
                return -10;
            }
            else
            {
                return 1;
            }

        }

        [HttpPost]
        [Route("api/Users/PostToGetUser")]
        //Routing for PostToGetUser
        public User PostToGetUser([FromBody]User user)
        {
            try
            {
                return user.GetUser();
            }
            catch(Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
           

        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("api/Users/UpdateCancelationCounter")]
        //Routing for UpdateCancelationCounter
        public void Put(string email)
        {
            User u = new User();
            u.UpdateCancelation(email);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}