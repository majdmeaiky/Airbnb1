using Airbnb.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Airbnb.Models
{
    //-----Review class-----//

    //Fields
    public class Review
    {
        int apartmentId;
        string reviewId;
        DateTime date;
        string reviewerName;
        string description;
        static int counter = 0;

        //Getters + Setters
        public int ApartmentId { get => apartmentId; set => apartmentId = value; }
        public string ReviewId { get => reviewId; set => reviewId = value; }
        public DateTime Date { get => date; set => date = value; }
        public string ReviewerName { get => reviewerName; set => reviewerName = value; }
        public string Description { get => description; set => description = value; }

        //Constructor
        public Review(int apartmentId, string ReviewId, DateTime date, string reviewerName, string description)
        {
            this.ApartmentId = apartmentId;
            this.ReviewId = ReviewId;
            this.Date = date;
            this.ReviewerName = reviewerName;
            this.Description = description;
        }
        public Review(int apartmentId, DateTime date, string reviewerName, string description)
        {
            this.ApartmentId = apartmentId;
            this.ReviewId = counter.ToString() + "R";
            this.Date = date;
            this.ReviewerName = reviewerName;
            this.Description = description;
            counter++;
        }

        //Empty constructor
        public Review()
        {

        }

       public int InsertReview()
        {
            DataServices ds = new DataServices();
            this.ReviewId = counter.ToString() + "R";
            counter++;
            return ds.InsertReview(this);
            
        }
        //Data Services method - get reviews
        public List<Review> GetReviews(string id)
        {
            DataServices ds = new DataServices();
            return ds.GetReviews(id);
        }
    }
    //----------//
}