using Airbnb.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Airbnb.Controllers
{
    //Apartments controller
    public class ApartmentsController : ApiController
    {
       
        // GET api/<controller>
        [HttpGet]
        public List<Apartment> Get()
        {
            Apartment ap = new Apartment();
            return ap.getApartments();
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/Apartments/GetApartment")]
        //Routing for getApartment
        public Apartment GetApartment(int id)
        {
            Apartment ap = new Apartment();
            return ap.getApartment(id);
        }

        [HttpGet]
        [Route("api/Apartments/GetApartmentsFromSearch")]
        //Routing for GetApartmentsFromSearch
        public List<Apartment> GetApartmentsFromSearch(int ReviewNum, int BedroomsNum, DateTime CheckIn, DateTime CheckOut, float MinPrice, float MaxPrice, int MinDistance, int MaxDistance)
        {
            Apartment ap = new Apartment();
            return ap.getApartmentsFromSearch(ReviewNum, BedroomsNum, CheckIn, CheckOut, MinPrice, MaxPrice, MinDistance, MaxDistance);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("api/Apartments/UpdateApartmentCancelationCounter")]
        //Routing for UpdateApartmentCancelationCounter
        public void Put(int id)
        {
            Apartment a = new Apartment();
            a.UpdateCancelationCounter(id);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}