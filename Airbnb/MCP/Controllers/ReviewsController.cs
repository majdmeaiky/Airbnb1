using Airbnb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Airbnb.Controllers
{
    //Reviews controller
    public class ReviewsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/Reviews/GetReviews")]
        //Routing for GetReviews
        public List<Review> GetReviews(string id)
        {
            Review rw = new Review();
            return rw.GetReviews(id);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("api/Reviews/GetReviews")]
        public int Post([FromBody]Review review)
        {
            return review.InsertReview();
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