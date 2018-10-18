using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.SimpleModel;

namespace WebApplication2.Models.ViewModels
{
    public class IndexViewModel
    {
        public List<Regions> Regions { get; set; }
        public List<ChannelType> Channels { get; set; }
        public IEnumerable<DrenajType> drejtype { get; set; }
    }
}