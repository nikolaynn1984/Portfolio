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
        public ActionResult AddFolder(int id, string name, int parentid)
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
        public ActionResult RemoveFolder(int id)
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
        [HttpPost]
        public ActionResult RemoveFile(int id)
        {
            try
            {
                bool result = repository.RemoveFile(id);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Index");
            }
            
        }
        [HttpPost]
        public ActionResult RenameFile(int id, string name)
        {
            try
            {
                bool result = repository.RenameFile(id, name);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Index");
            }
            
        }
        [HttpPost]
        public async  Task<ActionResult> UploadFile(int id)
        {
            try
            {
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase file = files[0];
                Models.File fileresult = await repository.UploadFile(file, id);
                return Json(fileresult, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Index");
            }
            
        }
        [HttpGet]
        public async Task<ActionResult> GetFileById(int id)
        {
            var file = await repository.GetFileById(id);
            return Json(file, JsonRequestBehavior.AllowGet);
        }
        
        public FileResult DowloadFile(int id)
        {
            string path = repository.LoadFile(id);
            string name = Path.GetFileName(path);
            string mime = MimeMapping.GetMimeMapping(path);
            return File(path, mime, name);
        }
    }
}