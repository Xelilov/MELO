using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.EntityModel;
namespace WebApplication2.SimpleModel
{
    public class Context : SimpleContext
    {
        public Context():base("con") { Initializer(); }

        
        public SimpleDbSet<Drenaj> Drenaj { get; set; }
        public SimpleDbSet<Regions> Region { get; set; }
        public SimpleDbSet<Village> village { get; set; }
        public SimpleDbSet<Riverbandcs> riverband { get; set; }
        public SimpleDbSet<Device> Device { get; set; }
        public SimpleDbSet<Channels> Channel { get; set; }
        public override void Initializer()
        {            
            Drenaj = new SimpleDbSet<Drenaj>(_sqlConnection);
            Region = new SimpleDbSet<Regions>(_sqlConnection);
            village = new SimpleDbSet<Village>(_sqlConnection);
            riverband = new SimpleDbSet<Riverbandcs>(_sqlConnection);
            Device = new SimpleDbSet<Device>(_sqlConnection);
            Channel = new SimpleDbSet<Channels>(_sqlConnection);
        }
    }
}