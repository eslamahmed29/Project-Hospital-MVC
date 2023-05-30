using el7elm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace el7elm.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        admin ad = new admin();
        DB db = new DB();

        //show list of admins
        
        public ActionResult Show()
        {
            return View(db.admins.ToList());
        }

        // details
        [Authorize]
        [HttpGet, ActionName("Admin_Details")]
        public ActionResult Admin_Details(int? id)
        {
            admin ad = db.admins.Find(id);
            return View(ad);
        }
        [Authorize]
        [HttpPost, ActionName("Admin_Details")]
        public ActionResult Admin_Details(admin ad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ad).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Show");
            }
            return View();
        }

        // edit 
        [Authorize]
        public ActionResult Admin_Edit(int? id)
        {
            admin ad = db.admins.Find(id);
            return View(ad);
        }
        [Authorize]
        [HttpPost, ActionName("Admin_Edit")]
        public ActionResult Admin_Edit(admin ad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ad).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Show");
            }
            return View();
        }
        [Authorize]
        // add admin
        [HttpGet]
        public ActionResult Add_Admin()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Add_Admin(admin ad)
        {
            if (ModelState.IsValid)
            {
                db.admins.Add(ad);
                db.SaveChanges();
                return RedirectToAction("Show");
            }
            return View();
        }

        // generate bill 
        [Authorize]
        [HttpGet]
        public ActionResult Generate_bill()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Generate_bill(payment p)
        {
            if (ModelState.IsValid)
            {
                db.payments.Add(p);
                db.SaveChanges();
                return RedirectToAction("Payment");
            }
            return View();
        }

        // payment (Show list of payment)
        [Authorize]
        public ActionResult Payment()
        {
            return View(db.payments.ToList());
        }

        // edit payment
        [Authorize]
        public ActionResult Pay_Edit(int? id)
        {
            payment p = db.payments.Find(id);
            return View(p);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Pay_Edit(payment p)
        {
            if (ModelState.IsValid)
            {
                db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Payment");
            }
            return View();
        }

       
    }
}