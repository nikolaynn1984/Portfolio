using Storage.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Storage.Controllers
{
    public class FilesController : Controller
    {
        private FilesRepository repository;
        public FilesController()
        {
            repository = new FilesRepository();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Get(int id)
        {
            var file = await repository.GetFileById(id);
            return Json(file, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Edit(int id, string name)
        {
            try
            {
                bool result = repository.RenameFile(id, name);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public async Task<ActionResult> Upload(int id)
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
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                bool result = repository.RemoveFile(id);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public FileResult Dowload(int id)
        {
            string path = repository.LoadFile(id);
            string name = Path.GetFileName(path);
            string mime = MimeMapping.GetMimeMapping(path);
            return File(path, mime, name);
        }
    }
}