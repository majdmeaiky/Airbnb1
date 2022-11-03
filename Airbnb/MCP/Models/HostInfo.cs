using Airbnb.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Airbnb.Models
{
    //-----Host info class-----//

    //Fields
    public class HostInfo
    {
        int hostId;
        string hostSince;
        int numOfApartments;
        
        int totalIncome;
        int totalcanCelation;

        //Constructor
        public HostInfo(int hostId, string hostSince, int numOfApartments, int totalIncome, int totalcanCelation)
        {
            this.hostId = hostId;
            this.hostSince = hostSince;
            this.NumOfApartments = numOfApartments;
       
            this.totalIncome = totalIncome;
            this.totalcanCelation = totalcanCelation;
        }
        //Empty constructor
        public HostInfo() { }

        //Getters + Setters
        public int HostId { get => hostId; set => hostId = value; }
        public string HostSince { get => hostSince; set => hostSince = value; }
      
        public int TotalIncome { get => totalIncome; set => totalIncome = value; }
        public int TotalcanCelation { get => totalcanCelation; set => totalcanCelation = value; }
        public int NumOfApartments { get => numOfApartments; set => numOfApartments = value; }

        //Data Services method - get host info
        public List<HostInfo> GetInfo()
        {
            DataServices ds = new DataServices();
            return ds.GetHostInfo();
        }
    }
    //----------//
}