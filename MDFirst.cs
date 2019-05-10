using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace First_Level_Test.Models
{
    public class MDFirst
    {
        public int ID {get;set;}
        public string Name { get; set; }
        public string Address { get; set; }
        public int City_ID{ get; set; }
        public string EmailID { get; set; }

        public string CityNAme { get; set; }
      //  public List<MDFirst> citylist { get; set; }
    }
}