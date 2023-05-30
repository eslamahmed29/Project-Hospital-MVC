using el7elm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace el7elm.Controllers
{
    public class PatientController : Controller
    {
        patient p = new patient();
        DB db = new DB();
        // GET: Patient

        // patient register 
        [HttpGet]
        public ActionResult Reg_Pat()
        {
             return View();
        }
        [HttpPost]
        public ActionResult Reg_Pat(patient pat)
        {
            if(ModelState.IsValid)
            {
                if (HasNullFields(p))
                {
                    ModelState.AddModelError("", "Please fill in all the required fields.");
                    return View(p);
                }
                if (!Regex.IsMatch(p.email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                {
                    ModelState.AddModelError("Email", "Invalid email format.");
                    return View(p);
                }
                db.patients.Add (pat);
                db.SaveChanges();
                return RedirectToAction("Login","Home");
            }
            return View();
        }
        private bool HasNullFields(patient patient)
        {
            // Check if any required fields are null
            // Return true if any field is null, false otherwise

            // Example implementation:
            if (string.IsNullOrEmpty(patient.name) ||
                string.IsNullOrEmpty(patient.email) ||
                string.IsNullOrEmpty(patient.password) ||
                 string.IsNullOrEmpty(patient.phone) ||
                 patient.gender == "Select Gender" ||
                 string.IsNullOrEmpty(patient.address)
                )
            {
                return true;
            }

            return false;
        }

        // show prescription 
        [Authorize]
        public ActionResult My_Pre()
        {
            var res = db.prescriptions.Where(a => a.patient_id == HomeController.id_pat).ToList();
            
             return View (res);
        }

        // make appointment
        [Authorize]
        [HttpGet]

        public ActionResult Make_App()
        {
            return View ();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Make_App(appointment app)
        {
            app.patient_id = HomeController.id_pat;
            if(ModelState.IsValid)
            {
                db.appointments.Add(app); db.SaveChanges();
                return RedirectToAction("Show_Avail", "Doctor");
            }
            return View();
        }

        // show patient 
        
        public ActionResult Show_Patient()
        {
            return View(db.patients.ToList());
        }

       

    }
}