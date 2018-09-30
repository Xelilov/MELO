using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.SimpleModel;


namespace WebApplication2.Models.ViewModels
{
    public class SebekelerViewModel
    {
        public IEnumerable<Drenaj> Drenaj { get; set; }
        public IEnumerable<Riverbandcs> riverbandcs { get; set; }
    }
}