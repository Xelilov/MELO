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
        public ActionResult Login()
        {
            using (Context con = new Context())
            {
                RegionList = con.Region.QueryStringAsList("select * from Meloregions").ToList();
                VillageList= con.village.QueryStringAsList($"select * from MUNICIPALITIES").ToList();
                DrenajList = con.Drenaj.QueryStringAsList($"select SHAPE.STAsText() as shape,NAME,OBJECTID,PASSING_AREAS, Region_ID from DRENAJ").ToList();
                RiverbandList = con.riverband.QueryStringAsList("select SHAPE.STAsText() as shape, NAME,OBJECTID,Region_ID from RIVERBAND").ToList();
                DeviceList = con.Device.QueryStringAsList("select SHAPE.STAsText() as shape, NAME,OBJECTID,Municipality_id from DEVICE").ToList();
                ChannelList= con.Channel.QueryStringAsList("select SHAPE.STAsText() as shape, NAME,OBJECTID,Municipality_id from CHANNELS").ToList();
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
            List<Regions> regin = new List<Regions>();
            using (Context con=new Context())
            {               
                regin = con.Region.QueryStringAsList("select * from Meloregions").ToList();
            }
            return View(regin);
        }

        public ActionResult Village(int? id)
        {
            string regionid = "0" + id;
            List<Village> Villages = new List<Village>();
            using (Context con = new Context())
            {
                Villages = con.village.QueryStringAsList($"select * from MUNICIPALITIES where Rayon_code='{regionid}'").ToList();
            }
            return Json(new { data = Villages }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Laylar(int? id)
        {            
            LaylarViewModel laylarViewModel = new LaylarViewModel();
            
            //using (Context con = new Context())
            //{
            //    laylarViewModel.Device = con.Device.QueryStringAsList("select * from DEVICE").ToList();
            //    laylarViewModel.Channels = con.Channel.QueryStringAsList("select * from CHANNELS").ToList();
            //}
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
            using (Context con = new Context())
            {
                
                for (int i = 0; i < id.Length; i++)
                {
                    if (id[i] == 0)
                    {
                        sebekelerViewModel.Drenaj = DrenajList.ToList();
                        break;
                    }
                    else
                    {
                        string Reggionname = con.Region.QueryStringFind($"select * from MELOREGIONS where OBJECTID_1='{id[i]}'").NAME_AZ;

                        //sebekelerViewModel.Drenaj = con.Drenaj.QueryStringAsList($"select SHAPE.STAsText() as shape,NAME,OBJECTID,PASSING_AREAS from DRENAJ where PASSING_AREAS ='{Reggionname}'").ToList();
                        sebekelerViewModel.Drenaj = DrenajList.Where(s => s.PASSING_AREAS == Reggionname).ToList();
                        sebekelerViewModel.riverbandcs = RiverbandList;
                    }
                }

            }
            var jsonResult = Json(new { data = sebekelerViewModel }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}