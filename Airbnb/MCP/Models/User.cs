using Airbnb.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Airbnb.Models
{
    //-----User class-----//

    //Fields
    public class User
    {
        string mail;
        string password;
        bool manager;
        int canclecounter;
        DateTime signedfrom;

        //Getters + Setters
        public string Mail { get => mail; set => mail = value; }
        public string Password { get => password; set => password = value; }
        public bool Manager { get => manager; set => manager = value; }
        public int Canclecounter { get => canclecounter; set => canclecounter = value; }
        public DateTime Signedfrom { get => signedfrom; set => signedfrom = value; }

        //Constructor (new user)
        public User(string mail, string password, bool manager)
        {
            this.Mail = mail;
            this.Password = password;
            this.Manager = manager;
            this.Canclecounter = 0;
            this.Signedfrom = DateTime.Now;
        }

        //Constructor (existing user)
        public User(string mail, string password, bool manager,int canclecounter, DateTime signedfrom)
        {
            this.Mail = mail;
            this.Password = password;
            this.Manager = manager;
            this.Canclecounter = canclecounter;
            this.Signedfrom = signedfrom;
        }

        //Empty constructor
        public User()
        {

        }

        //-----Data Services Methods-----//

        //Update cancelation counter
        public void UpdateCancelation(string email)
        {
            DataServices ds = new DataServices();
            ds.UpdateUserCancelationCounter(email);
        }
     
        //Get user
        public User GetUser()
        {
            DataServices ds = new DataServices();
            return ds.readUser(this);
        }

        //Insert user
        public int Insertuser()
        {
            DataServices ds = new DataServices();
            if (ds.insertUser(this) == -10)
            {
                return -10;
            }         
            return 1;
        }
        //----------//
    }
    //----------//
}