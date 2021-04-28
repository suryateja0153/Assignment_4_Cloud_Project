using Assignment_4_Cloud_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using Assignment_4_Cloud_Project.DataAccess;
using Microsoft.EntityFrameworkCore;
using Assignment_4_Cloud_Project.APIManager;
using System.Net;

namespace Assignment_4_Cloud_Project.Controllers
{
    public class HomeController : Controller
    {
        public Assignment_4_Cloud_ProjectDBContext dbContext;
        public HomeController(Assignment_4_Cloud_ProjectDBContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Intro()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Data()
        {
            List<Supplier> tab = dbContext.Suppliers.Take(10).ToList();

            return View(tab);
        }

        public async Task<ViewResult> Update()
        {
            APIHandler webHandler = new APIHandler();
            List<MedicalData> result = webHandler.GetMedDetails();

            Supplier obj = new Supplier();
            Locale obj1 = new Locale();

            foreach (MedicalData value in result)
            {
                obj.provider_id = value.provider_id;
                obj.provider_name = value.provider_name;
                obj.drg_definition = value.drg_definition;
                obj.provider_street_address = value.provider_street_address;
                obj.provider_zip_code = value.provider_zip_code;
                obj.total_discharges = value.total_discharges;
                obj.average_covered_charges = value.average_covered_charges;
                obj.average_medicare_payments = value.average_medicare_payments;
                obj.average_medicare_payments_2 = value.average_medicare_payments_2;
 
                obj1.provider_city = value.provider_city;
                obj1.provider_state = value.provider_state;
                obj1.hospital_referral_region_description = value.hospital_referral_region_description;

                dbContext.Add(obj);
                dbContext.Add(obj1);
            }

            await dbContext.SaveChangesAsync();
            return View("Index",result);
        }

        public IActionResult EditData()
        {
            return View();
        }

        public IActionResult Visualization()
        {
            List<Supplier> vis = dbContext.Suppliers.Take(10).ToList();

            return View(vis);
        }

        //Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                dbContext.Entry(supplier).State = EntityState.Modified;
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(supplier);
        }

        //Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Supplier supplier = dbContext.Suppliers.Find(id);

            if (supplier == null)
            {
                return HttpNotFound();
            }

            return View(supplier);
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = dbContext.Suppliers.Find(id);
            dbContext.Suppliers.Remove(supplier);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    internal class HttpStatusCodeResult : ActionResult
    {
        private HttpStatusCode badRequest;

        public HttpStatusCodeResult(HttpStatusCode badRequest)
        {
            this.badRequest = badRequest;
        }
    }
}
