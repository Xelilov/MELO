using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.EntityModel;
namespace WebApplication2.SimpleModel
{
    public class Context : SimpleContext
    {
        public Context() : base("con") { Initializer(); }


        public SimpleDbSet<Drenaj> Drenaj { get; set; }
        public SimpleDbSet<Regions> Region { get; set; }
        public SimpleDbSet<Village> village { get; set; }
        public SimpleDbSet<Riverbandcs> riverband { get; set; }
        public SimpleDbSet<Device> Device { get; set; }
        public SimpleDbSet<Channels> Channel { get; set; }
        public SimpleDbSet<Wells> Well { get; set; }
        public SimpleDbSet<ChannelType> channeltype { get; set; }
        public SimpleDbSet<DrenajType> drenajtype { get; set; }
        public SimpleDbSet<RiverbandType> riverbandtype { get; set; }
        public SimpleDbSet<WellType> welltype { get; set; }
        public override void Initializer()
        {            
            Drenaj = new SimpleDbSet<Drenaj>(_sqlConnection);
            Region = new SimpleDbSet<Regions>(_sqlConnection);
            village = new SimpleDbSet<Village>(_sqlConnection);
            riverband = new SimpleDbSet<Riverbandcs>(_sqlConnection);
            Device = new SimpleDbSet<Device>(_sqlConnection);
            Channel = new SimpleDbSet<Channels>(_sqlConnection);
            Well = new SimpleDbSet<Wells>(_sqlConnection);
            channeltype = new SimpleDbSet<ChannelType>(_sqlConnection);
            drenajtype = new SimpleDbSet<DrenajType>(_sqlConnection);
            riverbandtype = new SimpleDbSet<RiverbandType>(_sqlConnection);
            welltype = new SimpleDbSet<WellType>(_sqlConnection);

        }
    }
}