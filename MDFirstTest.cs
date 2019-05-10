using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace First_Level_Test.Models
{
    public class MDFirstTest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string EmailID { get; set; }
       public string Is_Act { get; set; }
       public List<MDFirstTest> cityname { get; set; }
    }
}