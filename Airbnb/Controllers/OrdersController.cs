using Airbnb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Airbnb.Controllers
{
    //Orders controller
    public class OrdersController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [Route("api/Orders/Get")]
        //Routing for Get
        public List<Order> Get(string userEmail)
        {
            Order or = new Order();
            return or.getOrdersPerUser(userEmail);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("api/Orders/insertorder")]
        //Routing for insertorder
        public int Post([FromBody]Order order)
        {
           return order.AddOrder();
            
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("api/Orders/Delete")]
        //Routing for Delete
        public void Delete(int id,string email)
        {
            Order or = new Order();
             or.DeleteOrder(id,email);
        }
    }
}