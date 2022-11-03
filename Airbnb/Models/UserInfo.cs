using Airbnb.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Airbnb.Models
{
    //-----User info class-----//


    //Fields
    public class UserInfo
    {
        string email;
        string signedFrom;
        int rentNum;
        int totalPayments;
        int cancelationCounter;

        //Constructor
        public UserInfo(string email, string signedFrom, int rentNum, int totalPayments, int cancelationCounter)
        {

            this.email = email;
            this.signedFrom = signedFrom;
            this.rentNum = rentNum;
            this.totalPayments = totalPayments;
            this.cancelationCounter = cancelationCounter;
        }

        //Empty constructor
        public UserInfo(){}

        //Getters + Setters
        public string Email { get => email; set => email = value; }
        public int RentNum { get => rentNum; set => rentNum = value; }
        public int TotalPayments { get => totalPayments; set => totalPayments = value; }
        public int CancelationCounter { get => cancelationCounter; set => cancelationCounter = value; }
        public string SignedFrom { get => signedFrom; set => signedFrom = value; }

        public List<UserInfo> GetInfo()
        {
            DataServices ds = new DataServices();
            return ds.GetUserInfo();
        }
    }
    //----------//
}