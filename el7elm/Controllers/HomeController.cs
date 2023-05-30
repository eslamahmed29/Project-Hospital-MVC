using el7elm.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace el7elm.Controllers
{
    public class Login_model
    {

        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }

    }
    public class HomeController : Controller
    {
        DB db = new DB();
        admin ad = new admin();
        doctor doc = new doctor();
        patient pat= new patient();
        public static bool is_adm { get; set; }
        public static bool is_doc { get; set; }
        public static bool is_pat { get; set; }

        public static int? id_doc { get; set; }
        public static int? id_adm { get; set; }
        public static int? id_pat { get; set; }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login_model log)
        {
            //doctor doc = new doctor();
            //admin ad    = new admin();
            //patient pat=new patient();

            if (is_admin(log.email, log.password))
            {
                FormsAuthentication.SetAuthCookie(log.email, false);
                return RedirectToAction("Show", "Admin");
            }
            else if (is_doctor(log.email, log.password))
            {
                FormsAuthentication.SetAuthCookie(log.email, false);
                return RedirectToAction("Show_App", "Doctor");
            }
            else if (is_patient(log.email, log.password))
            {
                FormsAuthentication.SetAuthCookie(log.email, false);
                return RedirectToAction("My_Pre", "Patient");
            }
            else
            {
                ModelState.AddModelError("", "invalid email or password");
            }
            return View(log);
        }

        public bool is_patient(string un, string pass)
        {
            var res3 = db.patients.Where(p => p.email == un && p.password == pass).FirstOrDefault();
            if (res3 != null)
            {
                is_pat = true;
                id_pat = res3.patient_id;
                return true;
            }
            return false;
        }
        public bool is_admin(string un, string pass)
        {
            var res1 =  db.admins.Where(a => un == a.email && pass == a.password).FirstOrDefault();
            if (res1 != null)
            {
                is_adm = true;
                id_adm = res1.admin_id;
                return true;

            }
            return false;
        }
        public bool is_doctor(string un, string pass)
        {
            var res2 = db.doctors.Where(d => d.email == un && d.password == pass).FirstOrDefault();
            if (res2 != null)
            {
                is_doc = true;
                id_doc = res2.doc_id;
                return true;
            }
            return false;
        }

        public ActionResult Index()
        {
            return View();
        }

        // logout

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();

            HttpCookie rFormsCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            rFormsCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(rFormsCookie);

            HttpCookie rSessionCookie = new HttpCookie("ASP.NET_SessionId", "");
            rSessionCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(rSessionCookie);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();


            return RedirectToAction("Index", "Home");
        }

    }
}