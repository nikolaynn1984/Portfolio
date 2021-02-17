using TravelDatabase;
using TravelDatabase.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using TravelDatabase.Interfaces;

namespace Travel.Controllers
{
    public class OffersController : Controller
    {
        private readonly IRidesData ridesData;

        public OffersController(IRidesData RidesData)
        {
            this.ridesData = RidesData;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(ridesData.GetRidesList());
        }
        [HttpPost]
        public IActionResult Index(int? from, int? to, DateTime? date)
        {
            return View(ridesData.SearchRides(from, to, date));
        }
        [HttpGet]
        public IActionResult AddRide()
        {
            ViewBag.CitiList = ridesData.GetCityes();
            ViewBag.CarList = ridesData.GetCars();
            return View();
        }
        [HttpPost]
        public IActionResult AddRide(Ride ride)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CitiList = ridesData.GetCityes();
                ViewBag.CarList = ridesData.GetCars();
               
                return View(ride);
            }
            ridesData.AddRide(ride);
            return RedirectToAction("Index");
        }

    }
}
