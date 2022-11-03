using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Airbnb.Models.DAL
{
    // DataServices for Airbnb
    public class DataServices
    {
        public static List<User> Userslist = new List<User>();
        public static List<Apartment> Apartmnentslist = new List<Apartment>();
        public static List<Order> ordersPerUserslist = new List<Order>();
        public static List<Review> reviewsPerApartment = new List<Review>();

        // Database connection
        private SqlConnection Connect()
        {
            // read the connection string from the web.config file
            string connectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            // create the connection to the db
            SqlConnection con = new SqlConnection(connectionString);

            // open the database connection
            con.Open();

            return con;

        }

        // Fetch users query
        private SqlCommand CreateSelectUsersCommand(SqlConnection con)
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = "selectUsers";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }
        //=========Possible order conflict methods=========//
        // check for conflict
        public int CheckOrderConflicts(Order order)
        {
            SqlConnection con = Connect();
            //string commandStr = "SELECT * FROM Users";
            SqlCommand command = CreateCheckOrderConflicts(con, order);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            Userslist.Clear();
            while (dr.Read())
            {
                return 1; // Conflict exists
            }
            return 0; // No conflict exists
        }
        
        //Check for conflicts query
        public SqlCommand CreateCheckOrderConflicts(SqlConnection con, Order order)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", order.ApartmentId);
            command.Parameters.AddWithValue("@since", order.Since);
            command.Parameters.AddWithValue("@until", order.Until);

            command.CommandText = "checkOrderConflict";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }
        //==================//


        //=========Order insertion methods=========//

        // Insert order
        public int InsertOrder(Order order)
        {
            SqlConnection con = Connect();
            int checkConflict = CheckOrderConflicts(order);
            if (checkConflict == 1)
            {
                return 1;
            }

            // Create Command
            SqlCommand command = CreateInsertOrderCommand(con, order);

            // Execute
            int numAffected = command.ExecuteNonQuery();

            // Close Connection

            con.Close();

            return 0;
        }

        //Insert order query
        private SqlCommand CreateInsertOrderCommand(SqlConnection con, Order order)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@mail", order.Mail);
            command.Parameters.AddWithValue("@name", order.ApartmentName);
            command.Parameters.AddWithValue("@id", order.ApartmentId);
            command.Parameters.AddWithValue("@since", order.Since);
            command.Parameters.AddWithValue("@until", order.Until);
            command.CommandText = "insertOrder";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            // command.CommandTimeout = 10; // in seconds
            command.CommandTimeout = 10;
            return command;
        }
        //==================//

        //=========User's methods=========//

        //Fetch user to render
        public User readUser(User user)
        {
            SqlConnection con = Connect();
            //string commandStr = "SELECT * FROM Users";
            SqlCommand command = CreateSelectUsersCommand(con);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            Userslist.Clear();
            while (dr.Read())
            {

                string mail1 = dr["mail"].ToString();
                string password1 = dr["password"].ToString();
                bool isManager = Convert.ToBoolean(dr["is_Manager"]);
                int canclecounter = Int32.Parse(dr["canclecounter"].ToString());
                DateTime signedfrom = Convert.ToDateTime(dr["signedfrom"].ToString());
                Userslist.Add(new User(mail1, password1, isManager, canclecounter, signedfrom));
            }

            for (int i = 0; i < Userslist.Count; i++)
            {
                if (Userslist[i].Mail == user.Mail && Userslist[i].Password == user.Password)
                {
                    return Userslist[i];

                }
            }

            return null;

        }

        //Insert user query
        private SqlCommand CreateInsertUserCommand(SqlConnection con, User user)
        {

            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@mail", user.Mail);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@manager", user.Manager);

            command.CommandText = "insertToUsers";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 100; // in seconds

            return command;
        }
        
        //Insert user
        public int insertUser(User user)
        {
            try {
                SqlConnection con = Connect();

                // Create Command
                SqlCommand command = CreateInsertUserCommand(con, user);

                // Execute
                int numAffected = command.ExecuteNonQuery();

                // Close Connection

                con.Close();
                return numAffected;
            }

            catch
            {
                return -10;
            }



        }
        //==================//

        //=========Apartment's methods=========//

        // Fetch apartments query
        private SqlCommand CreateSelectApartmentsCommand(SqlConnection con)
        {

            SqlCommand command = new SqlCommand();


            command.CommandText = "selectApartments";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }

        //Fetch apartments to render
        public List<Apartment> getApartments()
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateSelectApartmentsCommand(con);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            Apartmnentslist.Clear();
            while (dr.Read())
            {

                int id = Int32.Parse(dr["id"].ToString());
                string name = dr["name"].ToString();
                string description = dr["description"].ToString();
                string neighborhood_overview = dr["neighborhood_overview"].ToString();
                string picturePath = dr["picture_url"].ToString();
                string neighborhood = dr["neighbourhood"].ToString();
                float latitude = float.Parse(dr["latitude"].ToString());
                float longitude = float.Parse(dr["longitude"].ToString());
                string property_type = dr["property_type"].ToString();
                string bathrooms_text = dr["bathrooms_text"].ToString();
                int numOfBedrooms;
                if (dr["bedrooms"].ToString() == "")
                {
                    numOfBedrooms = 0;
                }
                else
                {
                    numOfBedrooms = Int32.Parse(dr["bedrooms"].ToString());
                }
                int numOfBeds = Int32.Parse(dr["beds"].ToString());
                string arr = dr["amenities"].ToString();
                JArray jarray = JArray.Parse(arr);
                List<string> amenities = new List<string>();
                foreach (string item in jarray)
                {
                    amenities.Add(item.ToString());
                }
                float price = float.Parse(dr["price"].ToString());
                int minimumNights = Int32.Parse(dr["minimum_nights"].ToString());
                int maximumNights = Int32.Parse(dr["maximum_nights"].ToString());
                double numberOfReviews = Convert.ToDouble(dr["number_of_reviews"].ToString());
                double reviewScoresRating = Convert.ToDouble(dr["review_scores_rating"].ToString());
                int hostId = Int32.Parse(dr["host_id"].ToString());
                // int rentalDays = Int32.Parse(dr["rental days"].ToString());
                int cancelationCounter = Int32.Parse(dr["cancelation counter"].ToString());
                float distance = float.Parse(dr["distance"].ToString());
                Apartmnentslist.Add(new Apartment(id, name, description, neighborhood_overview, picturePath, neighborhood, latitude, longitude, property_type, bathrooms_text, numOfBedrooms, numOfBeds, amenities, price, minimumNights, maximumNights, numberOfReviews, reviewScoresRating, hostId, cancelationCounter, distance));
                amenities.Clear();
            }
            return Apartmnentslist;
        }
        //Fetch apartment based on its ID query
        private SqlCommand CreateSelectApartmentCommand(SqlConnection con, int id)
        {

            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", id);
            command.CommandText = "selectApartment";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }

        //Fetch apartment based on its id - render
        public Apartment getApartment(int id1)
        {

            SqlConnection con = Connect();
            SqlCommand command = CreateSelectApartmentCommand(con, id1);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {

                int id = Int32.Parse(dr["id"].ToString());
                string name = dr["name"].ToString();
                string description = dr["description"].ToString();
                string neighborhood_overview = dr["neighborhood_overview"].ToString();
                string picturePath = dr["picture_url"].ToString();
                string neighborhood = dr["neighbourhood"].ToString();
                float latitude = float.Parse(dr["latitude"].ToString());
                float longitude = float.Parse(dr["longitude"].ToString());
                string property_type = dr["property_type"].ToString();
                string bathrooms_text = dr["bathrooms_text"].ToString();
                int numOfBedrooms;
                if (dr["bedrooms"].ToString() == "")
                {
                    numOfBedrooms = 0;
                }
                else
                {
                    numOfBedrooms = Int32.Parse(dr["bedrooms"].ToString());
                }
                int numOfBeds = Int32.Parse(dr["beds"].ToString());

                string arr = dr["amenities"].ToString();
                JArray jarray = JArray.Parse(arr);
                List<string> amenities = new List<string>();
                foreach (string item in jarray)
                {
                    amenities.Add(item.ToString());
                }
                float price = float.Parse(dr["price"].ToString());
                int minimumNights = Int32.Parse(dr["minimum_nights"].ToString());

                int maximumNights = Int32.Parse(dr["maximum_nights"].ToString());
                double numberOfReviews = Convert.ToDouble(dr["number_of_reviews"].ToString());
                double reviewScoresRating = Convert.ToDouble(dr["review_scores_rating"].ToString());
                int hostId = Int32.Parse(dr["host_id"].ToString());
                //int rentalDays = Int32.Parse(dr["rental days"].ToString());
                int cancelationCounter = Int32.Parse(dr["cancelation counter"].ToString());
                float distance = float.Parse(dr["distance"].ToString());
                Apartment tmp = new Apartment(id, name, description, neighborhood_overview, picturePath, neighborhood, latitude, longitude, property_type, bathrooms_text, numOfBedrooms, numOfBeds, amenities, price, minimumNights, maximumNights, numberOfReviews, reviewScoresRating, hostId, cancelationCounter, distance);
                return tmp;
            }
            return null;

        }

        //Fetch apartments to show, based on several desired properties - query
        private SqlCommand CreateSelectApartmentFromSearchCommand(SqlConnection con, int reviewNum, int bedroomsNum, DateTime checkin, DateTime checkout, float MinPrice, float MaxPrice, int MinDistance, int MaxDistance)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@reviewNum", reviewNum);
            command.Parameters.AddWithValue("@bedrooms", bedroomsNum);
            command.Parameters.AddWithValue("@checkin", checkin);
            command.Parameters.AddWithValue("@checkout", checkout);
            command.Parameters.AddWithValue("@MinPrice", MinPrice);
            command.Parameters.AddWithValue("@MaxPrice", MaxPrice);
            command.Parameters.AddWithValue("@MinDistance", MinDistance);
            command.Parameters.AddWithValue("@MaxDistance", MaxDistance);
            command.CommandText = "selectApartmentsFromSearch";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;

            return command;
        }

        //Render apartments from the above search
        public List<Apartment> getApartmentsFromSearch(int reviewNum, int bedroomsNum, DateTime checkin, DateTime checkout, float MinPrice, float MaxPrice, int MinDistance, int MaxDistance)
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateSelectApartmentFromSearchCommand(con, reviewNum, bedroomsNum, checkin, checkout, MinPrice, MaxPrice, MinDistance, MaxDistance);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            Apartmnentslist.Clear();
            while (dr.Read())
            {
                int id = Int32.Parse(dr["id"].ToString());
                string name = dr["name"].ToString();
                string description = dr["description"].ToString();
                string neighborhood_overview = dr["neighborhood_overview"].ToString();
                string picturePath = dr["picture_url"].ToString();
                string neighborhood = dr["neighbourhood"].ToString();
                float latitude = float.Parse(dr["latitude"].ToString());
                float longitude = float.Parse(dr["longitude"].ToString());
                string property_type = dr["property_type"].ToString();
                string bathrooms_text = dr["bathrooms_text"].ToString();
                int numOfBedrooms;
                if (dr["bedrooms"].ToString() == "")
                {
                    numOfBedrooms = 0;
                }
                else
                {
                    numOfBedrooms = Int32.Parse(dr["bedrooms"].ToString());
                }
                int numOfBeds = Int32.Parse(dr["beds"].ToString());
                string arr = dr["amenities"].ToString();
                JArray jarray = JArray.Parse(arr);
                List<string> amenities = new List<string>();
                foreach (string item in jarray)
                {
                    amenities.Add(item.ToString());
                }
                float price = float.Parse(dr["price"].ToString());
                int minimumNights = Int32.Parse(dr["minimum_nights"].ToString());
                int maximumNights = Int32.Parse(dr["maximum_nights"].ToString());
                double numberOfReviews = Convert.ToDouble(dr["number_of_reviews"].ToString());
                double reviewScoresRating = Convert.ToDouble(dr["review_scores_rating"].ToString());
                int hostId = Int32.Parse(dr["host_id"].ToString());
                //int rentalDays = Int32.Parse(dr["rental days"].ToString());
                int cancelationCounter = Int32.Parse(dr["cancelation counter"].ToString());
                float distance = float.Parse(dr["distance"].ToString());

                Apartmnentslist.Add(new Apartment(id, name, description, neighborhood_overview, picturePath, neighborhood, latitude, longitude, property_type, bathrooms_text, numOfBedrooms, numOfBeds, amenities, price, minimumNights, maximumNights, numberOfReviews, reviewScoresRating, hostId, cancelationCounter, distance));
                amenities.Clear();
            }
            return Apartmnentslist;
        }
        //==================//

        //=========User's order methods=========//

        //Selection query for user's orders
        private SqlCommand createSelectOrders(SqlConnection con, string email)
        {

            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@email", email);

            command.CommandText = "selectOrdersPerUser";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            //   command.CommandTimeout = 10; // in seconds

            return command;
        }

        //Rendering user's orders
        public List<Order> getOrdersPerUser(string userEmail)
        {
            SqlConnection con = Connect();
            SqlCommand command = createSelectOrders(con, userEmail);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            ordersPerUserslist.Clear();
            while (dr.Read())
            {
                string mail = dr["mail"].ToString();
                string apartmentName= dr["apartment_Name"].ToString();
                int apartmentId = Int32.Parse(dr["apartment_Id"].ToString());
                DateTime since = Convert.ToDateTime(dr["since"].ToString());
                string since1 = since.ToLongDateString();
                DateTime until = Convert.ToDateTime(dr["until"].ToString());
                string until1 = until.ToLongDateString();

                ordersPerUserslist.Add(new Order(mail, apartmentName, apartmentId, since1, until1));
            }
            return ordersPerUserslist;
        }

        //Deletion query for user's order
        private SqlCommand DeleteOrderCommand(SqlConnection con, int id, string email)
        {

            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@email", email);
            command.CommandText = "DeleteOrderCommand";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            //   command.CommandTimeout = 10; // in seconds

            return command;
        }
        //Delete order
        public int DeleteOrder(int id, string email )
        {
           
            SqlConnection con = Connect();
            SqlCommand command = DeleteOrderCommand(con, id, email);


            int numAffected = command.ExecuteNonQuery();

            // Close Connection

            con.Close();
            return numAffected;
        }
        //==================//

        //=========Cancelation counter methods=========//

        //----Users----//
        //Update user's cancelation counter
        public int UpdateUserCancelationCounter(string email)
        {
            try
            {
                SqlConnection con = Connect();

                // Create Command
                SqlCommand command = CreateUpdateUserCancelationCounter(con, email);

                // Execute
                int numAffected = command.ExecuteNonQuery();

                // Close Connection

                con.Close();
                return numAffected;
            }

            catch
            {
                return -10;
            }
        }

        //Update user's cancelation counter query
        private SqlCommand CreateUpdateUserCancelationCounter(SqlConnection con, string email)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@email", email);

            command.CommandText = "UpdateCancelationCounter";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            //   command.CommandTimeout = 10; // in seconds

            return command;
        }
        //--------//

        //----Apartments----//
        //Same for apartment, update it's cancelation counter
        public int UpdateApartmentCancelationCounter(int id)
        {
            try
            {
                SqlConnection con = Connect();

                // Create Command
                SqlCommand command = CreateUpdateApartmentCancelationCounter(con, id);

                // Execute
                int numAffected = command.ExecuteNonQuery();

                // Close Connection

                con.Close();
                return numAffected;
            }

            catch
            {
                return -10;
            }
        }

        //Update apartment's cancelation counter query
        private SqlCommand CreateUpdateApartmentCancelationCounter(SqlConnection con, int id)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", id);

            command.CommandText = "UpdateAptCancelationCounter";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            //   command.CommandTimeout = 10; // in seconds

            return command;
        }
        //--------//
        //==================//

        //=========Apartment's reviews methods=========//
        //post review
        public int InsertReview(Review review)
        {
            try
            {
                SqlConnection con = Connect();

                // Create Command
                SqlCommand command = CreateInsertReviewCommand(con, review);

                // Execute
                int numAffected = command.ExecuteNonQuery();

                // Close Connection

                con.Close();
                return numAffected;
            }

            catch
            {
                return -10;
            }
        }
        //post review query
       private SqlCommand CreateInsertReviewCommand(SqlConnection con , Review review)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@aptid", review.ApartmentId);
            command.Parameters.AddWithValue("@revid", review.ReviewId);
            command.Parameters.AddWithValue("@date", review.Date);
            command.Parameters.AddWithValue("@name", review.ReviewerName);
            command.Parameters.AddWithValue("@description", review.Description);
            


            command.CommandText = "AddNewReview";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            //   command.CommandTimeout = 10; // in seconds

            return command;
        }

        //--------------
        private SqlCommand createSelectReviewsPerApartment(SqlConnection con, string id)
        {

            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", id);

            command.CommandText = "selectReviewsPerApartment";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            //   command.CommandTimeout = 10; // in seconds

            return command;
        }

        //Get reviews for render
        public List<Review> GetReviews(string id)
        {
            SqlConnection con = Connect();
            SqlCommand command = createSelectReviewsPerApartment(con, id);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            reviewsPerApartment.Clear();
            while (dr.Read())
            {

                int apartmentId = Int32.Parse(dr["listing_id"].ToString());
                string reviewId = dr["id"].ToString();
                DateTime date = Convert.ToDateTime(dr["date"].ToString());
                string reviewerName = dr["reviewer_name"].ToString();
                string description = dr["comments"].ToString();

                reviewsPerApartment.Add(new Review(apartmentId, reviewId, date, reviewerName, description));
            }
            return reviewsPerApartment;
        }
        //==================//

        //=========Admin privileges methods=========//
        //----User's info methods----//
        public List<UserInfo> GetUserInfo()
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateGetUserInfo(con);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<UserInfo> infoList = new List<UserInfo>();
            while (dr.Read())
            {

                string email = (dr["mail"].ToString());
                int numOfOrders = Int32.Parse(dr["numberOfOrders"].ToString());

                DateTime signedfrom = Convert.ToDateTime(dr["signedfrom"].ToString());
               
                string signedfrom1 = signedfrom.ToLongDateString();
                string totalpaymentsStr = dr["revenew"].ToString();
                int totalpayments;
                if (totalpaymentsStr == null || totalpaymentsStr == "")
                {
                    totalpayments = 0;
                }
                else
                {
                    totalpayments = Int32.Parse(totalpaymentsStr);
                }
                int canclecounter = Int32.Parse(dr["canclecounter"].ToString());
                infoList.Add(new UserInfo(email, signedfrom1, numOfOrders, totalpayments, canclecounter));

            }
            return infoList;
        }
        //Create method
        private SqlCommand CreateGetUserInfo(SqlConnection con)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "GetUserInfo";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            //   command.CommandTimeout = 10; // in seconds

            return command;
        }
        //--------//

        //----Host's info methods----//
        public List<HostInfo> GetHostInfo()
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateGetHostInfo(con);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
           List<HostInfo> infoList = new List<HostInfo>();
            while (dr.Read())
            {

                int id  = Int32.Parse(dr["host_id"].ToString());
               

                DateTime host_since = Convert.ToDateTime(dr["host_since"].ToString());
                string host_since1 = host_since.ToLongDateString();
                int numOfApartments = Int32.Parse(dr["numberofapartments"].ToString());
                string totalRevenewSTR = dr["total_revenew"].ToString();
                int totalRevenew;
                if (totalRevenewSTR == null|| totalRevenewSTR=="")
                {
                    totalRevenew = 0;
                }
                else
                {
                    totalRevenew = Int32.Parse(totalRevenewSTR);
                }
                int canclecounter = Int32.Parse(dr["total_cancelation"].ToString());
                infoList.Add(new  HostInfo(id, host_since1, numOfApartments, totalRevenew, canclecounter));
            }
            return infoList;
        }       
       
        //Fetch host info query
    private SqlCommand CreateGetHostInfo(SqlConnection con)
    {
        SqlCommand command = new SqlCommand();
        command.CommandText = "GetHostInfo";
        command.Connection = con;
        command.CommandType = System.Data.CommandType.StoredProcedure;
        //   command.CommandTimeout = 10; // in seconds

        return command;
    }

        //--------//

        //----Apartment's info methods----//

        //Apartment's info render

        public List<AptInfo> GetAptInfo()
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateGetAptInfo(con);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<AptInfo> infoList = new List<AptInfo>();
            while (dr.Read())
            {

                int id = Int32.Parse(dr["id"].ToString());



                string totalReserverSTR = dr["total_days_reserved"].ToString();
                int totalReserver;
                if (totalReserverSTR == null || totalReserverSTR=="")
                {
                    totalReserver = 0;
                }
                else
                {
                    totalReserver = Int32.Parse(totalReserverSTR);
                }
                int canclecounter = Int32.Parse(dr["cancelation counter"].ToString());
                infoList.Add(new AptInfo(id, totalReserver, canclecounter));

            }
            return infoList;

        }

        //Apartment's info query
        private SqlCommand CreateGetAptInfo(SqlConnection con)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "GetAptInfo";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            //   command.CommandTimeout = 10; // in seconds

            return command;
        }
        //--------//

        //==================//
    }
}