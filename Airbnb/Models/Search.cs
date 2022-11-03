using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Airbnb.Models
{
    //-----Search class-----//

    //Fields
    public class Search
    {
        int bedroomsNum;
        double minPrice;
        double maxPrice;

        //Constructor
        public Search(int bedroomsNum, double minPrice, double maxPrice)
        {
            this.BedroomsNum = bedroomsNum;
            this.MinPrice = minPrice;
            this.MaxPrice = maxPrice;
        }

        //Empty constructor
        public Search()
        {

        }

        //Getters + Setters
        public int BedroomsNum { get => bedroomsNum; set => bedroomsNum = value; }
        public double MinPrice { get => minPrice; set => minPrice = value; }
        public double MaxPrice { get => maxPrice; set => maxPrice = value; }
    }
    //----------//
}