using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Travel.Controllers
{
    public class MainViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
