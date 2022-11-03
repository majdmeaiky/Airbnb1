using Airbnb.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Airbnb.Models
{
    //-----Order class-----//

    //Fields
    public class Order
    {
        string mail;
        string apartmentName;
        int apartmentId;
        string since;
        string until;
     
        //Constructor
        public Order(string mail, string apartmentName, int apartmentId, string since, string until)
        {
            this.mail = mail;
            this.ApartmentName = apartmentName;
            this.apartmentId = apartmentId;
            this.since = since;
            this.until = until;
           
        }

        //Empty constructor
        public Order()
        {

        }

        //Getters + Setters
        public string Mail { get => mail; set => mail = value; }
        public string ApartmentName { get => apartmentName; set => apartmentName = value; }
        public int ApartmentId { get => apartmentId; set => apartmentId = value; }
        public string Since { get => since; set => since = value; }
        public string Until { get => until; set => until = value; }


        //-----Data Services Methods-----//

        //Add order
        public int AddOrder()
        {
            DataServices ds = new DataServices();
           return ds.InsertOrder(this);
        }

        //Get Orders for user
        public List<Order> getOrdersPerUser(string userEmail)
        {
            DataServices ds = new DataServices();
            return ds.getOrdersPerUser(userEmail);
        }

        //Delete order for user
        public void DeleteOrder(int id,string email)
        {
            DataServices ds = new DataServices();
            ds.DeleteOrder(id,email);
        }
        //----------//
    }
    //----------//
}
