﻿using Assignment_4_Cloud_Project.Models;
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

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //_logger = logger;
        //}

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
            //List<Supplier> tab = dbContext.Suppliers.AsEnumerable()
            //.Select(t => new Supplier
            //{
            //    provider_name = ["provider_name"],
            //    drg_definition = ["drg_definition"],
            //    provider_street_address = ["provider_street_address"],
            //    average_covered_charges = ["average_covered_charges"],
            //    average_medicare_payments = ["average_medicare_payments"]
            //})
            //.ToList();

            return View("Data");
        }
        public IActionResult Visualization()
        {
            return View();
        }

        //public IActionResult Update()
        //{
        //    APIHandler webHandler = new APIHandler();
        //    //Dictionary<string, string> result = webHandler.GetMedDetails();
        //    List<MedicalData> result = webHandler.GetMedDetails();
        //    return View();
        //}

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
                //obj1.provider_id = value.provider_id;

                dbContext.Add(obj);
                dbContext.Add(obj1);
                //dbContext.SaveChanges();
                //dbContext.Add(value);
                //dbContext.MedicalDatas.Add(value);
                //dbContext.SaveChanges();
                //dbContext.MedicalDatas.AsNoTracking();
            }

            await dbContext.SaveChangesAsync();
            return View("Index",result);
        }

        public IActionResult EditData()
        {
            return View();
        }

        //Create 
        //public async Task<ViewResult> Edit()
        //{
        //    APIHandler webHandler = new APIHandler();
        //    List<MedicalData> result = webHandler.GetMedDetails();

        //    foreach (var item in result)
        //    {
        //        if (dbContext.MedicalDatas.Count() == 0)
        //        {
        //            dbContext.MedicalDatas.Add(item);
        //        }
        //    }
        //    foreach (var item in dbContext.MedicalDatas)
        //    {
        //        Supplier supplier = new Supplier();
        //        Locale locale = new Locale();
        //        supplier.provider_id = item.provider_id;
        //        supplier.provider_name = item.provider_name;
        //        supplier.provider_street_address = item.provider_street_address;
        //        supplier.provider_zip_code = item.provider_zip_code;
        //        supplier.total_discharges = item.total_discharges;
        //        supplier.drg_definition = item.drg_definition;
        //        supplier.average_covered_charges = item.average_covered_charges;
        //        supplier.average_medicare_payments = item.average_medicare_payments;
        //        supplier.average_medicare_payments_2 = item.average_medicare_payments_2;
        //        locale.provider_city = item.provider_city;
        //        locale.provider_state = item.provider_state;
        //        locale.hospital_referral_region_description = item.hospital_referral_region_description;
        //        dbContext.Locales.Add(locale);
        //        supplier.Locale = locale;
        //        dbContext.Suppliers.Add(supplier);
        //    }
        //    await dbContext.SaveChangesAsync();
        //    return View("Index", result);
        //}

        //public IActionResult Data(string searchProvState, string searchProvCity, string sortOrder)
        //{
        //    IQueryable<Supplier> Hosp = dbContext.Suppliers.Include(p => p.Locale);
        //    if (!String.IsNullOrEmpty(searchProvState))
        //    {
        //        Hosp = Hosp.Where(p => p.Locale.provider_state.Contains(searchProvState));
        //    }
        //    if (!String.IsNullOrEmpty(searchProvCity))
        //    {
        //        Hosp = Hosp.Where(p => p.Locale.provider_city.Contains(searchProvCity));
        //    }

        //    var HospsProvNames = Hosp;
        //    var HospsProvCities = Hosp;
        //    ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "namesDesc" : "namesAsc";
        //    ViewBag.CitySortParam = String.IsNullOrEmpty(sortOrder) ? "cityDesc" : "cityAsc";
        //    ViewBag.StateSortParam = String.IsNullOrEmpty(sortOrder) ? "states" : "";
        //    ViewBag.DischargesSortParam = sortOrder == "MinDis" ? "discharges" : "MinDis";
        //    ViewBag.CovChargesSortParam = sortOrder == "MinCovCharge" ? "covCharges" : "MinCovCharge";
        //    ViewBag.TotalPmtsSortParam = sortOrder == "MinTotalPmts" ? "totalPmts" : "MinTotalPmts";
        //    ViewBag.MedicSortParam = sortOrder == "MinMedic" ? "medicare" : "MinMedic";
        //    switch (sortOrder)
        //    {
        //        case "namesDesc":
        //            Hosp = Hosp.OrderByDescending(h => h.provider_name);
        //            break;
        //        case "namesAsc":
        //            Hosp = Hosp.OrderBy(h => h.provider_name);
        //            break;
        //        case "cityDesc":
        //            Hosp = Hosp.OrderByDescending(h => h.Locale.provider_city);
        //            break;
        //        case "cityAsc":
        //            Hosp = Hosp.OrderBy(h => h.Locale.provider_city);
        //            break;
        //        case "states":
        //            Hosp = Hosp.OrderByDescending(h => h.Locale.provider_state);
        //            break;
        //        case "MinDis":
        //            Hosp = Hosp.OrderBy(h => h.total_discharges);
        //            break;
        //        case "discharges":
        //            Hosp = Hosp.OrderByDescending(h => h.total_discharges);
        //            break;
        //        case "MinCovCharge":
        //            Hosp = Hosp.OrderBy(h => h.average_covered_charges);
        //            break;
        //        case "covCharges":
        //            Hosp = Hosp.OrderByDescending(h => h.average_covered_charges);
        //            break;
        //        case "MinTotalPmts":
        //            Hosp = Hosp.OrderBy(h => h.average_medicare_payments);
        //            break;
        //        case "totalPmts":
        //            Hosp = Hosp.OrderByDescending(h => h.average_medicare_payments);
        //            break;
        //        case "MinMedic":
        //            Hosp = Hosp.OrderBy(h => h.average_medicare_payments_2);
        //            break;
        //        case "medic":
        //            Hosp = Hosp.OrderByDescending(h => h.average_medicare_payments_2);
        //            break;
        //        default:
        //            Hosp = Hosp.OrderBy(h => h.Locale.provider_state);
        //            break;
        //    }
        //    return View(Hosp.ToList());
        //}

        //public IActionResult AboutUs()
        //{
        //    return View();
        //}

        //List<Supplier> suppliers = new List<Supplier>();
        //List<Locale> locales = new List<Locale>();

        //public IActionResult Visualization()
        //{
        //    IQueryable<Hospital> Hosp = dbContext.Hospitals
        //                             .GroupBy(h => h.provider_state)
        //                             .Select(cl => new Hospital
        //                             {
        //                                 provider_state = cl.Key,
        //                                 total_discharges = cl.Sum(c => c.total_discharges),
        //                                 average_medicare_payments = cl.Average(c => c.average_medicare_payments),
        //                                 average_medicare_payments_2 = cl.Average(c => c.average_medicare_payments_2),
        //                                 average_covered_charges = cl.Average(c => c.average_covered_charges)
        //                             })
        //                             .OrderBy(h => h.provider_state);

        //    List<string> State = new List<string>();
        //    foreach (var item in Hosp)
        //    {
        //        State.Add(item.provider_state);
        //    }
        //    List<int> TotalDischarges = new List<int>();
        //    foreach (var item in Hosp)
        //    {
        //        TotalDischarges.Add(item.total_discharges);
        //    }
        //    ViewBag.Title = "Total Discharges by State";
        //    ViewBag.Desc = new List<string> { "We can infer that the number of medical discharges per year is correlated with the usage of the Health Care System. How about looking at the number of Discharges per State? What states use the Health Care System the most?", "The State of Florida has the most discharges: 991", "The State of Arkansas has the less discharges: 11", "The Average of Total Discharges is: 27" };
        //    ViewBag.Data = String.Join(",", TotalDischarges.Select(d => d));
        //    ViewBag.Labels = String.Join(",", State.Select(d => "\"" + d + "\""));
        //    ViewBag.Label = "Total Discharges";

        //    return View("Visualization", Hosp);
        //}

        public IActionResult StateAveragePayments()
        {
            IQueryable<Supplier> Hosp = dbContext.Suppliers
                                                 .GroupBy(h => h.provider_name)
                                                 .Select(cl => new Supplier
                                                 {
                                                     provider_name = cl.Key,
                                                     total_discharges = cl.Sum(c => c.total_discharges),
                                                     average_medicare_payments = cl.Average(c => c.average_medicare_payments),
                                                     average_medicare_payments_2 = cl.Average(c => c.average_medicare_payments_2),
                                                     average_covered_charges = cl.Average(c => c.average_covered_charges)
                                                 })
                                                 .OrderBy(h => h.provider_name);

            List<string> State = new List<string>();
            foreach (var item in Hosp)
            {
                State.Add(item.provider_name);
            }
            List<float> TotalPayments = new List<float>();
            foreach (var item in Hosp)
            {
                TotalPayments.Add(item.average_medicare_payments);
            }

            ViewBag.Title = "Average Total Payments by State";
            ViewBag.Desc = new List<string> { "How do states differ in their average charges for a DRG? Here is the information with the most expensive states coming first:", "The State of Hawaii has the highest average total payment of: $156,158", "The State of Alabama has the lowest average total payment of:  $2,673", "Average Total of Payment by State is $9,707" };
            ViewBag.Data = String.Join(",", TotalPayments.Select(d => d));
            ViewBag.Labels = String.Join(",", State.Select(d => "\"" + d + "\""));
            ViewBag.Label = "Average Payments";

            return View("Visualization", Hosp);
        }

        public IActionResult StatePaymentDifference()
        {
            IQueryable<Supplier> Hosp = dbContext.Suppliers
                                                 .GroupBy(h => h.provider_name)
                                                 .Select(cl => new Supplier
                                                 {
                                                     provider_name = cl.Key,
                                                     total_discharges = cl.Sum(c => c.total_discharges),
                                                     average_medicare_payments = cl.Max(c => c.average_medicare_payments) - cl.Min(c => c.average_medicare_payments),
                                                     average_medicare_payments_2 = cl.Max(c => c.average_medicare_payments_2) - cl.Min(c => c.average_medicare_payments_2),
                                                     average_covered_charges = cl.Max(c => c.average_covered_charges) - cl.Min(c => c.average_covered_charges)
                                                 })
                                                 .OrderBy(h => h.provider_name);

            List<string> State = new List<string>();
            foreach (var item in Hosp)
            {
                State.Add(item.provider_name);
            }
            List<float> PaymentDifference = new List<float>();
            foreach (var item in Hosp)
            {
                PaymentDifference.Add(item.average_medicare_payments);
            }

            ViewBag.Title = "Difference in Average Total Payments by State";
            ViewBag.Desc = new List<string> { "How about the difference between the highest and the lowest charges in the same state? Here is the breakdown of locations with the largest difference, which can help find more affordable options in a region.", "The State of Illinois has the highest difference in total payments", "he State of Hawaii has the lowest difference in total payments" };
            ViewBag.Data = String.Join(",", PaymentDifference.Select(d => d));
            ViewBag.Labels = String.Join(",", State.Select(d => "\"" + d + "\""));
            ViewBag.Label = "Average Payments";

            return View("Visualization", Hosp);
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
        // GET: /Movies/Delete/5
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

        // POST: /Movies/Delete/5

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
