using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Landing.Library.Model;
using Landing.Library.Interfaces;
using Landing.Server.Database.Query;

namespace Landing.Server.Data
{
    /// <summary>
    /// Обработка данных портфолио/проектов
    /// </summary>
    public class PortfolioData : IPortfolio
    {

        private readonly BasePortfolio database;
        private readonly BaseImages imagedata;
        
        public PortfolioData()
        {
            this.database = new BasePortfolio(Connection.GetConnectionString());
            this.imagedata = new BaseImages(Connection.GetConnectionString());

        }
        /// <summary>
        /// Получить все записи портфолио
        /// </summary>
        /// <returns>Список</returns>
        public async Task<IEnumerable<Portfolio>> GetItems()
        {
            List<Portfolio> list = new List<Portfolio>();
            list = await database.GetItems();
            List<Images> images = await imagedata.GetItemByLocation("Portfolio");
            
            foreach(Portfolio item in list)
            {
                if(item.ImageId != 0)
                {
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
        public async Task<Portfolio> GetItemById(int id)
        {
            Portfolio item = new Portfolio();
            item = await database.GetItemById(id);

            if(item.ImageId != 0)
            {
                item.GetImages = await imagedata.GetItemById(item.ImageId);
            }
            
            return item;
        }

        /// <summary>
        /// Добавление новой записи в портфолио
        /// </summary>
        /// <param name="item">Объект Portfolio </param>
        /// <returns>Объект Portfolio с заданым идентификатором</returns>
        public async Task<Portfolio> Add(Portfolio item)
        {
            try
            {
                if(item.ImageId != 0)
                {
                    item.GetImages = await imagedata.GetItemById(item.ImageId);
                }
                item = await database.Add(item);

                
            }
            catch
            {
                item = null;
            }
            return item;
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
                result = await database.Remove(id);
            }
            catch
            {
                result = false;
                error = "Что то пошло не так";
            }
            return (Result: result, Error: error);
        }
        /// <summary>
        /// Редактирование записи портфолио
        /// </summary>
        /// <param name="portfolio">Объект Portfolio с обновлеными даными</param>
        /// <returns>Portfolio если редактирование прошло успешно, в противном случа null</returns>
        public async Task<Portfolio> Edit(Portfolio portfolio)
        {
            if(portfolio.ImageId != 0)
            {
                portfolio.GetImages = await imagedata.GetItemById(portfolio.ImageId);
            }
            Portfolio item = await database.Edit(portfolio);
            
            return item;
        }


    }
}
