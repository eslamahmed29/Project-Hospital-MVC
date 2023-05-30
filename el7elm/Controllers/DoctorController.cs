using el7elm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace el7elm.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        doctor doc = new doctor();
        prescription pre = new prescription();
        appointment app = new appointment();
        DB db = new DB();

        // show doctors 
        [Authorize]
        public ActionResult Show_Doc()
        {
            return View(db.doctors.ToList());
        }
        // add doctors
        [Authorize]
        [HttpGet]
        public ActionResult Add_Doc()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Add_Doc(doctor d)
        {
            if (ModelState.IsValid)
            {
                db.doctors.Add(d);
                db.SaveChanges();
                return RedirectToAction("Show_Doc");
            }
            return View();
        }

        // edit doctor
        [Authorize]

        public ActionResult Edit_Doc(int? id)
        {
            doctor d = db.doctors.Find(id);
            return View(d);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit_Doc(doctor d)
        {
            if (ModelState.IsValid)
            {
                db.Entry(d).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Show_Doc");
            }
            return View();
        }
     

        // details of doctor 
        
        [HttpGet]
        public ActionResult Details_Doc(int id)
        {
            doctor d = db.doctors.Find(id);
            return View(d);
        }
        
        [HttpPost]
        public ActionResult Details_Doc(doctor d)
        {
            if (ModelState.IsValid)
            {
                db.Entry(d).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Show_Doc");
            }
            return View();
        }


        // prescription 

        // make prescription 
        [Authorize]
        [HttpGet]
        public ActionResult Make_Pre()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Make_Pre(prescription pre)
        {
            int? id = HomeController.id_doc;
            pre.doc_id = id;
            if (ModelState.IsValid)
            {
                if (HasNullFields(pre))
                {
                    ModelState.AddModelError("", "Please fill in all the required fields.");
                    return View(pre);
                }
                db.prescriptions.Add(pre);
                db.SaveChanges();
                return RedirectToAction("Show_Pre");
            }
            return View();
        }
        private bool HasNullFields(prescription pre)
        {
            // Check if any required fields are null
            // Return true if any field is null, false otherwise

            // Example implementation:
            if ((pre.patient_id == null) ||
                string.IsNullOrEmpty(pre.description)
                )
            {
                return true;
            }

            return false;
        }


        // show prescription

        [Authorize]
        public ActionResult Show_Pre()
        {
            var one = db.prescriptions.ToList();
            var two = new List<relation_model>();
            foreach (var item in one)
            {
                if (HomeController.id_doc == item.doc_id)
                {
                    var x = new relation_model
                    {
                        pre_id = item.pre_id,
                        description = item.description,
                        doc_id = item.doc_id,
                        patient_id = item.patient_id,
                    };
                    var pat = db.patients.Where(ef => ef.patient_id == item.patient_id).FirstOrDefault();
                    if (pat != null)
                    {
                        x.patient_phone = pat.phone;
                        x.patient_birthdate = pat.birthdate;
                        x.patient_address = pat.address;
                        x.patient_name = pat.name;
                        x.patient_gender = pat.gender;
                    }
                    two.Add(x);
                }
            }
            return View(two);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Show_Pre(relation_model model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            return Content("Success");
        }
        // edit prescription 

        [Authorize]
        public ActionResult Edit_Pre(int? id)
        {
            prescription pre = db.prescriptions.Find(id);
            return View (pre);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit_Pre(prescription pre)
        {
            pre.doc_id = HomeController.id_doc;
            if (ModelState.IsValid)
            {
                db.Entry(pre).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Show_Pre");
            }
            return View();
        }

       

        // show appointment 

        [Authorize]
        public ActionResult Show_App()
        {
            var res = db.appointments.Where(a => a.doc_id == HomeController.id_doc).ToList();
            if (res == null)
            {
                ModelState.AddModelError("", "No one Here");
            }
            return View(res);
        }

        // schedule appointment 
        [Authorize]
        [HttpGet]
        public ActionResult Schedule_App()
        {
            return View ();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Schedule_App(avail_appointments avail)
        {
            avail.doc_id = HomeController.id_doc;
            if (ModelState.IsValid)
            {
                db.avail_appointments.Add(avail);
                db.SaveChanges();
                return RedirectToAction("Show_Avail");
            }
            return View();
        }

        // show avail appointment 

        [Authorize]
        public ActionResult Show_Avail()
        {

            return View (db.avail_appointments.ToList());
        }

    }
}