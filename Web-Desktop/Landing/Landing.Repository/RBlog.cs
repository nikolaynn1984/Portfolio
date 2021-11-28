using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Landing.Interface;
using Landing.Model.Data;
using Landing.Repository.Interfase;
using Landing.Repository.Loader;

namespace Landing.Repository
{
    /// <summary>
    /// Репозиторий Блог
    /// </summary>
    public class RBlog : IBlog
    {
        public static event Action<Blog> BlogEvenetChanged;

        private readonly IDataHandler<Blog> data;
        private static Blog blog;
        private readonly RImages images;
        public RBlog(string url, string token)
        {
            blog = new Blog();
            data = new DataHandler<Blog>(blog, url, token);
            images = new RImages(url, token);
        }

        /// <summary>
        /// Получаем список блогов
        /// </summary>
        /// <returns>Список</returns>
        public async Task<IEnumerable<Blog>> GetItems()
        {
            IEnumerable<Blog> list = await data.GetItems();
            foreach (Blog item in list)
            {
                if (item.ImageId != 0)
                    await images.Download(item.GetImages);
            }
            return list;
        }
        /// <summary>
        /// Получаем информацию о блоке по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Модель блог</returns>
        public async Task<Blog> GetItemById(int id)
        {
            Blog item = await data.GetItemById(id);
            if(item.ImageId != 0)  await images.Download(item.GetImages);
            return item;
        }
        /// <summary>
        /// Добавление блога с картинкой
        /// </summary>
        /// <param name="model">Модель блога с данными</param>
        /// <param name="path">путь к изображению</param>
        /// <returns>Модель блога с идентификатором</returns>
        public async Task<Blog> Add(Blog model, string path)
        {
            Images img = await images.Upload(path, FileLoacations.Blog, model.Name);
            if (img != null)
            {
                model.ImageId = img.Id;
                model.CreadDate = DateTime.Now;
                model.GetImages = img;
                Blog item = await data.Add(model);
                if (item != null)
                {
                    await images.Download(img);
                    BlogEvenetChanged?.Invoke(item);
                }

                return item;
            }

            return null;
        }
        /// <summary>
        /// Добавление блога с картинкой
        /// </summary>
        /// <param name="model">Модель блога с данными</param>
        /// <param name="bytes">Массив бит</param>
        /// <param name="name">Название файла</param>
        /// <returns>Модель блог с идентификатором</returns>
        public async Task<Blog> Add(Blog model, byte[] bytes, string name)
        {
            Images img = await images.Upload(bytes, name, FileLoacations.Blog, model.Name);
            if (img != null)
            {
                model.ImageId = img.Id;
                model.CreadDate = DateTime.Now;
                model.GetImages = img;
                Blog item = await data.Add(model);
                if (item != null)
                {
                    await images.Download(img);
                    BlogEvenetChanged?.Invoke(item);
                }

                return item;
            }

            return null;
        }
        /// <summary>
        /// Добавляем блок
        /// </summary>
        /// <param name="model">Модель с данными</param>
        /// <returns>Модель с присвоенным идентификатором если добавление прошло успешно, в противном случае null</returns>
        public async Task<Blog> Add(Blog model)
        {
            model.CreadDate = DateTime.Now;
            var item = await data.Add(model);
            if (item != null) BlogEvenetChanged?.Invoke(item);
            return item;
        }
        /// <summary>
        /// Удаление данных по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>true -если успешно, в пртивном случае false</returns>
        public async Task<(bool Result, string Error)> Delete(int id)
        {
            bool item = await data.Delete(id);
            if (!item) { return (Result: item, Error: "Что то пошло не так"); }
            return (Result: item, Error: "");
        }
        /// <summary>
        /// Редактирование блога с путем файла
        /// </summary>
        /// <param name="model">Модель данных</param>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Модель данных если редактирование прошло успешно</returns>
        public async Task<Blog> Edit(Blog model, string path)
        {
            Images img = await images.Upload(path, FileLoacations.Blog, model.Name);
            if (img != null)
            {
                model.ImageId = img.Id;
                model.GetImages = img;
                Blog item = await data.Edit(model);
                if (item != null)
                {
                    await images.Download(img);
                    BlogEvenetChanged?.Invoke(item);
                }
                return item;
            }

            return null;
        }
        /// <summary>
        /// Редактирование данных
        /// </summary>
        /// <param name="model">Модель данных</param>
        /// <param name="bytes">Масиив бит файла</param>
        /// <param name="name">Название файла</param>
        /// <returns>Модель данных если рузультат успешный</returns>
        public async Task<Blog> Edit(Blog model, byte[] bytes, string name)
        {
            Images img = await images.Upload(bytes, name, FileLoacations.Blog, model.Name);
            if (img != null)
            {
                model.ImageId = img.Id;
                model.GetImages = img;
                Blog item = await data.Edit(model);
                if (item != null)
                {
                    await images.Download(img);
                    BlogEvenetChanged?.Invoke(item);
                }
                return item;
            }

            return null;
        }
        /// <summary>
        /// Удаление данных по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>true -если успешно, в пртивном случае false</returns>
        public async Task<Blog> Edit(Blog model)
        {
            Blog item = await data.Edit(model);
            if (item != null) { BlogEvenetChanged?.Invoke(item); }

            return item;
        }


    }
}
