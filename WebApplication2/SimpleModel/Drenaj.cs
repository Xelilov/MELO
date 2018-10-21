using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.SimpleModel
{
    public class Drenaj
    {
        public int OBJECTID { get; set; }
        public string TYPE { get; set; }
        public string NAME { get; set; }
        public string shape { get; set; }
        public int Region_ID { get; set; }
        public decimal FACTICAL_LENGTH { get; set; }
        public int Municipality_id { get; set; }
    }
}