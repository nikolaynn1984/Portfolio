using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Landing.Interface;
using Landing.Model.Data;
using Landing.Server.Database.Query;

namespace Landing.Server.Data
{
    /// <summary>
    /// Блог
    /// </summary>
    public class BlogData : IBlog
    {
        private readonly BaseImages baseImages;
        private readonly BaseBlog baseBlog;
        public BlogData()
        {
            this.baseBlog = new BaseBlog(Connection.GetConnectionString());
            this.baseImages = new BaseImages(Connection.GetConnectionString());
        }
        /// <summary>
        /// Получить все записи
        /// </summary>
        /// <returns>Список</returns>
        public async Task<IEnumerable<Blog>> GetItems()
        {
            List<Blog> list = await baseBlog.GetItems();
            List<Images> images = await baseImages.GetItemByLocation("Blog");
            if(list != null)
            {
                foreach (var item in list)
                {
                    if(item.ImageId != 0)
                    item.GetImages = images.SingleOrDefault(i => i.Id == item.ImageId);
                }
            }
           
            return list;
        }
        /// <summary>
        /// Получаем данные по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор </param>
        /// <returns>Модлеь данных</returns>
        public async Task<Blog> GetItemById(int id)
        {
            Blog item = await baseBlog.GetItemById(id);
            if (item != null)
            {
                if(item.ImageId != 0)
                item.GetImages = await baseImages.GetItemById(item.ImageId);
            }

            return item;
        }
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="model">Объект Blog </param>
        /// <returns>Объект Blog с заданым идентификатором</returns>
        public async Task<Blog> Add(Blog blog)
        {
            try
            {
                if(blog.ImageId != 0)
                {
                    blog.GetImages = await baseImages.GetItemById(blog.ImageId);
                }  
                blog = await baseBlog.Add(blog);
            }
            catch
            {
                return null;
            }
            return blog;
        }
        /// <summary>
        /// Удаление записи 
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>true - если редактирование прошло успешно, в противном случае false</returns>
        public async Task<(bool Result, string Error)> Delete(int id)
        {
            bool result = true;
            string error = "Ok";
            try
            {
                var item = await baseBlog.Remove(id);
                if (!item)
                {
                    return (Result: false, Error: "Идентификатор не найден");
                }
            }
            catch
            {
                result = false;
                error = "Что то пошло не так";
            }
            return (Result: result, Error: error);
        }
        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="model">Объект Blog с обновлеными даными</param>
        /// <returns>Blog если редактирование прошло успешно, в противном случа null</returns>
        public async Task<Blog> Edit(Blog blog)
        {
            try
            {
                if(blog.ImageId != 0)
                {
                    blog.GetImages = await baseImages.GetItemById(blog.ImageId);
                }
                Blog item = await baseBlog.Edit(blog);

                return item;
            }
            catch
            {
                return null;
            }            
        }
    }
}
