using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.ViewModels;
using WebApplication2.SimpleModel;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        MelogisEntities db = new MelogisEntities();        
        static List<Regions> RegionList = new List<Regions>();
        static List<Village> VillageList = new List<Village>();
        static List<Drenaj> DrenajList = new List<Drenaj>();
        static List<Riverbandcs> RiverbandList = new List<Riverbandcs>();
        static List<Device> DeviceList = new List<Device>();
        static List<Channels> ChannelList = new List<Channels>();
        static List<Wells> WellList = new List<Wells>();
        static List<ChannelType> ChTypeList = new List<ChannelType>();
        static List<DrenajType> drejTypeList = new List<DrenajType>();
        static List<RiverbandType> rbTypeList = new List<RiverbandType>();
        static List<WellType> welltypeList = new List<WellType>();

        public ActionResult Login()
        {
            using (Context con = new Context())
            {
                RegionList = con.Region.QueryStringAsList("select * from Meloregions").ToList();
                VillageList = con.village.QueryStringAsList($"select * from RESIDENTIALAREA").ToList();
                DrenajList = con.Drenaj.QueryStringAsList($"select SHAPE.STAsText() as shape,TYPE,OBJECTID, Region_ID from DRENAJ").ToList();
                RiverbandList = con.riverband.QueryStringAsList("select SHAPE.STAsText() as shape,TYPE,OBJECTID,Region_ID from RIVERBAND").ToList();
                DeviceList = con.Device.QueryStringAsList("select SHAPE.STAsText() as shape, NAME,OBJECTID,Municipality_id from DEVICE").ToList();
                ChannelList = con.Channel.QueryStringAsList("select SHAPE.STAsText() as shape,SERVED_AREA, TYPE, NAME,OBJECTID,Municipality_id,Region_ID from CHANNELS").ToList();
                WellList = con.Well.QueryStringAsList("select SHAPE.STAsText() as shape,OBJECTID ,Region_ID,WELL_TYPE from WELL").ToList();
                ChTypeList = con.channeltype.QueryStringAsList("select distinct type from CHANNELS").ToList();
                drejTypeList = con.drenajtype.QueryStringAsList("select distinct type, Region_ID from DRENAJ").ToList();
                rbTypeList = con.riverbandtype.QueryStringAsList("select distinct type, Region_ID from RIVERBAND").ToList();
                welltypeList = con.welltype.QueryStringAsList("select distinct WELL_TYPE, Region_ID from Well").ToList();

            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username,string pass)
        {
            try
            {
                tblUser user = db.tblUsers.Where(s => s.UserName == username && s.Password == pass).First();
                if (db.tblUsers.Where(s => s.UserName == username && s.Password == pass).First() != null)
                {
                    Session["UserLogin"] = true;
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return RedirectToAction("Login");
            }

            return RedirectToAction("Login");
        }
        [UserAuthenticationController]
        public ActionResult Index()
        { 
            IndexViewModel IndexVM = new IndexViewModel();
            using (Context con=new Context())
            {               
                IndexVM.Regions = con.Region.QueryStringAsList("select * from Meloregions").ToList();
                IndexVM.Channels = ChTypeList;

            }
            return View(IndexVM);
        }


        public ActionResult Attributes(int[] id, int[] idVil, string[] Attr)
        {
            List<ChannelMultiSelectList> ChanelsAttribute = new List<ChannelMultiSelectList>();
            List<Channels> AllChannels = new List<Channels>();
            List<ChannelAttr> AttributList = new List<ChannelAttr>();
            if (id!=null)
            {
                for (int i = 0; i < id.Length; i++)
                {
                    if (id[i] == 0)
                    {
                        ChanelsAttribute.Add(new ChannelMultiSelectList
                        {
                            MultiSeletChanell = ChannelList
                        });
                        break;
                    }
                    else
                    {
                        if (idVil != null)
                        {
                            for (int x = 0; x < idVil.Length; x++)
                            {
                                if (idVil[i] == 0)
                                {

                                }
                                else
                                {
                                    ChanelsAttribute.Add(new ChannelMultiSelectList
                                    {
                                        MultiSeletChanell = ChannelList.Where(s => s.Region_ID == id[i] && s.Municipality_id == idVil[x]).ToList()
                                    });
                                }
                            }
                        }
                        else
                        {
                            ChanelsAttribute.Add(new ChannelMultiSelectList
                            {
                                MultiSeletChanell = ChannelList.Where(s => s.Region_ID == id[i]).ToList()
                            });
                        }
                    }
                }
            }
            int count = 0;
            decimal length = 0;
            if (ChanelsAttribute.Count!=0)
            {
                for (int i = 0; i < ChanelsAttribute.Count; i++)
                {
                    foreach (var item in ChTypeList)
                    {
                        count = ChanelsAttribute[i].MultiSeletChanell.Where(s => s.TYPE == item.TYPE).Count();
                        var sametyps = ChanelsAttribute[i].MultiSeletChanell.Where(s => s.TYPE == item.TYPE).ToList();
                        for (int c = 0; c < sametyps.Count; c++)
                        {
                            if (sametyps[i].SERVED_AREA!=null)
                            {
                                length += Convert.ToDecimal(sametyps[i].SERVED_AREA);
                            }
                        }
                        
                        if (AttributList.Where(d => d.TypeName == item.TYPE).Count() == 0)
                        {
                            AttributList.Add(new ChannelAttr
                            {
                                TypeName = item.TYPE,
                                TypeCount = count,
                                TypeLength = length
                            }); 
                        }
                        else
                        {
                            var dd = AttributList.Find(s => s.TypeName == item.TYPE);
                            dd.TypeCount += count;
                            dd.TypeLength += length;
                        }
                        count = 0;
                        length = 0;
                    }
                }
            }

            List<ChannelAttr> arrtibut = new List<ChannelAttr>();
            
            if (Attr != null)
            {
                if (AttributList.Count!=0)
                {
                    for (int i = 0; i < Attr.Length; i++)
                    {
                        if (Attr[i] == "0")
                        {
                            arrtibut = AttributList;
                        }
                        else
                        {
                            arrtibut.Add(new ChannelAttr {
                                TypeName= AttributList.Where(s => s.TypeName == Attr[i]).First().TypeName,
                                TypeCount = AttributList.Where(s => s.TypeName == Attr[i]).First().TypeCount,
                                TypeLength = AttributList.Where(s => s.TypeName == Attr[i]).First().TypeLength
                            });
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < Attr.Length; i++)
                    {
                        if (Attr[i] == "0")
                        {
                            foreach (var item in ChTypeList)
                            {
                                var sameType = ChannelList.Where(s => s.TYPE == item.TYPE).ToList();
                                var typelengt = 0;
                                for (int x = 0; x < sameType.Count; x++)
                                {
                                    decimal number;
                                    if (sameType[x].SERVED_AREA != null && Decimal.TryParse(sameType[x].SERVED_AREA, out number))
                                    {
                                        typelengt += Convert.ToInt32(sameType[x].SERVED_AREA);
                                    }
                                }
                                arrtibut.Add(new ChannelAttr
                                {
                                    TypeCount = ChannelList.Where(s => s.TYPE == item.TYPE).Count(),
                                    TypeName = item.TYPE,
                                    TypeLength= typelengt

                                });
                            }
                        }
                        else
                        {
                            var sameTypee = ChannelList.Where(s => s.TYPE == Attr[i]).ToList();
                            decimal typelengt = 0;
                            for (int x = 0; x < sameTypee.Count; x++)
                            {
                                decimal number;
                                if (sameTypee[x].SERVED_AREA != null && Decimal.TryParse(sameTypee[x].SERVED_AREA, out number))
                                {
                                    typelengt += Convert.ToDecimal(sameTypee[x].SERVED_AREA);
                                }
                            }
                            arrtibut.Add(new ChannelAttr
                            {
                                TypeCount = ChannelList.Where(s => s.TYPE == Attr[i]).Count(),
                                TypeName = Attr[i],
                                TypeLength = typelengt
                            });                           
                        }
                    }
                }                
            }
            else
            {
                arrtibut = AttributList;
            }
            
            var jsonResult = Json(new { data = arrtibut }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        //public ActionResult Dataload()
        //{
        //    using (Context con = new Context())
        //    {
        //        RegionList = con.Region.QueryStringAsList("select * from Meloregions").ToList();
        //        VillageList = con.village.QueryStringAsList($"select * from MUNICIPALITIES").ToList();
        //        DrenajList = con.Drenaj.QueryStringAsList($"select SHAPE.STAsText() as shape,NAME,OBJECTID,PASSING_AREAS, Region_ID from DRENAJ").ToList();
        //        RiverbandList = con.riverband.QueryStringAsList("select SHAPE.STAsText() as shape, NAME,OBJECTID,Region_ID from RIVERBAND").ToList();
        //        DeviceList = con.Device.QueryStringAsList("select SHAPE.STAsText() as shape, NAME,OBJECTID,Municipality_id from DEVICE").ToList();
        //        ChannelList = con.Channel.QueryStringAsList("select SHAPE.STAsText() as shape, NAME,OBJECTID,Municipality_id from CHANNELS").ToList();
        //    }
        //    return Content("");
        //}

        public ActionResult Village(int[] id)
        {            
            List<MutlySelect> ms = new List<MutlySelect>();
            for (int i = 0; i < id.Length; i++)
            {
                if (id[i]==0)
                {
                    ms.Add(new MutlySelect
                    {
                        Villages = VillageList
                    });
                    break;
                }
                else
                {
                    ms.Add(new MutlySelect
                    {
                        Villages = VillageList.Where(s => s.REGION_ID == id[i]).ToList()
                    });
                    
                }
            }
            var jsonResult = Json(new { data = ms }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult Laylar(int? id)
        {            
            LaylarViewModel laylarViewModel = new LaylarViewModel();
            laylarViewModel.Device = DeviceList.Where(s=>s.Municipality_id==id).ToList();
            laylarViewModel.Channels = ChannelList.Where(s=>s.Municipality_id==id).ToList();
            var jsonResult = Json(new { data = laylarViewModel }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        

        public ActionResult All(int[] id)
        {           
            SebekelerViewModel sbwm = new SebekelerViewModel();
            List<MUltiSelectDrenaj> MsDrej = new List<MUltiSelectDrenaj>();
            List<MultiSelectRiverband> Msriver = new List<MultiSelectRiverband>();
            List<MultiSelectWell> MsWell = new List<MultiSelectWell>();
            List<ChannelType> MsChannel = new List<ChannelType>();
            using (Context con = new Context())
            {                
                for (int i = 0; i < id.Length; i++)
                {
                    if (id[i] == 0000)
                    {
                        var drej = drejTypeList;
                        var riverband = rbTypeList;
                        var wells = welltypeList.ToList();
                        var channel = ChannelList;
                        foreach (var item in channel)
                        {
                            if (MsChannel.Where(s => s.TYPE == item.TYPE).Count() == 0)
                            {
                                MsChannel.Add(new ChannelType
                                {
                                    TYPE = item.TYPE
                                });
                            }
                        }

                        foreach (var item in drej)
                        {
                            if (MsDrej.Where(s => s.DrejType == item.TYPE).Count() == 0)
                            {
                                MsDrej.Add(new MUltiSelectDrenaj
                                {
                                    DrejType = item.TYPE
                                });
                            }
                        }
                        foreach (var item in riverband)
                        {
                            if (Msriver.Where(s => s.Riverbandtype == Convert.ToString(item.TYPE)).Count() == 0)
                            {
                                Msriver.Add(new MultiSelectRiverband
                                {
                                    Riverbandtype = Convert.ToString(item.TYPE)
                                });
                            }
                        }
                        foreach (var item in wells)
                        {
                            if (MsWell.Where(s => s.WellType == item.WELL_TYPE).Count() == 0)
                            {
                                MsWell.Add(new MultiSelectWell
                                {
                                    WellType = item.WELL_TYPE
                                });
                            }
                        }
                        sbwm.ChannelTypes = MsChannel;
                        sbwm.DrenajselectList = MsDrej;
                        sbwm.RiverbandselectList = Msriver;
                        sbwm.WellselectList = MsWell;
                        break;
                    }
                    else
                    {
                        var chan = ChannelList.Where(c => c.Region_ID == id[i]).ToList();
                        foreach (var item in chan)
                        {
                            if (MsChannel.Where(s => s.TYPE == item.TYPE).Count() == 0)
                            {
                                MsChannel.Add(new ChannelType
                                {
                                    TYPE = item.TYPE
                                });
                            }
                        }
                        var drej = drejTypeList.Where(s => s.Region_ID == id[i]).ToList();
                        foreach (var item in drej)
                        {
                            if (MsDrej.Where(s => s.DrejType == item.TYPE).Count() == 0)
                            {
                                MsDrej.Add(new MUltiSelectDrenaj
                                {
                                    DrejType=item.TYPE
                                });
                            }
                        }

                        var riverband = rbTypeList.Where(s => s.Region_ID == id[i]).ToList();
                        foreach (var item in riverband)
                        {
                            if (Msriver.Where(s => s.Riverbandtype == Convert.ToString(item.TYPE)).Count() == 0)
                            {
                                Msriver.Add(new MultiSelectRiverband
                                {
                                    Riverbandtype = Convert.ToString(item.TYPE)
                                });
                            }
                        }

                        var wells = welltypeList.Where(s => s.Region_ID == id[i]).ToList();
                        foreach (var item in wells)
                        {
                            if (MsWell.Where(s => s.WellType ==item.WELL_TYPE).Count() == 0)
                            {
                                MsWell.Add(new MultiSelectWell
                                {
                                    WellType = item.WELL_TYPE
                                });
                            }
                        }                        
                    }
                }
            }
            sbwm.ChannelTypes = MsChannel;
            sbwm.DrenajselectList = MsDrej;
            sbwm.RiverbandselectList = Msriver;
            sbwm.WellselectList = MsWell;


            var jsonResult = Json(new { data = sbwm }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }



        public ActionResult Koordinats(int[] Listid, string[] networkselectlist) {
            List<Koordinatscs> mapkd = new List<Koordinatscs>();
            var x = 1;
            for (int i = 0; i < x; i++)
            {
                if (x<=Listid.Length)
                {
                    for (int q = 0; q < Listid.Length; q++)
                    {
                        var Drenaj01 = DrenajList.Where(w => w.Region_ID == Listid[q]).ToList();
                        var Riverband01 = RiverbandList.Where(w => w.Region_ID == Listid[q]).ToList();
                        var Well01 = WellList.Where(w => w.Region_ID == Listid[q]).ToList();
                        for (int d = 0; d < networkselectlist.Length; d++)
                        {
                            var Drtype = Drenaj01.Where(s => s.TYPE == networkselectlist[d]).ToList();
                            foreach (var item in Drtype)
                            {
                                mapkd.Add(new Koordinatscs
                                {
                                    Koordi = item.shape
                                });
                            }
                            decimal number;
                            if (Decimal.TryParse(networkselectlist[d], out number))
                            {
                                var Rvtype = Riverband01.Where(s => s.TYPE == number).ToList();
                                foreach (var item in Rvtype)
                                {
                                    mapkd.Add(new Koordinatscs
                                    {
                                        Koordi = item.SHAPE
                                    });
                                }
                            }                            
                            var Wltype = Well01.Where(s => s.WELL_TYPE == networkselectlist[d]).ToList();
                            foreach (var item in Wltype)
                            {
                                mapkd.Add(new Koordinatscs
                                {
                                    Koordi = item.SHAPE
                                });
                            }

                        }
                    }
                    x++;
                }
            }


            var jsonResult = Json(new { data = mapkd }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}