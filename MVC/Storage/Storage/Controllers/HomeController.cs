using Storage.Models;
using Storage.Models.ViewModel;
using System.Web.Mvc;
using Storage.Repository;
using Storage.Services;

namespace Storage.Controllers
{
    public class HomeController : Controller
    {
        private readonly FolderRepository folders;
        private readonly FilesRepository files;
        public HomeController()
        {
            this.folders = new FolderRepository();
            this.files = new FilesRepository();
        }
        [HttpGet]
        public ActionResult Index()
        {
            var model = new FilesViewModel()
            {
                Seed = 0,
                GetFolders = folders.Get(),
                GetFiles = files.Get()
                
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
                bool result = folders.Add(id, name, parentid, out file);
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
                bool result = folders.Remove(id);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Index");
            }
            
        }
        
    }
}