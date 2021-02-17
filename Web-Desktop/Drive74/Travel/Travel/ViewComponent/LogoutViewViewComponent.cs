using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Component
{
    public class LogoutViewViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
