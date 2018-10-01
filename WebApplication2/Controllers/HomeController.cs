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

        public ActionResult Login()
        {
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


        public ActionResult Laylar()
        {            
            LaylarViewModel laylarViewModel = new LaylarViewModel();
            using (Context con = new Context())
            {
                laylarViewModel.Device = con.Device.QueryStringAsList("select * from DEVICE").ToList();
                laylarViewModel.Channels = con.Channel.QueryStringAsList("select * from CHANNELS").ToList();
            }

            return Json(new { data=laylarViewModel }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult All(int[] id)
        {

            List<Drenaj> drej = new List<Drenaj>();
            SebekelerViewModel sebekelerViewModel = new SebekelerViewModel();
            using (Context con = new Context())
            {
                //string list = "";
                //List<string> list2 = new List<string>();
                for (int i = 0; i < id.Length; i++)
                {
                    if (id[i] == 0)
                    {
                        sebekelerViewModel.Drenaj = con.Drenaj.QueryStringAsList($"select * from DRENAJ").ToList();
                        break;
                    }
                    else
                    {
                        string Reggionname = con.Region.QueryStringFind($"select * from MELOREGIONS where OBJECTID_1='{id[i]}'").NAME_AZ;
                        //list2.Add(Reggionname);
                        //list += Reggionname + ",";
                        sebekelerViewModel.Drenaj = con.Drenaj.QueryStringAsList($"select * from DRENAJ where PASSING_AREAS ='{Reggionname}'").ToList();
                    }
                }
                //list = list.Remove(list.Length - 1, 1);
                //sebekelerViewModel.Drenaj = con.Drenaj.QueryStringAsList($"select * from DRENAJ where PASSING_AREAS IN ({list2})").ToList();

                sebekelerViewModel.riverbandcs = con.riverband.QueryStringAsList("select * from RIVERBAND").ToList();
            }          

            return Json(new { data = sebekelerViewModel }, JsonRequestBehavior.AllowGet);
        }
    }
}