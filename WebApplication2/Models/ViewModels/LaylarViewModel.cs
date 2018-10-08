using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.SimpleModel;

namespace WebApplication2.Models.ViewModels
{
    public class LaylarViewModel
    {
        public IEnumerable<Channels> Channels { get; set; }
        public IEnumerable<Device> Device { get; set; }
        

    }
}