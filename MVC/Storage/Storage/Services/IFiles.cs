using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Storage.Services
{
    public interface IFiles
    {
        IEnumerable<StoreFile> Get();
        Task<StoreFile> GetById(int id);
        Task<StoreFile> Upload(HttpPostedFileBase upload, int parentid);
        Task<bool> Rename(int id, string name);
        Task<bool> Remove(int id);
        void RemoveParent(int id);
        Task<string> Load(int id);
    }
}
