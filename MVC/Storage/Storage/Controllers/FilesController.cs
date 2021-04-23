using Storage.Repository;
using Storage.Services;
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
        private readonly FilesRepository files;
        public FilesController()
        {
            this.files = new FilesRepository();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Get(int id)
        {
            var file = await files.GetById(id);
            return Json(file, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(int id, string name)
        {
            try
            {
                bool result = await files.Rename(id, name);
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
                HttpFileCollectionBase file = Request.Files;
                HttpPostedFileBase fileNew = file[0];
                Models.StoreFile fileresult = await files.Upload(fileNew, id);
                return Json(fileresult, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                bool result = await files.Remove(id);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public async Task<FileResult> Dowload(int id)
        {
            string path = await files.Load(id);
            string name = Path.GetFileName(path);
            string mime = MimeMapping.GetMimeMapping(path);
            return File(path, mime, name);
        }
    }
}