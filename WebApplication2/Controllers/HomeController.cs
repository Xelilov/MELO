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
        static List<Channels> ChannelList = new List<Channels>();
        static List<Drenaj> DrenajList = new List<Drenaj>();
        static List<Riverbandcs> RiverbandList = new List<Riverbandcs>();
        static List<Device> DeviceList = new List<Device>();
        static List<Wells> WellList = new List<Wells>();
        static List<Departments> DepartmentsList = new List<Departments>();
        static List<Pumpstation> PumpstationList = new List<Pumpstation>();
        static List<Buildings> buildinglist = new List<Buildings>();
        static List<Exploitationroad> exploitationroadList = new List<Exploitationroad>();


        static List<ChannelType> ChTypeList = new List<ChannelType>();
        static List<DrenajType> drejTypeList = new List<DrenajType>();

        public ActionResult Login()
        {
            using (Context con = new Context())
            {
                RegionList = con.Region.QueryStringAsList("select * from Meloregions").ToList();
                VillageList = con.village.QueryStringAsList($"select * from RESIDENTIALAREA").ToList();
                ChannelList = con.Channel.QueryStringAsList("select SHAPE.STAsText() as shape,FACTICAL_LENGTH, TYPE, NAME,OBJECTID,Municipality_id,Region_ID from CHANNELS").ToList();
                DrenajList = con.Drenaj.QueryStringAsList($"select SHAPE.STAsText() as shape,TYPE,OBJECTID,FACTICAL_LENGTH, Region_ID,Municipality_id from DRENAJ").ToList();
                RiverbandList = con.riverband.QueryStringAsList("select SHAPE.STAsText() as shape,TYPE,OBJECTID,LENGTH,Region_ID,Municipality_id from RIVERBAND").ToList();
                DeviceList = con.Device.QueryStringAsList("select SHAPE.STAsText() as shape, NAME,OBJECTID,Municipality_id,Region_ID from DEVICE").ToList();
                WellList = con.Well.QueryStringAsList("select SHAPE.STAsText() as shape,OBJECTID ,Region_ID,WELL_TYPE,Municipality_id from WELL").ToList();
                DepartmentsList = con.department.QueryStringAsList("select SHAPE.STAsText() as shape,OBJECTID,AD,Region_ID from DEPARTMENTS").ToList();
                PumpstationList = con.pumpstation.QueryStringAsList("select SHAPE.STAsText() as shape,OBJECTID,NAME,Region_ID,Municipality_id from PUMPSTATION").ToList();
                buildinglist = con.building.QueryStringAsList("select SHAPE.STAsText() as shape,OBJECTID,NAME,Region_ID,Municipality_id from BUILDINGS").ToList();
                exploitationroadList = con.exploitationroad.QueryStringAsList("select SHAPE.STAsText() as shape,OBJECTID,NAME,Region_ID,LENGHT,Municipality_id from EXPLOITATION_ROAD").ToList();


                ChTypeList = con.channeltype.QueryStringAsList("select distinct type from CHANNELS").ToList();
                drejTypeList = con.drenajtype.QueryStringAsList("select distinct type from DRENAJ").ToList();
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
                IndexVM.drejtype = drejTypeList;
            }
            return View(IndexVM);
        }


        public ActionResult Attributes(int[] id, int[] idVil, string[] Attr)
        {
            List<ChannelMultiSelectList> ChanelsAttribute = new List<ChannelMultiSelectList>();
            List<Channels> AllChannels = new List<Channels>();
            List<ChannelAttr> AttributList = new List<ChannelAttr>();

            AttributList.Add(new ChannelAttr
            {
                TypeName = "Magistral",
                TypeTitle= "tych-Magistral",
                TypeCount = 0,
                TypeLength = 0

            });
            AttributList.Add(new ChannelAttr
            {
                TypeName = "1-ci dərəcəli Kanallar",
                TypeTitle = "tych-1",
                TypeCount = 0,
                TypeLength = 0
            });
            AttributList.Add(new ChannelAttr
            {
                TypeName = "2-ci dərəcəli Kanallar",
                TypeTitle = "tych-2",
                TypeCount = 0,
                TypeLength = 0

            });
            AttributList.Add(new ChannelAttr
            {
                TypeName = "3-ci dərəcəli Kanallar",
                TypeTitle = "tych-3",
                TypeCount = 0,
                TypeLength = 0
            });
            AttributList.Add(new ChannelAttr
            {
                TypeName = "1-ci dərəcəli kollektorlar",
                TypeTitle = "tydj-1",
                TypeCount = 0,
                TypeLength = 0
            });
            AttributList.Add(new ChannelAttr
            {
                TypeName = "2-ci dərəcəli yığıcı kollektorlar",
                TypeTitle = "tydj-2",
                TypeCount = 0,
                TypeLength = 0
            });
            AttributList.Add(new ChannelAttr
            {
                TypeName = "3-ci dərəcəli ilkin drenlər",
                TypeTitle = "tydj-3",
                TypeCount = 0,
                TypeLength = 0
            });
            AttributList.Add(new ChannelAttr
            {
                TypeName = "Mühafizə Bəndləri",
                TypeTitle = "type-2",
                TypeCount = 0,
                TypeLength = 0
            });
            AttributList.Add(new ChannelAttr
            {
                TypeName = "Müşahidə Quyuları",
                TypeTitle = "type-3",
                TypeCount = 0,
                TypeLength = 0
            });
            AttributList.Add(new ChannelAttr
            {
                TypeName = "Hidrotexniki Qurğular",
                TypeTitle = "type-4",
                TypeCount = 0,
                TypeLength = 0
            });
            AttributList.Add(new ChannelAttr
            {
                TypeName = "İdarələr",
                TypeTitle = "type-5",
                TypeCount = 0,
                TypeLength = 0
            });
            AttributList.Add(new ChannelAttr
            {
                TypeName = "Nasos Stansiyaları",
                TypeTitle = "type-6",
                TypeCount = 0,
                TypeLength = 0
            });
            AttributList.Add(new ChannelAttr
            {
                TypeName = "Binalar ve tikintilər",
                TypeTitle = "type-7",
                TypeCount = 0,
                TypeLength = 0
            });
            AttributList.Add(new ChannelAttr
            {
                TypeName = "İstismar yolları",
                TypeTitle = "type-8",
                TypeCount = 0,
                TypeLength = 0
            });


            if (id != null && idVil==null)
            {
                for (int i = 0; i < id.Length; i++)
                {
                    if (id[i]==0)
                    {
                        var chnls = ChannelList.Where(c =>c.TYPE == "Magistral").ToList();
                        var test = AttributList.Where(s => s.TypeName == "Magistral").First();
                        test.TypeCount += chnls.Count;
                        for (int c = 0; c < chnls.Count; c++)
                        {
                            if (chnls[c].TYPE == "Magistral")
                            {
                                test.TypeLength += chnls[c].FACTICAL_LENGTH;
                            }
                        }
                        var chnls1 = ChannelList.Where(c =>c.TYPE == "1").ToList();
                        var test1 = AttributList.Where(s => s.TypeName == "1-ci dərəcəli Kanallar").First();
                        test1.TypeCount += chnls1.Count;
                        for (int c = 0; c < chnls1.Count; c++)
                        {
                            test1.TypeLength += chnls1[c].FACTICAL_LENGTH;
                        }
                        var chnls2 = ChannelList.Where(c =>c.TYPE == "2").ToList();
                        var test2 = AttributList.Where(s => s.TypeName == "2-ci dərəcəli Kanallar").First();
                        test2.TypeCount += chnls2.Count;
                        for (int c = 0; c < chnls2.Count; c++)
                        {
                            test2.TypeLength += chnls2[c].FACTICAL_LENGTH;
                        }
                        var chnls3 = ChannelList.Where(c =>c.TYPE == "3").ToList();
                        var test3 = AttributList.Where(s => s.TypeName == "3-ci dərəcəli Kanallar").First();
                        test3.TypeCount += chnls3.Count;
                        for (int c = 0; c < chnls3.Count; c++)
                        {
                            test3.TypeLength += chnls3[c].FACTICAL_LENGTH;
                        }

                        var drej1 = DrenajList.Where(d =>d.TYPE == "1").ToList();
                        var listdrej1 = AttributList.Where(s => s.TypeName == "1-ci dərəcəli kollektorlar").First();
                        listdrej1.TypeCount += drej1.Count;
                        for (int d = 0; d < drej1.Count; d++)
                        {
                            listdrej1.TypeLength += drej1[d].FACTICAL_LENGTH;
                        }
                        var drej2 = DrenajList.Where(d =>d.TYPE == "2").ToList();
                        var listdrej2 = AttributList.Where(s => s.TypeName == "2-ci dərəcəli yığıcı kollektorlar").First();
                        listdrej2.TypeCount += drej2.Count;
                        for (int d = 0; d < drej2.Count; d++)
                        {
                            listdrej2.TypeLength += drej2[d].FACTICAL_LENGTH;
                        }
                        var drej3 = DrenajList.Where(d =>d.TYPE == "3").ToList();
                        var listdrej3 = AttributList.Where(s => s.TypeName == "3-ci dərəcəli ilkin drenlər").First();
                        listdrej3.TypeCount += drej3.Count;
                        for (int d = 0; d < drej3.Count; d++)
                        {
                            listdrej3.TypeLength += drej3[d].FACTICAL_LENGTH;
                        }

                        var riverband = RiverbandList;
                        var listriver = AttributList.Where(l => l.TypeName == "Mühafizə Bəndləri").First();
                        listriver.TypeCount += riverband.Count;
                        for (int r = 0; r < riverband.Count; r++)
                        {
                            listriver.TypeLength += riverband[r].LENGTH;
                        }
                        var well = WellList;
                        var listwell = AttributList.Where(l => l.TypeName == "Müşahidə Quyuları").First();
                        listwell.TypeCount += well.Count;

                        var device = DeviceList;
                        var listdevice = AttributList.Where(l => l.TypeName == "Hidrotexniki Qurğular").First();
                        listdevice.TypeCount += device.Count;

                        var departments = DepartmentsList;
                        var listdepar = AttributList.Where(l => l.TypeName == "İdarələr").First();
                        listdepar.TypeCount += departments.Count;

                        var pumpstation = PumpstationList;
                        var listpump = AttributList.Where(l => l.TypeName == "Nasos Stansiyaları").First();
                        listpump.TypeCount += departments.Count;

                        var buildings = buildinglist;
                        var listbuild = AttributList.Where(l => l.TypeName == "Binalar ve tikintilər").First();
                        listbuild.TypeCount += buildings.Count;

                        var road = exploitationroadList;
                        var listroad = AttributList.Where(l => l.TypeName == "İstismar yolları").First();
                        listroad.TypeCount += road.Count;
                        for (int r = 0; r < road.Count; r++)
                        {
                            listroad.TypeLength += road[r].LENGHT;
                        }
                        break;
                    }
                    else
                    {
                        var chnls = ChannelList.Where(c => c.Region_ID == id[i]&& c.TYPE== "Magistral").ToList();
                        var test = AttributList.Where(s => s.TypeName == "Magistral").First();
                        test.TypeCount += chnls.Count;
                        for (int c = 0; c < chnls.Count; c++)
                        {
                            if (chnls[c].TYPE=="Magistral")
                            {                                
                                test.TypeLength += chnls[c].FACTICAL_LENGTH;
                            }
                        }
                        var chnls1 = ChannelList.Where(c => c.Region_ID == id[i] && c.TYPE == "1").ToList();
                        var test1 = AttributList.Where(s => s.TypeName == "1-ci dərəcəli Kanallar").First();
                        test1.TypeCount += chnls1.Count;
                        for (int c = 0; c < chnls1.Count; c++)
                        {
                                test1.TypeLength += chnls1[c].FACTICAL_LENGTH;
                        }
                        var chnls2 = ChannelList.Where(c => c.Region_ID == id[i] && c.TYPE == "2").ToList();
                        var test2 = AttributList.Where(s => s.TypeName == "2-ci dərəcəli Kanallar").First();
                        test2.TypeCount += chnls2.Count;
                        for (int c = 0; c < chnls2.Count; c++)
                        {
                            test2.TypeLength += chnls2[c].FACTICAL_LENGTH;
                        }
                        var chnls3 = ChannelList.Where(c => c.Region_ID == id[i] && c.TYPE == "3").ToList();
                        var test3 = AttributList.Where(s => s.TypeName == "3-ci dərəcəli Kanallar").First();
                        test3.TypeCount += chnls3.Count;
                        for (int c = 0; c < chnls3.Count; c++)
                        {
                            test3.TypeLength += chnls3[c].FACTICAL_LENGTH;
                        }

                        var drej1 = DrenajList.Where(d => d.Region_ID == id[i] && d.TYPE == "1").ToList();
                        var listdrej1= AttributList.Where(s => s.TypeName == "1-ci dərəcəli kollektorlar").First();
                        listdrej1.TypeCount += drej1.Count;
                        for (int d = 0; d < drej1.Count; d++)
                        {
                            listdrej1.TypeLength += drej1[d].FACTICAL_LENGTH;
                        }
                        var drej2 = DrenajList.Where(d => d.Region_ID == id[i] && d.TYPE == "2").ToList();
                        var listdrej2 = AttributList.Where(s => s.TypeName == "2-ci dərəcəli yığıcı kollektorlar").First();
                        listdrej2.TypeCount += drej2.Count;
                        for (int d = 0; d < drej2.Count; d++)
                        {
                            listdrej2.TypeLength += drej2[d].FACTICAL_LENGTH;
                        }
                        var drej3 = DrenajList.Where(d => d.Region_ID == id[i] && d.TYPE == "3").ToList();
                        var listdrej3 = AttributList.Where(s => s.TypeName == "3-ci dərəcəli ilkin drenlər").First();
                        listdrej3.TypeCount += drej3.Count;
                        for (int d = 0; d < drej3.Count; d++)
                        {
                            listdrej3.TypeLength += drej3[d].FACTICAL_LENGTH;
                        }

                        var riverband = RiverbandList.Where(r => r.Region_ID == id[i]).ToList();
                        var listriver = AttributList.Where(l => l.TypeName == "Mühafizə Bəndləri").First();
                        listriver.TypeCount += riverband.Count;
                        for (int r = 0; r < riverband.Count; r++)
                        {
                            listriver.TypeLength += riverband[r].LENGTH;
                        }
                        var well = WellList.Where(r => r.Region_ID == id[i]).ToList();
                        var listwell = AttributList.Where(l => l.TypeName == "Müşahidə Quyuları").First();
                        listwell.TypeCount += well.Count;
                        
                        var device = DeviceList.Where(r => r.Region_ID == id[i]).ToList();
                        var listdevice = AttributList.Where(l => l.TypeName == "Hidrotexniki Qurğular").First();
                        listdevice.TypeCount += device.Count;

                        var departments = DepartmentsList.Where(r => r.Region_ID == id[i]).ToList();
                        var listdepar = AttributList.Where(l => l.TypeName == "İdarələr").First();
                        listdepar.TypeCount += departments.Count;

                        var pumpstation = PumpstationList.Where(r => r.Region_ID == id[i]).ToList();
                        var listpump = AttributList.Where(l => l.TypeName == "Nasos Stansiyaları").First();
                        listpump.TypeCount += departments.Count;

                        var buildings = buildinglist.Where(r => r.Region_ID == id[i]).ToList();
                        var listbuild = AttributList.Where(l => l.TypeName == "Binalar ve tikintilər").First();
                        listbuild.TypeCount += buildings.Count;

                        var road = exploitationroadList.Where(r => r.Region_ID == id[i]).ToList();
                        var listroad = AttributList.Where(l => l.TypeName == "İstismar yolları").First();
                        listroad.TypeCount += road.Count;
                        for (int r = 0; r < road.Count; r++)
                        {
                            listroad.TypeLength += road[r].LENGHT;
                        }
                    }
                }
            }
            if (idVil != null)
            {
                for (int i = 0; i < idVil.Length; i++)
                {
                    var chnls = ChannelList.Where(c => c.Municipality_id == idVil[i] && c.TYPE == "Magistral").ToList();
                    var test = AttributList.Where(s => s.TypeName == "Magistral").First();
                    test.TypeCount += chnls.Count;
                    for (int c = 0; c < chnls.Count; c++)
                    {
                        if (chnls[c].TYPE == "Magistral")
                        {
                            test.TypeLength += chnls[c].FACTICAL_LENGTH;
                        }
                    }
                    var chnls1 = ChannelList.Where(c => c.Municipality_id == idVil[i] && c.TYPE == "1").ToList();
                    var test1 = AttributList.Where(s => s.TypeName == "1-ci dərəcəli Kanallar").First();
                    test1.TypeCount += chnls1.Count;
                    for (int c = 0; c < chnls1.Count; c++)
                    {
                        test1.TypeLength += chnls1[c].FACTICAL_LENGTH;
                    }
                    var chnls2 = ChannelList.Where(c => c.Municipality_id == idVil[i] && c.TYPE == "2").ToList();
                    var test2 = AttributList.Where(s => s.TypeName == "2-ci dərəcəli Kanallar").First();
                    test2.TypeCount += chnls2.Count;
                    for (int c = 0; c < chnls2.Count; c++)
                    {
                        test2.TypeLength += chnls2[c].FACTICAL_LENGTH;
                    }
                    var chnls3 = ChannelList.Where(c => c.Municipality_id == idVil[i] && c.TYPE == "3").ToList();
                    var test3 = AttributList.Where(s => s.TypeName == "3-ci dərəcəli Kanallar").First();
                    test3.TypeCount += chnls3.Count;
                    for (int c = 0; c < chnls3.Count; c++)
                    {
                        test3.TypeLength += chnls3[c].FACTICAL_LENGTH;
                    }

                    var drej1 = DrenajList.Where(d => d.Region_ID == idVil[i] && d.TYPE == "1").ToList();
                    var listdrej1 = AttributList.Where(s => s.TypeName == "1-ci dərəcəli kollektorlar").First();
                    listdrej1.TypeCount += drej1.Count;
                    for (int d = 0; d < drej1.Count; d++)
                    {
                        listdrej1.TypeLength += drej1[d].FACTICAL_LENGTH;
                    }
                    var drej2 = DrenajList.Where(d => d.Region_ID == idVil[i] && d.TYPE == "2").ToList();
                    var listdrej2 = AttributList.Where(s => s.TypeName == "2-ci dərəcəli yığıcı kollektorlar").First();
                    listdrej2.TypeCount += drej2.Count;
                    for (int d = 0; d < drej2.Count; d++)
                    {
                        listdrej2.TypeLength += drej2[d].FACTICAL_LENGTH;
                    }
                    var drej3 = DrenajList.Where(d => d.Region_ID == idVil[i] && d.TYPE == "3").ToList();
                    var listdrej3 = AttributList.Where(s => s.TypeName == "3-ci dərəcəli ilkin drenlər").First();
                    listdrej3.TypeCount += drej3.Count;
                    for (int d = 0; d < drej3.Count; d++)
                    {
                        listdrej3.TypeLength += drej3[d].FACTICAL_LENGTH;
                    }

                    var riverband = RiverbandList.Where(r => r.Region_ID == idVil[i]).ToList();
                    var listriver = AttributList.Where(l => l.TypeName == "Mühafizə Bəndləri").First();
                    listriver.TypeCount += riverband.Count;
                    for (int r = 0; r < riverband.Count; r++)
                    {
                        listriver.TypeLength += riverband[r].LENGTH;
                    }
                    var well = WellList.Where(r => r.Region_ID == idVil[i]).ToList();
                    var listwell = AttributList.Where(l => l.TypeName == "Müşahidə Quyuları").First();
                    listwell.TypeCount += well.Count;

                    var device = DeviceList.Where(r => r.Municipality_id == idVil[i]).ToList();
                    var listdevice = AttributList.Where(l => l.TypeName == "Hidrotexniki Qurğular").First();
                    listdevice.TypeCount += device.Count;

                    var departments = DepartmentsList.Where(r => r.Region_ID == idVil[i]).ToList();
                    var listdepar = AttributList.Where(l => l.TypeName == "İdarələr").First();
                    listdepar.TypeCount += departments.Count;

                    var pumpstation = PumpstationList.Where(r => r.Municipality_id == idVil[i]).ToList();
                    var listpump = AttributList.Where(l => l.TypeName == "Nasos Stansiyaları").First();
                    listpump.TypeCount += departments.Count;

                    var buildings = buildinglist.Where(r => r.Municipality_id == idVil[i]).ToList();
                    var listbuild = AttributList.Where(l => l.TypeName == "Binalar ve tikintilər").First();
                    listbuild.TypeCount += buildings.Count;

                    var road = exploitationroadList.Where(r => r.Municipality_id == idVil[i]).ToList();
                    var listroad = AttributList.Where(l => l.TypeName == "İstismar yolları").First();
                    listroad.TypeCount += road.Count;
                    for (int r = 0; r < road.Count; r++)
                    {
                        listroad.TypeLength += road[r].LENGHT;
                    }
                    
                }
            }
            List<ChannelAttr> attr = new List<ChannelAttr>();
            if (Attr != null)
            {
                for (int i = 0; i < Attr.Length; i++)
                {
                    if (Attr[i]== "type-0")
                    {
                        attr = AttributList;
                        break;
                    }
                    else
                    {
                        attr.Add(AttributList.Where(s => s.TypeTitle == Attr[i]).First());
                    }
                }

            }
            else
            {
                attr = AttributList;
            }

            var jsonResult = Json(new { data = attr }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

            //if (id!=null)
            //{
            //    for (int i = 0; i < id.Length; i++)
            //    {
            //        if (id[i] == 0)
            //        {
            //            ChanelsAttribute.Add(new ChannelMultiSelectList
            //            {
            //                MultiSeletChanell = ChannelList
            //            });
            //            break;
            //        }
            //        else
            //        {
            //            if (idVil != null)
            //            {
            //                for (int x = 0; x < idVil.Length; x++)
            //                {
            //                    if (idVil[i] == 0)
            //                    {

            //                    }
            //                    else
            //                    {
            //                        ChanelsAttribute.Add(new ChannelMultiSelectList
            //                        {
            //                            MultiSeletChanell = ChannelList.Where(s => s.Region_ID == id[i] && s.Municipality_id == idVil[x]).ToList()
            //                        });
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                ChanelsAttribute.Add(new ChannelMultiSelectList
            //                {
            //                    MultiSeletChanell = ChannelList.Where(s => s.Region_ID == id[i]).ToList()
            //                });
            //            }
            //        }
            //    }
            //}
            //int count = 0;
            //decimal length = 0;
            //if (ChanelsAttribute.Count!=0)
            //{
            //    for (int i = 0; i < ChanelsAttribute.Count; i++)
            //    {
            //        foreach (var item in ChTypeList)
            //        {
            //            count = ChanelsAttribute[i].MultiSeletChanell.Where(s => s.TYPE == item.TYPE).Count();
            //            var sametyps = ChanelsAttribute[i].MultiSeletChanell.Where(s => s.TYPE == item.TYPE).ToList();
            //            for (int c = 0; c < sametyps.Count; c++)
            //            {
            //                if (sametyps[i].SERVED_AREA!=null)
            //                {
            //                    length += Convert.ToDecimal(sametyps[i].SERVED_AREA);
            //                }
            //            }

            //            if (AttributList.Where(d => d.TypeName == item.TYPE).Count() == 0)
            //            {
            //                AttributList.Add(new ChannelAttr
            //                {
            //                    TypeName = item.TYPE,
            //                    TypeCount = count,
            //                    TypeLength = length
            //                }); 
            //            }
            //            else
            //            {
            //                var dd = AttributList.Find(s => s.TypeName == item.TYPE);
            //                dd.TypeCount += count;
            //                dd.TypeLength += length;
            //            }
            //            count = 0;
            //            length = 0;
            //        }
            //    }
            //}

            //List<ChannelAttr> arrtibut = new List<ChannelAttr>();

            //if (Attr != null)
            //{
            //    if (AttributList.Count!=0)
            //    {
            //        for (int i = 0; i < Attr.Length; i++)
            //        {
            //            if (Attr[i] == "0")
            //            {
            //                arrtibut = AttributList;
            //            }
            //            else
            //            {
            //                arrtibut.Add(new ChannelAttr {
            //                    TypeName= AttributList.Where(s => s.TypeName == Attr[i]).First().TypeName,
            //                    TypeCount = AttributList.Where(s => s.TypeName == Attr[i]).First().TypeCount,
            //                    TypeLength = AttributList.Where(s => s.TypeName == Attr[i]).First().TypeLength
            //                });
            //            }
            //        }
            //    }
            //    else
            //    {
            //        for (int i = 0; i < Attr.Length; i++)
            //        {
            //            if (Attr[i] == "0")
            //            {
            //                foreach (var item in ChTypeList)
            //                {
            //                    var sameType = ChannelList.Where(s => s.TYPE == item.TYPE).ToList();
            //                    var typelengt = 0;
            //                    for (int x = 0; x < sameType.Count; x++)
            //                    {
            //                        decimal number;
            //                        if (sameType[x].SERVED_AREA != null && Decimal.TryParse(sameType[x].SERVED_AREA, out number))
            //                        {
            //                            typelengt += Convert.ToInt32(sameType[x].SERVED_AREA);
            //                        }
            //                    }
            //                    arrtibut.Add(new ChannelAttr
            //                    {
            //                        TypeCount = ChannelList.Where(s => s.TYPE == item.TYPE).Count(),
            //                        TypeName = item.TYPE,
            //                        TypeLength= typelengt

            //                    });
            //                }
            //            }
            //            else
            //            {
            //                var sameTypee = ChannelList.Where(s => s.TYPE == Attr[i]).ToList();
            //                decimal typelengt = 0;
            //                for (int x = 0; x < sameTypee.Count; x++)
            //                {
            //                    decimal number;
            //                    if (sameTypee[x].SERVED_AREA != null && Decimal.TryParse(sameTypee[x].SERVED_AREA, out number))
            //                    {
            //                        typelengt += Convert.ToDecimal(sameTypee[x].SERVED_AREA);
            //                    }
            //                }
            //                arrtibut.Add(new ChannelAttr
            //                {
            //                    TypeCount = ChannelList.Where(s => s.TYPE == Attr[i]).Count(),
            //                    TypeName = Attr[i],
            //                    TypeLength = typelengt
            //                });                           
            //            }
            //        }
            //    }                
            //}
            //else
            //{
            //    arrtibut = AttributList;
            //}

            //var jsonResult = Json(new { data = "Salam" }, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return jsonResult;
        }
               

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

        
        public ActionResult MapShow(int[] id, string[] networkselectlist)
        {
            SebekelerViewModel sbwm = new SebekelerViewModel();
            List<Channels> MsChannel = new List<Channels>();
            List<Drenaj> MsDrenaj = new List<Drenaj>();
            List<Riverbandcs> MsRiverband = new List<Riverbandcs>();
            List<Wells> MsWell = new List<Wells>();
            List<Device> MsDevice = new List<Device>();
            List<Departments> MSDepartments = new List<Departments>();
            List<Pumpstation> MsPumpstation = new List<Pumpstation>();
            List<Buildings> MsBuilding = new List<Buildings>();
            List<Exploitationroad> MsExploitationroad = new List<Exploitationroad>();

            for (int i = 0; i < id.Length; i++)
            {
                if (id[i]==0000)
                {
                    for (int n = 0; n < networkselectlist.Length; n++)
                    {
                        if (networkselectlist[n].Contains("channel"))
                        {
                            string x = networkselectlist[n];
                            var channeltype = x.Substring(8);
                            var slctChannel = ChannelList.Where(c =>c.TYPE == channeltype).ToList();
                            foreach (var item in slctChannel)
                            {
                                MsChannel.Add(item);
                            }
                            sbwm.SelectedChannels = MsChannel;
                        }
                        if (networkselectlist[n].Contains("drenaj"))
                        {
                            string x = networkselectlist[n];
                            var drenajtype = x.Substring(7);
                            var slctDrenaj = DrenajList.Where(d =>d.TYPE == drenajtype).ToList();
                            foreach (var item in slctDrenaj)
                            {
                                MsDrenaj.Add(item);
                            }
                            sbwm.SelectedDrenajs = MsDrenaj;
                        }
                        if (networkselectlist[n].Contains("network-12"))
                        {
                            sbwm.SelectedRiverband = RiverbandList;
                        }
                        if (networkselectlist[n].Contains("network-16"))
                        {
                            sbwm.SelectedWell = WellList;
                        }
                        if (networkselectlist[n].Contains("network-17"))
                        {
                            sbwm.SelectedDevice = DeviceList;
                        }
                        if (networkselectlist[n].Contains("network-18"))
                        {
                            sbwm.SelectedDepartments = DepartmentsList;
                        }
                        if (networkselectlist[n].Contains("network-19"))
                        {
                            sbwm.SelectedPumpstation = PumpstationList;
                        }
                        if (networkselectlist[n].Contains("network-20"))
                        {
                            sbwm.SelectedBuilding = buildinglist;
                        }
                        if (networkselectlist[n].Contains("network-21"))
                        {
                            sbwm.SelectedExploitationroad = exploitationroadList;
                        }
                    }
                    break;
                }
                else
                {
                    for (int n = 0; n < networkselectlist.Length; n++)
                    {
                        if (networkselectlist[n].Contains("channel"))
                        {
                            string x = networkselectlist[n];
                            var channeltype = x.Substring(8);
                            var slctChannel = ChannelList.Where(c => c.Region_ID == id[i] && c.TYPE == channeltype).ToList();
                            foreach (var item in slctChannel)
                            {
                                MsChannel.Add(item);
                            }
                            sbwm.SelectedChannels = MsChannel;
                        }
                        if (networkselectlist[n].Contains("drenaj"))
                        {
                            string x = networkselectlist[n];
                            var drenajtype = x.Substring(7);
                            var slctDrenaj = DrenajList.Where(d => d.Region_ID == id[i] && d.TYPE == drenajtype).ToList();
                            foreach (var item in slctDrenaj)
                            {
                                MsDrenaj.Add(item);
                            }
                            sbwm.SelectedDrenajs = MsDrenaj;
                        }
                        if (networkselectlist[n].Contains("network-12"))
                        {
                            var slctRiverband = RiverbandList.Where(r => r.Region_ID == id[i]).ToList();
                            foreach (var item in slctRiverband)
                            {
                                MsRiverband.Add(item);
                            }
                            sbwm.SelectedRiverband = MsRiverband;
                        }
                        if (networkselectlist[n].Contains("network-16"))
                        {
                            var slctWell = WellList.Where(w => w.Region_ID == id[i]).ToList();
                            foreach (var item in slctWell)
                            {
                                MsWell.Add(item);
                            }
                            sbwm.SelectedWell = MsWell;
                        }
                        if (networkselectlist[n].Contains("network-17"))
                        {
                            var slctDevide = DeviceList.Where(d => d.Region_ID == id[i]).ToList();
                            foreach (var item in slctDevide)
                            {
                                MsDevice.Add(item);
                            }
                            sbwm.SelectedDevice = MsDevice;
                        }
                        if (networkselectlist[n].Contains("network-18"))
                        {
                            var scltDepartmant = DepartmentsList.Where(dp => dp.Region_ID == id[i]).ToList();
                            foreach (var item in scltDepartmant)
                            {
                                MSDepartments.Add(item);
                            }                            
                            sbwm.SelectedDepartments = MSDepartments;
                        }
                        if (networkselectlist[n].Contains("network-19"))
                        {
                            var slctPumpstation = PumpstationList.Where(p => p.Region_ID == id[i]).ToList();
                            foreach (var item in slctPumpstation)
                            {
                                MsPumpstation.Add(item);
                            }
                            sbwm.SelectedPumpstation = MsPumpstation;
                        }
                        if (networkselectlist[n].Contains("network-20"))
                        {
                            var slctBuilding = buildinglist.Where(b => b.Region_ID == id[i]).ToList();
                            foreach (var item in slctBuilding)
                            {
                                MsBuilding.Add(item);
                            }
                            sbwm.SelectedBuilding = MsBuilding;
                        }
                        if (networkselectlist[n].Contains("network-21"))
                        {
                            var slctExploitationroad = exploitationroadList.Where(e => e.Region_ID == id[i]).ToList();
                            foreach (var item in slctExploitationroad)
                            {
                                MsExploitationroad.Add(item);
                            }
                            sbwm.SelectedExploitationroad = MsExploitationroad;
                        }
                    }
                }
            }
            var jsonResult = Json(new { data = sbwm }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        
    }
}