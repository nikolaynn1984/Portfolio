using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Landing.Interface;
using Landing.Model.Data;
using Landing.Repository.Interfase;
using Landing.Repository.Loader;

namespace Landing.Repository
{
    public class RPortfolio : IPortfolio
    {
        public static event Action<Portfolio> PortfolioEventChanged;

        private readonly IDataHandler<Portfolio> data;
        private static Portfolio portfolio;
        private readonly RImages images;
        public RPortfolio(string url, string token)
        {
            portfolio = new Portfolio();
            this.data = new DataHandler<Portfolio>(portfolio, url, token);
            this.images = new RImages(url, token);
        }
        /// <summary>
        /// Получение списка данных и загрузаем тзображение
        /// </summary>
        /// <returns>Список даныых</returns>
        public async Task<IEnumerable<Portfolio>> GetItems()
        {
            IEnumerable<Portfolio> list = await data.GetItems();
            foreach(Portfolio item in list)
            {
                if (item.ImageId != 0)
                    await images.Download(item.GetImages);
            }
           
            return await data.GetItems(); 
        }
        /// <summary>
        /// Данные по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Модель с данными</returns>
        public async Task<Portfolio> GetItemById(int id)
        {
            Portfolio item = await data.GetItemById(id);
            if (item.ImageId != 0) await images.Download(item.GetImages);
            return item;
        }
        /// <summary>
        /// Добавление проекта в портфолии с картинкой
        /// </summary>
        /// <param name="model">Модель проекта с данными</param>
        /// <param name="path">путь к изображению</param>
        /// <returns>Модель портфоли с идентификатором</returns>
        public async Task<Portfolio> Add(Portfolio model, string path)
        {

            Images img = await images.Upload(path, FileLoacations.Portfolio, model.Name);
            if (img != null)
            {
                model.ImageId = img.Id;
                Portfolio item = await data.Add(model);
                model.GetImages = img;
                if (item != null)
                {
                    await images.Download(img);
                    PortfolioEventChanged?.Invoke(item);
                }

                return item;
            }

            return null;
            
        }
        /// <summary>
        /// Добавление проекта в портфолии с картинкой
        /// </summary>
        /// <param name="model">Модель проекта с данными</param>
        /// <param name="bytes">Массив бит</param>
        /// <param name="name">Название файла</param>
        /// <returns>Модель портфоли с идентификатором</returns>
        public async Task<Portfolio> Add(Portfolio model, byte[] bytes, string name)
        {
            Images img = await images.Upload(bytes, name,  FileLoacations.Portfolio, model.Name);
            if (img != null)
            {
                model.ImageId = img.Id;
                Portfolio item = await data.Add(model);
                model.GetImages = img;
                if (item != null)
                {
                    await images.Download(img);
                    PortfolioEventChanged?.Invoke(item);
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
        public async Task<Portfolio>  Add(Portfolio model )
        {
            model.ImageId = 0;
            Portfolio item = await data.Add(model);
            if (item != null)
            {
                PortfolioEventChanged?.Invoke(item);
            }
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
        /// Редактирование проекта с путем файла
        /// </summary>
        /// <param name="model">Модель данных</param>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Модель данных если редактирование прошло успешно</returns>
        public async Task<Portfolio> Edit(Portfolio model, string path)
        {
            Images img = await images.Upload(path, FileLoacations.Portfolio, model.Name);
            if(img != null)
            {
                model.ImageId = img.Id;
                model.GetImages = img;
                Portfolio item = await data.Edit(model);
                if (item != null)
                {
                    await images.Download(img);
                    PortfolioEventChanged?.Invoke(item);
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
        public async Task<Portfolio> Edit(Portfolio model, byte[] bytes, string name)
        {
            Images img = await images.Upload(bytes, name, FileLoacations.Portfolio, model.Name);
            if (img != null)
            {
                model.ImageId = img.Id;
                model.GetImages = img;
                Portfolio item = await data.Edit(model);
                if (item != null)
                {
                    await images.Download(img);
                    PortfolioEventChanged?.Invoke(item);
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
        public async Task<Portfolio> Edit(Portfolio model)
        {
            Portfolio item = await data.Edit(model);
            if (item != null)
            {
                await images.Download(item.GetImages);
                PortfolioEventChanged?.Invoke(item);
            }
            return item;
        }


    }
}
