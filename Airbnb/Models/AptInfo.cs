using Airbnb.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Airbnb.Models
{
    //-----Apartment's info class-----//

    //Fields
    public class AptInfo
    {
        int aptId;
        int rentDays;
        int totalCancelation;

        //Constructor
        public AptInfo(int aptId, int rentDays, int totalCancelation)
        {
            this.aptId = aptId;
            this.rentDays = rentDays;
            this.totalCancelation = totalCancelation;
        }

        //Empty constructor
        public AptInfo(){}

        //Getters + Setters
        public int AptId { get => aptId; set => aptId = value; }
        public int RentDays { get => rentDays; set => rentDays = value; }
        public int TotalCancelation { get => totalCancelation; set => totalCancelation = value; }

        //Data Services method - get info
        public List<AptInfo> GetInfo()
        {
            DataServices ds = new DataServices();
            return ds.GetAptInfo();
        }
    }
    //-----Apartment's info class-----//
}