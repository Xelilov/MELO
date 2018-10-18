using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.SimpleModel;


namespace WebApplication2.Models.ViewModels
{
    public class SebekelerViewModel
    {
        //public List<MUltiSelectDrenaj> DrenajselectList { get; set; }
        //public List<MultiSelectRiverband> RiverbandselectList { get; set; }
        //public List<MultiSelectWell> WellselectList { get; set; }
        //public List<ChannelType> ChannelTypes { get; set; }

        public List<Channels> SelectedChannels { get; set; }
        public List<Drenaj> SelectedDrenajs { get; set; }
        public List<Riverbandcs> SelectedRiverband { get; set; }
        public List<Wells> SelectedWell { get; set; }
        public List<Device> SelectedDevice { get; set; }
        public List<Departments> SelectedDepartments { get; set; }
        public List<Pumpstation> SelectedPumpstation { get; set; }
        public List<Buildings> SelectedBuilding { get; set; }
        public List<Exploitationroad> SelectedExploitationroad { get; set; }

    }
}