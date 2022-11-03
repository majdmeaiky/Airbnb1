using Airbnb.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Airbnb.Models
{
    //-----Apartment class-----//

    //Fields
    public class Apartment
    {
        int id;
        string name;
        string description;
        string neighborhood_overview;
        string picturePath;
        string neighborhood;
        float latitude;
        float longitude;
        string property_type;
        string bathrooms_text;
        int numOfBedrooms;
        int numOfBeds;
        List<string> amenities;
        float price;
        int minimumNights;
        int maximumNights;
        double numberOfReviews;
        double reviewScoresRating;
        int hostId;
        int cancelationCounter;
        float distanceFromCenter;

        //Constructor
        public Apartment(int id, string name, string description, string neighborhood_overview, string picturePath, string neighborhood, float latitude, float longitude, string property_type, string bathrooms_text, int numOfBedrooms, int numOfBeds, List<string> amenities, float price, int minimumNights, int maximumNights, double numberOfReviews, double reviewScoresRating, int hostId, int cancelationCounter, float distanceFromCenter)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.neighborhood_overview = neighborhood_overview;
            this.picturePath = picturePath;
            this.neighborhood = neighborhood;
            this.latitude = latitude;
            this.longitude = longitude;
            this.property_type = property_type;
            this.bathrooms_text = bathrooms_text;
            this.numOfBedrooms = numOfBedrooms;
            this.numOfBeds = numOfBeds;
            this.amenities = amenities;
            this.price = price;
            this.minimumNights = minimumNights;
            this.maximumNights = maximumNights;
            this.numberOfReviews = numberOfReviews;
            this.reviewScoresRating = reviewScoresRating;
            this.HostId = hostId;
            this.cancelationCounter = cancelationCounter;
            this.DistanceFromCenter = distanceFromCenter;     
        }
        //Empty constructor
        public Apartment()
        {

        }

        //Getters + Setters
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string Neighborhood_overview { get => neighborhood_overview; set => neighborhood_overview = value; }
        public string PicturePath { get => picturePath; set => picturePath = value; }
        public string Neighborhood { get => neighborhood; set => neighborhood = value; }
        public float Latitude { get => latitude; set => latitude = value; }
        public float Longitude { get => longitude; set => longitude = value; }
        public string Property_type { get => property_type; set => property_type = value; }
        public string Bathrooms_text { get => bathrooms_text; set => bathrooms_text = value; }
        public int NumOfBedrooms { get => numOfBedrooms; set => numOfBedrooms = value; }
        public int NumOfBeds { get => numOfBeds; set => numOfBeds = value; }
        public List<string> Amenities { get => amenities; set => amenities = value; }
        public float Price { get => price; set => price = value; }
        public int MinimumNights { get => minimumNights; set => minimumNights = value; }
        public int MaximumNights { get => maximumNights; set => maximumNights = value; }
        public double NumberOfReviews { get => numberOfReviews; set => numberOfReviews = value; }
        public double ReviewScoresRating { get => reviewScoresRating; set => reviewScoresRating = value; }
        public int CancelationCounter { get => cancelationCounter; set => cancelationCounter = value; }
        public int HostId { get => hostId; set => hostId = value; }
        public float DistanceFromCenter { get => distanceFromCenter; set => distanceFromCenter = value; }

        //-----Data Services Methods-----//

        //Fetch apartments
        public List<Apartment> getApartments()
        {
            DataServices ds = new DataServices();
            return ds.getApartments();
        }

        //Fetch apartment based on ID
        public Apartment getApartment(int id)
        {
            DataServices ds = new DataServices();
            return ds.getApartment(id);
        }

        //Fetch apartments, based on several desired properties
        public List<Apartment> getApartmentsFromSearch(int reviewNum, int bedroomsNum, DateTime checkin, DateTime checkout, float MinPrice, float MaxPrice, int MinDistance, int MaxDistance)
        {
            DataServices ds = new DataServices();
            return ds.getApartmentsFromSearch(reviewNum,bedroomsNum, checkin, checkout, MinPrice, MaxPrice, MinDistance, MaxDistance);
        }

        //Update cancelation counter
        public void UpdateCancelationCounter(int id)
        {
            DataServices ds = new DataServices();
            ds.UpdateApartmentCancelationCounter(id);
        }
        //----------//
    }
    //----------//
}