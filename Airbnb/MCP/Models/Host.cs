using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Airbnb.Models
{
    //-----Host class-----//

    //Fields
    public class Host
    {
        int id;
        string name;
        DateTime since;
        string about;
        string picturePath;
        int numOfApartments;

        //Constructor
        public Host(int id, string name, DateTime since, string about, string picturePath, int numOfApartments)
        {
            this.id = id;
            this.name = name;
            this.since = since;
            this.about = about;
            this.picturePath = picturePath;
            this.numOfApartments = numOfApartments;
        }

        //Empty constructor
        public Host()
        {

        }

        //Getters + Setters
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public DateTime Since { get => since; set => since = value; }
        public string About { get => about; set => about = value; }
        public string PicturePath { get => picturePath; set => picturePath = value; }
        public int NumOfApartments { get => numOfApartments; set => numOfApartments = value; }
    }
    //----------//

}