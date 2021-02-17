using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelDatabase;
using TravelDatabase.Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        Repository repository = new Repository();
        //https://localhost:44329/
        // GET api/values
        [HttpGet]
        [Route("cities")]
        public ActionResult<IEnumerable<Cityes>> GetCities()
        {
            return repository.GetCityes().ToList();
        }

        [HttpGet]
        [Route("cars")]
        public ActionResult<IEnumerable<Car>> GetCars()
        {
            return repository.GetCars().ToList();
        }

        [HttpGet]
        [Route("rides")]
        public ActionResult<IEnumerable<Ride>> GetRides()
        {
            return repository.GetRidesList().ToList();
        }



        [HttpPost]
        public void Post([FromBody] Ride ride)
        {
            repository.AddRide(ride);
        }
    }
}
