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
        public ActionResult Login()
        {
            using (Context con = new Context())
            {
                RegionList = con.Region.QueryStringAsList("select * from Meloregions").ToList();
                VillageList = con.village.QueryStringAsList($"select * from RESIDENTIAL_AREA").ToList();
                DrenajList = con.Drenaj.QueryStringAsList($"select SHAPE.STAsText() as shape,NAME,OBJECTID,PASSING_AREAS, Region_ID from DRENAJ").ToList();
                RiverbandList = con.riverband.QueryStringAsList("select SHAPE.STAsText() as shape, NAME,OBJECTID,Region_ID from RIVERBAND").ToList();
                DeviceList = con.Device.QueryStringAsList("select SHAPE.STAsText() as shape, NAME,OBJECTID,Municipality_id from DEVICE").ToList();
                ChannelList = con.Channel.QueryStringAsList("select SHAPE.STAsText() as shape, TYPE, NAME,OBJECTID,Municipality_id,Region_ID from CHANNELS").ToList();
                WellList = con.Well.QueryStringAsList("select SHAPE.STAsText() as shape,NAME,OBJECTID ,Region_ID from WELL").ToList();
                ChTypeList= con.channeltype.QueryStringAsList("select distinct type from CHANNELS").ToList();
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
            if (ChanelsAttribute.Count!=0)
            {
                for (int i = 0; i < ChanelsAttribute.Count; i++)
                {
                    foreach (var item in ChTypeList)
                    {
                        count = ChanelsAttribute[i].MultiSeletChanell.Where(s => s.TYPE == item.TYPE).Count();
                        if (AttributList.Where(d => d.TypeName == item.TYPE).Count() == 0)
                        {
                            AttributList.Add(new ChannelAttr
                            {
                                TypeName = item.TYPE,
                                TypeCount = count
                            });
                        }
                        else
                        {
                            var dd = AttributList.Find(s => s.TypeName == item.TYPE);
                            dd.TypeCount += count;
                        }
                        count = 0;
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
                            arrtibut = AttributList.Where(s => s.TypeName == Attr[i]).ToList();
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
                                arrtibut.Add(new ChannelAttr
                                {
                                    TypeCount = ChannelList.Where(s => s.TYPE == item.TYPE).Count(),
                                    TypeName = item.TYPE
                                });
                            }
                        }
                        else
                        {
                            arrtibut.Add(new ChannelAttr
                            {
                                TypeCount = ChannelList.Where(s => s.TYPE == Attr[i]).Count(),
                                TypeName = Attr[i]
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
                        Villages = VillageList.Where(s => s.Region_ID_1 == id[i]).ToList()
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
            List<Drenaj> drej = new List<Drenaj>();
            SebekelerViewModel sebekelerViewModel = new SebekelerViewModel();

            List<SebekelerViewModel> sbwm = new List<SebekelerViewModel>();
            using (Context con = new Context())
            {
                
                for (int i = 0; i < id.Length; i++)
                {
                    if (id[i] == 0)
                    {
                        sbwm.Add(new SebekelerViewModel
                        {
                            DrenajselectList = DrenajList,
                            RiverbandselectList = RiverbandList,
                            WellselectList = WellList
                        });
                        break;
                    }
                    else
                    {
                        sbwm.Add(new SebekelerViewModel
                        {
                            DrenajselectList= DrenajList.Where(s => s.Region_ID == id[i]).ToList(),
                            RiverbandselectList= RiverbandList.Where(s => s.Region_ID == id[i]).ToList(),
                            WellselectList= WellList.Where(s => s.Region_ID == id[i]).ToList()
                        });
                        //sebekelerViewModel.Drenaj = DrenajList.Where(s => s.Region_ID == id[i]).ToList();
                        //sebekelerViewModel.riverbandcs = RiverbandList.Where(s => s.Region_ID == id[i]).ToList();
                        //sebekelerViewModel.Well = WellList.Where(s => s.Region_ID == id[i]).ToList();

                    }
                }
                

            }
            var jsonResult = Json(new { data = sbwm }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}