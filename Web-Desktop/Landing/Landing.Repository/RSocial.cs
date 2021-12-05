using System;
using Landing.Library.Model;
using Landing.Repository.Interfase;
using Landing.Repository.Loader;
using System.Collections.Generic;
using System.Threading.Tasks;
using Landing.Library.Interfaces;

namespace Landing.Repository
{
    public class RSocial : ISocial
    {
        public static event Action<Social> SocialEventChanged;

        private readonly IDataHandler<Social> data;
        private static Social social;
        private readonly RImages images;
        /// <summary>
        /// Конструкто списка соц сетей
        /// </summary>
        /// <param name="url">Путь API</param>
        /// <param name="token">Токен</param>
        public RSocial(string url, string token)
        {
            social = new Social();
            this.data = new DataHandler<Social>(social, url, token);
            images = new RImages(url, token);
        }

        /// <summary>
        /// Получение списка данных
        /// </summary>
        /// <returns>Список даныых</returns>
        public async Task<IEnumerable<Social>> GetItems()
        {
            IEnumerable<Social> list = await data.GetItems();
            foreach (Social item in list)
            {
                if (item.ImageId != 0)
                    await images.Download(item.GetImages);
            }
            return list;
        }
        /// <summary>
        /// Данные по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Модель с данными</returns>
        public async Task<Social> GetItemById(int id)
        {
            Social item = await data.GetItemById(id);
            if(item.ImageId != 0) await images.Download(item.GetImages);
            return item;
        }
        /// <summary>
        /// Добавить поле с картинкой
        /// </summary>
        /// <param name="model">Модель данных</param>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Редактированная модель</returns>
        public async Task<Social> Add(Social model, string path)
        {
            Images img = await images.Upload(path, FileLoacations.Social, model.Name);
            if (img != null)
            {
                model.ImageId = img.Id;
                Social item = await data.Add(model);
                model.GetImages = img;
                if (item != null)
                {
                    await images.Download(img);
                    SocialEventChanged?.Invoke(item);
                }

                return item;
            }

            return null;
        }
        /// <summary>
        /// Добавить поле с картинкой
        /// </summary>
        /// <param name="model">Модель данных</param>
        /// <param name="bytes">биты</param>
        /// <param name="name">Название</param>
        /// <returns>Редактированая модель данных</returns>
        public async Task<Social> Add(Social model, byte[] bytes, string name)
        {
            Images img = await images.Upload(bytes, name, FileLoacations.Social, model.Name);
            if (img != null)
            {
                model.ImageId = img.Id;
                
                Social item = await data.Add(model);
                model.GetImages = img;
                if (item != null)
                {
                    await images.Download(img);
                    SocialEventChanged?.Invoke(item);
                }

                return item;
            }

            return null;
        }
        /// <summary>
        /// Добавление данных
        /// </summary>
        /// <param name="model">Добавляемые данные</param>
        /// <returns>Данные с идентификатором если успех, в противном случае null</returns>
        public async Task<Social> Add(Social model)
        {
            var item = await data.Add(model);
            if (item != null) SocialEventChanged?.Invoke(item);

            return item;
        }
        /// <summary>
        /// Удаление данных по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>true -если успешно, в пртивном случае false</returns>
        public async Task<(bool Result, string Error)> Delete(int id)
        {
            var item = await data.Delete(id);
            if (!item) return (Result: item, Error: "Что то пошло не так");
            return (Result: item, Error: "");
        }
        /// <summary>
        /// Редактирование поля с путем файла
        /// </summary>
        /// <param name="model">Модель данных</param>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Модель данных если редактирование прошло успешно</returns>
        public async Task<Social> Edit(Social model, string path)
        {
            Images img = await images.Upload(path, FileLoacations.Social, model.Name);
            if (img != null)
            {
                model.ImageId = img.Id;

                Social item = await data.Edit(model);
                model.GetImages = img;
                if (item != null)
                {
                    await images.Download(img);
                    _ = Task.Run(() => SocialEventChanged?.Invoke(item));
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
        public async Task<Social> Edit(Social model, byte[] bytes, string name)
        {
            Images img = await images.Upload(bytes, name, FileLoacations.Social, model.Name);
            if (img != null)
            {
                model.ImageId = img.Id;

                Social item = await data.Edit(model);
                model.GetImages = img;
                if (item != null)
                {
                    await images.Download(img);
                    SocialEventChanged?.Invoke(item);
                }
                return item;
            }

            return null;
        }
        /// <summary>
        /// Редактирование данных
        /// </summary>
        /// <param name="model">Обновленные данные</param>
        /// <returns>Обновленные данные если успех, в противном случае null</returns>
        public async Task<Social> Edit(Social model)
        {
            var item = await data.Edit(model);
            if (item != null) SocialEventChanged?.Invoke(item);
            return item;
        }
    }
}
