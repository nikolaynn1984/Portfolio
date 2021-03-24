using Storage.Models;
using Storage.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Storage.Repository;
using System.IO;
using System.Threading.Tasks;

namespace Storage.Controllers
{
    public class HomeController : Controller
    {
        private FilesRepository repository;
        public HomeController()
        {
            this.repository = new FilesRepository();
        }
        [HttpGet]
        public ActionResult Index()
        {
            var model = new FilesViewModel()
            {
                Seed = 0,
                GetFolders = repository.GetFolders(),
                GetFiles = repository.GetFiles()
                
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(int id, string name, int parentid)
        {
            try
            {
                if (!ModelState.IsValid) return Json(false, JsonRequestBehavior.AllowGet);
                Folder file = new Folder();
                bool result = repository.AddFolder(id, name, parentid, out file);
                if (result) return Json(file, JsonRequestBehavior.AllowGet);
                else return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Index");
            }
           
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                bool result = repository.RemoveFolder(id);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Index");
            }
            
        }
        
       
       
        
        
        
    }
}