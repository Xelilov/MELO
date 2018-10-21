using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.SimpleModel
{
    public class Riverbandcs
    {
        public int OBJECTID { get; set; }
        public decimal TYPE { get; set; }
        public string NAME { get; set; }
        public string SHAPE { get; set; }
        public int Region_ID { get; set; }
        public decimal LENGTH { get; set; }
        public int Municipality_id { get; set; }
    }
}